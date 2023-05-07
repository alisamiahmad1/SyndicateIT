﻿using SyndicateIT.Model.UtilityModel.Session;
using SyndicateIT.UtilityComponent;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyndicateIT.Model.ViewModel.ProfileManagement.ProfileDetails
{
    [Serializable]
    public class EducationViewModel : ViewModelBase
    {
        #region Properties 

        public int Education_ID{ get; set; }
        public Nullable<System.Guid> User_ID { get; set; }
        public string School { get; set; }
        public string Degree { get; set; }
        public string Grade { get; set; }
        public string FieldOfStudy { get; set; }
        public string ActivitiesSocieties { get; set; }
        public string Description { get; set; }
        public Nullable<System.Boolean> IsCurrentDate { get; set; }
        public Nullable<System.DateTime> ToDate { get; set; }
        public Nullable<System.DateTime> FromDate { get; set; }
        public Nullable<System.DateTimeOffset> ENTRY_DATE { get; set; }
        public Nullable<System.Guid> ENTRY_USER_ID { get; set; }
        public Nullable<System.Guid> MODIFICATION_USER_ID { get; set; }
        public Nullable<System.DateTimeOffset> MODIFICATION_DATE { get; set; }

        public List<EducationViewModel> Educations { get; set; }

        public bool IsEdit
        {
            get
            {
                if ((SessionContent.Container.Profiles.SourceID == (int)ProfileSourceType.MyProfile) ||SessionContent.Container.Profiles.SourceID == (int)ProfileSourceType.ProfileInforamtion && SessionContent.Container.Profiles.PersonalInformations != null && SessionContent.Container.Profiles.PersonalInformations.User_ID != null)
                    return true;
                else
                    return false;
            }
        }

        public EducationViewModel()
        {
            Educations = new List<EducationViewModel>();
        }

        #endregion
    }
}
