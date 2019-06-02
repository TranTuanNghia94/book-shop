using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DoAn.Models
{
    public class Record
    {
        [Key]
        public int id { get; set; }
        public string username { get; set; }
        public string action { get; set; }
        public DateTime date_action { get; set; }
    }
}