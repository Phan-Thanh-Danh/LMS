using System;
using System.Threading;
using System.Threading.Tasks;
using backend.Repository.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace backend.Services
{
    public class PromotionBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<PromotionBackgroundService> _logger;
        private readonly TimeSpan _checkInterval = TimeSpan.FromMinutes(5); // Kiểm tra mỗi 5 phút

        public PromotionBackgroundService(
            IServiceProvider serviceProvider,
            ILogger<PromotionBackgroundService> logger
        )
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Promotion Background Service is starting.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var promotionRepository =
                            scope.ServiceProvider.GetRequiredService<IPromotionRepository>();

                        _logger.LogInformation(
                            "Checking for expired promotions at: {time}",
                            DateTimeOffset.Now
                        );
                        int deactivatedCount =
                            await promotionRepository.DeactivateExpiredPromotionsAsync();

                        if (deactivatedCount > 0)
                        {
                            _logger.LogInformation(
                                "Successfully deactivated {count} expired promotions.",
                                deactivatedCount
                            );
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while deactivating expired promotions.");
                }

                await Task.Delay(_checkInterval, stoppingToken);
            }

            _logger.LogInformation("Promotion Background Service is stopping.");
        }
    }
}
