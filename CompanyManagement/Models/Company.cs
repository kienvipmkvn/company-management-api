using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CompanyManagement.Models
{
    public class Company
    {
        public int CompanyID { get; set; }
        [Required]
        [Display(Name = ("Company name"))]
        public string Name { get; set; }
        [Display(Name = ("Number of employees"))]
        public int? NumberofEmployee { get; set; }
        public string Address { get; set; }
        public string ImgPath { get; set; }

        [Display(Name = ("Establishment day"))]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? EstablishmentDay { get; set; }
        public List<Product> ListProduct { get; set; }
    }
}