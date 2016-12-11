using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GawayzAdmin.ViewModels
{
    public class ProductViewModel
    {
        
        public int ProductId { get; set; }
        [Required(ErrorMessage = "This field is required")]
      
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public string EnProductDescription { get; set; }
        public string ArProductDescription { get; set; }
        public int ProductQPerPage { get; set; }
        public int ProductSurveyQPerPage { get; set; }
        public int ProductOrder { get; set; }
        public List<int> BusinessOrder { get; set; }
        public DateTime EnterdDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool Active { get; set; }
        public int CompanyIdKey { get; set; }
        public bool IsSurvey
        {
            get { return BusinessOrder.Any(x => x == 2); }
        }
        public bool IsCode
        {
            get { return BusinessOrder.Any(x => x == 3); }
        }
        public bool IsBrandAwareness
        {
            get { return BusinessOrder.Any(x => x == 1); }
        }
    }
}