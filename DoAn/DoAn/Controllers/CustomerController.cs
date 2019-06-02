using DoAn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAn.Controllers
{
    
    public class CustomerController : Controller
    {
        /* Page Size */
        int page_size = 8;
 

        /* LIST CUSTOMER */
        public ActionResult Index()
        {
            if(Session["id"] != null && ((string)Session["role"] == "admin" || Convert.ToInt32(Session["id"]) == 1))
            {
                using (var db = new DBShop())
                {
                    int page_number = db.DbCustomer.Where(item => item.flag == false).Count();
                    db.Dispose();
                    ViewBag.Page =  page_number / page_size + (page_number % page_size == 0 ? 0 : 1);
                }
                return View();
                
                
            }
            else 
            {
                return Redirect("~/Book/ShowBook");
            }
            //ViewBag.Customer = CustomerAction.Show_Customer();
            //ViewBag.ID = Id;
            
        }


        /* REGISTER ACCOUNT */
        [HttpPost]
        public ActionResult Register(string Email, string First_Name, string Last_Name, 
            string Phone, string Address,string Date_Birth  ,string Gender, string Password)
        {
            CustomerAction.Create_Customer(Email, First_Name, Last_Name, Address, Phone, Password, Date_Birth, Gender);
            RecordAction.Create_Record(Email, Email + " create account.", DateTime.Now.Date);
            return Redirect("~/Book/ShowBook");
        }

        
        /* DELETE CUSTOMER */
        public ActionResult DeleteCustomer(int Id)
        {
            if (Id != 0)
            {
                CustomerAction.Delete_Customer(Id);
                RecordAction.Create_Record(CustomerAction.Find_Customer((int)Session["id"]).email, CustomerAction.Find_Customer((int)Session["id"]).email + " lock account " + CustomerAction.Find_Customer(Id).email, DateTime.Now.Date);
                return Redirect("~/Customer/Index/");
            }          
            return Redirect("~/Customer/Index/");
        }


        /* EDIT CUSTOMER */
        [HttpGet]
        public ActionResult Editcustomer(int Id)
        {
            //ViewBag.ID = Id;
            if(Id != 0)
            {
                Customer customer = CustomerAction.Find_Customer(Id);
                ViewBag.Customer = customer;
                return View();
            }
            else
            {
                return Redirect("~/Book/ShowBook");
            }
            
        }

        [HttpPost]
        public ActionResult EditCustomer(int Id, string Email, string Password, string First_name, string Last_name, string Address, 
            string Phone, string Date_Birth, string Gender)
        {
            ViewBag.ID = Id;
            CustomerAction.Edit_Customer(Id, Email, First_name, Last_name, Password, Phone, Date_Birth, Gender, Address);
            RecordAction.Create_Record(CustomerAction.Find_Customer((int)Session["id"]).email, CustomerAction.Find_Customer((int)Session["id"]).email + " update infomation.", DateTime.Now.Date);
            return Redirect("~/Customer/Index/" + Session["id"]);
        }

        /* ---------------------------------------------------------- */



        /* SHOW CUSTOMER INFORMATION BY ID */
        public ActionResult ViewCustomer(int Id)
        {
            ViewBag.Customer = CustomerAction.Find_Customer(Id);
            return View();
        }


        /* GET CUSTOMER BY ID */
        public ActionResult Customer_Info(int ID)
        {
            if(Session["id"] != null)
            {
                using (var db = new DBShop())
                {
                    int page = db.DbBill.Count();
                    ViewBag.Customer = CustomerAction.Find_Customer(ID);
                    ViewBag.Page = page / page_size + (page % page_size == 0 ? 0 : 1);
                    db.Dispose();
                }
                   
               
                return View();
            }
            else
            {
                return Redirect("~/Book/ShowBook");
            }
            
        }


        /* RESTORE CUSTOMERS WERE DELETED */
        public ActionResult UnLockCustomer(int Id)
        {
            if(Id != 0)
            {
                CustomerAction.UnLock(Id);
                RecordAction.Create_Record(CustomerAction.Find_Customer((int)Session["id"]).email, CustomerAction.Find_Customer((int)Session["id"]).email + " unlock account " + CustomerAction.Find_Customer(Id).email, DateTime.Now.Date);
                return Redirect("~/Home/RestoreData");
            }
            return Redirect("~/Customer/Index/");
        }





        /* JSON */


        /* Get Customer By ID */
        public JsonResult Staff(int Id)
        {
            return Json(CustomerAction.Find_Customer(Id), JsonRequestBehavior.AllowGet);
        }


        /* Set Role For User*/
        public JsonResult CreateStaff(int Id, string Role)
        {
            CustomerAction.Create_Staff(Id, Role);
            //return Redirect("~/Customer/Index/" + Session["id"]);
            RecordAction.Create_Record(CustomerAction.Find_Customer((int)Session["id"]).email, CustomerAction.Find_Customer((int)Session["id"]).email + " update role " + Role + "  for account " + CustomerAction.Find_Customer(Id).email, DateTime.Now.Date);
            return Json(JsonRequestBehavior.AllowGet);

        }


        /* Get Bill By Page */
        public JsonResult GetBill(int page)
        {
            return Json(BillAction.GetDetail((int)Session["id"], page_size, page), JsonRequestBehavior.AllowGet);
        }


        /* Show Customer By Page Number */
        public JsonResult ShowCustomer(int page_number)
        {
            
            return Json(CustomerAction.Show_Customer(page_number, page_size), JsonRequestBehavior.AllowGet);
        }


        /* Find Customer By Email */
        public JsonResult FindCustomer(string Email)
        {
            
            return Json(CustomerAction.Find_Customer(Email), JsonRequestBehavior.AllowGet);
        }


        /* Find Customer By ID */
        public JsonResult GetCustomer(int Id)
        {
            
            return Json(CustomerAction.Find_Customer(Id), JsonRequestBehavior.AllowGet);
        }


        /* Validation Email */
        public JsonResult IsAlreadySigned(string Email)
        {
            var tmp = CustomerAction.CheckCustomer(Email);
            return Json(tmp, JsonRequestBehavior.AllowGet);
        }


        /* SEARCH CUSTOMER BY EMAIL */
        public JsonResult Search(string Email)
        {

            return Json(CustomerAction.Find_Customer(Email), JsonRequestBehavior.AllowGet);
        }

    }
}