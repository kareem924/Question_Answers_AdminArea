using System.Collections.Generic;
using DataAccess;

namespace WebAdmin.ViewModels
{
    public sealed class QuestionsViewModel
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
        public int QuestionNo { get; set; }
        public string QuestionText { get; set; }
        public int GroupNo { get; set; }
        public List<int> ChoicesToDelete { get; set; }
        public List<ChoicesViewModel> ChoicesItems { get; set; }

        public byte[] RowVersion { get; set; }
        public string MessageToClient { get; set; }
        public ObjectState ObjectState { get; set; }
    }
}