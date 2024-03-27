namespace SchoolProSite.WEB.Models
{
    public class InstructorGetModel
    {
        public int Id { get; set; }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public DateTime? HireDate { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
