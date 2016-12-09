using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GawayzAdmin.ViewModels
{
    public class BusinessRulesViewModel
    {
        public int BusinessRuleID { get; set; }
        public string BusinessRuleName { get; set; }
        public virtual List<ProductsBusinessRulesViewModel> ProductsBusinessRules { get; set; }
    }
}