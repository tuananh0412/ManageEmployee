using System;
using System.Collections.Generic;

namespace ManageEmployee.Models
{
    public partial class Intern
    {
        public int EmployeeId { get; set; }
        public string? Majors { get; set; }
        public int? Semester { get; set; }
        public string? UniversityName { get; set; }

        public virtual Employee Employee { get; set; } = null!;
    }
}
