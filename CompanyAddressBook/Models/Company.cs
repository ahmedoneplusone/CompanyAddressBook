using System.ComponentModel.DataAnnotations;

namespace CompanyAddressBook.Models
{
    public class Company
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Name is required"),MaxLength(100,ErrorMessage ="Name must be less than 100 characters")]
        public string? Name { get; set; }
        public int MaxContacts { get; set; } = 0;
        public int ContactAge { get; set; }
        public virtual List<Contact>? Contacts { get; set; }
    }
}
