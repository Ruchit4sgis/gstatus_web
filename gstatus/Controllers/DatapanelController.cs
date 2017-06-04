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

namespace gstatus.Controllers
{
    public class DatapanelController : Controller
    {
        // GET: Datapanel
        public ActionResult ImportData()
        {            
           if(Session["Roal"].ToString() != "admin")
            {
                return RedirectToAction("~/Home/Home");
            }
            return View();
        }

        //datagstatus Datagstatus = new datagstatus();
        //var a = Datagstatus.student_data.ToList();
        //return View(a);

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["datagstatus"].ConnectionString);
        OleDbConnection Econ;

        private void ExcelConn(string filepath)
        {
            string constr = string.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 12.0 Xml;HDR=YES;""", filepath);
            Econ = new OleDbConnection(constr);

        }



        [HttpPost]
        public ActionResult ImportData(HttpPostedFileBase file,string command)
        {
            if(command == "g_details")
            {
                string filename = Guid.NewGuid() + Path.GetExtension(file.FileName);
                string filepath = "/excelfolder/" + filename;
                file.SaveAs(Path.Combine(Server.MapPath("/excelfolder"), filename));
                InsertExceldata_studentdetails(filepath, filename);
                ViewBag.Success = "Successfully Data Impororted";                
            }
            else if (command== "g_code")
            {
                string filename = Guid.NewGuid() + Path.GetExtension(file.FileName);
                string filepath = "/excelfolder/" + filename;
                file.SaveAs(Path.Combine(Server.MapPath("/excelfolder"), filename));
                InsertExceldata_gstatus_code(filepath, filename);
                ViewBag.Success = "Successfully Data Impororted";
            }
            return View();
        }
        

        private void InsertExceldata_studentdetails(string fileepath, string filename)
        {
            string fullpath = Server.MapPath("/excelfolder/") + filename;
            ExcelConn(fullpath);
            string query = string.Format("Select * from [{0}]", "Sheet1$");
            OleDbCommand Ecom = new OleDbCommand(query, Econ);
            Econ.Open();

            DataSet ds = new DataSet();
            OleDbDataAdapter oda = new OleDbDataAdapter(query, Econ);
            Econ.Close();
            oda.Fill(ds);

            DataTable dt = ds.Tables[0];

            SqlBulkCopy objbulk = new SqlBulkCopy(con);
            objbulk.DestinationTableName = "student_data";
            objbulk.ColumnMappings.Add("Member_id", "Member_id");
            objbulk.ColumnMappings.Add("Name", "Name");
            objbulk.ColumnMappings.Add("standard", "standard");
            objbulk.ColumnMappings.Add("room_cup", "room_cup");
            objbulk.ColumnMappings.Add("A_year", "A_year");
            con.Open();
            objbulk.WriteToServer(dt);
            con.Close();
        }
        private void InsertExceldata_gstatus_code(string fileepath, string filename)
        {
            string fullpath = Server.MapPath("/excelfolder/") + filename;
            ExcelConn(fullpath);
            string query = string.Format("Select * from [{0}]", "Sheet1$");
            OleDbCommand Ecom = new OleDbCommand(query, Econ);
            Econ.Open();

            DataSet ds = new DataSet();
            OleDbDataAdapter oda = new OleDbDataAdapter(query, Econ);
            Econ.Close();
            oda.Fill(ds);

            DataTable dt = ds.Tables[0];

            SqlBulkCopy objbulk = new SqlBulkCopy(con);
            objbulk.DestinationTableName = "gcodes";
            objbulk.ColumnMappings.Add("code", "code");
            objbulk.ColumnMappings.Add("c_Attributs", "c_Attributs");
            objbulk.ColumnMappings.Add("status", "status");
            con.Open();
            objbulk.WriteToServer(dt);
            con.Close();
        }

        public ActionResult student()
        {
            string file = @"c:\downloade\Student Detail Import Format.xlsx";
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            return File(file, contentType, Path.GetFileName(file));
        }

        public ActionResult gcode()
        {
            string file = @"c:\downloade\gCodes.xlsx";
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            return File(file, contentType, Path.GetFileName(file));
        }
        public ActionResult records()
        {
            string file = @"c:\downloade\records.xlsx";
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            return File(file, contentType, Path.GetFileName(file));
        }


    }
}