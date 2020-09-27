using KalWeb.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace KalWeb.Controllers
{
    public class LoginController : Controller
    {
        SqlConnection con = null;
        SqlCommand cmd;
        DataTable dt;
        SqlDataAdapter da;
        DataSet ds;

        // GET: Login
        public ActionResult Index()
        {
            return View();
            //return RedirectToAction("Index", "/kalweb/Home");
        }
        [HttpGet]
        public ActionResult Login()
        {
           
            return View();
        }

        [HttpPost]
        public string ChkLogin(string data)
        {
            string Rout = "fail";

            con = new SqlConnection(ConfigurationManager.ConnectionStrings["Model11"].ToString());
            cmd = new SqlCommand("PRD_CheckLogin", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", System.Web.HttpContext.Current.Request["uname"]);
            cmd.Parameters.AddWithValue("@Password", System.Web.HttpContext.Current.Request["pass"]);
            // da = new SqlDataAdapter(cmd);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            //DataTable dt = new DataTable();
            //da.Fill(dt);
            if (dr.Read())
            {

                ViewBag.Agencyid = Int32.Parse(dr.GetValue(0).ToString());
                ViewBag.Roles = Int32.Parse(dr.GetValue(1).ToString());
                ViewBag.Userid = Int32.Parse(dr.GetValue(2).ToString());

                Session["agencyid"] = Int32.Parse(dr.GetValue(0).ToString());
                Session["roles"] = Int32.Parse(dr.GetValue(1).ToString());
                Session["userid"] = Int32.Parse(dr.GetValue(2).ToString());
                Session["username"] = (System.Web.HttpContext.Current.Request["uname"].ToString());
                Rout = "pass";
            }
            dr.Close();
            con.Close();
            return Rout;

            /*if (dt != null && dt.Rows.Count > 0)
            {
                dt.Columns["Id"].ColumnName = "Id";
                dt.Columns["Name"].ColumnName = "Name";
                return DataTableToJSONNew(dt);
            }
            else
            {
                return DataTableToJSONNew(dt);
            }*/
        }

        //[HttpPost]
        //public ActionResult Login(Login objLogin)
        //{
        //    return RedirectToAction("Index", "Home");
        //}
    }
}