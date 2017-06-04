using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Configuration;
using System.IO;
using gstatus.Models;

namespace gstatus.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Home()
        {
            // datagstatus Datagstatus = new datagstatus();
            datagstatus a = new datagstatus();
            List<object> mymodel = new List<object>();
            mymodel.Add(a.student_data.ToList());
            mymodel.Add(a.gcode.ToList());
            ViewBag.id = new SelectList(a.gcode, "id", "c_Attributs");
            return View(mymodel);
        }

        [HttpPost]
        public ActionResult Home(string Reason, int? student_id, DateTime date1, string room_s, string room_sb)
        {
            datagstatus a = new datagstatus();
            List<object> mymodel = new List<object>();
            string user_1 = Session["UserName"].ToString();
            if (room_sb == "search")
            {
                mymodel.Add(a.student_data.Where(x => x.room_cup.Contains(room_s)).ToList());
                mymodel.Add(a.gcode.ToList());
                ViewBag.id = new SelectList(a.gcode, "id", "c_Attributs");

            } else
            {

                mymodel.Add(a.student_data.ToList());
                mymodel.Add(a.gcode.ToList());
                ViewBag.id = new SelectList(a.gcode, "id", "c_Attributs");
                using (var ctx = new datagstatus())
                {

                    var studentList = ctx.student_data.SqlQuery("Select * from student_data where Id = " + student_id).ToList();
                    //var asd = studentList[0].ToString();
                    //string qwerer = asd[Member_id];
                    string Member_ID = studentList[0].Member_id.ToString();
                    string _Name = studentList[0].Name.ToString();
                    string Room = studentList[0].room_cup.ToString();
                    string Standard = studentList[0].standard.ToString();
                    record rec = new record
                    {
                        Member_id = Member_ID,
                        Name = _Name,
                        date = date1,
                        c_Attributs = Reason,
                        user = user_1
                    };
                    using (var dbctx = new datagstatus())
                    {
                        dbctx.record.Add(rec);
                        dbctx.SaveChanges();
                    }

                }
            }

            ViewBag.reasons = Reason;
            var month = ""; var day = "";
            if (date1.Day < 10)
            {
                day = "0" + date1.Day;
            }

            else if (date1.Day > 9 && date1.Day < 32)
            {
                day = date1.Day.ToString();
            }
            if (date1.Month < 10)
            {
                month = "0" + date1.Month;
            }
            else if(date1.Month > 9 && date1.Month <13 )
            {
                month = date1.Month.ToString();
            }
            ViewBag.date11 = date1.Year+"-"+month+ "-" +day;
            ViewBag.room_ss = room_s;

           
            return View(mymodel);
        }

        public ActionResult Index()
        {

            return View();
        }

        datagstatus db = new datagstatus();


        [HttpPost]
        public ActionResult Index(Login login)
        {
            if (ModelState.IsValid)
             {
                var obj = db.Login.Where(a => a.Name.Equals(login.Name) && a.password.Equals(login.password)).FirstOrDefault();
                if(obj != null)
                {
                    ViewBag.loginstatus = "Jay Swaminarayan";
                    Session["UserName"] = obj.Name.ToString();
                    Session["Roal"] = obj.roal.ToString();
                }
                
                //db.Login.Add(login);
                //db.SaveChanges();
             }
            return RedirectToAction("Home");
        }

        public ActionResult Logout()
        {
            Session["UserName"] = null;
            return RedirectToAction("Home");
        }

        
    }
}


