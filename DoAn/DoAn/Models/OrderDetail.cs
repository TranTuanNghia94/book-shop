using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DoAn.Models
{
    public class OrderDetail
    {
        [Key]
        public int id { get; set; }

        [Required]
        public int id_order { get; set; }

        [Required]
        public int id_sach { get; set; }

        [Required]
        public string ten_sach { get; set; }

        [Required]
        public string tac_gia { get; set; }

        [Required]
        public int so_luong { get; set; }

        [Required]
        public double don_gia { get; set; }

        [Required]
        public double thanh_tien { get; set; }

        public string date_buy { get; set; }

    }
}