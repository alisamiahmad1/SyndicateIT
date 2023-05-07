using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using SyndicateIT.Models;
using System.Collections.Generic;
using System.Configuration;
using SyndicateIT.Model.DomainModel.UserManagement;
using System.IO;
using System.Web.UI.WebControls;
using System.Web.UI;
using SyndicateIT.Model.UtilityModel.Session;
using SyndicateIT.Helper.CultureHelper;
using System.Threading;
using SyndicateIT.UtilityComponent;
using SyndicateIT.Model.ViewModel.ProfileManagement.ProfileDetails;
using SyndicateIT.Model.DomainModel.ProfileManagement;
using SyndicateIT.DataLayer.DataContext;
using SyndicateIT.Model.ViewModel.SetupManagement.Shift;
using SyndicateIT.Model.ViewModel.SetupManagement.Status;
using SyndicateIT.Model.DomainModel.SetupManagement.Status;
using SyndicateIT.Model.ViewModel.SetupManagement.DegreeInsurance;
using SyndicateIT.Model.ViewModel.SetupManagement.ROLE;
using SyndicateIT.Model.DomainModel.SetupManagement.ROLE;
using SyndicateIT.Model.ViewModel.SetupManagement.RelationTypes;
using SyndicateIT.Model.DomainModel.SetupManagement.RelationTypes;
using SyndicateIT.Model.ViewModel.SetupManagement.Country;
using SyndicateIT.Model.DomainModel.SetupManagement.Country;
using SyndicateIT.Model.ViewModel.SetupManagement.Religion;
using SyndicateIT.Model.DomainModel.SetupManagement.Religion;
using SyndicateIT.Model.ViewModel.SetupManagement.Nationality;
using SyndicateIT.Model.ViewModel.SetupManagement.Department;
using SyndicateIT.Model.DomainModel.SetupManagement.Department;
using SyndicateIT.Model.DomainModel.SetupManagement.Nationality;
using SyndicateIT.Model.ViewModel.SetupManagement.Job;
using SyndicateIT.Model.DomainModel.SetupManagement.Job;
using SyndicateIT.Model.ViewModel.SetupManagement.Region;
using SyndicateIT.Model.ViewModel.SetupManagement.Kaddaa;
using SyndicateIT.Model.DomainModel.SetupManagement.Kaddaa;
using SyndicateIT.Model.DomainModel.SetupManagement.Shift;
using SyndicateIT.Model.DomainModel.SetupManagement.Region;
using SyndicateIT.Model.ViewModel.SetupManagement.Place;
using SyndicateIT.Model.DomainModel.SetupManagement.Place;
using SyndicateIT.Model.ViewModel.SetupManagement.Gender;
using SyndicateIT.Model.DomainModel.SetupManagement.Gender;
using SyndicateIT.Model.ViewModel.SetupManagement.MaritalStatus;
using SyndicateIT.Model.DomainModel.SetupManagement.MaritalStatus;
using SyndicateIT.Model.ViewModel.GuarantyType;
using SyndicateIT.Model.ViewModel.SetupManagement.DegreeGuarantee;
using SyndicateIT.Model.DomainModel.SetupManagement.DegreeGuarantee;
using SyndicateIT.Model.DomainModel.GuarantyType;
using SyndicateIT.Model.ViewModel.InsuranceType;
using SyndicateIT.Model.DomainModel.InsuranceType;
using SyndicateIT.Model.DomainModel.DegreeInsurance;
using SyndicateIT.Model.ViewModel.SetupManagement.Educationalsystem;
using SyndicateIT.Model.DomainModel.SetupManagement.Educationalsystem;

namespace SyndicateIT.Controllers
{
    public class BaseController : Controller
    {

        #region Public    


        public JsonResult GetEducationalSystem()
        {
            try
            {
                var model = new EducationalsystemContentViewModel();
                var lst = new EducationalsystemDomainModel().GetListEducationalsystem(model);
                List<SelectListItem> ItemList = new List<SelectListItem>();
                if (lst != null && lst.Count > 0)
                {
                    foreach (EducationalsystemGridViewModel grid in lst)
                    {
                        ItemList.Add(new SelectListItem()
                        {
                            Text = grid.Name.ToUpperFirstLetter(),
                            Value = grid.Educationalsystem_ID.ToString(),
                        });
                    }
                }
                JsonResult j = Json(new SelectList(ItemList, "Value", "Text"), JsonRequestBehavior.AllowGet);
                return j;
            }
            catch (Exception ex)
            {
                JsonResult j = Json(ex, JsonRequestBehavior.AllowGet);
                return j;
            }
        }


