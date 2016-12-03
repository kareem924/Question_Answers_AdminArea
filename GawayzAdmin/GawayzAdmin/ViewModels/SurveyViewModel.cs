using System.ComponentModel.DataAnnotations;

namespace GawayzAdmin.ViewModels
{
    public class SurveyViewModel
    {
        public int SurveyQuestionId { get; set; }
        public int ProductId { get; set; }
        public int SurveyQuestionNo { get; set; }
        [Required(ErrorMessage = "Survey Question Text is required")]
        public string SurveyQuestionText { get; set; }
        public int GroupNo { get; set; }
    }
}