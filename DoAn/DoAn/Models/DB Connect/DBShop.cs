using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;


namespace DoAn.Models
{
    public class DBShop:DbContext
    {
        public DBShop() : base("Book_Demo")
        {
            //Database.Connection.ConnectionString = "workstation id=shopofbook.mssql.somee.com;packet size=4096;user id=Mr_Kai_SQLLogin_1;pwd=wvnj24xamx;data source=shopofbook.mssql.somee.com;persist security info=False;initial catalog=shopofbook";
        }
        public DbSet<Customer> DbCustomer { get; set; }
        public DbSet<Book> DbBook { get; set; }
        public DbSet<Order> DbOrder { get; set; }
        public DbSet<OrderDetail> DbBill { get; set; }
        public DbSet<Record> DbRecord { get; set; }
        public DbSet<TotalVisit> DbVisit { get; set; }
        public DbSet<Genre> DbGenre { get; set; }


     }
}