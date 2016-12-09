using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
   public class ProductsSurveyQuestions
    {
        [Key]
        public int SurveyQuestionID { get; set; }
        public int ProductID { get; set; }

        public int SurveyQuestionNo { get; set; }

        public string SurveyQuestionText { get; set; }

        public int GroupNo { get; set; }

    }
}
