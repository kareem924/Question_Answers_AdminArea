using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    [Table("Companies")]
    public class Companies
    {
        public Companies()
        {
            products = new List<Products>();
        }
        [Key]
        public int CompanyID { get; set; }
        public string CompanyName { get; set; }
        public string CompanyProfile { get; set; }
        public DateTime RegDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool Active { get; set; }
        public virtual List<Products> products { get; set; }
    }
}
