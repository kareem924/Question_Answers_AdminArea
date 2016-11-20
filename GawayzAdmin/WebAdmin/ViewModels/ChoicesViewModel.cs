using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess;

namespace WebAdmin.ViewModels
{
    public class ChoicesViewModel
    {
        public int ChoiceId { get; set; }
        public int ChoiceTypeId { get; set; }
        public int QuestionId { get; set; }
        public int ProductId { get; set; }
        public string ChoiceLetter { get; set; }
        public string ChoiceText { get; set; }
        public QuestionsViewModel Questions { get; set; }
        public byte[] RowVersion { get; set; }
      
        public ObjectState ObjectState { get; set; }
    }
}