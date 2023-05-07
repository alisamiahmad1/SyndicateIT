using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SyndicateIT.Model.ViewModel.SetupManagement.Translation
{
    public class TranslationContentViewModel 
    {
        public int TableID { get; set; }
        public int TableName { get; set; }
        public List<TranslationViewModel> TranslationList { get; set; }
        public TranslationContentViewModel()
        {
            TranslationList = new List<TranslationViewModel>();
        }
       
    }
}