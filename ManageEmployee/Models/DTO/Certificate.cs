using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ManageEmployee.Models.DTO
{
    public partial class Certificate
    {
        public Certificate(string? certificateName, string? certificateRank, int employeeId)
        {
            CertificateName = certificateName;
            CertificateRank = certificateRank;
            EmployeeId = employeeId;
        }

        [Display(Name = "Mã bằng cấp")]
        public int CertificateId { get; set; }

        [Display(Name = "Tên bằng")]
        public string? CertificateName { get; set; }

        [Display(Name = "Xếp loại bằng")]
        public string? CertificateRank { get; set; }
        public int EmployeeId { get; set; }

        public virtual Employee Employee { get; set; } = null!;
    }
}
