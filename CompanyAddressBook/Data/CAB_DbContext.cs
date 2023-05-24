using CompanyAddressBook.Models;
using Microsoft.EntityFrameworkCore;

namespace CompanyAddressBook.Data
{
    public class CAB_DbContext : DbContext
    {
        public CAB_DbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Company>? companies { get; set; }
        public DbSet<Contact>? Contacts { get; set; }
    }
}
