using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ManageEmployee.Models.DTO
{
    public class EmployeeDTO
    {
        public EmployeeDTO()
        { }

        public EmployeeDTO(string fullName, DateTime? dateOfBirth, string? gender, string? address, string? phoneNumber, string? email)
        {
            FullName = fullName;
            DateOfBirth = dateOfBirth;
            Gender = gender;
            Address = address;
            PhoneNumber = phoneNumber;
            Email = email;
        }

        //Experience
        public EmployeeDTO(string fullName, DateTime? dateOfBirth, string? gender, string? address, string? phoneNumber, string? email, int? expInYear, string? proSkill)
        {
            FullName = fullName;
            DateOfBirth = dateOfBirth;
            Gender = gender;
            Address = address;
            PhoneNumber = phoneNumber;
            Email = email;
            ExpInYear = expInYear;
            ProSkill = proSkill;
        }

        //Fresher
        public EmployeeDTO(string fullName, DateTime? dateOfBirth, string? gender, string? address, string? phoneNumber, string? email, DateTime? graduationDate, string? graduationRank, string? education)
        {
            FullName = fullName;
            DateOfBirth = dateOfBirth;
            Gender = gender;
            Address = address;
            PhoneNumber = phoneNumber;
            Email = email;
            GraduationDate = graduationDate;
            GraduationRank = graduationRank;
            Education = education;
        }

        //Intern
        public EmployeeDTO(string fullName, DateTime? dateOfBirth, string? gender, string? address, string? phoneNumber, string? email, string? majors, int? semester, string? universityName)
        {
            FullName = fullName;
            DateOfBirth = dateOfBirth;
            Gender = gender;
            Address = address;
            PhoneNumber = phoneNumber;
            Email = email;
            Majors = majors;
            Semester = semester;
            UniversityName = universityName;
        }

        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Họ tên không được để trống")]
        [Display(Name = "Họ tên")]
        public string FullName { get; set; } = null!;

        [Required(ErrorMessage = "Ngày sinh không được để trống")]
        [Display(Name = "Ngày sinh")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? DateOfBirth { get; set; }

        [Required(ErrorMessage = "Giới tính không được để trống")]
        [Display(Name = "Giới tính")]
        public string? Gender { get; set; }

        [Required(ErrorMessage = "Địa chỉ không được để trống")]
        [Display(Name = "Địa chỉ")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        [Display(Name = "Số điện thoại")]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Email không được để trống")]
        [Display(Name = "Địa chỉ email")]
        public string? Email { get; set; }

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
        public ICollection<Certificate>? Certificates { get; set; }
    }
}
