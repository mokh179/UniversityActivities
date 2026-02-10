using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using UniversityActivities.Application.DTOs.Enums;
using UniversityActivities.Application.Interfaces.IUnitOfWork;
using UniversityActivities.Application.Interfaces.Repositories.Activies.Background;
using UniversityActivities.Application.UserCases.BackgroundServices.Interface.Activity;

namespace UniversityActivities.Application.ApplyUseCases.BackgroundServices.Activity
{
    public class UpdateActivityStatusUseCase : IUpdateActivityStatusUseCase
    {
        private readonly IActivityStatusRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UpdateActivityStatusUseCase> _logger;

        public UpdateActivityStatusUseCase(IUnitOfWork unitOfWork,
            IActivityStatusRepository repository, ILogger<UpdateActivityStatusUseCase> logger)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _logger = logger;   
        }


       
        public async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                "[ActivityStatusUpdater] Execution started at {Time}",
                DateTime.UtcNow);

            var now = DateTime.UtcNow;

            try
            {
                // =========================
                // Upcoming -> InProgress
                // =========================
                var toInProgress =
                    await _repository.GetUpcomingToStartAsync(
                        now,
                        cancellationToken);

                if (toInProgress.Any())
                {
                    _logger.LogInformation(
                        "[ActivityStatusUpdater] {Count} activities moving to InProgress",
                        toInProgress.Count);
                }

                await _repository.UpdateStatusAsync(
                    toInProgress,
                    DTOs.Enums.StatusEnums.Inprogress,
                    cancellationToken);

                // =========================
                // InProgress -> Completed
                // =========================
                var toCompleted =
                    await _repository.GetInProgressToCompleteAsync(
                        now,
                        cancellationToken);

                if (toCompleted.Any())
                {
                    _logger.LogInformation(
                        "[ActivityStatusUpdater] {Count} activities moving to Completed",
                        toCompleted.Count);
                    await _repository.UpdateStatusAsync(
                    toCompleted,
                    DTOs.Enums.StatusEnums.Completed,
                    cancellationToken);
                }

                _logger.LogInformation(
                    "[ActivityStatusUpdater] Execution finished successfully");



                await _unitOfWork.SaveChangesAsync(cancellationToken);

            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "[ActivityStatusUpdater] Unexpected error occurred");

                throw;
            }
        }

    }
}
