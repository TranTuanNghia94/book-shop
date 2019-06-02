using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace DoAn.Models
{
    public class CustomerAction
    {
        /* CREATE CUSTOMER  */
        public static void Create_Customer(string Email, string First_name, string Last_name, string Address, string Phone, string Password, string Date,
            string Gender)
        {
            Customer customer = new Customer
            {
                email = Email,
                first_name = First_name,
                last_name = Last_name,
                password = Password,
                address = Address,
                date = Date,
                gender = Gender,
                phone = Phone,
                flag = false,
                role = "guest"
            };
            
            using (var db = new DBShop())
                {
                    db.DbCustomer.Add(customer);
                    db.SaveChanges();
                    db.Dispose();
                }

          
        }


        /* DELETE CUSTOMER */
        public static void  Delete_Customer(int Id)
        {
            if (Id != 0)
            {
                using (var db = new DBShop())
                {
                    var customer = db.DbCustomer.Find(Id);
                    customer.flag = true;
                    db.Entry(customer).State = EntityState.Modified;
                    db.SaveChanges();
                    db.Dispose();

                }
            }
        }
        

        /* FIND CUSTOMER BY ID */
        public static Customer Find_Customer(int Id)
        {
            Customer customer = null;
            if (Id != 0)
            {
                using (var db = new DBShop())
                {
                    customer = db.DbCustomer.Where(item => item.id_customer == Id).FirstOrDefault();
                    db.Dispose();
                }
                return customer;
            }
            else
                return customer;
            
        }


        /* FIND CUSTOMER BY EMAIL, FIRSTNAME, LASTNAME */
        public static List<Customer> Find_Customer(string Email)
        {
            List<Customer> list_customer = null;
            using (var db = new DBShop())
            {
                list_customer = db.DbCustomer.
                    Where(item => item.email.Contains(Email) || item.first_name.Contains(Email) || item.last_name.Contains(Email)).
                    OrderBy(item => item.first_name).ToList();
                db.Dispose();
            }
            return list_customer;
        }


        /* CHECK LOGIN CUSTOMER */
        public static Customer Find_Customer(string Email, string Password)
        {
            Customer customer = null;
            using (var db = new DBShop())
            {
                customer = db.DbCustomer.Where(item => item.email == Email && item.password == Password && item.flag == false).FirstOrDefault();
                db.Dispose();
            }
            return customer;
        }


        /* FIND CUSTOMER BY EMAIL */
        public static Customer CheckCustomer(string Email)
        {
            Customer customer = null;
            using (var db = new DBShop())
            {
                customer = db.DbCustomer.Where(item => item.email == Email).FirstOrDefault();
                db.Dispose();
            }
            return customer;
        }


        /* EDIT CUSTOMER */
        public static void Edit_Customer(int Id,string Email, string First_name, string Last_name, string Password, string Phone, string Date, string Gender,
            string Address)
        {
            try
            {
                using (var db = new DBShop())
                {
                    Customer customer = db.DbCustomer.Find(Id);
                    customer.email = Email;
                    customer.first_name = First_name;
                    customer.last_name = Last_name;
                    customer.password = Password;
                    customer.address = Address;
                    customer.date = Date;
                    customer.phone = Phone;
                    customer.gender = Gender;
                    db.Entry(customer).State = EntityState.Modified;
                    db.SaveChanges();
                    db.Dispose();
                }
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join(";",errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, "The validation errors are: " + fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }

        }


        /* SHOW CUSTOMER BY PAGE NUMBER */
        public static List<Customer> Show_Customer(int page_number, int page_size)
        {
            List<Customer> list_customer = null;
            using (var db = new DBShop())
            {
                list_customer = db.DbCustomer.Where(item => item.flag == false).OrderBy(item => item.first_name).Skip((page_number - 1) * page_size).Take(page_size).ToList();
                db.Dispose();
            }
            return list_customer;
        }


        /* LIST CUSTOMERS WERE DELETED */
        public static List<Customer> Get_Customer_Delete(int page_number, int page_size)
        {
            List<Customer> list_customer = null;
            using (var db = new DBShop())
            {
                list_customer = db.DbCustomer.Where(item => item.flag == true).OrderBy(item => item.first_name).Skip((page_number - 1) * page_size).Take(page_size).ToList();
                db.Dispose();
            }
            return list_customer;
        }


        /* SET ROLE FOR USER */
        public static void Create_Staff(int Id, string Role)
        {
            using (var db = new DBShop())
            {
                var staff = db.DbCustomer.Find(Id);
                staff.role = Role;
                db.Entry(staff).State = EntityState.Modified;
                db.SaveChanges();
                db.Dispose();
            }
        }


        /* RESTORE CUSTOMER ACCOUNT */
        public static void UnLock(int Id)
        {
            using (var db = new DBShop())
            {
                var kh = db.DbCustomer.Find(Id);
                if(kh.flag)
                {
                    kh.flag = false;
                    db.Entry(kh).State = EntityState.Modified;
                    db.SaveChanges();
                    db.Dispose();
                }
            }
        }

    }

}