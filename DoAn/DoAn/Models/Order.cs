using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DoAn.Models
{
    public class Order
    {
        [Key]
        public int id { get; set; }

        [Required]
        public int id_customer { get; set; }

        [Required]
        public string date_create { get; set; }

    }
}