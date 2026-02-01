using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Application.AuthorizationModule.Models;
using UniversityActivities.Application.AuthorizationModule.Services.Interfaces;
using UniversityActivities.Application.Interfaces.IUnitOfWork;
using UniversityActivities.Application.UserCases.Activities.Admin;
using UniversityActivities.Domain.Enums;

namespace UniversityActivities.Application.ApplyUseCases.AdminActivties
{
    public class AssignManagementSupervisorUseCase:IAssignManagementSupervisorUseCase
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IIdentityRoleMangment _identityService;
        private readonly IUnitOfWork _unitOfWork;

        public AssignManagementSupervisorUseCase(
            IAuthorizationService authorizationService,
            IIdentityRoleMangment identityService,
            IUnitOfWork unitOfWork)
        {
            _authorizationService = authorizationService;
            _identityService = identityService;
            _unitOfWork = unitOfWork;
        }
        public async Task ExecuteAsync(
        int currentUserId,
        AssignManagementSupervisorCommand dto)
        {
            // Authorization (SuperAdmin فقط)
            await _authorizationService.EnsureAuthorizedAsync(
                currentUserId,
                AuthorizationAction.AssignManagementSupervisor,
                new AuthorizationContext());

            // Ensure system role
            await _identityService.EnsureUserInRoleAsync(
                dto.UserId,
                ActivityRoles.Supervisor.ToString());

            // Prevent duplicate assignment
            var exists = await _unitOfWork.ManagementSupervisors
                .ExistsAsync(dto.UserId, dto.ManagementId);

            if (exists)
                return;

            // Save assignment
            var entity = new ManagementSupervisors
            {
                UserId = dto.UserId,
                ManagementId = dto.ManagementId
            };

            await _unitOfWork.ManagementSupervisors.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
