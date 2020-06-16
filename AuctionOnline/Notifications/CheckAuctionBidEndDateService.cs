using System;
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
                TimeSpan.FromSeconds(5));

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            var scope = scopeFactory.CreateScope();

            var dbContext = scope.ServiceProvider.GetRequiredService<AuctionDbContext>();

            var model = new NotificationProduct
            {
                Name = "Product Hosted Service " + DateTime.Now,
                IsAvailable = true
            };

            dbContext.NotificationProducts.Add(model);
            dbContext.SaveChanges();
            hubContext.Clients.All.SendAsync("refreshNotifications");

            var count = Interlocked.Increment(ref executionCount) + "----" + DateTime.Now.ToLongDateString();

            _logger.LogInformation(
                "Timed Hosted Service is working. Count: {Count}", count);
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
