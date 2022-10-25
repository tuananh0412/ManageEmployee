using System;
using System.Collections.Generic;

namespace ManageEmployee.Models.DTO
{
    public partial class Fresher
    {
        public Fresher(int employeeId, DateTime? graduationDate, string? graduationRank, string? education)
        {
            EmployeeId = employeeId;
            GraduationDate = graduationDate;
            GraduationRank = graduationRank;
            Education = education;
        }

        public int EmployeeId { get; set; }
        public DateTime? GraduationDate { get; set; }
        public string? GraduationRank { get; set; }
        public string? Education { get; set; }

        public virtual Employee Employee { get; set; } = null!;
    }
}
