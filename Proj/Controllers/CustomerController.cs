using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proj.Models;
using Proj.ViewModels;
using System.Data.Entity;
using System.Runtime.Caching;
using System.Threading.Tasks;
using System.Web.UI;

namespace Proj.Controllers
{
    [AllowAnonymous]
    public class CustomerController : Controller
    {
        private ApplicationDbContext _context;

        //Init _context
        public CustomerController()
        {
            _context = new ApplicationDbContext();
        }

        //calling Dispose method to realese unmanaged resoures
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        
        //After submitting the form, here we save it in to DB
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer customer)
        {
            if(!ModelState.IsValid)
            {
                var viewModel = new NewCustomerViewModel
                {
                    Customer = customer,
                    MembershipTypes = _context.MembershipTypes.ToList()
                };
                return View("Update", viewModel);
            }
            if (customer.Id == 0)
                _context.Customers.Add(customer);
            else
            {
                var customerInDb = _context.Customers.Single(c => c.Id == customer.Id);
                customerInDb.Name = customer.Name;
                customerInDb.Birthday = customer.Birthday;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Customer");
        }

        //opens the form for editting
        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();

            var viewModel = new NewCustomerViewModel
            {
                Customer = customer,
                MembershipTypes = _context.MembershipTypes.ToList()

            };
            return View("Update", viewModel);
        }

        //Deletes record
        public ActionResult Delete(int Id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == Id);
            if(customer != null)
            {
                _context.Customers.Remove(customer);
                _context.SaveChanges();
            }
            else
            {
                return HttpNotFound();
            }

            return RedirectToAction("Index", "Customer");
        }

        //Recieves disposable object
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FormToCreateCustomer(Customer customer)
        {
            if(!ModelState.IsValid)
            {
                var viewModel = new NewCustomerViewModel
                {
                    Customer = customer,
                    MembershipTypes = _context.MembershipTypes.ToList()
                };

                return View("AddNewCutomer", viewModel);
            }

            _context.Customers.Add(customer);
            _context.SaveChanges();

            return RedirectToAction("Index", "Customer");
        }

        //Adds a new customer
        public ActionResult AddNewCutomer()
        {
            var membershipTypes = _context.MembershipTypes.ToList();

            var viewModel = new NewCustomerViewModel
            {
                Customer = new Customer(),
                MembershipTypes = membershipTypes
            };

            return View (viewModel);
        }

        //Customer list
        [OutputCache(Duration = 50, Location = OutputCacheLocation.Downstream, 
            VaryByParam = "none")]
        public ActionResult Index()
        {
            var customers = _context
               .Customers
               .Include(i => i.MembershipType)
               .ToList();

            return View(customers);
        }

        //Displays details
        [OutputCache(Duration = 50, Location = OutputCacheLocation.Downstream)]
        public ActionResult Details(int id)
        {
            var customer = _context.Customers.Include(i => i.MembershipType).SingleOrDefault(i => i.Id == id);

            if (customer == null)
                return HttpNotFound();

            return View(customer);
        }

        public ActionResult ApiView()
        {
            return View();
        }
    }
}