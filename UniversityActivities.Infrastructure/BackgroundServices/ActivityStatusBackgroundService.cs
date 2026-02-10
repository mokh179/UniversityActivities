using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Application.Common.Models.Background;
using UniversityActivities.Application.UserCases.BackgroundServices.Interface.Activity;

namespace UniversityActivities.Infrastructure.BackgroundServices
{
    public class ActivityStatusBackgroundService:BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<ActivityStatusBackgroundService> _logger;
        private readonly ActivityStatusBackgroundOptions _options;

        public ActivityStatusBackgroundService(
            IServiceScopeFactory scopeFactory,
            ILogger<ActivityStatusBackgroundService> logger,
            IOptions<ActivityStatusBackgroundOptions> options)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
            _options = options.Value;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (!_options.Enabled)
            {
                _logger.LogWarning(
                    "[ActivityStatusBackground] Service is disabled from configuration.");
                return;
            }

            _logger.LogInformation(
                "[ActivityStatusBackground] Service started. Interval: {Minutes} minutes",
                _options.IntervalMinutes);

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _scopeFactory.CreateScope();

                    var useCase = scope.ServiceProvider
                        .GetRequiredService<IUpdateActivityStatusUseCase>();

                    await useCase.ExecuteAsync(stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(
                        ex,
                        "[ActivityStatusBackground] Error while executing background task");
                }

                await Task.Delay(
                    TimeSpan.FromMinutes(_options.IntervalMinutes),
                    stoppingToken);
            }

            _logger.LogInformation(
                "[ActivityStatusBackground] Service stopped.");
        }
    }
}
