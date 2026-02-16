using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Application.Interfaces.IUnitOfWork;
using UniversityActivities.Application.Interfaces.Repositories.Activies.StudentActivies;
using UniversityActivities.Application.UserCases.Activities.Student;

namespace UniversityActivities.Application.Implementation.ApplyUseCases.Evaluations
{
    public class SubmitActivityEvaluationUseCase : ISubmitActivityEvaluationUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStudentActivityRepository _studentActivityRepository;
        private readonly IStudentActivityEvaluationRepository _evaluationRepository;

        public SubmitActivityEvaluationUseCase(
            IUnitOfWork unitOfWork,
            IStudentActivityRepository studentActivityRepository,
            IStudentActivityEvaluationRepository evaluationRepository)
        {
            _unitOfWork = unitOfWork;
            _studentActivityRepository = studentActivityRepository;
            _evaluationRepository = evaluationRepository;
        }
        public async Task ExecuteAsync(
            int studentId,
            int activityId,
            DTOs.Evaluation.SubmitActivityEvaluationDto items)
        {
            try
            {
                // =========================
                //  Validation
                // =========================
                if (items.Items == null || items.Items.Count == 0)
                    throw new InvalidOperationException("Evaluation items are required.");
                // =========================
                //  Check Attendance
                // =========================
                var isRegistered = await _studentActivityRepository
                    .IsRegisteredAsync(studentId, items.ActivityId);

                if (!isRegistered)
                    throw new InvalidOperationException(
                        "Student must be registered and attended the activity to evaluate it.");
                // =========================
                //  Prevent Duplicate Evaluation
                // =========================
                var hasEvaluated = await _evaluationRepository
                    .HasEvaluatedAsync(studentId, items.ActivityId);

                if (hasEvaluated)
                    throw new InvalidOperationException(
                        "Student has already evaluated this activity.");
                // =========================
                //  Submit Scores
                // =========================
                await _evaluationRepository.SubmitAsync(
                    studentId,
                    items.ActivityId,
                    items.Items);
                // =========================
                //  Submit Comment (Optional)
                // =========================
                if (!string.IsNullOrWhiteSpace(items.Comment))
                {
                    await _evaluationRepository.SubmitCommentAsync(
                        studentId,
                        items.ActivityId,
                        items.Comment.Trim());
                }

                // =========================
                //  Commit
                // =========================
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw ;
            }
        }
    }
}
