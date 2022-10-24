using System;
using System.Collections.Generic;

namespace ManageEmployee.Models
{
    public partial class Fresher
    {
        public int EmployeeId { get; set; }
        public DateTime? GraduationDate { get; set; }
        public string? GraduationRank { get; set; }
        public string? Education { get; set; }

        public virtual Employee Employee { get; set; } = null!;
    }
}
