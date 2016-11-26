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
                    ChoiceText = choice.ChoiceText
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
                QuestionTypeID = questionsViewModel.QuestionTypeId

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
            var companyViewModel =new CompanyViewModel();
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
    }
}