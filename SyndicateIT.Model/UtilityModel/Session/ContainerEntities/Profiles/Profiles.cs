using SyndicateIT.Model.ViewModel.ProfileManagement;
using SyndicateIT.Model.ViewModel.ProfileManagement.ProfileDetails;
using SyndicateIT.UtilityComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyndicateIT.Model.UtilityModel.Session.ContainerEntities.Profiles
{
    /// <summary>
    /// Class UserManagement.
    /// </summary>
    [Serializable()]
    public class Profiles : SessionElement
    {
        #region Constant

        /// <summary>
        /// [ERROR: invalid expression FieldName.Words.TheAndAllAsSentence]
        /// </summary>
        public const string SESSION_KEY = "Profiles";

        #endregion

        #region Properties          

        public UserType UserType { get; set; }

        public ProfilesSearchViewModel ProfilesSearch { get; set; }    
        
        public PersonalInformationsViewModel PersonalInformations { get; set; }

        public ContactInformationsViewModel ContactInformations { get; set; }

        public SyndicateIT.Model.ViewModel.ProfileManagement.ProfileDetails.InsuranceViewModel Insurances { get; set; }

       public RegistrationViewModel Registration { get; set; }

        public ApplicationStatusViewModel ApplicationStatus { get; set; }

        public SystemPrivilegesViewModel SystemPrivileges { get; set; }

        public DocumentViewModel Documents { get; set; }

        public AssignmentViewModel Assignments { get; set; }

        public List<JoinCourcesViewModel> AssignmentJoinCources { get; set; }

        public EmploymentViewModel Employments { get; set; }        

        public DependentViewModel Dependents { get; set; }

        public ExperienceViewModel Experiences { get; set; }

        public EducationViewModel Educations { get; set; }

        public List<DependentViewModel> ListDependents { get; set; }

        public List<ExperienceViewModel> ListExperiences { get; set; }
        public List<EducationViewModel> ListEducations { get; set; }
        public List<SchoolHistoryViewModel> ListSchoolHistories { get; set; }

        public bool IsSuccess { get; set; }

        public string Message { get; set; }

        public AlertType AlertType { get; set; }

        public List<GridProfilesViewModel> ListProfiles { get; set; }

        public bool IsLoadProfilesContent { get; set; }

        public Guid CurrentProfilesID { get; set; }

        public int SourceID { get; set; }

        public string CurrentProfilesName { get; set; }



        #endregion
    }
}
