using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AuctionOnline.Data;
using AuctionOnline.Models;
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
            var scope = scopeFactory.CreateScope();

            var dbContext = scope.ServiceProvider.GetRequiredService<AuctionDbContext>();

            DateTime now = DateTime.Now;

            var expiredItems = dbContext.Items.Where(t => t.BidEndDate.Value.Date.CompareTo(now.Date) <= 0 && t.AccountId == 1).OrderBy(d => d.BidEndDate).ToList();

            foreach (var item in expiredItems)
            {
                var model = new ExpiredItem
                {
                    ItemId = item.Id,
                    IsExpired = item.BidEndDate.Value.Date.CompareTo(now.Date) <= 0,
                    CurrentDate = now,
                    IsSeen = false
                };

                dbContext.ExpiredItems.Add(model);
            }

            dbContext.SaveChanges();
            hubContext.Clients.All.SendAsync("refreshNotifications");

            var count = Interlocked.Increment(ref executionCount) + "----" + now;

            _logger.LogInformation("Timed Hosted Service is working");
            _logger.LogInformation("Count {Count}", count);
            _logger.LogInformation("Total Expired Items: { Total}", expiredItems.Count);
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
