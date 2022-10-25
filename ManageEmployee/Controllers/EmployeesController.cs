using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ManageEmployee.Models;
using ManageEmployee.Models.DTO;

namespace ManageEmployee.Controllers
{
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
                    Exp exp = new Exp(
                        employee.FullName,
                        employee.DateOfBirth,
                        employee.Gender,
                        employee.Address,
                        employee.PhoneNumber,
                        employee.Email,
                        experience.ExpInYear,
                        experience.ProSkill);
                    exp.EmployeeId = employee.EmployeeId;
                    exp.Certificates = employee.Certificates;

                    string viewDetailsExperience = "~/Views/Employees/Experiences/DetailsExperience.cshtml";
                    return View(viewDetailsExperience, exp);
                }
                else if (fresher != null)
                {
                    Frs frs = new Frs(
                        employee.FullName,
                        employee.DateOfBirth,
                        employee.Gender,
                        employee.Address,
                        employee.PhoneNumber,
                        employee.Email,
                        fresher.GraduationDate,
                        fresher.GraduationRank,
                        fresher.Education);
                    frs.EmployeeId = employee.EmployeeId;
                    frs.Certificates = employee.Certificates;

                    string viewDetailsFresher = "~/Views/Employees/Freshers/DetailsFresher.cshtml";
                    return View(viewDetailsFresher, frs);
                }
                else if (intern != null)
                {
                    Itn itn = new Itn(
                        employee.FullName,
                        employee.DateOfBirth,
                        employee.Gender,
                        employee.Address,
                        employee.PhoneNumber,
                        employee.Email,
                        intern.Majors,
                        intern.Semester,
                        intern.UniversityName);
                    itn.EmployeeId = employee.EmployeeId;
                    itn.Certificates = employee.Certificates;

                    string viewDetailsIntern = "~/Views/Employees/Interns/DetailsIntern.cshtml";
                    return View(viewDetailsIntern, itn);
                }
            }
            return NotFound();
        }

        // GET: Employees/CreateExperience
        public IActionResult CreateExperience()
        {
            return View("~/Views/Employees/Experiences/CreateExperience.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateExperience(Exp exp)
        {
            if (ModelState.IsValid)
            {
                Employee employee = new Employee(
                    exp.FullName,
                    exp.DateOfBirth,
                    exp.Gender,
                    exp.Address,
                    exp.PhoneNumber,
                    exp.Email);

                _context.Add(employee);
                await _context.SaveChangesAsync();

                Experience experience = new Experience(
                    employee.EmployeeId,
                    exp.ExpInYear,
                    exp.ProSkill);
                _context.Add(experience);

                if (exp.CertificateName != null || exp.CertificateRank != null)
                {
                    Certificate certificate = new Certificate(
                        exp.CertificateName,
                        exp.CertificateRank,
                        employee.EmployeeId);
                    _context.Add(certificate);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }

        // GET: Employees/CreateFresher
        public IActionResult CreateFresher()
        {
            return View("~/Views/Employees/Fresher/CreateFresher.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateFresher(Frs frs)
        {
            if (ModelState.IsValid)
            {
                Employee employee = new Employee(
                    frs.FullName,
                    frs.DateOfBirth,
                    frs.Gender,
                    frs.Address,
                    frs.PhoneNumber,
                    frs.Email);

                _context.Add(employee);
                await _context.SaveChangesAsync();

                Fresher fresher = new Fresher(
                    employee.EmployeeId,
                    frs.GraduationDate,
                    frs.GraduationRank,
                    frs.Education);
                _context.Add(fresher);

                if (frs.CertificateName != null || frs.CertificateRank != null)
                {
                    Certificate certificate = new Certificate(
                        frs.CertificateName,
                        frs.CertificateRank,
                        employee.EmployeeId);
                    _context.Add(certificate);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }

        // GET: Employees/CreateIntern
        public IActionResult CreateIntern()
        {
            return View("~/Views/Employees/Intern/CreateIntern.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateIntern(Itn itn)
        {
            if (ModelState.IsValid)
            {
                Employee employee = new Employee(
                    itn.FullName,
                    itn.DateOfBirth,
                    itn.Gender,
                    itn.Address,
                    itn.PhoneNumber,
                    itn.Email);

                _context.Add(employee);
                await _context.SaveChangesAsync();

                Intern intern = new Intern(
                    employee.EmployeeId,
                    itn.Majors,
                    itn.Semester,
                    itn.UniversityName);
                _context.Add(intern);

                if (itn.CertificateName != null || itn.CertificateRank != null)
                {
                    Certificate certificate = new Certificate(
                        itn.CertificateName,
                        itn.CertificateRank,
                        employee.EmployeeId);
                    _context.Add(certificate);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
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
                    Exp exp = new Exp(
                        employee.FullName,
                        employee.DateOfBirth,
                        employee.Gender,
                        employee.Address,
                        employee.PhoneNumber,
                        employee.Email,
                        experience.ExpInYear,
                        experience.ProSkill);

                    string viewEditExperience = "~/Views/Employees/Experiences/EditExperience.cshtml";
                    return View(viewEditExperience, exp);
                }
                else if (fresher != null)
                {
                    Frs frs = new Frs(
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
                    return View(viewEditFresher, frs);
                }
                else if (intern != null)
                {
                    Itn itn = new Itn(
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
                    return View(viewEditIntern, itn);
                }
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Exp exp, Frs frs, Itn itn)
        {
            if (ModelState.IsValid)
            {
                var employee = await _context.Employees.FindAsync(id);
                var experience = await _context.Experiences.FindAsync(id);
                var fresher = await _context.Freshers.FindAsync(id);
                var intern = await _context.Interns.FindAsync(id);

                if (exp != null)
                {
                    if (employee != null)
                    {
                        employee.FullName = exp.FullName;
                        employee.DateOfBirth = exp.DateOfBirth;
                        employee.Gender = exp.Gender;
                        employee.Address = exp.Address;
                        employee.PhoneNumber = exp.PhoneNumber;
                        employee.Email = exp.Email;
                        _context.Update(employee);
                        if (experience != null)
                        {
                            experience.ExpInYear = exp.ExpInYear;
                            experience.ProSkill = exp.ProSkill;

                            _context.Update(experience); 
                            await _context.SaveChangesAsync();
                            return RedirectToAction(nameof(Index));
                        }
                        else if (fresher != null)
                        {
                            fresher.GraduationDate = frs.GraduationDate;
                            fresher.GraduationRank = frs.GraduationRank;
                            fresher.Education = frs.Education;

                            _context.Update(fresher);
                            await _context.SaveChangesAsync();
                            return RedirectToAction(nameof(Index));
                        }
                        else if (intern != null)
                        {
                            intern.Majors = itn.Majors;
                            intern.Semester = itn.Semester;
                            intern.UniversityName = itn.UniversityName;

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
                    Exp exp = new Exp(
                        employee.FullName,
                        employee.DateOfBirth,
                        employee.Gender,
                        employee.Address,
                        employee.PhoneNumber,
                        employee.Email,
                        experience.ExpInYear,
                        experience.ProSkill);
                    exp.EmployeeId = employee.EmployeeId;
                    exp.Certificates = employee.Certificates;

                    string viewDeleteExperience = "~/Views/Employees/Experiences/DeleteExperience.cshtml";
                    return View(viewDeleteExperience, exp);
                }
                else if (fresher != null)
                {
                    Frs frs = new Frs(
                        employee.FullName,
                        employee.DateOfBirth,
                        employee.Gender,
                        employee.Address,
                        employee.PhoneNumber,
                        employee.Email,
                        fresher.GraduationDate,
                        fresher.GraduationRank,
                        fresher.Education);
                    frs.EmployeeId = employee.EmployeeId;
                    frs.Certificates = employee.Certificates;

                    string viewDeleteFresher = "~/Views/Employees/Freshers/DeleteFresher.cshtml";
                    return View(viewDeleteFresher, frs);
                }
                else if (intern != null)
                {
                    Itn itn = new Itn(
                        employee.FullName,
                        employee.DateOfBirth,
                        employee.Gender,
                        employee.Address,
                        employee.PhoneNumber,
                        employee.Email,
                        intern.Majors,
                        intern.Semester,
                        intern.UniversityName);
                    itn.EmployeeId = employee.EmployeeId;
                    itn.Certificates = employee.Certificates;

                    string viewDeleteIntern = "~/Views/Employees/Interns/DeleteIntern.cshtml";
                    return View(viewDeleteIntern, itn);
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
        public async Task<IActionResult> CreateCertificate(int id, Ctfc ctfc)
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
            return View();
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
        public async Task<IActionResult> EditCertificate(int id, Ctfc ctfc)
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
