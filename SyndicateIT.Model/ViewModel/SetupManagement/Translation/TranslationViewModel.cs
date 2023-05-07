using SyndicateIT.Model.ViewModel.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SyndicateIT.Model.ViewModel.SetupManagement.Translation
{
    public class TranslationViewModel
    { 
        public String TableName { get; set; }
        public int LanguageID { get; set; }
        public string LanguageName { get; set; }
        public string FieldName { get; set; }
        public string FieldID { get; set; }
        public int TableID { get; set; }
        public string descriptionField { get; set; }

        public AlertViewModel Alert { get; set; }

        public TranslationViewModel()
        {
            Alert = new AlertViewModel();
        }

    }
}