        public JsonResult GetRelative()
        {
            try
            {
                var model = new PersonalInformationsContentViewModel();
                var lst = new ProfileManagementDomainModel().GetListUsers(model);
                List<SelectListItem> ItemList = new List<SelectListItem>();
                if (lst != null && lst.Count > 0)
                {
                    foreach (PersonalInformationsGridViewModel grid in lst)
                    {
                        ItemList.Add(new SelectListItem()
                        {
                            Text = grid.FirstNameName_Ar.ToUpperFirstLetter() + " " + grid.LastName_Ar.ToUpperFirstLetter(),
                            Value = grid.User_ID.ToString(),
                        });
                    }
                }
                JsonResult j = Json(new SelectList(ItemList, "Value", "Text"), JsonRequestBehavior.AllowGet);
                return j;
            }
            catch (Exception ex)
            {
                JsonResult j = Json(ex, JsonRequestBehavior.AllowGet);
                return j;
            }
        }
        public JsonResult GetRelationShip()
        {
            try
            {

                var model = new RelationTypesContentViewModel();
                var lst = new RelationTypesDomainModel().GetListRelationTypes(model);
                List<SelectListItem> ItemList = new List<SelectListItem>();
                if (lst != null && lst.Count > 0)
                {
                    foreach (RelationTypesGridViewModel grid in lst)
                    {
                        ItemList.Add(new SelectListItem()
                        {
                            Text = grid.Relation_Type_Title.ToUpperFirstLetter(),
                            Value = grid.Relation_Type_ID.ToString(),
                        });
                    }
                }

                JsonResult j = Json(new SelectList(ItemList, "Value", "Text"), JsonRequestBehavior.AllowGet);
                return j;
            }
            catch (Exception ex)
            {
                
                JsonResult j = Json(ex, JsonRequestBehavior.AllowGet);
                return j;
            }
        }
        public JsonResult GetCountry()
        {
            try
            {

                var model = new CountryContentViewModel();
                var lst = new CountryDomainModel().GetListCountry(model);
                List<SelectListItem> ItemList = new List<SelectListItem>();
                if (lst != null && lst.Count > 0)
                {
                    foreach (CountryGridViewModel grid in lst)
                    {
                        ItemList.Add(new SelectListItem()
                        {
                            Text = grid.COUNTRY_NAME.ToUpperFirstLetter(),
                            Value = grid.COUNTRY_ID.ToString(),
                        });
                    }
                }

                JsonResult j = Json(new SelectList(ItemList, "Value", "Text"), JsonRequestBehavior.AllowGet);
                return j;
            }
            catch (Exception ex)
            {
                //  string result = ex.ToString();
                //JsonResult j = Json(new SelectList(result, "result", "message"), JsonRequestBehavior.AllowGet);
                JsonResult j = Json(ex, JsonRequestBehavior.AllowGet);
                return j;
            }
        }
        public JsonResult GetROLEName()
        {
            try
            {

                var model = new ROLEContentViewModel();
                var lst = new ROLEDomainModel().GetListROLE(model);
                List<SelectListItem> ItemList = new List<SelectListItem>();
                if (lst != null && lst.Count > 0)
                {
                    foreach (ROLEGridViewModel grid in lst)
                    {
                        ItemList.Add(new SelectListItem()
                        {
                            Text = grid.ROLE_NAME.ToUpperFirstLetter(),
                            Value = grid.Role_ID.ToString(),
                        });
                    }
                }

                JsonResult j = Json(new SelectList(ItemList, "Value", "Text"), JsonRequestBehavior.AllowGet);
                return j;
            }
            catch (Exception ex)
            {
                //  string result = ex.ToString();
                //JsonResult j = Json(new SelectList(result, "result", "message"), JsonRequestBehavior.AllowGet);
                JsonResult j = Json(ex, JsonRequestBehavior.AllowGet);
                return j;
            }
        }      
        public JsonResult GetReligionName()
        {
            try
            {

                var model = new ReligionContentViewModel();
                var lst = new ReligionDomainModel().GetListReligion(model);
                List<SelectListItem> ItemList = new List<SelectListItem>();
                if (lst != null && lst.Count > 0)
                {
                    foreach (ReligionGridViewModel grid in lst)
                    {
                        ItemList.Add(new SelectListItem()
                        {
                            Text = grid.Religion_Name.ToUpperFirstLetter(),
                            Value = grid.Religion_ID.ToString(),
                        });
                    }
                }

                JsonResult j = Json(new SelectList(ItemList, "Value", "Text"), JsonRequestBehavior.AllowGet);
                return j;
            }
            catch (Exception ex)
            {
                //  string result = ex.ToString();
                //JsonResult j = Json(new SelectList(result, "result", "message"), JsonRequestBehavior.AllowGet);
                JsonResult j = Json(ex, JsonRequestBehavior.AllowGet);
                return j;
            }
        }
        public JsonResult GetNationality()
        {
            try
            {

                var model = new NationalityContentViewModel();
                var lst = new NationalityDomainModel().GetListNationality(model);
                List<SelectListItem> ItemList = new List<SelectListItem>();
                if (lst != null && lst.Count > 0)
                {
                    foreach (NationalityGridViewModel grid in lst)
                    {
                        ItemList.Add(new SelectListItem()
                        {
                            Text = grid.Nationality_Title.ToUpperFirstLetter(),
                            Value = grid.Nationality_ID.ToString(),
                        });
                    }
                }

                JsonResult j = Json(new SelectList(ItemList, "Value", "Text"), JsonRequestBehavior.AllowGet);
                return j;
            }
            catch (Exception ex)
            {
                //  string result = ex.ToString();
                //JsonResult j = Json(new SelectList(result, "result", "message"), JsonRequestBehavior.AllowGet);
                JsonResult j = Json(ex, JsonRequestBehavior.AllowGet);
                return j;
            }
        }
        public JsonResult GetDepartment()
        {
            try
            {

                var model = new DepartmentContentViewModel();
                var lst = new DepartmentDomainModel().GetListDepartment(model);
                List<SelectListItem> ItemList = new List<SelectListItem>();
                if (lst != null && lst.Count > 0)
                {
                    foreach (DepartmentGridViewModel grid in lst)
                    {
                        ItemList.Add(new SelectListItem()
                        {
                            Text = grid.DEPARTMENT_NAME.ToUpperFirstLetter(),
                            Value = grid.DEPARTMENT_ID.ToString(),
                        });
                    }
                }

                JsonResult j = Json(new SelectList(ItemList, "Value", "Text"), JsonRequestBehavior.AllowGet);
                return j;
            }
            catch (Exception ex)
            {
                //  string result = ex.ToString();
                //JsonResult j = Json(new SelectList(result, "result", "message"), JsonRequestBehavior.AllowGet);
                JsonResult j = Json(ex, JsonRequestBehavior.AllowGet);
                return j;
            }
        }
        public JsonResult GetJob()
        {
            try
            {

                var model = new JobContentViewModel();
                var lst = new JobDomainModel().GetListJob(model);
                List<SelectListItem> ItemList = new List<SelectListItem>();
                if (lst != null && lst.Count > 0)
                {
                    foreach (JobGridViewModel grid in lst)
                    {
                        ItemList.Add(new SelectListItem()
                        {
                            Text = grid.JOB_NAME.ToUpperFirstLetter(),
                            Value = grid.JOB_ID.ToString(),
                        });
                    }
                }

                JsonResult j = Json(new SelectList(ItemList, "Value", "Text"), JsonRequestBehavior.AllowGet);
                return j;
            }
            catch (Exception ex)
            {
                //  string result = ex.ToString();
                //JsonResult j = Json(new SelectList(result, "result", "message"), JsonRequestBehavior.AllowGet);
                JsonResult j = Json(ex, JsonRequestBehavior.AllowGet);
                return j;
            }
        }
        public JsonResult GetShift()
        {
            try
            {

                var model = new ShiftContentViewModel();
                var lst = new ShiftDomainModel().GetListShift(model);
                List<SelectListItem> ItemList = new List<SelectListItem>();
                if (lst != null && lst.Count > 0)
                {
                    foreach (ShiftGridViewModel grid in lst)
                    {
                        ItemList.Add(new SelectListItem()
                        {
                            Text = grid.SHIFT_NAME.ToUpperFirstLetter(),
                            Value = grid.SHIFT_ID.ToString(),
                        });
                    }
                }

                JsonResult j = Json(new SelectList(ItemList, "Value", "Text"), JsonRequestBehavior.AllowGet);
                return j;
            }
            catch (Exception ex)
            {
                //  string result = ex.ToString();
                //JsonResult j = Json(new SelectList(result, "result", "message"), JsonRequestBehavior.AllowGet);
                JsonResult j = Json(ex, JsonRequestBehavior.AllowGet);
                return j;
            }
        }
        public JsonResult GetStatus()
        {
            try
            {

                var model = new StatusContentViewModel();
                var lst = new StatusDomainModel().GetListStatus(model);
                List<SelectListItem> ItemList = new List<SelectListItem>();
                if (lst != null && lst.Count > 0)
                {
                    foreach (StatusGridViewModel grid in lst)
                    {
                        ItemList.Add(new SelectListItem()
                        {
                            Text = grid.Status_Name.ToUpperFirstLetter(),
                            Value = grid.Status_ID.ToString(),
                        });
                    }
                }

                JsonResult j = Json(new SelectList(ItemList, "Value", "Text"), JsonRequestBehavior.AllowGet);
                return j;
            }
            catch (Exception ex)
            {
                //  string result = ex.ToString();
                //JsonResult j = Json(new SelectList(result, "result", "message"), JsonRequestBehavior.AllowGet);
                JsonResult j = Json(ex, JsonRequestBehavior.AllowGet);
                return j;
            }
        }
        public JsonResult GetRegion(int Country_ID = -1)
        {
            try
            {
                var model = new RegionContentViewModel();
                if (Country_ID != -1)
                {
                    model.Country_ID = Country_ID;
                }

                var lst = new RegionDomainModel().GetListRegion(model);
                List<SelectListItem> ItemList = new List<SelectListItem>();
                if (lst != null && lst.Count > 0)
                {
                    foreach (RegionGridViewModel grid in lst)
                    {
                        ItemList.Add(new SelectListItem()
                        {
                            Text = grid.Region_Name.ToUpperFirstLetter(),
                            Value = grid.Region_ID.ToString(),
                        });
                    }
                }

                JsonResult j = Json(new SelectList(ItemList, "Value", "Text"), JsonRequestBehavior.AllowGet);
                return j;
            }
            catch (Exception ex)
            {
                //  string result = ex.ToString();
                //JsonResult j = Json(new SelectList(result, "result", "message"), JsonRequestBehavior.AllowGet);
                JsonResult j = Json(ex, JsonRequestBehavior.AllowGet);
                return j;
            }
        }
        public JsonResult GetState(int Region_ID = -1)
        {
            try
            {

                var model = new KaddaaContentViewModel();
                if (Region_ID != -1)
                {
                    model.Region_ID = Region_ID;
                }
                var lst = new KaddaaDomainModel().GetListKaddaa(model);
                List<SelectListItem> ItemList = new List<SelectListItem>();
                if (lst != null && lst.Count > 0)
                {
                    foreach (KaddaaGridViewModel grid in lst)
                    {
                        ItemList.Add(new SelectListItem()
                        {
                            Text = grid.Kaddaa_NAME.ToUpperFirstLetter(),
                            Value = grid.STP_Kaddaa_ID.ToString(),
                        });
                    }
                }

                JsonResult j = Json(new SelectList(ItemList, "Value", "Text"), JsonRequestBehavior.AllowGet);
                return j;
            }
            catch (Exception ex)
            {
                //  string result = ex.ToString();
                //JsonResult j = Json(new SelectList(result, "result", "message"), JsonRequestBehavior.AllowGet);
                JsonResult j = Json(ex, JsonRequestBehavior.AllowGet);
                return j;
            }
        }
        public JsonResult GetKaddaaID(int Region_ID = -1)
        {
            try
            {

                var model = new KaddaaContentViewModel();
                if (Region_ID != -1)
                {
                    model.Region_ID = Region_ID;
                }
                var lst = new KaddaaDomainModel().GetListKaddaa(model);
                List<SelectListItem> ItemList = new List<SelectListItem>();
                if (lst != null && lst.Count > 0)
                {
                    foreach (KaddaaGridViewModel grid in lst)
                    {
                        ItemList.Add(new SelectListItem()
                        {
                            Text = grid.Kaddaa_NAME.ToUpperFirstLetter(),
                            Value = grid.STP_Kaddaa_ID.ToString(),
                        });
                    }
                }

                JsonResult j = Json(new SelectList(ItemList, "Value", "Text"), JsonRequestBehavior.AllowGet);
                return j;
            }
            catch (Exception ex)
            {
                //  string result = ex.ToString();
                //JsonResult j = Json(new SelectList(result, "result", "message"), JsonRequestBehavior.AllowGet);
                JsonResult j = Json(ex, JsonRequestBehavior.AllowGet);
                return j;
            }
        }
        public JsonResult GetRegionID(int Region_ID = -1)
        {
            try
            {

                var model = new RegionContentViewModel();
                if (Region_ID != -1)
                {
                    model.Region_ID = Region_ID;
                }
                var lst = new RegionDomainModel().GetListRegion(model);
                List<SelectListItem> ItemList = new List<SelectListItem>();
                if (lst != null && lst.Count > 0)
                {
                    foreach (RegionGridViewModel grid in lst)
                    {
                        ItemList.Add(new SelectListItem()
                        {
                            Text = grid.Region_Name.ToUpperFirstLetter(),
                            Value = grid.Region_ID.ToString(),
                        });
                    }
                }

                JsonResult j = Json(new SelectList(ItemList, "Value", "Text"), JsonRequestBehavior.AllowGet);
                return j;
            }
            catch (Exception ex)
            {
                //  string result = ex.ToString();
                //JsonResult j = Json(new SelectList(result, "result", "message"), JsonRequestBehavior.AllowGet);
                JsonResult j = Json(ex, JsonRequestBehavior.AllowGet);
                return j;
            }
        }
        public JsonResult GetCity(int Kaddaa_ID = -1)
        {
            try
            {

                var model = new PlaceContentViewModel();
                if (Kaddaa_ID != -1)
                {
                    model.Kaddaa_ID = Kaddaa_ID;
                }
                var lst = new PlaceDomainModel().GetListPlace(model);
                List<SelectListItem> ItemList = new List<SelectListItem>();
                if (lst != null && lst.Count > 0)
                {
                    foreach (PlaceGridViewModel grid in lst)
                    {
                        ItemList.Add(new SelectListItem()
                        {
                            Text = grid.Place_Name.ToUpperFirstLetter(),
                            Value = grid.Place_ID.ToString(),
                        });
                    }
                }

                JsonResult j = Json(new SelectList(ItemList, "Value", "Text"), JsonRequestBehavior.AllowGet);
                return j;
            }
            catch (Exception ex)
            {
                //  string result = ex.ToString();
                //JsonResult j = Json(new SelectList(result, "result", "message"), JsonRequestBehavior.AllowGet);
                JsonResult j = Json(ex, JsonRequestBehavior.AllowGet);
                return j;
            }
        }
        public JsonResult GetPlaceOfBirth(int Country_ID = -1)
        {
            try
            {

                var model = new PlaceContentViewModel();
                if (Country_ID != -1)
                {
                    model.Country_ID = Country_ID;
                }
                var lst = new PlaceDomainModel().GetListPlace(model);
                List<SelectListItem> ItemList = new List<SelectListItem>();
                if (lst != null && lst.Count > 0)
                {
                    foreach (PlaceGridViewModel grid in lst)
                    {
                        ItemList.Add(new SelectListItem()
                        {
                            Text = grid.Place_Name.ToUpperFirstLetter(),
                            Value = grid.Place_ID.ToString(),
                        });
                    }
                }

                JsonResult j = Json(new SelectList(ItemList, "Value", "Text"), JsonRequestBehavior.AllowGet);
                return j;
            }
            catch (Exception ex)
            {
                //  string result = ex.ToString();
                //JsonResult j = Json(new SelectList(result, "result", "message"), JsonRequestBehavior.AllowGet);
                JsonResult j = Json(ex, JsonRequestBehavior.AllowGet);
                return j;
            }
        }
        public JsonResult GetGender()
        {
            try
            {
                var model = new GenderContentViewModel();
                var lst = new GenderDomainModel().GetListGender(model);
                List<SelectListItem> ItemList = new List<SelectListItem>();
                if (lst != null && lst.Count > 0)
                {
                    foreach (GenderGridViewModel grid in lst)
                    {
                        ItemList.Add(new SelectListItem()
                        {
                            Text = grid.GENDER_NAME.ToUpperFirstLetter(),
                            Value = grid.GENDER_ID.ToString(),
                        });
                    }
                }

                JsonResult j = Json(new SelectList(ItemList, "Value", "Text"), JsonRequestBehavior.AllowGet);
                return j;
            }
            catch (Exception ex)
            {
                //  string result = ex.ToString();
                //JsonResult j = Json(new SelectList(result, "result", "message"), JsonRequestBehavior.AllowGet);
                JsonResult j = Json(ex, JsonRequestBehavior.AllowGet);
                return j;
            }
        }
        public JsonResult GetMarital_Status()
        {
            try
            {

                var model = new MaritalStatusContentViewModel();
                var lst = new MaritalStatusDomainModel().GetListMaritalStatus(model);
                List<SelectListItem> ItemList = new List<SelectListItem>();
                if (lst != null && lst.Count > 0)
                {
                    foreach (MaritalStatusGridViewModel grid in lst)
                    {
                        ItemList.Add(new SelectListItem()
                        {
                            Text = grid.Marital_Status_Title.ToUpperFirstLetter(),
                            Value = grid.Marital_Status_ID.ToString(),
                        });
                    }
                }

                JsonResult j = Json(new SelectList(ItemList, "Value", "Text"), JsonRequestBehavior.AllowGet);
                return j;
            }
            catch (Exception ex)
            {
                //  string result = ex.ToString();
                //JsonResult j = Json(new SelectList(result, "result", "message"), JsonRequestBehavior.AllowGet);
                JsonResult j = Json(ex, JsonRequestBehavior.AllowGet);
                return j;
            }
        }
        public JsonResult GetTypeGuarantee()
        {
            try
            {

                var model = new GuaranteeTypeContentViewModel();
                var lst = new GuaranteeTypeDomainModel().GetListGuaranteeType(model);
                List<SelectListItem> ItemList = new List<SelectListItem>();
                if (lst != null && lst.Count > 0)
                {
                    foreach (GuaranteeTypeGridViewModel grid in lst)
                    {
                        ItemList.Add(new SelectListItem()
                        {
                            Text = grid.Type_Guarantee_By_Language_Name.ToUpperFirstLetter(),
                            Value = grid.Type_Guarantee_Id.ToString(),
                        });
                    }
                }

                JsonResult j = Json(new SelectList(ItemList, "Value", "Text"), JsonRequestBehavior.AllowGet);
                return j;
            }
            catch (Exception ex)
            {
                //  string result = ex.ToString();
                //JsonResult j = Json(new SelectList(result, "result", "message"), JsonRequestBehavior.AllowGet);
                JsonResult j = Json(ex, JsonRequestBehavior.AllowGet);
                return j;
            }
        }
        public JsonResult GetDegreeGuarantee()
        {
            try
            {

                var model = new DegreeGuaranteeContentViewModel();
                var lst = new DegreeGuaranteeDomainModel().GetListDegreeGuarantee(model);
                List<SelectListItem> ItemList = new List<SelectListItem>();
                if (lst != null && lst.Count > 0)
                {
                    foreach (DegreeGuaranteeGridViewModel grid in lst)
                    {
                        ItemList.Add(new SelectListItem()
                        {
                            Text = grid.Degree_Guarantee_Name.ToUpperFirstLetter(),
                            Value = grid.Degree_Guarantee_ID.ToString(),
                        });
                    }
                }

                JsonResult j = Json(new SelectList(ItemList, "Value", "Text"), JsonRequestBehavior.AllowGet);
                return j;
            }
            catch (Exception ex)
            {
                //  string result = ex.ToString();
                //JsonResult j = Json(new SelectList(result, "result", "message"), JsonRequestBehavior.AllowGet);
                JsonResult j = Json(ex, JsonRequestBehavior.AllowGet);
                return j;
            }
        }
        public JsonResult GetTypeInsurance()
        {
            try
            {

                var model = new InsuranceTypeContentViewModel();
                var lst = new InsuranceTypeDomainModel().GetListInsuranceType(model);
                List<SelectListItem> ItemList = new List<SelectListItem>();
                if (lst != null && lst.Count > 0)
                {
                    foreach (InsuranceTypeGridViewModel grid in lst)
                    {
                        ItemList.Add(new SelectListItem()
                        {
                            Text = grid.Type_Insurance_By_Language_Name.ToUpperFirstLetter(),
                            Value = grid.Type_Insurance_Id.ToString(),
                        });
                    }
                }

                JsonResult j = Json(new SelectList(ItemList, "Value", "Text"), JsonRequestBehavior.AllowGet);
                return j;
            }
            catch (Exception ex)
            {
                //  string result = ex.ToString();
                //JsonResult j = Json(new SelectList(result, "result", "message"), JsonRequestBehavior.AllowGet);
                JsonResult j = Json(ex, JsonRequestBehavior.AllowGet);
                return j;
            }
        }
        public JsonResult GetDegreeInsurance()
        {
            try
            {

                var model = new SyndicateIT.Model.ViewModel.DegreeInsurance.DegreeInsuranceContentViewModel();
                var lst = new DegreeInsuranceDomainModel().GetListDegreeInsurance(model);
                List<SelectListItem> ItemList = new List<SelectListItem>();
                if (lst != null && lst.Count > 0)
                {
                    foreach (var  grid in lst)
                    {
                        ItemList.Add(new SelectListItem()
                        {
                            Text = grid.Degree_Insurance_Name.ToUpperFirstLetter(),
                            Value = grid.Degree_Insurance_Id.ToString(),
                        });
                    }
                }

                JsonResult j = Json(new SelectList(ItemList, "Value", "Text"), JsonRequestBehavior.AllowGet);
                return j;
            }
            catch (Exception ex)
            {
                string result = ex.ToString();
                JsonResult j = Json(new SelectList(result, "result", "message"), JsonRequestBehavior.AllowGet);         
                return j;
            }
        }

        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            string cultureName = null;

