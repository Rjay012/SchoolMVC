using SchoolMVC.Models.AddressModels;
using SchoolMVC.Models.StandardModels;
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
        [MaxLength(100), MinLength(6)]
        [DisplayName("Student Name:")]
        public string StudentName { get; set; }
        [Required]
        [ForeignKey("StandardModel")]
        [DisplayName("Standard:")]
        public int StandardID { get; set; }
        [Required]
        [StringLength(50)]
        [DisplayName("Row Version:")]
        public string RowVersion { get; set; }

        public AddressModel Address { get; set; }
        public StandardModel Standard { get; set; }

        public IEnumerable<SelectListItem> SelectListStandard { get; set; }
    }
}