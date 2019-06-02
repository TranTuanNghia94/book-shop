using DoAn.Models;
using DoAn.Models.Action;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAn.Controllers
{
    public class BookController : Controller
    {
        
        /* SIZE OF PAGE */
        int page_size = 12;


        /* CREATE BOOK */
        [HttpGet]
        public ActionResult CreateBook()
        {
            if((string)Session["role"] == "admin" || Convert.ToInt32(Session["id"]) == 1)
            {
                ViewBag.Genre = GenreAction.Get_All_Genre();
                return View();
            }
            else
                return Redirect("~/Book/ShowBook");

        }

        [HttpPost]
        public ActionResult CreateBook(string Ten_Sach,string Tac_Gia, string The_Loai, string Nha_Xuat_Ban,string Mo_Ta,double Gia_Ban, HttpPostedFileBase File)
        {
            Book book = new Book();
            string path = "";
            if (File.ContentLength > 0)
            {
                string File_Name = Path.GetFileName(File.FileName);
                path = Path.Combine(Server.MapPath("~/UploadFiles"), File_Name);
                File.SaveAs(path);
                BookAction.Create_Book(Ten_Sach, Tac_Gia, The_Loai, Nha_Xuat_Ban, Mo_Ta, Gia_Ban, File_Name);
            }
            RecordAction.Create_Record(CustomerAction.Find_Customer((int)Session["id"]).email, CustomerAction.Find_Customer((int)Session["id"]).email + " create new book.", DateTime.Now.Date);
            return Redirect("~/Book/CreateBook");
        }
        
        /* ------------------------------------------------------------- */



        /* EDIT BOOK */
        [HttpGet]
        public ActionResult EditBook(int Id)
        {
            if ((string)Session["role"] == "admin" || (string)Session["role"] == "staff")
            {
                ViewBag.Book = BookAction.Find_Book(Id);
                ViewBag.Genre = GenreAction.Get_All_Genre();
                return View();
            }
            else
            {
                return Redirect("~/Book/ShowBook");
            }
                
        }

        [HttpPost]
        public ActionResult EditBook(int Id, string Ten_Sach, string Tac_Gia, string The_Loai, string Nha_Xuat_Ban,string Mo_Ta, double Gia_Ban )
        {
            
            BookAction.Edit_Book(Id, Ten_Sach, Tac_Gia, The_Loai, Nha_Xuat_Ban, Mo_Ta,Gia_Ban);
            RecordAction.Create_Record(CustomerAction.Find_Customer((int)Session["id"]).email, CustomerAction.Find_Customer((int)Session["id"]).email + " update book " + Ten_Sach, DateTime.Now.Date);
            return Redirect("~/Book/BookManager/" + Session["id"]);
        }

        /* ------------------------------------------------------------- */



        /* UPDATE IMAGE BOOK */
        public ActionResult UpdateImg(int Id, HttpPostedFileBase File)
        {
            string path = "";
            if (File.ContentLength > 0)
            {
                string File_Name = Path.GetFileName(File.FileName);
                path = Path.Combine(Server.MapPath("~/UploadFiles"), File_Name);
                File.SaveAs(path);
                BookAction.Update_Img(Id, File_Name);
            }
            RecordAction.Create_Record(CustomerAction.Find_Customer((int)Session["id"]).email, CustomerAction.Find_Customer((int)Session["id"]).email + " change image from " + BookAction.Find_Book(Id).ten_sach, DateTime.Now.Date);
            return Redirect("~/Book/BookDetail/" + Id);
        }



        /* DELETE BOOK */
        public ActionResult DeleteBook(int Id)
        {
            BookAction.Delete_Book(Id);
            RecordAction.Create_Record(CustomerAction.Find_Customer((int)Session["id"]).email, CustomerAction.Find_Customer((int)Session["id"]).email + " delete book " + BookAction.Find_Book(Id).ten_sach, DateTime.Now.Date);
            return Redirect("~/Book/BookManager/" + Session["id"]);
        }
        

        /* RESTORE BOOKS WERE DELETED */
        public ActionResult Unlock_Book(int Id)
        {
            BookAction.Restore_Book(Id);
            RecordAction.Create_Record(CustomerAction.Find_Customer((int)Session["id"]).email, CustomerAction.Find_Customer((int)Session["id"]).email + " restore book " + BookAction.Find_Book(Id).ten_sach, DateTime.Now.Date);
            return Redirect("~/Home/RestoreData");
        }


        /* SHOW BOOK */
        [HttpGet]
        public ActionResult ShowBook()
        {

            using (var db = new DBShop())
            {
                int page_number = db.DbBook.Where(item => item.flag == false).Count();
                ViewBag.PageNumber = (page_number / page_size) + (page_number % page_size == 0 ? 0 : 1);
                db.Dispose();
            }
                
            if (Session["id"] == null)
            {
                if (ReportAction.Find_Month(DateTime.Now.Month) != null)
                {
                    ReportAction.Change_Visit(1, DateTime.Now.Month);
                }
                else
                {
                    ReportAction.Create_Visit(1, DateTime.Now.Month);
                }

            }
            return View();
        }

        /* BOOK DETAIL */
        public ActionResult BookDetail(int Id)
        {
            if(Id != 0)
            {
                ViewBag.Book = BookAction.Find_Book(Id);
                return View();
            }
            else
            {
                return Redirect("~/Book/ShowBook");
            }
         
        }


        /* BOOK MANAGER */
        [HttpGet]
        public ActionResult BookManager()
        {
            if ((string)Session["role"] == "admin" || (string)Session["role"] == "staff")
            {
                using (var db = new DBShop())
                {
                    int page_number = db.DbBook.Where(item => item.flag == false).Count();
                    ViewBag.PageNumber = (page_number / page_size) + (page_number % page_size == 0 ? 0 : 1);
                    ViewBag.Genre = GenreAction.Get_All_Genre();
                    db.Dispose();
                }
                return View();
            }
            else
                return Redirect("~/Book/ShowBook");

        }


        /* CREATE GENRE */
        [HttpPost]
        public ActionResult CreateGenre(string Name)
        {
            GenreAction.Create_Genre(Name);
            RecordAction.Create_Record(CustomerAction.Find_Customer((int)Session["id"]).email, CustomerAction.Find_Customer((int)Session["id"]).email + " create new genre " + Name, DateTime.Now.Date);
            return Redirect("~/Book/BookManager");
        }


        /* UPDATE GENRE */
        [HttpPost]
        public ActionResult UpdateGenre(int ID,string Name)
        {
            GenreAction.Update_Genre(ID, Name);
            RecordAction.Create_Record(CustomerAction.Find_Customer((int)Session["id"]).email, CustomerAction.Find_Customer((int)Session["id"]).email + " update genre " + Name, DateTime.Now.Date);
            return Redirect("~/Book/BookManager");
        }


        /* DELETE GENRE */
        public ActionResult DeleteGenre(int ID)
        {
            GenreAction.Lock_Genre(ID);
            RecordAction.Create_Record(CustomerAction.Find_Customer((int)Session["id"]).email, CustomerAction.Find_Customer((int)Session["id"]).email + " delete genre" + GenreAction.Find_By_ID(ID).genre, DateTime.Now.Date);
            return Redirect("~/Book/BookManager/" + Session["id"]);
        }


        /* RESTORE GENRES WERE DELETED */
        public ActionResult UnlockGenre(int ID)
        {
            GenreAction.Unlock_Genre(ID);
            RecordAction.Create_Record(CustomerAction.Find_Customer((int)Session["id"]).email, CustomerAction.Find_Customer((int)Session["id"]).email + " restorer genre" + GenreAction.Find_By_ID(ID).genre, DateTime.Now.Date);
            return Redirect("~/Home/RestoreData");
        }







        /* jSON */
        public JsonResult IsAlreadyExist(string Ten_Sach)
        {
            return Json(BookAction.find(Ten_Sach), JsonRequestBehavior.AllowGet);
        }

        /* Get Book By Page Number */
        public JsonResult GetAllBook(int page)
        {
            return Json(BookAction.Show_Book(page,page_size), JsonRequestBehavior.AllowGet);
        }

        /* Find Book By Name */
        public JsonResult FindBook(string name)
        {
            return Json(BookAction.Find_Book(name), JsonRequestBehavior.AllowGet);
        }

        /* Find Book By Genre */
        public JsonResult GetBookByGenre(string name)
        {
            return Json(BookAction.Get_Book_By_Genre(name), JsonRequestBehavior.AllowGet);
        }

        /* Validation Genre By Name */
        public JsonResult ValidGenre(string Name)
        {
            return Json(GenreAction.Find_By_Name(Name), JsonRequestBehavior.AllowGet);
        }

        /* Find Genre By ID */
        public JsonResult GetdGenre(int ID)
        {
            return Json(GenreAction.Find_By_ID(ID), JsonRequestBehavior.AllowGet);
        }



    }
}