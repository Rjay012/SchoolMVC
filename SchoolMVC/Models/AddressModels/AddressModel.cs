using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using SchoolMVC.Models.StudentModels;

namespace SchoolMVC.Models.AddressModels
{
    public class AddressModel
    {
        [Key]
        [ForeignKey("StudentModel")]
        [Required]
        public int AddressID { get; set; }
        [Required]
        [DisplayName("Address 1:")]
        public string Address1 { get; set; }
        [DisplayName("Address 2:")]
        public string Address2 { get; set; }
        [DisplayName("City:")]
        public string City { get; set; }
        [DisplayName("State:")]
        public string State { get; set; }

        public StudentModel Student { get; set; }
    }
}