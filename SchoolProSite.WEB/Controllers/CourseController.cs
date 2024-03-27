using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolProSite.DAL.Interfaces;
using SchoolProSite.WEB.Models;

namespace SchoolProSite.WEB.Controllers
{
    public class CourseController : Controller
    {
        private readonly IDaoCourse daoCourse;
        private readonly IDaoDepartment daoDepartment;

        public CourseController(IDaoCourse daoCourse, IDaoDepartment daoDepartment)
        {
            this.daoCourse = daoCourse;
            this.daoDepartment = daoDepartment;
        }

        // GET: CourseController
        public ActionResult Index()
        {
            var courses = this.daoCourse.GetCourses().Select(cd => new Models.CourseGetModel(cd));

            return View(courses);
        }

        // GET: CourseController/Details/5
        public ActionResult Details(int id)
        {
            var course = this.daoCourse.GetCourse(id);

            CourseGetModel courseGetModel = new CourseGetModel(course);

            return View(courseGetModel);
        }

        // GET: CourseController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CourseController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CourseController/Edit/5
        public ActionResult Edit(int id)
        {
            var course = this.daoCourse.GetCourse(id);

            CourseGetModel courseGetModel = new CourseGetModel(course);

            ViewData["Department"] = new SelectList(this.daoDepartment.GetDepartments(), "DepartmentId", "Name");

            return View(courseGetModel);
        }

        // POST: CourseController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
