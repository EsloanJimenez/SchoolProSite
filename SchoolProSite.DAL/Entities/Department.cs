#nullable disable

using SchoolProSite.DAL.Core;
using System.ComponentModel.DataAnnotations;

namespace SchoolProSite.DAL.Entities
{
    public partial class Department : BaseEntities
    {
        public Department()
        {
            Courses = new HashSet<Course>();
        }

        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "El nombre del departamento es requerido")]
        [StringLength(50, ErrorMessage ="El nombre del departamento no puede ser mayor a 50 caracteres")]
        public string Name { get; set; }
        public decimal Budget { get; set; }
        public DateTime StartDate { get; set; }
        public int? Administrator { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
    }
}