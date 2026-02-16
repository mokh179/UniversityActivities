using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Domain.Enums;

namespace UniversityActivities.Domain.Entities
{
    public class ClubJoinRequest:AuditableEntity
    {
        public int ClubId { get; set; }
        public Club Club { get; set; } = null!;

        public int UserId { get; set; }

        public JoinRequestStatus Status { get; set; } = JoinRequestStatus.Pending;

        public string? RejectionReason { get; set; }
    }
}
