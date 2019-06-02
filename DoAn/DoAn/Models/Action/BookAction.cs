using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DoAn.Models
{
    public class BookAction
    {
        /* CREATE BOOK */
        public static void Create_Book(string Ten_Sach, string Tac_Gia, string The_Loai, string Nha_Xuat_Ban, string Mo_Ta, double Gia_Ban, string Hinh)
        {
            using (var db = new DBShop())
            {
                db.DbBook.Add(new Book
                {
                    ten_sach = Ten_Sach,
                    tac_gia = Tac_Gia,
                    the_loai = The_Loai,
                    nha_xuat_ban = Nha_Xuat_Ban,
                    mo_ta = Mo_Ta,
                    gia_ban = Gia_Ban,
                    hinh = Hinh, 
                    flag = false
                });
                db.SaveChanges();
                db.Dispose();
            }
        }


        /* FIND BOOK BY ID */
        public static Book Find_Book(int id)
        {
            Book book = null;
            using (var db = new DBShop())
            {
                book = db.DbBook.Find(id);
                db.Dispose();
            }
            return book;

        }


        /* FIND BOOK BY NAME */
        public static Book find(string Name)
        {
            Book book;
            using (var db = new DBShop())
            {
                book = db.DbBook.Where(item => item.ten_sach == Name).FirstOrDefault();
                db.Dispose();
            }
            return book;
        }



        /* FIND LIST BOOK BY NAME, AUTHOR, GENRE */
        public static List<Book> Find_Book(string Search_book)
        {
            List<Book> list_book = null;
            using (var db = new DBShop())
            {
                list_book = db.DbBook.Where(item => (item.ten_sach.Contains(Search_book) || item.the_loai.Contains(Search_book) || item.tac_gia.Contains(Search_book)) && item.flag == false).ToList();
                db.Dispose();
            }
            return list_book;
        }


        /* FIND BOOK BY GENRE */
        public static List<Book> Get_Book_By_Genre(string Search_book)
        {
            List<Book> list_book = null;
            using (var db = new DBShop())
            {
                list_book = db.DbBook.Where(item => item.the_loai.Contains(Search_book) && item.flag == false).ToList();
                db.Dispose();
            }
            return list_book;
        }


        /* DELETE BOOK */
        public static void Delete_Book(int Id)
        {
            using (var db = new DBShop())
            {
                var book = db.DbBook.Find(Id);
                book.flag = true;
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                db.Dispose();
            }
        }


        /*  EDIT BOOK  */
        public static void Edit_Book(int Id, string Ten_Sach, string Tac_Gia, string The_Loai, string Nha_Xuat_Ban, string Mo_Ta, double Gia_Ban)
        {
            using (var db = new DBShop())
            {
                var book = db.DbBook.Find(Id);
                book.ten_sach = Ten_Sach;
                book.tac_gia = Tac_Gia;
                book.the_loai = The_Loai;
                book.nha_xuat_ban = Nha_Xuat_Ban;
                book.mo_ta = Mo_Ta;
                book.gia_ban = Gia_Ban;
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                db.Dispose();
            }
        }


        /* UPDATE IMAGE BOOK */
        public static void Update_Img(int Id ,string Hinh)
        {
            using (var db = new DBShop())
            {
                var book = db.DbBook.Find(Id);
                book.hinh = Hinh;
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                db.Dispose();
            }
        }


        /*  SHOW BOOK BY PAGE NUMBER */
        public static List<Book> Show_Book(int page_number, int page_size)
        {

            List<Book> list_book = null;
            using (var db = new DBShop())
            {
                list_book = db.DbBook.Where(item => item.flag == false).OrderBy(a => a.the_loai).
                    Skip((page_number - 1) * page_size).Take(page_size).ToList();
                db.Dispose();
            }
            return list_book;
        }


        /* LIST BOOKS WERE DELETED */
        public static List<Book> Get_Book_Delete(int page_number, int page_size)
        {

            List<Book> list_book = null;
            using (var db = new DBShop())
            {
                list_book = db.DbBook.Where(item => item.flag == true).OrderBy(a => a.the_loai).
                    Skip((page_number - 1) * page_size).Take(page_size).ToList();
                db.Dispose();
            }
            return list_book;
        }


        /* RESTORE BOOK */
        public static void Restore_Book(int Id)
        {
            using (var db = new DBShop())
            {
                var tmp = db.DbBook.Find(Id);
                tmp.flag = false;
                db.Entry(tmp).State = EntityState.Modified;
                db.SaveChanges();
                db.Dispose();
            }
        }



    }
}