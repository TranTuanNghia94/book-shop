using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoAn.Models
{
    public class BillAction
    {
        /* CREATE BILL */
        public static void Create_Bill(int Id_Order, int Id_Sach, string Ten_Sach, 
            string Tac_Gia, int So_Luong, double Don_Gia, double Thanh_Tien)
        {
            string day = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;
            using (var db = new DBShop())
            {
                db.DbBill.Add(new OrderDetail
                {
                    id_order = Id_Order,
                    id_sach = Id_Sach,
                    ten_sach = Ten_Sach,
                    tac_gia = Tac_Gia,
                    so_luong = So_Luong,
                    don_gia = Don_Gia,
                    thanh_tien = Thanh_Tien,
                    date_buy = day
                });
                db.SaveChanges();
                db.Dispose();
            }
        }


        /* GET BILL BY PAGE NUMBER */
        public static List<Tuple<Order, OrderDetail>> GetDetail(int ID, int page_size, int page_number)
        {
            List<Tuple<Order, OrderDetail>> lst = new List<Tuple<Order, OrderDetail>>();
            Tuple<Order, OrderDetail> tup;
            using (var db = new DBShop())
            {
                /* get orders by page number */
                var order = db.DbOrder.Where(item => item.id_customer == ID).OrderByDescending(item => item.date_create).Skip((page_number - 1) * page_size).Take(page_size).ToList();

                for (int i = 0; i < order.Count; i++)
                {
                    int id = order[i].id;
                    /* get bills by page number  */
                    var bill = db.DbBill.Where(item => item.id_order == id).OrderByDescending(item => item.id).Skip((page_number - 1) * page_size).Take(page_size).ToList();

                    for(int j = 0; j < bill.Count; j++)
                    {
                        tup = new Tuple<Order, OrderDetail>(order[i], bill[j]);
                        lst.Add(tup);
                    }
                }
                db.Dispose();  
            }
            return lst;
        }


    }
}