using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Application.DTOs.Activities.Student;
using UniversityActivities.Application.Interfaces.Repositories.Activies.StudentActivies;
using UniversityActivities.Application.UserCases.Activities.Student;

namespace UniversityActivities.Application.Implementation.ApplyUseCases.StudentActivties
{
    public class GenerateAttendanceCertificateUseCase : IGenerateAttendanceCertificateUseCase
    {
        private readonly IStudentActivityRepository _studentActivityRepo;
        private readonly IStudentActivityEvaluationRepository _evaluationRepo;
        private readonly IStudentActivityQueryRepository _activityQuery;
        public GenerateAttendanceCertificateUseCase(IStudentActivityRepository studentActivityRepository
                                                   ,IStudentActivityEvaluationRepository evaluationRepo
                                                    ,IStudentActivityQueryRepository activityQuery)
        {
            _studentActivityRepo= studentActivityRepository;
            _evaluationRepo= evaluationRepo;
            _activityQuery= activityQuery;
        }
        public async Task<AttendanceCertificateDto> ExecuteAsync(int studentId, int activityId)
        {
            var attended = await _studentActivityRepo.HasAttendedAsync(studentId, activityId);
            if (!attended)
                return new AttendanceCertificateDto();
               // throw new Exception("Student did not attend activity");

            var evaluated = await _evaluationRepo.HasEvaluatedAsync(studentId, activityId);
            if (!evaluated)
                return new AttendanceCertificateDto();
            //throw new Exception("Evaluation required");

            var activity = await _activityQuery.GetCertificateDetails(activityId, studentId);
            return new AttendanceCertificateDto
            {
                StudentName = activity.StudentName,
                ActivityTitle = activity.ActivityTitle,
                ManagementName = activity.ManagementName,
                ActivityDate = activity.ActivityDate,
                IssuedAt = DateTime.UtcNow,
                CertificateNumber = Guid.NewGuid().ToString("N")[..10]
            };
        }
    }
}
