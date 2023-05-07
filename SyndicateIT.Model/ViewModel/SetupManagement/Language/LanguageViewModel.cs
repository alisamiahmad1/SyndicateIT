using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SyndicateIT.DataLayer.DataContext;
using SyndicateIT.UtilityComponent.Enum;
using SyndicateIT.UtilityComponent;

namespace SyndicateIT.Model.ViewModel.SetupManagement.Language
{
    public class LanguageViewModel : LANGUAGE
    {
        public String Language
        {
            get
            {
                if (LANGUAGE_ID != 0 && LANGUAGE_ID.ToString() != "")
                {
                    return TypeAction.Edit + "Language";
                }
                else
                {
                    return TypeAction.Add + "Language";
                }
            }
        }
    }
}