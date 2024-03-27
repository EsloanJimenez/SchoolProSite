using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolProSite.DAL.Dao;
using SchoolProSite.DAL.Entities;
using SchoolProSite.DAL.Exceptions;
using SchoolProSite.DAL.Interfaces;
using SchoolProSite.WEB.Models;

namespace SchoolProSite.WEB.Controllers
{
    public class InstructorController : Controller
    {
        private readonly IDaoInstructor daoInstructor;
        public InstructorController(IDaoInstructor daoInstructor) { 
            this.daoInstructor = daoInstructor;
        }

        // GET: InstructorController
        public ActionResult Index()
        {
            var instructor = this.daoInstructor.GetInstructor().Select(cd => new Models.InstructorGetModel()
            {
                Id = cd.Id,
                LastName = cd.LastName,
                FirstName = cd.FirstName,
                HireDate = cd.HireDate,
                CreationDate = cd.CreationDate
            });

            return View(instructor);
        }

        // GET: InstructorController/Details/5
        public ActionResult Details(int id)
        {
            var instructor = this.daoInstructor.GetInstructor(id);

            var modelInst = new Models.InstructorGetModel()
            {
                Id = instructor.Id,
                LastName = instructor.LastName,
                FirstName = instructor.FirstName,
                HireDate = instructor.HireDate,
                CreationDate = instructor.CreationDate
            };

            return View(modelInst);
        }

        // GET: InstructorController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: InstructorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(InstructorGetModel instructorGetModel)
        {
            try
            {
                Instructor instructor = new Instructor()
                {
                    LastName = instructorGetModel.LastName,
                    FirstName = instructorGetModel.FirstName,
                    HireDate = instructorGetModel?.HireDate,
                    CreationUser = 1,
                    CreationDate = DateTime.Now
                };

                this.daoInstructor.SaveInstructor(instructor);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: InstructorController/Edit/5
        public ActionResult Edit(int id)
        {
            var instructor = this.daoInstructor.GetInstructor(id);

            var modelInst = new Models.InstructorGetModel()
            {
                Id = instructor.Id,
                LastName = instructor.LastName,
                FirstName = instructor.FirstName,
                HireDate = instructor.HireDate
            };

            return View(modelInst);
        }

        // POST: InstructorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(InstructorGetModel instructorGetModel)
        {
            try
            {
                Instructor instructor = new Instructor()
                {
                    Id = instructorGetModel.Id,
                    LastName = instructorGetModel.LastName,
                    FirstName = instructorGetModel.FirstName,
                    HireDate = instructorGetModel?.HireDate,
                    UserMod = 1,
                    ModifyDate = DateTime.Now
                };

                this.daoInstructor.UpdateInstructor(instructor);

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
