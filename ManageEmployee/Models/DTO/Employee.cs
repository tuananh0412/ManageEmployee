using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ManageEmployee.Models.DTO
{
    public partial class Employee
    {
        public Employee()
        {
            Certificates = new HashSet<Certificate>();
        }

        public Employee(string fullName, DateTime? dateOfBirth, string? gender, string? address, string? phoneNumber, string? email)
        {
            FullName = fullName;
            DateOfBirth = dateOfBirth;
            Gender = gender;
            Address = address;
            PhoneNumber = phoneNumber;
            Email = email;
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

        public virtual Experience? Experience { get; set; }
        public virtual Fresher? Fresher { get; set; }
        public virtual Intern? Intern { get; set; }

        [Display(Name = "Bằng cấp")]
        public virtual ICollection<Certificate>? Certificates { get; set; }
    }
}
