using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolProSite.DAL.Entities;
using SchoolProSite.DAL.Exceptions;
using SchoolProSite.DAL.Interfaces;
using SchoolProSite.WEB.Models;

namespace SchoolProSite.WEB.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDaoDepartment daoDepartment;
        public DepartmentController(IDaoDepartment daoDepartment) {
            this.daoDepartment = daoDepartment;
        }

        // GET: DepartmentController
        public ActionResult Index()
        {
            var departments = this.daoDepartment.GetDepartments().Select(cd => new Models.DepartmentGetModel()
            {
                Administrator = cd.Administrator,
                Budget = cd.Budget,
                DepartmentId = cd.DepartmentId,
                Name = cd.Name,
                StarDate = cd.StartDate

            });

            return View(departments);
        }

        // GET: DepartmentController/Details/5
        public ActionResult Details(int id)
        {
            var department = this.daoDepartment.GetDepartment(id);

            var modelDepto = new Models.DepartmentGetModel()
            {
                DepartmentId = department.DepartmentId,
                Name = department.Name,
                Budget = department.Budget,
                StarDate = department.StartDate,
                Administrator = department.Administrator
            };

            return View(modelDepto);
        }

        // GET: DepartmentController/Create
        public ActionResult Create()
        {


            return View();
        }

        // POST: DepartmentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DepartmentGetModel departmentGetModel)
        {
            try
            {
                Department department = new Department()
                {
                    Name = departmentGetModel.Name,
                    Budget = departmentGetModel.Budget,
                    StartDate = departmentGetModel.StarDate,
                    Administrator = departmentGetModel.Administrator,
                    CreationUser = 1,
                    CreationDate = DateTime.Now,
                };

                this.daoDepartment.SaveDepartment(department);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DepartmentController/Edit/5
        public ActionResult Edit(int id, string name)
        {
            var department = this.daoDepartment.GetDepartment(id);

            var modelDepto = new Models.DepartmentGetModel()
            {
                DepartmentId = department.DepartmentId,
                Name = department.Name,
                Budget = department.Budget,
                StarDate = department.StartDate,
                Administrator = department.Administrator
            };

            return View(modelDepto);
        }

        // POST: DepartmentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DepartmentGetModel departmentGetModel)
        {
            try
            {
                Department department = new Department()
                {
                    DepartmentId = departmentGetModel.DepartmentId,
                    Name = departmentGetModel.Name,
                    Budget = departmentGetModel.Budget,
                    StartDate = departmentGetModel.StarDate,
                    Administrator = departmentGetModel.Administrator,
                    UserMod = 1,
                    ModifyDate = DateTime.Now
                };

                this.daoDepartment.UpdateDepartment(department);

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
