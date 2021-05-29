using DNion_Market.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DNion_Market.Controllers
{
    public class MasterController : Controller
    {
        // GET: Master
        private dnEntities dn = new dnEntities();
        private SqlConnection con;
        public ActionResult CustomerList()
        {
            return View();
        }
        public ActionResult read_CustomerList()
        {
            var query = (from a in dn.Customers
                        select a);
            ViewBag.cust = query.ToList().OrderBy(a => a.id);
            return View();
        }

        public ActionResult add_Customer()
        {
            return View();
        }
        public ActionResult insert_Customer(string name, string addr, string email, string gender, DateTime? tgl_lahir)
        {
            string constr = ConfigurationManager.ConnectionStrings["dn_db_connecter"].ToString();
            con = new SqlConnection(constr);

            SqlCommand com = new SqlCommand("SP_Insert_Customer", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@name", name);
            com.Parameters.AddWithValue("@addr", addr);
            com.Parameters.AddWithValue("@email", email);
            com.Parameters.AddWithValue("@gender", gender);
            com.Parameters.AddWithValue("@tgl_lahir", tgl_lahir);

            con.Open();
            int i = com.ExecuteNonQuery();
            con.Close();
            if(i >= 1)
            {
                return RedirectToAction("CustomerList");
            }
            else
            {
                return null;
            }
        }
        public ActionResult edit_Customer(int id)
        {
            var query = (from a in dn.Customers
                         where a.id == id
                         select a);
            ViewBag.cust = query;
            ViewBag.id = id;
            return View();
        }
        public ActionResult update_Customer(int id, string name, string addr, string email)
        {
            string constr = ConfigurationManager.ConnectionStrings["dn_db_connecter"].ToString();
            con = new SqlConnection(constr);

            SqlCommand com = new SqlCommand("SP_Update_Customer", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@id", id);
            com.Parameters.AddWithValue("@name", name);
            com.Parameters.AddWithValue("@addr", addr);
            com.Parameters.AddWithValue("@email", email);

            con.Open();
            int i = com.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
            {
                return RedirectToAction("CustomerList");
            }
            else
            {
                return null;
            }
        }
        public ActionResult delete_Customer(int id)
        {
            string constr = ConfigurationManager.ConnectionStrings["dn_db_connecter"].ToString();
            con = new SqlConnection(constr);

            SqlCommand com = new SqlCommand("SP_Delete_Customer", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@id", id);
            con.Open();
            int i = com.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
            {
                return RedirectToAction("read_CustomerList");
            }
            else
            {
                return null;
            }
        }
    }
}