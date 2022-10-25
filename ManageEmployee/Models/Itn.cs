using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using ManageEmployee.Models.DTO;

namespace ManageEmployee.Models
{
    public class Itn : Empl
    {
        public Itn()
        { }

        public Itn(string fullName, DateTime? dateOfBirth, string? gender, string? address, string? phoneNumber, string? email,
            string? majors, int? semester, string? universityName) : base(fullName, dateOfBirth, gender, address, phoneNumber, email)
        {
            Majors = majors;
            Semester = semester;
            UniversityName = universityName;
        }

        [Display(Name = "Chuyên ngành đang học")]
        public string? Majors { get; set; }

        [Display(Name = "Học kì đang học")]
        public int? Semester { get; set; }

        [Display(Name = "Trường đang học")]
        public string? UniversityName { get; set; }
    }
}
