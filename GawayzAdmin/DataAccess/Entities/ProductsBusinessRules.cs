using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
   public class ProductsBusinessRules
    {
        [Key]
        public int ProductBID { get; set; }
        [ForeignKey("Product")]
        public int ProductID { get; set; }
        [ForeignKey("BusinessRules")]
        public int BusinessRuleID { get; set; }
        public int BusinessRuleOrder { get; set; }
        public Products Product { get; set; }
        public BusinessRules BusinessRules { get; set; }
    }
}
