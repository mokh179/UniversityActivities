using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Application.DTOs.Enums;
using UniversityActivities.Application.DTOs.Scan.Models;
using UniversityActivities.Application.Interfaces.IUnitOfWork;
using UniversityActivities.Application.UseCases.Activities.Scan;

namespace UniversityActivities.Application.Implementation.ApplyUseCases.AdminActivties.Scan
{
    public class HandleActivityScanUseCase : IHandleActivityScanUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public HandleActivityScanUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ActivityScanResult> ExecuteAsync(
            int activityId,
            int? userId,
            CancellationToken cancellationToken)
        {
            if (userId == null)
            {
                return new ActivityScanResult
                {
                    ActivityId = activityId,
                    Action = ScanAction.RedirectToLogin
                };
            }

            var activity = await _unitOfWork
                .ActivityScanRepository
                .GetPublishedActivityAsync(activityId, cancellationToken);

            if (activity == null)
            {
                return new ActivityScanResult
                {
                    ActivityId = activityId,
                    Action = ScanAction.ActivityClosed
                };
            }

            if (activity.ActivityStatusId == (int)StatusEnums.Upcoming)
            {
                return new ActivityScanResult
                {
                    ActivityId = activityId,
                    Action = ScanAction.ActivityNotStarted
                };
            }

            var studentActivity = await _unitOfWork
                .ActivityScanRepository
                .GetStudentActivityAsync(activityId, userId.Value, cancellationToken);

            bool isRegistered = studentActivity != null;
            bool isAttended = studentActivity?.AttendedAt != null;

            bool hasEvaluation = await _unitOfWork
                .ActivityScanRepository
                .HasEvaluationAsync(activityId, userId.Value, cancellationToken);

            if (activity.ActivityStatusId == (int)StatusEnums.Inprogress)
            {
                if (!isRegistered)
                {
                    var newRegistration = new StudentActivity
                    {
                        ActivityId = activityId,
                        StudentId = userId.Value,
                        CreatedAt = DateTime.UtcNow
                    };

                    await _unitOfWork
                        .ActivityScanRepository
                        .RegisterAsync(newRegistration, cancellationToken);

                    newRegistration.AttendedAt = DateTime.UtcNow;

                    await _unitOfWork
                        .ActivityScanRepository
                        .MarkAttendanceAsync(newRegistration, cancellationToken);

                    await _unitOfWork.SaveChangesAsync(cancellationToken);

                    return new ActivityScanResult
                    {
                        ActivityId = activityId,
                        Action = ScanAction.RegisterAndAttend
                    };
                }

                if (!isAttended)
                {
                    studentActivity!.AttendedAt = DateTime.UtcNow;

                    await _unitOfWork
                        .ActivityScanRepository
                        .MarkAttendanceAsync(studentActivity, cancellationToken);

                    await _unitOfWork.SaveChangesAsync(cancellationToken);

                    return new ActivityScanResult
                    {
                        ActivityId = activityId,
                        Action = ScanAction.MarkAttendance
                    };
                }

                return new ActivityScanResult
                {
                    ActivityId = activityId,
                    Action = ScanAction.AlreadyAttended
                };
            }

            if (activity.ActivityStatusId == (int)StatusEnums.Completed)
            {
                if (!isAttended)
                {
                    return new ActivityScanResult
                    {
                        ActivityId = activityId,
                        Action = ScanAction.ActivityClosed
                    };
                }

                if (!hasEvaluation)
                {
                    return new ActivityScanResult
                    {
                        ActivityId = activityId,
                        Action = ScanAction.RedirectToEvaluation
                    };
                }

                return new ActivityScanResult
                {
                    ActivityId = activityId,
                    Action = ScanAction.RedirectToCertificate
                };
            }

            return new ActivityScanResult
            {
                ActivityId = activityId,
                Action = ScanAction.ActivityClosed
            };
        }
    }
}
