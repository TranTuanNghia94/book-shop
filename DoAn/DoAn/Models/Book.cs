using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAn.Models
{
    public class Book
    {
        [Key]
        public int id_sach { get; set; }

        [Required]
        public string ten_sach { get; set; }

        [Required]
        public string the_loai { get; set; }

        [Required]
        public string tac_gia { get; set; }

        [Required]
        public string nha_xuat_ban { get; set; }

   
        [Required]
        public string mo_ta { get; set; }

   
        [Required]
        public double gia_ban { get; set; }

        [Required]
        public string hinh { get; set; }


        public bool flag { get; set; }


       

    }
}