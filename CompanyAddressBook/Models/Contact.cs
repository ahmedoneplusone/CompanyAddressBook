using System.ComponentModel.DataAnnotations;

namespace CompanyAddressBook.Models
{
    public class Contact
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required"), MaxLength(100, ErrorMessage = "Name must be less than 100 characters")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Phone Number is required"), RegularExpression(@"\d{10}", ErrorMessage ="Please enter a valid phone number")]
        public string? Phone { get; set; }
        [Required(ErrorMessage = "Age is required"), Range(18,100,ErrorMessage ="Please enter a valid age (from 18 to 100)")]
        public int Age { get; set; }
        public virtual Company? Company { get; set; }
    }
}
