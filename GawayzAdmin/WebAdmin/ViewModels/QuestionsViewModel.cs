using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DataAccess;

namespace WebAdmin.ViewModels
{
    public sealed class QuestionsViewModel : IObjectWithState
    {
        public QuestionsViewModel()
        {
            ChoicesItems = new List<ChoicesViewModel>();
            ChoicesToDelete = new List<int>();
        }
        
        public int QuestionId { get; set; }
        public int QuestionTypeId { get; set; }
        public int ProductId { get; set; }
     
        public string AnswerLetter { get; set; }
        public int AnswerId { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
        public int QuestionNo { get; set; }
        [Required(ErrorMessage = "Question Text is required")]
        public string QuestionText { get; set; }
        [Required(ErrorMessage = "Group No. is required")]
        public int GroupNo { get; set; }
        public List<int> ChoicesToDelete { get; set; }
        public List<ChoicesViewModel> ChoicesItems { get; set; }

    
        public string MessageToClient { get; set; }
        public ObjectState ObjectState { get; set; }
    }
}