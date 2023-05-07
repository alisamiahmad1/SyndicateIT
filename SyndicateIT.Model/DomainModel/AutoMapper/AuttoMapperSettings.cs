using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SyndicateIT.Model.DomainModel.DegreeGuarantee;
using SyndicateIT.Model.DomainModel.DegreeInsurance;
using SyndicateIT.Model.DomainModel.GuarantyType;
using SyndicateIT.Model.DomainModel.Insurance;
using SyndicateIT.Model.DomainModel.InsuranceType;
using SyndicateIT.Model.DomainModel.ProfileManagement;
using SyndicateIT.Model.DomainModel.SetupManagement.Branches;
using SyndicateIT.Model.DomainModel.SetupManagement.Corporate;
using SyndicateIT.Model.DomainModel.SetupManagement.Country;
using SyndicateIT.Model.DomainModel.SetupManagement.Department;
using SyndicateIT.Model.DomainModel.SetupManagement.DocumentExtension;
using SyndicateIT.Model.DomainModel.SetupManagement.Educationalsystem;
using SyndicateIT.Model.DomainModel.SetupManagement.Gender;
using SyndicateIT.Model.DomainModel.SetupManagement.Job;
using SyndicateIT.Model.DomainModel.SetupManagement.Kaddaa;
using SyndicateIT.Model.DomainModel.SetupManagement.Language;
using SyndicateIT.Model.DomainModel.SetupManagement.MaritalStatus;
using SyndicateIT.Model.DomainModel.SetupManagement.Nationality;
using SyndicateIT.Model.DomainModel.SetupManagement.PhoneType;
using SyndicateIT.Model.DomainModel.SetupManagement.Place;
using SyndicateIT.Model.DomainModel.SetupManagement.Region;
using SyndicateIT.Model.DomainModel.SetupManagement.RelationTypes;
using SyndicateIT.Model.DomainModel.SetupManagement.Religion;
using SyndicateIT.Model.DomainModel.SetupManagement.ROLE;
using SyndicateIT.Model.DomainModel.SetupManagement.Shift;
using SyndicateIT.Model.DomainModel.SetupManagement.Status;
using SyndicateIT.Model.DomainModel.User_Documents;

namespace SyndicateIT.Model.DomainModel.AutoMapper
{
    [Serializable()]
    public static class AuttoMapperSettings
    {
        /// <summary>
        /// Initializes mappings.
        /// </summary>
        public static void Initialize()
        {                  
            new User_ContactDomainModel().InitializeMapper();             
            new ProfileManagementDomainModel().InitializeMapper();
            new GenderDomainModel().InitializeMapper();        
            new ReligionDomainModel().InitializeMapper();
            new DepartmentDomainModel().InitializeMapper();
            new JobDomainModel().InitializeMapper();
            new ShiftDomainModel().InitializeMapper();
            new StatusDomainModel().InitializeMapper();          
            new CorporateDomainModel().InitializeMapper();         
            new ROLEDomainModel().InitializeMapper();
            new LanguageDomainModel().InitializeMapper();
            new BranchesDomainModel().InitializeMapper();
            new InsurancesDomainModel().InitializeMapper();
            new InsuranceTypeDomainModel().InitializeMapper();
            new GuaranteeTypeDomainModel().InitializeMapper();
            new EducationalsystemDomainModel().InitializeMapper();
            new DegreeInsuranceDomainModel().InitializeMapper();
            new DegreeGuaranteeDomainModel().InitializeMapper();     
            new CountryDomainModel().InitializeMapper();
            new PlaceDomainModel().InitializeMapper();
            new RegionDomainModel().InitializeMapper();
            new KaddaaDomainModel().InitializeMapper();
            new PhoneTypeDomainModel().InitializeMapper();
            new MaritalStatusDomainModel().InitializeMapper();
            new NationalityDomainModel().InitializeMapper();
            new RelationTypesDomainModel().InitializeMapper();
            new DocumentExtensionDomainModel().InitializeMapper();
            new User_DocumentsDomainModel().InitializeMapper();


        }
    }
}
