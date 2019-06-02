using DoAn.Models;
using DoAn.Models.Action;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAn.Controllers
{
    public class HomeController : Controller
    {

      
       
        /* LOGIN */
        [HttpGet]
        public ActionResult Login()
        {
            Session["role"] = null;
            Session["id"] = null;
            return Redirect("~/Book/ShowBook");
        }
        
        public ActionResult Login(string Mail, string Pass)
        {
            string page = "";
            var customer = CustomerAction.Find_Customer(Mail, Pass);

            if (customer != null)
            {
                ViewBag.ID = customer.id_customer;
                if (customer.password == Pass && (customer.role.Equals("admin") || customer.role.Equals("staff")) || customer.id_customer == 1)
                {
                    page = "~/Customer/Index";
                    Session["id"] = customer.id_customer;
                    Session["role"] = customer.role;
                    Session.Timeout = 24;
                    
                }
                else if (customer.password == Pass && customer.role.Equals("guest"))
                {
                    page = "~/Book/ShowBook";
                    Session["id"] = customer.id_customer;
                    Session["role"] = customer.role;
                    Session.Timeout = 24;
                   

                }
                
            }
            else if (customer == null)
            {
                page = "~/Book/ShowBook";
            }
            
            return Redirect(page);
        }
        /* ------------------------------- */


        /* LOGOUT */
        public ActionResult Logout()
        {
            string page = "";
            if (Session["id"] != null)
            {
                Session.Clear();
                page = "~/Book/ShowBook";
            }
            return Redirect(page);
        }
        

        /* SIZE OF PAGE */
        int page_size = 20;


        /* Show Record And Auto Delete Record After 3 Days */
        public ActionResult RecordUser()
        {
            if ((string)Session["role"] == "admin")
            {
                int page = RecordAction.ShowAll().Count;
                RecordAction.Auto_Delete_Record(3); // Set 3 Days Auto Delete Record
                ViewBag.Page = page / page_size +  ( page % page_size == 0 ? 0 : 1);
                return View();
            }
            else
                return Redirect("~/Book/ShowBook");
            
        }



        /* RESTORE DATA */
        public ActionResult RestoreData()
        {
            if((string)Session["role"] == "admin")
            {
                using (var db = new DBShop())
                {
                    int page_customer = db.DbCustomer.Where(item => item.flag == true).Count();
                    int page_book = db.DbBook.Where(item => item.flag == true).Count();
                    db.Dispose();
                    ViewBag.Customer = page_customer / page_size + (page_customer % page_size == 0 ? 0 : 1);
                    ViewBag.Book = page_book / page_size + (page_book % page_size == 0 ? 0 : 1);
                    ViewBag.Genre = GenreAction.Restore_Genre();
                }

                return View();
            }
            else
            {
                return Redirect("~/Book/ShowBook");
            }
            
        }



        /* Json  */
        public JsonResult Record_User(int page)
        {
            return Json(RecordAction.ShowAll(page, page_size), JsonRequestBehavior.AllowGet);
        }


        /* List Customers Were Deleted  */
        public JsonResult List_Lock_User(int page)
        {
            return Json(CustomerAction.Get_Customer_Delete(page, page_size), JsonRequestBehavior.AllowGet);
        }


        /*  List Books Were Deleted */
        public JsonResult List_Lock_Book(int page)
        {
            return Json(BookAction.Get_Book_Delete(page, page_size), JsonRequestBehavior.AllowGet);
        }

    
    }
}