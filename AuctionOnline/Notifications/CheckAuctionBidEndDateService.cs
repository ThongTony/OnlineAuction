using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using AuctionOnline.Data;
using AuctionOnline.Models;
using AuctionOnline.Utilities;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AuctionOnline.Notifications
{
    public class CheckAuctionBidEndDateService : IHostedService, IDisposable
    {
        private int executionCount = 0;
        private readonly ILogger<CheckAuctionBidEndDateService> _logger;
        private Timer _timer;

        private readonly IServiceScopeFactory scopeFactory;
        private readonly IHubContext<SignalRHub> hubContext;
        private const double notifiedPeriod = 5;

        public CheckAuctionBidEndDateService(
            ILogger<CheckAuctionBidEndDateService> logger,
            IHubContext<SignalRHub> hubContext,
            IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            this.hubContext = hubContext;
            this.scopeFactory = scopeFactory;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Hosted Service running.");

            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(notifiedPeriod));

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            DateTime now = DateTime.Now;

            var scope = scopeFactory.CreateScope();

            var dbContext = scope.ServiceProvider.GetRequiredService<AuctionDbContext>();

            var latestEndSessionBid = dbContext.Bids.Where(x => x.AccountId == 1 && x.Item.BidStatus == BidStatus.Complete).OrderByDescending(x => x.BidSession).FirstOrDefault();
            if (latestEndSessionBid != null)
            {
                var latestEndBid = dbContext.Bids.Where(x => x.BidSession == latestEndSessionBid.BidSession).OrderByDescending(x => x.CurrentBid).FirstOrDefault();

                var existingExpiredItem = dbContext.ExpiredItems.FirstOrDefault(x => x.ItemId == latestEndBid.ItemId && x.SessionId == latestEndBid.BidSession);

                if (existingExpiredItem == null)
                {
                    var model = new ExpiredItem
                    {
                        ItemId = latestEndBid.ItemId,
                        ExpiredDate = now,
                        SessionId = latestEndBid.BidSession,
                        IsSeen = false
                    };

                    dbContext.ExpiredItems.Add(model);

                    dbContext.SaveChanges();

                    _logger.LogInformation("Expired Item ID: { Expired Item}", latestEndBid.ItemId + "was sent notification successful!");
                }
                else
                {
                    _logger.LogInformation("Notification of expired Item ID: { Expired Item}", latestEndBid.ItemId + " is existed");
                }

                hubContext.Clients.All.SendAsync("refreshNotifications");
            }
            else
            {
                _logger.LogInformation("Expired Item: There is no expired items");
            }


            var count = Interlocked.Increment(ref executionCount) + "----" + now;

            _logger.LogInformation("Timed Hosted Service is working");
            _logger.LogInformation("Count {Count}", count);
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Hosted Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
