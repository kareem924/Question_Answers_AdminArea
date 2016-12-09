using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GawayzAdmin.ViewModels
{
    public class ProductsBusinessRulesViewModel
    {
        public int ProductBID { get; set; }
       
        public int ProductID { get; set; }
    
        public int BusinessRuleID { get; set; }
        public int BusinessRuleOrder { get; set; }
        public ProductViewModel Product { get; set; }
        public BusinessRulesViewModel BusinessRules { get; set; }
    }
}