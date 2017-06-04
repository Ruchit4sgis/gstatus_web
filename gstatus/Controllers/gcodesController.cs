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

namespace gstatus.Views
{
    public class gcodesController : Controller
    {
        private datagstatus db = new datagstatus();

        // GET: gcodes
        public ActionResult Index()
        {
            if (Session["Roal"].ToString() != "admin")
            {
                return RedirectToAction("~/Home/Home");
            }
            return View(db.gcode.ToList());
        }

        // GET: gcodes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            gcode gcode = db.gcode.Find(id);
            if (gcode == null)
            {
                return HttpNotFound();
            }
            return View(gcode);
        }

        // GET: gcodes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: gcodes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,code,c_Attributs,status")] gcode gcode)
        {
            if (ModelState.IsValid)
            {
                db.gcode.Add(gcode);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(gcode);
        }

        // GET: gcodes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            gcode gcode = db.gcode.Find(id);
            if (gcode == null)
            {
                return HttpNotFound();
            }
            return View(gcode);
        }

        // POST: gcodes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,code,c_Attributs,status")] gcode gcode)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gcode).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(gcode);
        }

        // GET: gcodes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            gcode gcode = db.gcode.Find(id);
            if (gcode == null)
            {
                return HttpNotFound();
            }
            return View(gcode);
        }

        // POST: gcodes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            gcode gcode = db.gcode.Find(id);
            db.gcode.Remove(gcode);
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
