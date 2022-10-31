using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ManageEmployee.Models;
using ManageEmployee.Models.DTO;
using Newtonsoft.Json;

namespace ManageEmployee.Controllers
{
    public enum EnumEmployeesType
    {
        Experience,
        Fresher,
        Intern
    }

    public class EmployeesController : Controller
    {
        private readonly QuanLyNhanVienContext _context;

        public EmployeesController(QuanLyNhanVienContext context)
        {
            _context = context;
        }

        // GET: Employees
        public async Task<IActionResult> Index(string searchString)
        {
            var employees = await _context.Employees.ToListAsync();

            var empIds = employees.Select(e => e.EmployeeId).ToList();
            var certificates = await _context.Certificates.Where(c => empIds.Contains(c.EmployeeId)).ToListAsync();
            foreach (var employee in employees)
            {
                employee.Certificates = certificates.Where(c => c.EmployeeId == employee.EmployeeId).ToList();
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                employees = employees.Where(s => s.FullName!.Contains(searchString)).ToList();
            }

            return View(employees);
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var employees = await _context.Employees.ToListAsync();
            var empIds = employees.Select(e => e.EmployeeId).ToList();
            var certificates = await _context.Certificates.Where(c => empIds.Contains(c.EmployeeId)).ToListAsync();
            foreach (var e in employees)
            {
                e.Certificates = certificates.Where(c => c.EmployeeId == e.EmployeeId).ToList();
            }

            var employee = employees.FirstOrDefault(m => m.EmployeeId == id);
            var experience = await _context.Experiences.FindAsync(id);
            var fresher = await _context.Freshers.FindAsync(id);
            var intern = await _context.Interns.FindAsync(id);

            if (employee != null)
            {
                if (experience != null)
                {
                    EmployeeDTO employeeDTO = new EmployeeDTO(
                        employee.FullName,
                        employee.DateOfBirth,
                        employee.Gender,
                        employee.Address,
                        employee.PhoneNumber,
                        employee.Email,
                        experience.ExpInYear,
                        experience.ProSkill);
                    employeeDTO.EmployeeId = employee.EmployeeId;
                    employeeDTO.Certificates = employee.Certificates;

                    string viewDetailsExperience = "~/Views/Employees/Experiences/DetailsExperience.cshtml";
                    return View(viewDetailsExperience, employeeDTO);
                }
                else if (fresher != null)
                {
                    EmployeeDTO employeeDTO = new EmployeeDTO(
                        employee.FullName,
                        employee.DateOfBirth,
                        employee.Gender,
                        employee.Address,
                        employee.PhoneNumber,
                        employee.Email,
                        fresher.GraduationDate,
                        fresher.GraduationRank,
                        fresher.Education);
                    employeeDTO.EmployeeId = employee.EmployeeId;
                    employeeDTO.Certificates = employee.Certificates;

                    string viewDetailsFresher = "~/Views/Employees/Freshers/DetailsFresher.cshtml";
                    return View(viewDetailsFresher, employeeDTO);
                }
                else if (intern != null)
                {
                    EmployeeDTO employeeDTO = new EmployeeDTO(
                        employee.FullName,
                        employee.DateOfBirth,
                        employee.Gender,
                        employee.Address,
                        employee.PhoneNumber,
                        employee.Email,
                        intern.Majors,
                        intern.Semester,
                        intern.UniversityName);
                    employeeDTO.EmployeeId = employee.EmployeeId;
                    employeeDTO.Certificates = employee.Certificates;

                    string viewDetailsIntern = "~/Views/Employees/Interns/DetailsIntern.cshtml";
                    return View(viewDetailsIntern, employeeDTO);
                }
            }
            return NotFound();
        }

