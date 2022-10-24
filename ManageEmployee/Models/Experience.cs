using System;
using System.Collections.Generic;

namespace ManageEmployee.Models
{
    public partial class Experience
    {
        public int EmployeeId { get; set; }
        public int? ExpInYear { get; set; }
        public string? ProSkill { get; set; }

        public virtual Employee Employee { get; set; } = null!;
    }
}
