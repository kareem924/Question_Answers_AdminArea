using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using DataAccess;
using DataAccess.Entities;
using GawayzAdmin.SecurityClasses;

namespace GawayzAdmin.ViewModels
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

                    ChoiceLetter = choice.ChoiceLetter,
                    ChoiceText = choice.ChoiceText,
                    IsSelected = choice.IsCorrect
                };
                questionsViewModel.ChoicesItems.Add(choicesViewModel);
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

                ProductID = questionsViewModel.ProductId,
                QuestionText = questionsViewModel.QuestionText,
                GroupNo = questionsViewModel.GroupNo,
                QuestionTypeID = questionsViewModel.QuestionTypeId,
                CreatedAt = DateTime.Now

            };
            int temporarySalesOrderItemId = -1;
            foreach (ChoicesViewModel choiceViewModel in questionsViewModel.ChoicesItems)
            {
                Choices choices = new Choices();

                choices.ChoiceID = choiceViewModel.ChoiceId;
                choices.ChoiceTypeID = choiceViewModel.ChoiceTypeId;
                choices.QuestionID = choiceViewModel.QuestionId;
                choices.ProductID = choiceViewModel.ProductId;
                choices.ChoiceLetter = choiceViewModel.ChoiceLetter;
                choices.ChoiceText = choiceViewModel.ChoiceText;
                choices.ObjectState = choiceViewModel.ObjectState;

                choices.IsCorrect = choiceViewModel.IsSelected;
                choices.CreatedAt = DateTime.Now;
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

        public static ProductsSurveyQuestions CreateSurveyFromSurveyViewModel(SurveyViewModel surveyViewModel)
        {
            var survey = new ProductsSurveyQuestions
            {
                SurveyQuestionID = surveyViewModel.SurveyQuestionId,
                ProductID = surveyViewModel.ProductId,
                GroupNo = surveyViewModel.GroupNo,
                SurveyQuestionNo = surveyViewModel.SurveyQuestionNo,
                SurveyQuestionText = surveyViewModel.SurveyQuestionText
            };
            return survey;
        }

        public static SurveyViewModel CreateSurveyViewModelFromSurvey(ProductsSurveyQuestions survey)
        {
            var questionViewModel = new SurveyViewModel
            {
                SurveyQuestionId = survey.SurveyQuestionID,
                ProductId = survey.ProductID,
                GroupNo = survey.GroupNo,
                SurveyQuestionNo = survey.SurveyQuestionNo,
                SurveyQuestionText = survey.SurveyQuestionText
            };

            return questionViewModel;
        }

        public static Companies CreateCompanyFromCompanyViewModel(CompanyViewModel companyViewModel)
        {
            var company = new Companies();

            company.CompanyID = companyViewModel.CompanyId;
            company.Active = companyViewModel.Active;
            company.CompanyName = companyViewModel.CompanyName;
            company.CompanyProfile = companyViewModel.CompanyProfile;

            company.RegDate = DateTime.Now;
            company.ModifiedDate = DateTime.Today;
            return company;
        }
        public static CompanyViewModel CreateCompanyviewmodelFromCompanies(Companies company)
        {
            var companyViewModel = new CompanyViewModel();
            companyViewModel.CompanyId = company.CompanyID;
            companyViewModel.Active = company.Active;
            companyViewModel.CompanyName = company.CompanyName;
            companyViewModel.CompanyProfile = company.CompanyProfile;
            return companyViewModel;
        }

        public static Products CreateProductFromProductViewModel(ProductViewModel productViewModel)
        {
            var products = new Products
            {
                ProductID = productViewModel.ProductId,
                CompanyID = productViewModel.CompanyIdKey,
                EnterdDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                ProductName = productViewModel.ProductName,
                ProductImage = productViewModel.ProductImage,
                ProductOrder = productViewModel.ProductOrder,
                ProductSurveyQPerPage = productViewModel.ProductSurveyQPerPage,
                enProductDescription = productViewModel.EnProductDescription,
                arProductDescription = productViewModel.ArProductDescription,
                Active = productViewModel.Active
            };
            return products;
        }

        public static ProductViewModel CreateProductViewModelFromProducts(Products product)
        {
            var productViewModel = new ProductViewModel
            {
                ProductId = product.ProductID,
                CompanyIdKey = product.CompanyID,
                EnterdDate = product.EnterdDate,
                ModifiedDate = product.ModifiedDate,
                ProductName = product.ProductName,
                ProductImage = product.ProductImage,
                ProductOrder = product.ProductOrder,
                ProductSurveyQPerPage = product.ProductSurveyQPerPage,
                EnProductDescription = product.enProductDescription,
                ArProductDescription = product.arProductDescription,
                Active = product.Active
            };
            return productViewModel;
        }

        public static Users CreateUsersFromUserViewModel(UsersViewModel userViewModel)
        {
            var encPass = StringCipher.Encrypt(userViewModel.ConfirmPassword,
               WebConfigurationManager.AppSettings["EncDecKey"]);
            var users = new Users
            {
                UserId = userViewModel.UserId,
                IsActive = userViewModel.IsActive,
                Password = encPass,
                UserName = userViewModel.UserName
            };
            return users;
        }

        public static EditUserViewModel CreatEditUserViewModelFromUser(Users user)
        {
            var editUserViewModel = new EditUserViewModel
            {
                UserId = user.UserId,
                IsActive = user.IsActive,
                UserName = user.UserName
            };
            return editUserViewModel;
        }

        public static Users CreateUserFromEditUserViewModel(EditUserViewModel editUserViewModel)
        {
            var editedUser = new Users
            {
                IsActive = editUserViewModel.IsActive,
                UserName = editUserViewModel.UserName,
                UserId = editUserViewModel.UserId
            };
            return editedUser;
        }

        public static Users CreateUserFromChangePasswordModel(ChangePassword changePassword)
        {
            var changePasswordEdited = new Users {Password = changePassword.ConfirmPassword};
            return changePasswordEdited;
        }
    }
}