        public IActionResult Create(EnumEmployeesType enumEmployeesType)
        {
            switch (enumEmployeesType)
            {
                case EnumEmployeesType.Experience:
                    string viewCreateExperience = "~/Views/Employees/Experiences/CreateExperience.cshtml";
                    return View(viewCreateExperience);

                case EnumEmployeesType.Fresher:
                    string viewCreateFresher = "~/Views/Employees/Freshers/CreateFresher.cshtml";
                    return View(viewCreateFresher);

                case EnumEmployeesType.Intern:
                    string viewCreateIntern = "~/Views/Employees/Interns/CreateIntern.cshtml";
                    return View(viewCreateIntern);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EnumEmployeesType enumEmployeesType, EmployeeDTO employeeDTO)
        {
            switch (enumEmployeesType)
            {
                case EnumEmployeesType.Experience:
                    if (ModelState.IsValid)
                    {
                        Employee employee = new Employee(
                            employeeDTO.FullName,
                            employeeDTO.DateOfBirth,
                            employeeDTO.Gender,
                            employeeDTO.Address,
                            employeeDTO.PhoneNumber,
                            employeeDTO.Email);

                        _context.Add(employee);
                        await _context.SaveChangesAsync();

                        Experience experience = new Experience(
                            employee.EmployeeId,
                            employeeDTO.ExpInYear,
                            employeeDTO.ProSkill);
                        _context.Add(experience);

                        if (employeeDTO.CertificateName != null || employeeDTO.CertificateRank != null)
                        {
                            Certificate certificate = new Certificate(
                                employeeDTO.CertificateName,
                                employeeDTO.CertificateRank,
                                employee.EmployeeId);
                            _context.Add(certificate);
                        }

                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    break;
                case EnumEmployeesType.Fresher:
                    if (ModelState.IsValid)
                    {
                        Employee employee = new Employee(
                            employeeDTO.FullName,
                            employeeDTO.DateOfBirth,
                            employeeDTO.Gender,
                            employeeDTO.Address,
                            employeeDTO.PhoneNumber,
                            employeeDTO.Email);

                        _context.Add(employee);
                        await _context.SaveChangesAsync();

                        Fresher fresher = new Fresher(
                            employee.EmployeeId,
                            employeeDTO.GraduationDate,
                            employeeDTO.GraduationRank,
                            employeeDTO.Education);
                        _context.Add(fresher);

                        if (employeeDTO.CertificateName != null || employeeDTO.CertificateRank != null)
                        {
                            Certificate certificate = new Certificate(
                                employeeDTO.CertificateName,
                                employeeDTO.CertificateRank,
                                employee.EmployeeId);
                            _context.Add(certificate);
                        }

                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    break;
                case EnumEmployeesType.Intern:
                    if (ModelState.IsValid)
                    {
                        Employee employee = new Employee(
                            employeeDTO.FullName,
                            employeeDTO.DateOfBirth,
                            employeeDTO.Gender,
                            employeeDTO.Address,
                            employeeDTO.PhoneNumber,
                            employeeDTO.Email);

                        _context.Add(employee);
                        await _context.SaveChangesAsync();

                        Intern intern = new Intern(
                            employee.EmployeeId,
                            employeeDTO.Majors,
                            employeeDTO.Semester,
                            employeeDTO.UniversityName);
                        _context.Add(intern);

                        if (employeeDTO.CertificateName != null || employeeDTO.CertificateRank != null)
                        {
                            Certificate certificate = new Certificate(
                                employeeDTO.CertificateName,
                                employeeDTO.CertificateRank,
                                employee.EmployeeId);
                            _context.Add(certificate);
                        }

                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    break;
            }
            return NotFound();
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Employees == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.FindAsync(id);
            var experience = await _context.Experiences.FindAsync(id);
            var fresher = await _context.Freshers.FindAsync(id);
            var intern = await _context.Interns.FindAsync(id);

            if (employee != null)
            {
                if (experience != null)
                {
                    EmployeeDTO employeeDTO = new EmployeeDTO(
                        employee.FullName,
                        employee.DateOfBirth,
                        employee.Gender,
                        employee.Address,
                        employee.PhoneNumber,
                        employee.Email,
                        experience.ExpInYear,
                        experience.ProSkill);

                    string viewEditExperience = "~/Views/Employees/Experiences/EditExperience.cshtml";
                    return View(viewEditExperience, employeeDTO);
                }
                else if (fresher != null)
                {
                    EmployeeDTO employeeDTO = new EmployeeDTO(
                        employee.FullName,
                        employee.DateOfBirth,
                        employee.Gender,
                        employee.Address,
                        employee.PhoneNumber,
                        employee.Email,
                        fresher.GraduationDate,
                        fresher.GraduationRank,
                        fresher.Education);

                    string viewEditFresher = "~/Views/Employees/Freshers/EditFresher.cshtml";
                    return View(viewEditFresher, employeeDTO);
                }
                else if (intern != null)
                {
                    EmployeeDTO employeeDTO = new EmployeeDTO(
                        employee.FullName,
                        employee.DateOfBirth,
                        employee.Gender,
                        employee.Address,
                        employee.PhoneNumber,
                        employee.Email,
                        intern.Majors,
                        intern.Semester,
                        intern.UniversityName);

                    string viewEditIntern = "~/Views/Employees/Interns/EditIntern.cshtml";
                    return View(viewEditIntern, employeeDTO);
                }
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EmployeeDTO employeeDTO)
        {
            if (ModelState.IsValid)
            {
                var employee = await _context.Employees.FindAsync(id);
                var experience = await _context.Experiences.FindAsync(id);
                var fresher = await _context.Freshers.FindAsync(id);
                var intern = await _context.Interns.FindAsync(id);

                if (employeeDTO != null)
                {
                    if (employee != null)
                    {
                        employee.FullName = employeeDTO.FullName;
                        employee.DateOfBirth = employeeDTO.DateOfBirth;
                        employee.Gender = employeeDTO.Gender;
                        employee.Address = employeeDTO.Address;
                        employee.PhoneNumber = employeeDTO.PhoneNumber;
                        employee.Email = employeeDTO.Email;
                        _context.Update(employee);
                        if (experience != null)
                        {
                            experience.ExpInYear = employeeDTO.ExpInYear;
                            experience.ProSkill = employeeDTO.ProSkill;

                            _context.Update(experience); 
                            await _context.SaveChangesAsync();
                            return RedirectToAction(nameof(Index));
                        }
                        else if (fresher != null)
                        {
                            fresher.GraduationDate = employeeDTO.GraduationDate;
                            fresher.GraduationRank = employeeDTO.GraduationRank;
                            fresher.Education = employeeDTO.Education;

                            _context.Update(fresher);
                            await _context.SaveChangesAsync();
                            return RedirectToAction(nameof(Index));
                        }
                        else if (intern != null)
                        {
                            intern.Majors = employeeDTO.Majors;
                            intern.Semester = employeeDTO.Semester;
                            intern.UniversityName = employeeDTO.UniversityName;

                            _context.Update(intern);
                            await _context.SaveChangesAsync();
                            return RedirectToAction(nameof(Index));
                        }
                    }
                }
            }
            return NotFound();
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var employees = await _context.Employees.ToListAsync();
            var empIds = employees.Select(e => e.EmployeeId).ToList();
            var certificates = await _context.Certificates.Where(c => empIds.Contains(c.EmployeeId)).ToListAsync();
            foreach (var e in employees)
            {
                e.Certificates = certificates.Where(c => c.EmployeeId == e.EmployeeId).ToList();
            }

            var employee = await _context.Employees.FindAsync(id);
            var experience = await _context.Experiences.FindAsync(id);
            var fresher = await _context.Freshers.FindAsync(id);
            var intern = await _context.Interns.FindAsync(id);

            if (employee != null)
            {
                if (experience != null)
                {
                    EmployeeDTO employeeDTO = new EmployeeDTO(
                        employee.FullName,
                        employee.DateOfBirth,
                        employee.Gender,
                        employee.Address,
                        employee.PhoneNumber,
                        employee.Email,
                        experience.ExpInYear,
                        experience.ProSkill);
                    employeeDTO.EmployeeId = employee.EmployeeId;
                    employeeDTO.Certificates = employee.Certificates;

                    string viewDeleteExperience = "~/Views/Employees/Experiences/DeleteExperience.cshtml";
                    return View(viewDeleteExperience, employeeDTO);
                }
                else if (fresher != null)
                {
                    EmployeeDTO employeeDTO = new EmployeeDTO(
                        employee.FullName,
                        employee.DateOfBirth,
                        employee.Gender,
                        employee.Address,
                        employee.PhoneNumber,
                        employee.Email,
                        fresher.GraduationDate,
                        fresher.GraduationRank,
                        fresher.Education);
                    employeeDTO.EmployeeId = employee.EmployeeId;
                    employeeDTO.Certificates = employee.Certificates;

                    string viewDeleteFresher = "~/Views/Employees/Freshers/DeleteFresher.cshtml";
                    return View(viewDeleteFresher, employeeDTO);
                }
                else if (intern != null)
                {
                    EmployeeDTO employeeDTO = new EmployeeDTO(
                        employee.FullName,
                        employee.DateOfBirth,
                        employee.Gender,
                        employee.Address,
                        employee.PhoneNumber,
                        employee.Email,
                        intern.Majors,
                        intern.Semester,
                        intern.UniversityName);
                    employeeDTO.EmployeeId = employee.EmployeeId;
                    employeeDTO.Certificates = employee.Certificates;

                    string viewDeleteIntern = "~/Views/Employees/Interns/DeleteIntern.cshtml";
                    return View(viewDeleteIntern, employeeDTO);
                }
            }

            return NotFound();
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            var experience = await _context.Experiences.FindAsync(id);
            var fresher = await _context.Freshers.FindAsync(id);
            var intern = await _context.Interns.FindAsync(id);

            var certificates = await _context.Certificates.Where(c => c.EmployeeId == id).ToListAsync();

            if (employee != null)
            {
                _context.Certificates.RemoveRange(certificates);
                if (experience != null)
                {
                    _context.Experiences.Remove(experience);
                }
                else if (fresher != null)
                {
                    _context.Freshers.Remove(fresher);
                }
                else if (intern != null)
                {
                    _context.Interns.Remove(intern);
                }
                _context.Employees.Remove(employee);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        // GET: Employees/CreateExperience
        public IActionResult CreateCertificate()
        {
            return View("~/Views/Employees/Certificate/CreateCertificate.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCertificate(int id, CertificateDTO ctfc)
        {
            if (ModelState.IsValid)
            {
                Certificate cert = new Certificate(
                    ctfc.CertificateName,
                    ctfc.CertificateRank,
                    id);

                _context.Add(cert);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Employees", new { id = id });
            }
            return NotFound();
        }

        // GET: Employees/EditEditCertificate/5
        public async Task<IActionResult> EditCertificate(int? id)
        {
            if (id == null || _context.Certificates == null)
            {
                return NotFound();
            }

            var certificate = await _context.Certificates.FindAsync(id);

            string viewEditCertificate = "~/Views/Employees/Certificate/EditCertificate.cshtml";
            return View(viewEditCertificate, certificate);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCertificate(int id, CertificateDTO ctfc)
        {
            var certificate = _context.Certificates.Find(id);

            if (certificate != null)
            {
                certificate.CertificateName = ctfc.CertificateName;
                certificate.CertificateRank = ctfc.CertificateRank;
                _context.Update(certificate);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Employees", new { id = certificate.EmployeeId });
            }
            return NotFound();
        }

        public async Task<IActionResult> DeleteCertificate(int? id)
        {
            if (id == null || _context.Certificates == null)
            {
                return NotFound();
            }

            var certificate = await _context.Certificates.FindAsync(id);

            string viewDeleteCertficate = "~/Views/Employees/Certificate/DeleteCertificate.cshtml";
            return View(viewDeleteCertficate, certificate);
        }

        [HttpPost, ActionName("DeleteCertificate")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCertificateConfirmed(int id)
        {
            var certificate = await _context.Certificates.FindAsync(id);

            if (certificate != null)
            {
                _context.Certificates.Remove(certificate);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Employees", new { id = certificate.EmployeeId });
            }

            return NotFound();
        }

        private bool EmployeeExists(int id)
        {
          return _context.Employees.Any(e => e.EmployeeId == id);
        }

    }
}
