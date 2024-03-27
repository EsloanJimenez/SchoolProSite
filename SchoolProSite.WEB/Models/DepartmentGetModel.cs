namespace SchoolProSite.WEB.Models
{
    public class DepartmentGetModel
    {
        public int DepartmentId { get; set; }
        public string? Name { get; set; }
        public decimal Budget { get; set; }
        public DateTime StarDate { get; set; }
        public int? Administrator {  get; set; }
    }
}
