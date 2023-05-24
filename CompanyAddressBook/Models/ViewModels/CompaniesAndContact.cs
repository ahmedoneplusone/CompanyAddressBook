using Microsoft.AspNetCore.Mvc.Rendering;

namespace CompanyAddressBook.Models.ViewModels
{
    public class CompaniesAndContact
    {
        public Contact? contact { get; set; }
        public List<SelectListItem>? companies { get; set; }
    }
}
