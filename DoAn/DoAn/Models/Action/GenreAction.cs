using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DoAn.Models.Action
{
    public class GenreAction
    {
        /* CREATE GENRE */
        public static void Create_Genre(string Name_Genre)
        {
            using (var db = new DBShop())
            {
                db.DbGenre.Add(new Genre { genre = Name_Genre, flag = false});
                db.SaveChanges();
                db.Dispose();
            }
        }


        /* UPDATE GENRE */
        public static void Update_Genre(int id, string Name_Genre)
        {
            using (var db = new DBShop())
            {
                var tmp = db.DbGenre.Find(id);
                tmp.genre = Name_Genre;
                db.Entry(tmp).State = EntityState.Modified;
                db.SaveChanges();
                db.Dispose();
            }
        }


        /* FIND GENRE BY ID */
        public static Genre Find_By_ID(int ID)
        {
            using (var db = new DBShop())
            {
                var tmp = db.DbGenre.Find(ID);
                db.Dispose();
                return tmp;
            }
        }


        /* FIND GENRE BY NAME */
        public static Genre Find_By_Name(string Genre)
        {
            using (var db = new DBShop())
            {
                var tmp = db.DbGenre.Where(item => item.genre == Genre).FirstOrDefault();
                db.Dispose();
                return tmp;
            }
        }


        /*DELETE GENRE */
        public static void Lock_Genre(int id)
        {
            using (var db = new DBShop())
            {
                var tmp = db.DbGenre.Find(id);
                tmp.flag = true;
                db.Entry(tmp).State = EntityState.Modified;
                db.SaveChanges();
                db.Dispose();
            }
        }


        /* RESTORE GENRE */
        public static void Unlock_Genre(int id)
        {
            using (var db = new DBShop())
            {
                var tmp = db.DbGenre.Where(item => item.id == id && item.flag == true).FirstOrDefault();
                tmp.flag = false;
                db.Entry(tmp).State = EntityState.Modified;
                db.SaveChanges();
                db.Dispose();
            }
        }


        /* GET ALL GENRE */
        public static List<Genre> Get_All_Genre()
        {
            using (var db = new DBShop())
            {
                var tmp = db.DbGenre.Where(item => item.flag == false).OrderBy(item => item.genre).ToList();
                db.Dispose();
                return tmp;
            }
        }


        /* RESTORE GENRE WERE DELETED */
        public static List<Genre> Restore_Genre()
        {
            using (var db = new DBShop())
            {
                var tmp = db.DbGenre.Where(item => item.flag == true).ToList();
                db.Dispose();
                return tmp;
            }
        }


    }
}