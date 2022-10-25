using System;
using System.Collections.Generic;

namespace ManageEmployee.Models.DTO
{
    public partial class Experience
    {
        public Experience(int employeeId, int? expInYear, string? proSkill)
        {
            EmployeeId = employeeId;
            ExpInYear = expInYear;
            ProSkill = proSkill;
        }

        public int EmployeeId { get; set; }
        public int? ExpInYear { get; set; }
        public string? ProSkill { get; set; }

        public virtual Employee Employee { get; set; } = null!;
    }
}
