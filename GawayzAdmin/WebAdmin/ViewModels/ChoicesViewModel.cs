using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Web;
using DataAccess;

namespace WebAdmin.ViewModels
{
    public class ChoicesViewModel : IObjectWithState
    {
        public int ChoiceId { get; set; }
        public int ChoiceTypeId { get; set; }
        public int QuestionId { get; set; }
        public int ProductId { get; set; }
        [Required(ErrorMessage = "The Choice Letter is required")]
       
        public string ChoiceLetter { get; set; }
        [Required(ErrorMessage = "The Choice Text is required")]
        public string ChoiceText { get; set; }
        public bool IsSelected { get; set; }
        public QuestionsViewModel Questions { get; set; }
      
        public byte[] RowVersion { get; set; }
        public ObjectState ObjectState { get; set; }
    }
}