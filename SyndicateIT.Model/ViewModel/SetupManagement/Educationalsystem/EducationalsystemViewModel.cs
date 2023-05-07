﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SyndicateIT.DataLayer.DataContext;
using SyndicateIT.Model.ViewModel.Shared;
using SyndicateIT.UtilityComponent;

namespace SyndicateIT.Model.ViewModel.SetupManagement.Educationalsystem
{
    public class EducationalsystemViewModel : DataLayer.DataContext.Educationalsystem
    {

        public List<NavigationViewModel> Navigation { get; set; }

        public AlertViewModel Alert { get; set; }

        public string ClassTitle { get; set; }

        public string Title { get; set; }


        public String GenderTitle
        {
            get
            {
                if (Educationalsystem_ID != 0 && Educationalsystem_ID.ToString() != "")
                {
                    return TypeAction.Edit + "Gender";
                }
                else
                {
                    return TypeAction.Add + "Gender";
                }
            }
        }
    }
}
