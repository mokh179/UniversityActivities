using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Application.Interfaces.IUnitOfWork;
using UniversityActivities.Application.Interfaces.Repositories.Activies.StudentActivies;
using UniversityActivities.Application.UserCases.Activities.Student;

namespace UniversityActivities.Application.ApplyUseCases.StudentActivties
{
    public class RegisterStudentInActivityUseCase:IRegisterStudentInActivityUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStudentActivityRepository _studentActivityRepository;

        public RegisterStudentInActivityUseCase(
            IUnitOfWork unitOfWork,
            IStudentActivityRepository studentActivityRepository)
        {
            _unitOfWork = unitOfWork;
            _studentActivityRepository = studentActivityRepository;
        }

        public async Task ExecuteAsync(int studentId, int activityId)
        {
            // =========================
            // Prevent duplicate registration
            // =========================
            var isRegistered = await _studentActivityRepository
                .IsRegisteredAsync(studentId, activityId);

            if (isRegistered)
                throw new InvalidOperationException(
                    "Student is already registered in this activity.");

            // =========================
            // Register student
            // =========================
            await _studentActivityRepository.RegisterAsync(
                studentId,
                activityId);

            // =========================
            //  Commit
            // =========================
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
