using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using gstatus;
using gstatus.Models;

namespace gstatus.Controllers
{
    public class student_data_detailController : Controller
    {
        private datagstatus db = new datagstatus();

        // GET: student_data_detail
        public ActionResult Index()
        {
            if (Session["Roal"].ToString() != "admin")
            {
                return RedirectToAction("~/Home/Home");
            }
            return View(db.student_data.ToList());
        }

        // GET: student_data_detail/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            student_data student_data = db.student_data.Find(id);
            if (student_data == null)
            {
                return HttpNotFound();
            }
            return View(student_data);
        }

        // GET: student_data_detail/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: student_data_detail/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Member_id,Name,standard,room_cup,A_year")] student_data student_data)
        {
            if (ModelState.IsValid)
            {
                db.student_data.Add(student_data);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(student_data);
        }

        // GET: student_data_detail/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            student_data student_data = db.student_data.Find(id);
            if (student_data == null)
            {
                return HttpNotFound();
            }
            return View(student_data);
        }

        // POST: student_data_detail/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Member_id,Name,standard,room_cup,A_year")] student_data student_data)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student_data).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(student_data);
        }

        // GET: student_data_detail/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            student_data student_data = db.student_data.Find(id);
            if (student_data == null)
            {
                return HttpNotFound();
            }
            return View(student_data);
        }

        // POST: student_data_detail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            student_data student_data = db.student_data.Find(id);
            db.student_data.Remove(student_data);
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
    }
}
