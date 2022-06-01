using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC_HelloWorld.Models;
using MVC_HelloWorld.ViewModel;

namespace MVC_HelloWorld.Controllers
{
    public class ContactReasonsController : Controller
    {
        private ContactDbContext db = new ContactDbContext();

        // GET: ContactReasons
        public ActionResult Index()
        {
            return View(db.ContactReasons.ToList());
        }

        // GET: ContactReasons/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactReason contactReason = db.ContactReasons.Find(id);

            List<PersonViewModel> people = new List<PersonViewModel>();

            foreach(var ap in db.AppliedPeople.Where(e => e.ContactReasonId == id))
            {
                Person mainPerson = db.People.Where(e => e.Id == ap.PersonId).SingleOrDefault();

                PersonViewModel pvm = new PersonViewModel();
                pvm.Name = mainPerson.Name;
                pvm.EmailAddress = mainPerson.EmailAddress;
                pvm.Description = mainPerson.Description;

                people.Add(pvm);
            }

            ViewBag.People = people;

            if (contactReason == null)
            {
                return HttpNotFound();
            }
            return View(contactReason);
        }

        // GET: ContactReasons/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ContactReasons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Reason,Message")] ContactReason contactReason)
        {
            if (ModelState.IsValid)
            {
                db.ContactReasons.Add(contactReason);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(contactReason);
        }

        // GET: ContactReasons/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactReason contactReason = db.ContactReasons.Find(id);
            if (contactReason == null)
            {
                return HttpNotFound();
            }
            return View(contactReason);
        }

        // POST: ContactReasons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Reason,Message")] ContactReason contactReason)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contactReason).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contactReason);
        }

        // GET: ContactReasons/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactReason contactReason = db.ContactReasons.Find(id);
            if (contactReason == null)
            {
                return HttpNotFound();
            }
            return View(contactReason);
        }

        // POST: ContactReasons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ContactReason contactReason = db.ContactReasons.Find(id);
            db.ContactReasons.Remove(contactReason);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //Get /ContactReasons/AddPerson/5
        public ActionResult AddPerson(int? contactReasonId)
        {
            if (contactReasonId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.MainPeople = db.People.ToList();
            ViewBag.ContactReason = db.ContactReasons.Find(contactReasonId);

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddPerson([Bind(Include = "Name,EmailAddress,Description,ContactReasonId")] PersonViewModel pvm)
        {
            if(pvm != null)
            {
                int PersonId = db.People.Where(e => e.Name == pvm.Name).Select(e => e.Id).SingleOrDefault();

                AppliedPerson ap = new AppliedPerson();
                ap.ContactReasonId = pvm.ContactReasonId;
                ap.PersonId = PersonId;

                db.AppliedPeople.Add(ap);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(pvm);
        }
    }
}
