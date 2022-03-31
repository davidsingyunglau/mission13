using System;
using System.ComponentModel.DataAnnotations;

namespace MySQLFun.Models
{
    public class Teams
    {
        [Key]
        [Required]
        public int TeamID { get; set; }

        public string TeamName { get; set; }

        public int CaptainID { get; set; }
    }
}
