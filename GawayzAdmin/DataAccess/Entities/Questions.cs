using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    [Table("Questions")]
    public class Questions
    {
        public Questions()
        {
            Choices=new List<Choices>();
        }
        [Key]
        public int QuestionID { get; set; }
        public int QuestionTypeID { get; set; }
        public int ProductID { get; set; }
        public string AnswerLetter { get; set; }
        public int AnswerID { get; set; }
        public int QuestionNo { get; set; }
        public string QuestionText { get; set; }
        public int GroupNo { get; set; }
        public virtual List<Choices> Choices { get; set; }
        public byte[] RowVersion { get; set; }
        [NotMapped]
        public ObjectState ObjectState { get; set; }
    }
}
