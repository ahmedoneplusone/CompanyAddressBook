using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CompanyAddressBook.Data;
using CompanyAddressBook.Models;
using CompanyAddressBook.Models.ViewModels;

namespace CompanyAddressBook.Controllers
{
    public class ContactsController : Controller
    {
        private readonly CAB_DbContext _context;

        public ContactsController(CAB_DbContext context)
        {
            _context = context;
        }

        // GET: Contacts
        public async Task<IActionResult> Index()
        {
            return _context.Contacts != null ?
                        View(await _context.Contacts.Include(x => x.Company).ToListAsync()) :
                        Problem("Entity set 'CAB_DbContext.Contacts'  is null.");
        }

        // GET: Contacts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Contacts == null)
            {
                return NotFound();
            }

            var contact = await _context.Contacts.Include(x => x.Company)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        // GET: Contacts/Create/5
        public async Task<IActionResult> Create(int? id)
        {

            if (id == null || _context.companies == null)
            {
                return NotFound();
            }

                var model = new CompaniesAndContact
                {
                    companies = id==-1?await _context.companies.Select(n => new SelectListItem { Text = n.Name, Value = n.Id.ToString() }).ToListAsync()
                    :await _context.companies.Where(x=>x.Id==id).Select(n => new SelectListItem { Text = n.Name, Value = n.Id.ToString() }).ToListAsync()
                };
        
            return View(model);
        }

        // POST: Contacts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int? id, [Bind("Name,Phone,Age")] Contact contact, IFormCollection? formCollection)
        {
            if (id == null || _context.companies == null)
            {
                return NotFound();
            }
            if (id == -1)
            {
                id = int.Parse(formCollection["contact.Company"]);
            }
            var company = await _context.companies.FindAsync(id);
            if (company == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                contact.Company = company;
                _context.Add(contact);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Details), "Companies", new { id = id });
            }

            return View(contact);
        }

        // GET: Contacts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Contacts == null)
            {
                return NotFound();
            }

            var contact = await _context.Contacts.FindAsync(id);
            if (contact == null)
            {
                return NotFound();
            }
            return View(contact);
        }

        // POST: Contacts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Phone,Age")] Contact contact)
        {
            if (id != contact.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contact);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactExists(contact.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(contact);
        }

        // GET: Contacts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Contacts == null)
            {
                return NotFound();
            }

            var contact = await _context.Contacts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        // POST: Contacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Contacts == null)
            {
                return Problem("Entity set 'CAB_DbContext.Contacts'  is null.");
            }
            var contact = await _context.Contacts.FindAsync(id);
            if (contact != null)
            {
                _context.Contacts.Remove(contact);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactExists(int id)
        {
            return (_context.Contacts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
