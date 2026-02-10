using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Application.Interfaces.IUnitOfWork;
using UniversityActivities.Application.Interfaces.Repositories.Activies.StudentActivies;
using UniversityActivities.Application.UserCases.Activities.Student;

namespace UniversityActivities.Application.ApplyUseCases.StudentActivties
{
    public class MarkStudentAttendanceUseCase : IMarkStudentAttendanceUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStudentActivityRepository _studentActivityRepository;

        public MarkStudentAttendanceUseCase(
            IUnitOfWork unitOfWork,
            IStudentActivityRepository studentActivityRepository)
        {
            _unitOfWork = unitOfWork;
            _studentActivityRepository = studentActivityRepository;
        }

        public async Task ExecuteAsync(int studentId, int activityId)
        {
            // =========================
            //  Ensure student is registered
            // =========================
            var isRegistered = await _studentActivityRepository
                .IsRegisteredAsync(studentId, activityId);

            if (!isRegistered)
                throw new InvalidOperationException(
                    "Student must be registered in the activity before attendance can be marked.");

            // =========================
            //  Mark attendance
            // =========================
            await _studentActivityRepository.MarkAttendanceAsync(
                studentId,
                activityId);

            // =========================
            //  Commit
            // =========================
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
