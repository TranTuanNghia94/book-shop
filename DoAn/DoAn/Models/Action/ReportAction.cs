using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DoAn.Models
{
    public class ReportAction
    {
        /* TOP BOOKS WERE BOUGHT */
        public static List<Tuple<Book, int>> BestSeller()
        {
            List<Tuple<Book, int>> lst = new List<Tuple<Book, int>>();
            Tuple<Book, int> tup;
            using (var db = new DBShop())
            {
                var tmp = db.DbBill.GroupBy(item => item.id_sach).Select(item => new { ID = item.Key, Count = item.Count()}).OrderByDescending(item => item.Count).Take(10);
                foreach(var item in tmp)
                {
                    tup = new Tuple<Book, int>(BookAction.Find_Book(item.ID), item.Count);
                    lst.Add(tup);
                    break;
                }
                db.Dispose();
            }

            return lst;
        }


        /* CREATE VIEWS COUNTING */
        public static void Create_Visit(int Visit_Count, int Month)
        {
            using (var db = new DBShop())
            {
                db.DbVisit.Add(new TotalVisit { total_view = Visit_Count, month = Month});
                db.SaveChanges();
                db.Dispose();
            }
        }


        /* GET VIEWS COUNTING ALL MONTHS */
        public static List<TotalVisit> Get_Visit()
        {
            using (var db = new DBShop())
            {
                var tmp = db.DbVisit.OrderBy(item => item.month).ToList();
                db.Dispose();
                return tmp;
            }
        }


        /* INCREATE VIEWS COUNTING */
        public static void Change_Visit(int Visit_Count, int Month)
        {
            using (var db = new DBShop())
            {
                var visit = db.DbVisit.Where(item => item.month == Month).FirstOrDefault();
                visit.total_view = visit.total_view + Visit_Count;
                db.Entry(visit).State = EntityState.Modified;
                db.SaveChanges();
                db.Dispose();
            }
        }


        /* FIND VIEWS COUNTING BY MONTH */
        public static TotalVisit Find_Month(int Month)
        {
            using (var db = new DBShop())
            {
                var result = db.DbVisit.Where(item => item.month == Month).FirstOrDefault();
                db.Dispose();
                return result;
            }
        }


        /* TOP CUSTOMER BUY BOOKS */
        public static List<Customer> TopCustomer()
        {
            List<Customer> lst = new List<Customer>();
            using (var db = new DBShop())
            {
                var tmp = db.DbOrder.GroupBy(item => item.id_customer).Select(item => new {Id = item.Key ,Count = item.Count() }).OrderBy(item => item.Count).Take(10).ToList();
                foreach(var item in tmp)
                {
                    lst.Add(CustomerAction.Find_Customer(item.Id));
                }
                db.Dispose();
            }
            return lst;
        }


        /* PROFIT */
        public static List<Tuple<string, double>> Profits()
        {
            List<Tuple<string, double>> lst = new List<Tuple<string, double>>();
            Tuple<string, double> tup;
            using (var db = new DBShop())
            {
                var tmp = db.DbBill.GroupBy(item => item.date_buy.Contains(DateTime.Now.Month.ToString())).Select(item => new  {  ID = DateTime.Now.Month.ToString(), Total = item.Sum(total => total.thanh_tien) });
                foreach (var item in tmp)
                {
                    tup = new Tuple<string, double>(item.ID, item.Total);
                    lst.Add(tup);
                }
                db.Dispose();
            }
            return lst;
        }




    }
}