            // Attempt to read the culture cookie from Request
            HttpCookie cultureCookie = Request.Cookies["_culture"];
            if (cultureCookie != null)
                cultureName = cultureCookie.Value;
            else
                cultureName = Request.UserLanguages != null && Request.UserLanguages.Length > 0 ?
                        Request.UserLanguages[0] :  // obtain it from HTTP header AcceptLanguages
                        null;
            // Validate culture name
            cultureName = CultureHelper.GetImplementedCulture(cultureName); // This is safe

            // Modify current thread's cultures            
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(cultureName);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

            return base.BeginExecuteCore(callback, state);
        }      
        public async Task SendEmail(string email, string subject, string name, string callbackUrl, string text, string recepientEmail)
        {
            //WCFEmail.EmailManagerServiceClient client = new WCFEmail.EmailManagerServiceClient();
            //WCFEmail.EmailRequest request = new WCFEmail.EmailRequest();
            //request.Request = new WCFEmail.GenericRequest();
            //request.Request.RequestId = Guid.NewGuid().ToString();
            //request.recepientEmails = new string[1];
            //request.recepientEmails[0] = email;
            //request.fromEmail = recepientEmail;
            //request.fromEmailName = name;
            //request.isHtml = true;
            //request.body = text;
            //request.subject = subject;
            //await client.SendEmailAsync(request);
        }      
        public void AddErrors(IdentityResult result, string id = "")
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(id, error);
            }
        }    
        public ActionResult RedirectToLocal(string returnUrl)
        {
            return Redirect(returnUrl);
        }

        #region DropDown 

        public JsonResult GetCurrency()
        {
            try
            {
                var roles = SessionContent.AppStore.CurrencyList;
                JsonResult J = Json(new SelectList(roles, "Value", "Text"), JsonRequestBehavior.AllowGet);
                return J;
            }
            catch (Exception ex)
            {
            }
            return new JsonResult();
        }
        public JsonResult GetRoleList()
        {
            try
            {
                var roles = SessionContent.AppStore.RoleList.Where(p => p.Value != "1c0c039a-eeb2-4e50-b5e8-ad4f8ead8e1d").ToList();
                JsonResult J = Json(new SelectList(roles, "Value", "Text"), JsonRequestBehavior.AllowGet);
                return J;
            }
            catch (Exception ex)
            {
            }
            return new JsonResult();
        }
        public JsonResult GetUserReferenceList()
        {
            try
            {
                var roles = SessionContent.AppStore.UserList.ToList();
                JsonResult J = Json(new SelectList(roles, "Value", "Text"), JsonRequestBehavior.AllowGet);
                return J;
            }
            catch (Exception ex)
            {
            }
            return new JsonResult();
        }

        


        public JsonResult GetRoleWithTypeList(string type)
        {
            try
            {
                var roles = SessionContent.AppStore.RoleList;

                if (UserType.MemberBoard.ToString() == type)
                {
                    roles = roles.Where(p => p.Value == "d8425b0d-50b2-48cf-985a-d1deba4a4cd0").ToList();
                }
                else if (UserType.Members.ToString() == type)
                {
                    roles = roles.Where(p => p.Value == "fc185540-fd2a-4cf8-b7d0-425308e00b66").ToList();
                }                
                else
                {
                    roles = roles.Where(p => p.Value != "d8425b0d-50b2-48cf-985a-d1deba4a4cd0" && p.Value != "fc185540-fd2a-4cf8-b7d0-425308e00b66" && p.Value !=  "1c0c039a-eeb2-4e50-b5e8-ad4f8ead8e1d").ToList();
                }

                JsonResult J = Json(new SelectList(roles, "Value", "Text"), JsonRequestBehavior.AllowGet);
                return J;
            }
            catch (Exception ex)
            {
            }
            return new JsonResult();
        }
        public JsonResult GetChangeRequestType()
        {
            try
            {
                var roles = new List<SelectListItem>();
                roles.Add(new SelectListItem() { Text = "Subscriber Fees", Value = "1", });
                roles.Add(new SelectListItem() { Text = "Plan Limits", Value = "2", });
                JsonResult J = Json(new SelectList(roles, "Value", "Text"), JsonRequestBehavior.AllowGet);
                return J;
            }
            catch (Exception ex)
            {
            }
            return new JsonResult();
        }

        #endregion

        #region Export To Excel

        /// <summary>
        /// Generates the excel HTML.
        /// </summary>
        /// <param name="body">The body.</param>
        /// <param name="sheetName">Name of the sheet.</param>
        /// <param name="worksheetName">Name of the worksheet.</param>
        /// <param name="cssPath">The CSS path.</param>
        public void GenerateExcelHtml(string body, string sheetName, string worksheetName = "", string cssPath = "")
        {
            Response.Clear();
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.AddHeader("Content-Type", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            Response.AddHeader("Content-Type", "application/vnd.ms-excel");
            Response.ContentType = "application/vnd.ms-excel";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + sheetName + ".xls");
            Response.Write("<html xmlns:x='urn:schemas-microsoft-com:office:excel'>");
            Response.Write("<head>");
            Response.Write("<meta http-equiv='Content-Type' content='text/html;charset=UTF-8'>");
            Response.Write("<!--[if gte mso 9]>");
            Response.Write("<xml>");
            Response.Write("<x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet>");
            Response.Write("<x:Name>" + (!string.IsNullOrEmpty(worksheetName) ? worksheetName : sheetName) + "</x:Name>");
            Response.Write("<x:WorksheetOptions>");
            Response.Write("<x:Panes></x:Panes>");//shows excel grid lines            
            Response.Write("</x:WorksheetOptions>");
            Response.Write("</x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook>");
            Response.Write("</xml>");
            Response.Write("<![endif]-->");
            if (cssPath != null)
            {
                FileInfo fi = new FileInfo(Server.MapPath(cssPath));
                StreamReader sr = fi.OpenText();
                Response.Write("<style type='text/css'>" + sr.ReadToEnd() + "</style>");
                sr.Close();
            }
            Response.Write("</head>");
            Response.Write("<body>");
            Response.Write(body);
            Response.Write("</body></html>");
            Response.Flush();
            Response.End();

        }

        #endregion

        #region RenderPartialViewToString

        /// <summary>
        /// Renders a partial view to string
        /// </summary>
        /// <returns></returns>
        protected string RenderPartialViewToString()
        {
            return RenderPartialViewToString(null, null);
        }

        /// <summary>
        /// Renders a partial view to string
        /// </summary>
        /// <param name="viewName">The name or path of the view to render</param>
        /// <returns></returns>
        protected string RenderPartialViewToString(string viewName)
        {
            return RenderPartialViewToString(viewName, null);
        }

        /// <summary>
        /// Renders a partial view to string
        /// </summary>
        /// <param name="model">The model</param>
        /// <returns></returns>
        protected string RenderPartialViewToString(object model)
        {
            return RenderPartialViewToString(null, model);
        }

        /// <summary>
        /// Renders a partial view to string
        /// </summary>
        /// <param name="viewName">The name or path of the view to render</param>
        /// <param name="model">The model</param>
        /// <returns></returns>
        protected string RenderPartialViewToString(string viewName, object model)
        {
            return RenderPartialViewToString(viewName, model, null);
        }

        /// <summary>
        /// Renders the partial view to string.
        /// </summary>
        /// <param name="viewName">Name of the view.</param>
        /// <param name="model">The model.</param>
        /// <param name="controllerContext">The controller context.</param>
        /// <returns></returns>
        protected string RenderPartialViewToString(string viewName, object model, ControllerContext controllerContext)
        {
            try
            {
                if (string.IsNullOrEmpty(viewName))
                    viewName = ControllerContext.RouteData.GetRequiredString("action");

                ViewData.Model = model;

                using (StringWriter sw = new StringWriter())
                {
                    var context = controllerContext != null ? controllerContext : ControllerContext;
                    ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(context, viewName);
                    ViewContext viewContext = new ViewContext(context, viewResult.View, ViewData, TempData, sw);
                    viewResult.View.Render(viewContext, sw);

                    return sw.GetStringBuilder().ToString();
                }
            }
            catch (Exception ex) { return string.Format("{0}{1}", ex.Message, (ex.InnerException != null ? "<br />" + ex.InnerException.Message : string.Empty)); }
        }


        #endregion

        //#region Logs

        //private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        //public static void Log(string logMessage)
        //{
        //    try
        //    {

        //        log.Info(logMessage);
        //    }
        //    catch (Exception ex)
        //    {

        //    }

        //}

        //#endregion        

        public JsonResult GetMonth()
        {
            List<SelectListItem> ItemList = new List<SelectListItem>();
            ItemList.Add(new SelectListItem()
            {
                Value = "1",
                Text = "January",
            });
            ItemList.Add(new SelectListItem()
            {
                Value = "2",
                Text = "February",
            });
            ItemList.Add(new SelectListItem()
            {
                Value = "3",
                Text = "March",
            });
            ItemList.Add(new SelectListItem()
            {
                Value = "4",
                Text = "April",
            });
            ItemList.Add(new SelectListItem()
            {
                Value = "5",
                Text = "May",
            });
            ItemList.Add(new SelectListItem()
            {
                Value = "6",
                Text = "June",
            });
            ItemList.Add(new SelectListItem()
            {
                Value = "7",
                Text = "July",
            });
            ItemList.Add(new SelectListItem()
            {
                Value = "8",
                Text = "August",
            });
            ItemList.Add(new SelectListItem()
            {
                Value = "9",
                Text = "September",
            });
            ItemList.Add(new SelectListItem()
            {
                Value = "10",
                Text = "October",
            });
            ItemList.Add(new SelectListItem()
            {
                Value = "11",
                Text = "November",
            });
            ItemList.Add(new SelectListItem()
            {
                Value = "12",
                Text = "December",
            });

            JsonResult j = Json(new SelectList(ItemList, "Value", "Text"), JsonRequestBehavior.AllowGet);
            return j;
        }


            #endregion          
    

    }

}
