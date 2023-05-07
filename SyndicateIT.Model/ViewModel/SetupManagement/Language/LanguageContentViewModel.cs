using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SyndicateIT.Model.ViewModel.SetupManagement.Language
{
    public class LanguageContentViewModel
    {
        public int Language_ID { get; set; } = -1;
        public string Language_Name { get; set; } = "";
        public string Culture { get; set; } = "";
        public String Code { get; set; } = "";
        public Boolean Direction { get; set; } = true;
          public Boolean  IS_ACTIVE { get; set; } = true;
        public int START_ROW { get; set; } = -1;
        public int END_ROW { get; set; } = -1;
        public int TOP { get; set; } = -1;
        public Nullable<System.DateTime> ENTRY_DATE { get; set; } = Convert.ToDateTime("1/1/1900 12:00:00 AM").Date;
        public string ENTRY_USER_ID { get; set; } = "";
        public string MODIFICATION_USER_ID { get; set; } = "";
        public Nullable<System.DateTime> Modification_Date { get; set; } = Convert.ToDateTime("1/1/1900 12:00:00 AM").Date;

        public Boolean IS_REFRESH { get; set; }

    }
}
