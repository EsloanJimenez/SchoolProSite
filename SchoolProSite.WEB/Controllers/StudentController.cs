using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolProSite.DAL.Entities;
using SchoolProSite.DAL.Exceptions;
using SchoolProSite.DAL.Interfaces;
using SchoolProSite.WEB.Models;

namespace SchoolProSite.WEB.Controllers
{
    public class StudentController : Controller
    {
        private readonly IDaoStudent daoStudent;
        public StudentController(IDaoStudent daoStudent)
        {
            this.daoStudent = daoStudent;
        }

        // GET: StudentController
        public ActionResult Index()
        {
            var student = this.daoStudent.GetStudents().Select(cd => new Models.StudentGetModel()
            {
                Id = cd.Id,
                LastName = cd.LastName,
                FirstName = cd.FirstName,
                EnrollmentDate = cd.EnrollmentDate,
                CreationDate = cd.CreationDate
            });

            return View(student);
        }

        // GET: StudentController/Details/5
        public ActionResult Details(int id)
        {
            var student = this.daoStudent.GetStudent(id);

            var modelStu = new Models.StudentGetModel() {
                Id = student.Id,
                LastName = student.LastName,
                FirstName = student.FirstName,
                EnrollmentDate = student.EnrollmentDate,
                CreationDate = student.CreationDate
            };

            return View(modelStu);
        }

        // GET: StudentController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StudentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StudentGetModel studentGetModel)
        {
            try
            {
                Student student = new Student()
                {
                    LastName = studentGetModel.LastName,
                    FirstName = studentGetModel.FirstName,
                    EnrollmentDate = studentGetModel?.EnrollmentDate,
                    CreationDate = DateTime.Now,
                    CreationUser = 1
                };

                this.daoStudent.SaveStudent(student);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StudentController/Edit/5
        public ActionResult Edit(int id)
        {
            var student = this.daoStudent.GetStudent(id);

            var modelStu = new Models.StudentGetModel() 
            {
                Id= student.Id,
                LastName = student.LastName,
                FirstName = student.FirstName,
                EnrollmentDate = student.EnrollmentDate
            };

            return View(modelStu);
        }

        // POST: StudentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(StudentGetModel studentGetModel)
        {
            try
            {
                Student student = new Student()
                {
                    Id = studentGetModel.Id,
                    LastName = studentGetModel.LastName,
                    FirstName = studentGetModel.FirstName,
                    EnrollmentDate = studentGetModel.EnrollmentDate,
                    ModifyDate = DateTime.Now,
                    UserMod = 1
                };

                this.daoStudent.UpdateStudent(student);

                return RedirectToAction(nameof(Index));
            }
            catch(DaoException dex)
            {
                ViewBag.Message = dex.Message;
                return View();
            }
            catch
            {
                return View();
            }
        }
    }
}
