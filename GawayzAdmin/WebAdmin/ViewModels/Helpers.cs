using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess;
using DataAccess.Entities;

namespace WebAdmin.ViewModels
{
    public class Helpers
    {
        public static QuestionsViewModel CreateQuestionsViewModelFromQuestions(Questions questions)
        {
            QuestionsViewModel questionsViewModel = new QuestionsViewModel
            {
                QuestionId = questions.QuestionID,
                AnswerId = questions.AnswerID,
                AnswerLetter = questions.AnswerLetter,
                ObjectState = ObjectState.Unchanged,
                RowVersion = questions.RowVersion,
                ProductId = questions.ProductID,
                QuestionText = questions.QuestionText,
                GroupNo = questions.GroupNo,
                QuestionTypeId = questions.QuestionTypeID
            };
            foreach (Choices choice in questions.Choices)
            {
                ChoicesViewModel choicesViewModel = new ChoicesViewModel
                {
                    ChoiceId = choice.ChoiceID,
                    QuestionId = choice.QuestionID,
                    ChoiceTypeId = choice.ChoiceTypeID,
                    ProductId = choice.ProductID,
                    ObjectState = ObjectState.Unchanged,
                    RowVersion = choice.RowVersion,
                    ChoiceLetter = choice.ChoiceLetter,
                    ChoiceText = choice.ChoiceText
                };
                questionsViewModel.ChoicesViewModel.Add(choicesViewModel);
            }

            return questionsViewModel;
        }

        public static Questions CreateQuestionsFromQuestionsViewModel(QuestionsViewModel questionsViewModel)
        {
            Questions questions = new Questions
            {
                QuestionID = questionsViewModel.QuestionId,
                AnswerID = questionsViewModel.AnswerId,
                AnswerLetter = questionsViewModel.AnswerLetter,
                ObjectState = questionsViewModel.ObjectState,
                RowVersion = questionsViewModel.RowVersion,
                ProductID = questionsViewModel.ProductId,
                QuestionText = questionsViewModel.QuestionText,
                GroupNo = questionsViewModel.GroupNo,
                QuestionTypeID = questionsViewModel.QuestionTypeId

            };
            int temporarySalesOrderItemId = -1;
            foreach (ChoicesViewModel choiceViewModel in questionsViewModel.ChoicesViewModel)
            {
                Choices choices = new Choices();

                choices.ChoiceID = choiceViewModel.ChoiceId;
                choices.ChoiceTypeID = choiceViewModel.ChoiceTypeId;
                choices.QuestionID = choiceViewModel.QuestionId;
                choices.ProductID = choiceViewModel.ProductId;
                choices.ChoiceLetter = choiceViewModel.ChoiceLetter;
                choices.ChoiceText = choiceViewModel.ChoiceText;
                choices.ObjectState = choiceViewModel.ObjectState;
                choices.RowVersion = choiceViewModel.RowVersion;
                if (choiceViewModel.ObjectState != ObjectState.Added)
                    choices.ChoiceID = choiceViewModel.ChoiceId;
                else
                {
                    choices.ChoiceID = temporarySalesOrderItemId;
                    temporarySalesOrderItemId--;
                }
                questions.Choices.Add(choices);
            }

            return questions;
        }
        public static string GetMessageToClient(ObjectState objectState, string customerName)
        {
            string messageToClient = string.Empty;

            switch (objectState)
            {
                case ObjectState.Added:
                    messageToClient = string.Format("A questiom for {0} has been added to the database.", customerName);
                    break;

                case ObjectState.Modified:
                    messageToClient = string.Format("The customer name for this sales order has been updated to {0} in the database.", customerName);
                    break;
            }

            return messageToClient;
        }
    }
}