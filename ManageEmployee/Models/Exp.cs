using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using ManageEmployee.Models.DTO;

namespace ManageEmployee.Models
{
    public class Exp : Empl
    {
        public Exp()
        { }

        public Exp(string fullName, DateTime? dateOfBirth, string? gender, string? address, string? phoneNumber, string? email,
            int? expInYear, string? proSkill) : base(fullName, dateOfBirth, gender, address, phoneNumber, email)
        {
            ExpInYear = expInYear;
            ProSkill = proSkill;
        }

        [Display(Name = "Số năm kinh nghiệm")]
        public int? ExpInYear { get; set; }

        [Display(Name = "Kỹ năng chuyên môn")]
        public string? ProSkill { get; set; }
    }
}
