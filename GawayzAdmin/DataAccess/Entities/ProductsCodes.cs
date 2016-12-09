using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
 public   class ProductsCodes
    {
        [Key]
        public int CodeID { get; set; }
        [ForeignKey("Product")]
        public int ProductID { get; set; }
        public string Code { get; set; }
        public int CodeType { get; set; }
        public int CodeStatus { get; set; }
        public DateTime CreatedDate { get; set; }
        public Products Product { get; set; }
    }
}
