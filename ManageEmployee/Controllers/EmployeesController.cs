using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ManageEmployee.Models;

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

            if(employee != null)
            {
                Emp emp = new Emp();
                emp.EmployeeId = id;
                emp.FullName = employee.FullName;
                emp.DateOfBirth = employee.DateOfBirth;
                emp.Gender = employee.Gender;
                emp.Address = employee.Address;
                emp.PhoneNumber = employee.PhoneNumber;
                emp.Email = employee.Email;
                emp.Certificates = employee.Certificates;
                if (experience != null)
                {
                    emp.ExpInYear = experience.ExpInYear;
                    emp.ProSkill = experience.ProSkill;
                }
                if (fresher != null)
                {
                    emp.GraduationDate = fresher.GraduationDate;
                    emp.GraduationRank = fresher.GraduationRank;
                    emp.Education = fresher.Education;
                }
                if (intern != null)
                {
                    emp.Majors = intern.Majors;
                    emp.Semester = intern.Semester;
                    emp.UniversityName = intern.UniversityName;
                }
                return View(emp);
            }
            return NotFound();
        }

        // GET: Employees/CreateExperience
        public IActionResult CreateExperience()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateExperience([Bind("FullName,DateOfBirth,Gender,Address,PhoneNumber,Email,ExpInYear,ProSkill,CertificateName,CertificateRank")] Exp exp)
        {
            if (ModelState.IsValid)
            {
                Employee employee = new Employee();
                employee.FullName = exp.FullName;
                employee.DateOfBirth = exp.DateOfBirth;
                employee.Gender = exp.Gender;
                employee.Address = exp.Address; 
                employee.PhoneNumber = exp.PhoneNumber;
                employee.Email = exp.Email;

                _context.Add(employee);
                await _context.SaveChangesAsync();

                Experience experience = new Experience();
                experience.EmployeeId = employee.EmployeeId;
                experience.ExpInYear = exp.ExpInYear;
                experience.ProSkill = exp.ProSkill;

                if (exp.CertificateName != null || exp.CertificateRank != null)
                {
                    Certificate certificate = new Certificate();
                    certificate.CertificateName = exp.CertificateName;
                    certificate.CertificateRank = exp.CertificateRank;
                    certificate.EmployeeId = experience.EmployeeId;
                    _context.Add(certificate);
                }

                _context.Add(experience);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: Employees/CreateFresher
        public IActionResult CreateFresher()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateFresher([Bind("FullName,DateOfBirth,Gender,Address,PhoneNumber,Email,GraduationDate,GraduationRank,Education,CertificateName,CertificateRank")] Frs frs)
        {
            if (ModelState.IsValid)
            {
                Employee employee = new Employee();
                employee.FullName = frs.FullName;
                employee.DateOfBirth = frs.DateOfBirth;
                employee.Gender = frs.Gender;
                employee.Address = frs.Address;
                employee.PhoneNumber = frs.PhoneNumber;
                employee.Email = frs.Email;

                _context.Add(employee);
                await _context.SaveChangesAsync();

                Fresher fresher = new Fresher();
                fresher.EmployeeId = employee.EmployeeId;
                fresher.GraduationDate = frs.GraduationDate;
                fresher.GraduationRank = frs.GraduationRank;
                fresher.Education = frs.Education;

                if (frs.CertificateName != null || frs.CertificateRank != null)
                {
                    Certificate certificate = new Certificate();
                    certificate.CertificateName = frs.CertificateName;
                    certificate.CertificateRank = frs.CertificateRank;
                    certificate.EmployeeId = fresher.EmployeeId;
                    _context.Add(certificate);
                }

                _context.Add(fresher);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: Employees/CreateIntern
        public IActionResult CreateIntern()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateIntern([Bind("FullName,DateOfBirth,Gender,Address,PhoneNumber,Email,Majors,Semester,UniversityName,CertificateName,CertificateRank")] Itn itn)
        {
            if (ModelState.IsValid)
            {
                Employee employee = new Employee();
                employee.FullName = itn.FullName;
                employee.DateOfBirth = itn.DateOfBirth;
                employee.Gender = itn.Gender;
                employee.Address = itn.Address;
                employee.PhoneNumber = itn.PhoneNumber;
                employee.Email = itn.Email;

                _context.Add(employee);
                await _context.SaveChangesAsync();

                Intern intern = new Intern();
                intern.EmployeeId = employee.EmployeeId;
                intern.Majors = itn.Majors;
                intern.Semester = itn.Semester;
                intern.UniversityName = itn.UniversityName;

                if (itn.CertificateName != null || itn.CertificateRank != null)
                {
                    Certificate certificate = new Certificate();
                    certificate.CertificateName = itn.CertificateName;
                    certificate.CertificateRank = itn.CertificateRank;
                    certificate.EmployeeId = intern.EmployeeId;
                    _context.Add(certificate);
                }

                _context.Add(intern);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
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
                Emp emp = new Emp();
                emp.FullName = employee.FullName;
                emp.DateOfBirth = employee.DateOfBirth;
                emp.Gender = employee.Gender;
                emp.Address = employee.Address;
                emp.PhoneNumber = employee.PhoneNumber;
                emp.Email = employee.Email;
                if (experience != null)
                {
                    emp.ExpInYear = experience.ExpInYear;
                    emp.ProSkill = experience.ProSkill;
                }
                else if (fresher != null)
                {
                    emp.GraduationDate = fresher.GraduationDate;
                    emp.GraduationRank = fresher.GraduationRank;
                    emp.Education = fresher.Education;
                }
                else if (intern != null)
                {
                    emp.Majors = intern.Majors;
                    emp.Semester = intern.Semester;
                    emp.UniversityName = intern.UniversityName;
                }
                return View(emp);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, 
            [Bind("FullName,DateOfBirth,Gender,Address,PhoneNumber,Email,ExpInYear,ProSkill,GraduationDate,GraduationRank,Education,Majors,Semester,UniversityName")] Emp emp)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var employee = await _context.Employees.FindAsync(id);
                    var experience = await _context.Experiences.FindAsync(id);
                    var fresher = await _context.Freshers.FindAsync(id);
                    var intern = await _context.Interns.FindAsync(id);

                    if (employee != null)
                    {
                        employee.FullName = emp.FullName;
                        employee.DateOfBirth = emp.DateOfBirth;
                        employee.Gender = emp.Gender;
                        employee.Address = emp.Address;
                        employee.PhoneNumber = emp.PhoneNumber;
                        employee.Email = emp.Email;

                        if (experience != null)
                        {
                            experience.ExpInYear = emp.ExpInYear;
                            experience.ProSkill = emp.ProSkill;
                            _context.Update(experience);
                        }
                        else if (fresher != null)
                        {
                            fresher.GraduationDate = emp.GraduationDate;
                            fresher.GraduationRank = emp.GraduationRank;
                            fresher.Education = emp.Education;
                            _context.Update(fresher);
                        }
                        else if (intern != null)
                        {
                            intern.Majors = emp.Majors;
                            intern.Semester = emp.Semester;
                            intern.UniversityName = emp.UniversityName;
                            _context.Update(intern);
                        }

                        _context.Update(employee);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(emp.EmployeeId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View();
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
                Emp emp = new Emp();
                emp.FullName = employee.FullName;
                emp.DateOfBirth = employee.DateOfBirth;
                emp.Gender = employee.Gender;
                emp.Address = employee.Address;
                emp.PhoneNumber = employee.PhoneNumber;
                emp.Email = employee.Email;
                emp.Certificates = employee.Certificates;
                if (experience != null)
                {
                    emp.ExpInYear = experience.ExpInYear;
                    emp.ProSkill = experience.ProSkill;
                }
                else if (fresher != null)
                {
                    emp.GraduationDate = fresher.GraduationDate;
                    emp.GraduationRank = fresher.GraduationRank;
                    emp.Education = fresher.Education;
                }
                else if (intern != null)
                {
                    emp.Majors = intern.Majors;
                    emp.Semester = intern.Semester;
                    emp.UniversityName = intern.UniversityName;
                }
                return View(emp);
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
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCertificate(int id, [Bind("CertificateName,CertificateRank")] Ctfc ctfc)
        {
            if (ModelState.IsValid)
            {
                Certificate cert = new Certificate();
                cert.CertificateName = ctfc.CertificateName;
                cert.CertificateRank = ctfc.CertificateRank;
                cert.EmployeeId = id;

                _context.Add(cert);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Employees", new { id = id });
            }
            return View();
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> EditCertificate(int? id)
        {
            if (id == null || _context.Certificates == null)
            {
                return NotFound();
            }

            var certificate = await _context.Certificates.FindAsync(id);
            return View(certificate);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCertificate(int id, [Bind("CertificateName,CertificateRank")] Ctfc ctfc)
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


            return View(certificate);
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
