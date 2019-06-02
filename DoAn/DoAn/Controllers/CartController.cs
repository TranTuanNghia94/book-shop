using DoAn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAn.Controllers
{
    public class CartController : Controller
    {

        /* SHOW CART ITEMS */
        public ActionResult XemGioHang()
        {
            if( Session["id"] != null)
            {
                List<Item> item = new List<Item>();
                Dictionary<int, Item> tmp = (Dictionary<int, Item>)Session["cart"];
                if (tmp != null)
                {
                    foreach (var i in tmp.Values)
                    {
                        item.Add(i);
                    }
                    ViewBag.Cart = item;
                }
                else
                    ViewBag.Cart = null;
                return View();
            }
            else
                return Redirect("~/Book/ShowBook");

        }


        /* REMOVE SPECIFIC ITEM IN CART */
        public ActionResult Remove(int id)
        {
            Dictionary<int, Item> tmp = (Dictionary<int, Item>)Session["cart"];
            tmp.Remove(id);
            if (tmp.Count > 0)
                Session["cart"] = tmp;
            else
                Session["cart"] = null;
            return Redirect("~/Cart/XemGioHang");
        }


        /* CLEAR CART */
        public ActionResult RemoveAll()
        {
            Dictionary<int, Item> tmp = (Dictionary<int, Item>)Session["cart"];
            tmp.Clear();
            tmp = null;
            Session["cart"] = null;
            return Redirect("~/Cart/XemGioHang");
        }

        

        /* CREATE BILL AND SAVE DB */
        public ActionResult BuyBook()
        {
            List<Item> item = new List<Item>();
            Dictionary<int, Item> tmp = (Dictionary<int, Item>)Session["cart"];
            foreach (var i in tmp.Values)
            {
                item.Add(i);
            }
            int ID_Customer = int.Parse(Session["id"].ToString());
            string Date_Create = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;
            OrderAction.Create_Order(ID_Customer, Date_Create);
            int id_order = OrderAction.Find_Order();
            for (int i = 0; i < item.Count; i++)
            {
                BillAction.Create_Bill(
                    id_order,
                    item[i].book.id_sach,
                    item[i].book.ten_sach,
                    item[i].book.tac_gia,
                    item[i].so_luong,
                    item[i].book.gia_ban,
                    item[i].so_luong * item[i].book.gia_ban
                    );
            }
            tmp.Clear();
            item.Clear();
            Session["Cart"] = null;
            tmp = null;
            item = null;
            return Redirect("~/Cart/MyOrder");
        }


        /* SHOW CART */
        [HttpGet]
        public ActionResult MyOrder()
        {
            if(Session["id"] != null)
            {
                ViewBag.KH = CustomerAction.Find_Customer((int)Session["id"]);
                return View();
            }
            else
            {
                return Redirect("~/Book/ShowBook");
            }
            
        }
        

        /* JSON - ADD CART */
        public JsonResult Add_Cart(int id)
        {
            if (Session["cart"] == null)
            {
                Dictionary<int, Item> list_item = new Dictionary<int, Item>();

                list_item.Add(id, new Item { book = BookAction.Find_Book(id), so_luong = 1 });
                Session["cart"] = list_item;

            }
            else
            {
                Dictionary<int, Item> list_item = (Dictionary<int, Item>)Session["cart"];
                if (list_item.ContainsKey(id))
                {
                    var tmp = list_item[id];
                    tmp.so_luong = tmp.so_luong + 1;
                    list_item[id] = tmp;
                }
                else
                {
                    list_item.Add(id, new Item { book = BookAction.Find_Book(id), so_luong = 1 });
                }

            }
            return Json("Good", JsonRequestBehavior.AllowGet);
        }


    }
}