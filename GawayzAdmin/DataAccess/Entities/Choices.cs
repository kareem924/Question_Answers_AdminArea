using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    [Table("Choices")]
    public class Choices
    {
        public int ChoiceID { get; set; }
        public int ChoiceTypeID { get; set; }
        public int QuestionID { get; set; }
        public int ProductID { get; set; }
        public string ChoiceLetter { get; set; }
        public string ChoiceText { get; set; }
        public Questions SalesOrder { get; set; }
        public byte[] RowVersion { get; set; }
        [NotMapped]
        public ObjectState ObjectState { get; set; }
    }
}
