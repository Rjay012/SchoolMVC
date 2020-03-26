using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolMVC.Models.StandardModels
{
    public class StandardModel
    {
        [Key]
        public int StandardID { get; set; }
        [Required]
        public string StandardName { get; set; }
        [Required]
        public string Description { get; set; }
    }
}