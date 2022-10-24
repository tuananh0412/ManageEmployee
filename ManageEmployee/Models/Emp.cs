using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ManageEmployee.Models
{
    public class Emp : Employee
    {
        [Display(Name = "Số năm kinh nghiệm")]
        public int? ExpInYear { get; set; }

        [Display(Name = "Kỹ năng chuyên môn")]
        public string? ProSkill { get; set; }

        [Display(Name = "Ngày tốt nghiệp")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? GraduationDate { get; set; }

        [Display(Name = "Xếp hạng tốt nghiệp")]
        public string? GraduationRank { get; set; }

        [Display(Name = "Trường tốt nghiệp")]
        public string? Education { get; set; }

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

        [Display(Name = "Số bằng cấp")]
        public virtual ICollection<Certificate> Certificates { get; set; }
    }
}
