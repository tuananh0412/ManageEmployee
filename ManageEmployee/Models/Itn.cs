using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ManageEmployee.Models
{
    public class Itn : Employee
    {
        [Display(Name = "Chuyên ngành đang học")]
        public string? Majors { get; set; }

        [Display(Name = "Học kì đang học")]
        public int? Semester { get; set; }

        [Display(Name = "Trường đang học")]
        public string? UniversityName { get; set; }

        [Display(Name = "Tên bằng")]
        public string? CertificateName { get; set; }

        [Display(Name = "Xếp loại bằng")]
        public string? CertificateRank { get; set; }
    }
}
