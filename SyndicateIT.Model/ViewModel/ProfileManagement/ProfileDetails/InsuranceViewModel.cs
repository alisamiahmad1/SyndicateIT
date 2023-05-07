using SyndicateIT.Model.UtilityModel.Session;
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
    public class InsuranceViewModel : ViewModelBase
    {
        public System.Guid Insurance_Id { get; set; }
        public Nullable<System.Guid> Users_Id { get; set; }
        public Nullable<System.Guid> TypeGuarantee_Id { get; set; }
        public Nullable<System.Guid> TypeInsurance_Id { get; set; }
        public Nullable<System.Guid> DegreeInsurance_Id { get; set; }
        public Nullable<System.Guid> DegreeGuarantee_Id { get; set; }
        public Nullable<bool> HasGuarantee { get; set; } = true;
        public Nullable<bool> HasInsurance { get; set; } = true;
        public string TypeInsuranceNotes { get; set; }
        public string DegreeGuaranteeNotes { get; set; }
        public Nullable<System.DateTime> Entry_Date { get; set; }
        public Nullable<System.Guid> Entry_Users_Id { get; set; }
        public Nullable<System.Guid> Owner_Id { get; set; }
        public string Fts { get; set; }
        public Nullable<System.DateTime> Modification_Date { get; set; }
        public Nullable<System.Guid> Modification_Users_Id { get; set; }
        public Nullable<bool> IsActive { get; set; }

        public bool IsEdit
        {
            get
            {
                if (SessionContent.Container.Profiles.SourceID == (int)ProfileSourceType.ProfileInforamtion || SessionContent.Container.Profiles.SourceID == (int)ProfileSourceType.MyProfile)
                    return true;
                else
                    return false;
            }
        }
    }
}
