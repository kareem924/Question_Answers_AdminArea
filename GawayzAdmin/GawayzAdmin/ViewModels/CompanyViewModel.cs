using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GawayzAdmin.ViewModels
{
    public class CompanyViewModel
    {
        public int CompanyId { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string CompanyName { get; set; }
 
        public string CompanyProfile { get; set; }
        public DateTime RegDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool Active { get; set; }
    }
}