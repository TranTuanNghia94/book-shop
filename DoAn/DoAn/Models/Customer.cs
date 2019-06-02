using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAn.Models
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_customer { get; set; }

        [Required]
        [Remote("IsAlreadySigned", "Customer", HttpMethod = "Post", ErrorMessage = "This Mail Has Existed")]
        public string email { get; set; }

        [Required]
        //[MinLength(2, ErrorMessage = "YOUR NAME IS TOO SHORT")]
        public string first_name { get; set; }

        [Required]
        //[MinLength(2, ErrorMessage = "YOUR NAME IS TOO SHORT")]
        public string last_name { get; set; }

        [Required]
        //[MaxLength(25),MinLength(3, ErrorMessage = "YOUR PASSWORD IS TOO SHORT")]
        public string password { get; set; }

        [Required]
        //[MinLength(10, ErrorMessage = "PLEASE ENTER YOUR ADDRESS")]
        public string address { get; set; }

        [Required]
        //[MinLength(10, ErrorMessage = "YOUR PHONE NUMBER IS NOT AVAILABLE")]
        public string phone { get; set; }

        [Required]
        public string date { get; set; }

        [Required]
        public string gender { get; set; }

        public string role { get; set; }

        public bool flag { get; set; }

      

    }
}