using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoAn.Models
{
    public class OrderAction
    {

        /* CREATE ORDER  */
        public static void Create_Order(int Id_Customer, string Date_Create)
        {
            using (var db = new DBShop())
            {
                db.DbOrder.Add(new Order { id_customer = Id_Customer, date_create = Date_Create});
                db.SaveChanges();
                db.Dispose();
            }
        }


        /* GET CURRENT ORDER   */
        public static int Find_Order()
        {
            int id_order;
            using (var db = new DBShop())
            {
                id_order = db.DbOrder.Max(item => item.id);
                db.Dispose();
            }
            return id_order;
        }

    }
}