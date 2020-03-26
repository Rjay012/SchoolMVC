using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace SchoolMVC.Models.StudentModels
{
    public class StudentModel
    {
        [Key]
        [Required]
        [DisplayName("Student ID:")]
        public int StudentID { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 6)]
        [DisplayName("Student Name:")]
        public string StudentName { get; set; }
        [Required]
        [ForeignKey("Standard")]
        [DisplayName("Standard:")]
        public int StandardID { get; set; }
        [Required]
        [StringLength(50)]
        [DisplayName("Row Version:")]
        public string RowVersion { get; set; }
        [Required]
        [DisplayName("Address 1:")]
        public string Address1 { get; set; }
        [DisplayName("Address 2:")]
        public string Address2 { get; set; }
        [DisplayName("City:")]
        public string City { get; set; }
        [DisplayName("State:")]
        public string State { get; set; }

        public IEnumerable<SelectListItem> SelectListStandard { get; set; }
    }
}