using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolMVC.Models
{
    public class ViewModel
    {
        public student Student { get; set; }
        public address Address { get; set; }
        public Standard Standard { get; set; }
    }
}