using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ManageEmployee.Models.DTO
{
    public class CertificateDTO
    {
        [Display(Name = "Tên bằng")]
        public string? CertificateName { get; set; }

        [Display(Name = "Xếp loại bằng")]
        public string? CertificateRank { get; set; }
    }
}
