using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using ManageEmployee.Models.DTO;

namespace ManageEmployee.Models
{
    public class Frs : Empl
    {
        public Frs()
        { }

        public Frs(string fullName, DateTime? dateOfBirth, string? gender, string? address, string? phoneNumber, string? email,
            DateTime? graduationDate, string? graduationRank, string? education) : base(fullName, dateOfBirth, gender, address, phoneNumber, email)
        {
            GraduationDate = graduationDate;
            GraduationRank = graduationRank;
            Education = education;
        }

        [Display(Name = "Ngày tốt nghiệp")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? GraduationDate { get; set; }

        [Display(Name = "Xếp hạng tốt nghiệp")]
        public string? GraduationRank { get; set; }

        [Display(Name = "Trường tốt nghiệp")]
        public string? Education { get; set; }
    }
}
