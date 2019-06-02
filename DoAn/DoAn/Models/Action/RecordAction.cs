using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DoAn.Models
{
    public class RecordAction
    {
        /* Create Record */
        public static void Create_Record(string Username, string Action, DateTime Date_Action)
        {
            using (var db = new DBShop())
            {
                db.DbRecord.Add(new Record { username = Username, action = Action, date_action = Date_Action});
                db.SaveChanges();
                db.Dispose();
            }
        }


        /* Show All */
        public static List<Record> ShowAll()
        {
            List<Record> lst = new List<Record>();
            using (var db = new DBShop())
            {
                var tmp = db.DbRecord.OrderByDescending(a => a.id).ToList();
                lst = tmp;
            }
            return lst;
        }


        /* Show By Page Number */
        public static List<Record> ShowAll(int page_number, int page_size)
        {
            List<Record> lst = new List<Record>();
            using (var db = new DBShop())
            {
                var tmp = db.DbRecord.OrderByDescending(a => a.id).Skip((page_number - 1) * page_size).Take(page_size).ToList();
                lst = tmp;
            }
            return lst;
        }


        /* Auto Delete Record After N Days */
        public static void Auto_Delete_Record(int Day_Delete)
        {
            using (var db = new DBShop())
            {
                var record = db.DbRecord.ToList();
                for(int i = 0; i < record.Count(); i++)
                {
                    int day = DateTime.Now.Date.Subtract(record[i].date_action).Days;
                    if ( day > Day_Delete)
                    {
                        db.Entry(record[i]).State = EntityState.Deleted;
                        db.SaveChanges();
                    }
                }
                db.Dispose();
            }
        }

    }
}