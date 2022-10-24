using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ManageEmployee.Models
{
    public class Exp : Employee
    {
        [Display(Name = "Số năm kinh nghiệm")]
        public int? ExpInYear { get; set; }

        [Display(Name = "Kỹ năng chuyên môn")]
        public string? ProSkill { get; set; }

        [Display(Name = "Tên bằng")]
        public string? CertificateName { get; set; }

        [Display(Name = "Xếp loại bằng")]
        public string? CertificateRank { get; set; }
    }
}
