using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityActivities.Domain.RBAC.Common
{
    public class RoleBase:BaseEntity
    {
        [Required, MaxLength(100)]
        public string NameAr { get; set; }

        [Required, MaxLength(100)]
        public string NameEn { get; set; }
    }
}
