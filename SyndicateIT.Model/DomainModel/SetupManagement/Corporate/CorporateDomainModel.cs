using AutoMapper;
using SyndicateIT.DataLayer.DataContext;
using SyndicateIT.Model.UtilityModel.Session;
using SyndicateIT.Model.ViewModel.SetupManagement.Corporate;
using SyndicateIT.Model.ViewModel.Shared;
using SyndicateIT.UtilityComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace SyndicateIT.Model.DomainModel.SetupManagement.Corporate
{
    public class CorporateDomainModel : DomainModelBase
    {

        public CorporateContentViewModel GetCorporateContent()
        {
            CorporateContentViewModel model = new CorporateContentViewModel();
            model.Navigation = GetNavigationList("list", "Corporate");
            model.Title = "Corporate";
            model.ClassTitle = "fa fa-lg fa-fw fa-inbox";
            model.Alert = new AlertViewModel { HasAlert = false };
            return model;
        }

        public CorporateViewModel GetCorporateViewModel(String ID)
        {
            CorporateViewModel model = new CorporateViewModel();
            string title = string.Empty;
            if (ID != null && ID != "")
            {
                using (var db = new SyndicatDataEntities())
                {
                    var etCorporate = db.Corporates.Where(p => p.ID.ToString() == ID).FirstOrDefault();
                    model = Mapper.Map<DataLayer.DataContext.Corporate, CorporateViewModel>(etCorporate);
                }
                title = "Edit Corporate";
            }
            else
            {
                title = "New Corporate";
            }
            model.Navigation = GetNavigationList("update", title);
            model.Title = title;
            model.Alert = new AlertViewModel() { HasAlert = false };
            model.ClassTitle = "fa fa-lg fa-fw fa-inbox";
            return model;
        }


        public CorporateViewModel GetCorporateViewModel(CorporateViewModel model)
        {
            string title = string.Empty;
            if (model.ID != null && model.ID > 0)
            {
                title = "Edit Corporate";
            }
            else
            {
                title = "New Corporate";
            }
            model.Navigation = GetNavigationList("update", title);
            model.Title = title;
            model.Alert = new AlertViewModel() { HasAlert = false };
            model.ClassTitle = "fa fa-lg fa-fw fa-inbox";
            return model;
        }


        public List<CorporateGridViewModel> GetListCorporate(CorporateContentViewModel etCorporateContentViewModel)
        {
            string[] messages = new string[2];
            messages[1] = GetListCorporateBuisnessLogic(etCorporateContentViewModel);

            List<CorporateGridViewModel> lst = new List<CorporateGridViewModel>();

            SyndicateIT.UtilityComponent.Encryption.Encrypt(etCorporateContentViewModel.ID.ToString());

            using (var db = new SyndicatDataEntities())
            {


                var listdb = db.USP_GET_Corporate(etCorporateContentViewModel.ID, etCorporateContentViewModel.Name, etCorporateContentViewModel.LogoName, etCorporateContentViewModel.CountryCode, etCorporateContentViewModel.PhoneCode, etCorporateContentViewModel.CurrencyCode, etCorporateContentViewModel.Domain, etCorporateContentViewModel.CountryName, etCorporateContentViewModel.CustomerServicePhone, etCorporateContentViewModel.CustomerServiceEmail, etCorporateContentViewModel.IsSecure, etCorporateContentViewModel.MaxLengthCIF, etCorporateContentViewModel.MaxLengthMobile, etCorporateContentViewModel.LANGUAGE_ID, etCorporateContentViewModel.BankID, etCorporateContentViewModel.IS_ACTIVE, etCorporateContentViewModel.START_ROW, etCorporateContentViewModel.END_ROW, etCorporateContentViewModel.TOP, etCorporateContentViewModel.ENTRY_DATE, etCorporateContentViewModel.ENTRY_USER_ID, etCorporateContentViewModel.MODIFICATION_USER_ID, etCorporateContentViewModel.MODIFICATION_DATE).ToList();

                List<CorporateGridViewModel> Corporates = Mapper.Map<List<USP_GET_Corporate_Result>, List<CorporateGridViewModel>>(listdb);

                lst = Corporates;
            };

            return lst;

        }

        public String[] DeleteCorporate(CorporateViewModel etCorporateViewModel)
        {
            String[] message = new string[2];
            message[1] = DeleteCorporateBuisnessLogic(etCorporateViewModel);
            if (message[1] != string.Empty)
            {
                message[0] = "e";
                return message;
            }
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (var db = new SyndicatDataEntities())
                    {
                       
                        var etCorporate = db.Corporates.Where(p => p.ID == etCorporateViewModel.ID).FirstOrDefault();
                        if (etCorporate != null)
                        {
                            db.Corporates.Remove(etCorporate);
                            db.SaveChanges();
                            scope.Complete();
                            message[0] = "s";
                        }
                        else
                        {
                            message[0] = "e";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                message[0] = ex.ToString();
                return message;
            }
            return message;
        }

        public string[] SaveCorporate(CorporateViewModel etCorporateViewModel)
        {
            string[] messages = new string[2];
            messages[1] = SaveCorporateBuisnessLogic(etCorporateViewModel);
            if (messages[1] != string.Empty)
            {
                messages[0] = "error";
                return messages;
            }


            DataLayer.DataContext.Corporate etCorporate = new DataLayer.DataContext.Corporate();
           
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (var db = new SyndicatDataEntities())
                    {

                        if (etCorporateViewModel.ID != 0 && etCorporateViewModel.ID.ToString() != "")
                        {   // Update Table and Default row in Table_By_language
                            if (SessionContent.Container.Login.UserID.ToString() != string.Empty)
                            {
                                // Set the entry user id and the modification user id.

                                etCorporateViewModel.ENTRY_USER_ID = etCorporateViewModel.MODIFICATION_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());

                            }
                            var etCorporatedb = db.Corporates.Where(p => p.ID.ToString() == etCorporateViewModel.ID.ToString()).FirstOrDefault();
                            etCorporate = Mapper.Map<CorporateViewModel, DataLayer.DataContext.Corporate>(etCorporateViewModel);

                            if (etCorporate != null)
                            {



                                if (SessionContent.Container.Login.UserID.ToString() != string.Empty)
                                {
                                    etCorporateViewModel.MODIFICATION_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());
                                }

                                string[] properties = new string[20];

                                int count = -1;

                                if (etCorporateViewModel.Name != null)
                                {
                                    count++;
                                    properties[count] = "Name";
                                }
                                if (etCorporateViewModel.LogoName != null)
                                {
                                    count++;
                                    properties[count] = "LogoName";
                                }
                                if (etCorporateViewModel.CountryCode != null)
                                {
                                    count++;
                                    properties[count] = "CountryCode";
                                }
                                if (etCorporateViewModel.PhoneCode != null)
                                {
                                    count++;
                                    properties[count] = "PhoneCode";
                                }
                                if (etCorporateViewModel.CurrencyCode != null)
                                {
                                    count++;
                                    properties[count] = "CurrencyCode";
                                }
                                if (etCorporateViewModel.Domain != null)
                                {
                                    count++;
                                    properties[count] = "Domain";
                                }
                                if (etCorporateViewModel.CountryName != null)
                                {
                                    count++;
                                    properties[count] = "CountryName";
                                }
                                if (etCorporateViewModel.CustomerServicePhone != null)
                                {
                                    count++;
                                    properties[count] = "CustomerServicePhone";
                                }
                                if (etCorporateViewModel.CustomerServiceEmail != null)
                                {
                                    count++;
                                    properties[count] = "CustomerServiceEmail";
                                }
                                if (etCorporateViewModel.IsSecure != null)
                                {
                                    count++;
                                    properties[count] = "IsSecure";
                                }
                                if (etCorporateViewModel.MaxLengthCIF != null)
                                {
                                    count++;
                                    properties[count] = "MaxLengthCIF";
                                }

                                if (etCorporateViewModel.MaxLengthMobile != null)
                                {
                                    count++;
                                    properties[count] = "MaxLengthMobile";
                                }
                                if (etCorporateViewModel.BankID != null)
                                {
                                    count++;
                                    properties[count] = "BankID";
                                }

                                count++;
                                properties[count] = "IS_ACTIVE";

                                count++;
                                properties[count] = "MODIFICATION_USER_ID";
                                count++;
                                properties[count] = "MODIFICATION_DATE";


                                UtilityComponent.Utilities.MergeObject(etCorporatedb, etCorporate, true,
                                      properties
                                     );
                                etCorporatedb.MODIFICATION_DATE = DateTimeOffset.Now;

                          
                                db.SaveChanges();
                            }
                        }
                        else
                        {
                            //Insert new row to Table and Table_By_Language

                            // Need to add ENTRY_USER_ID 
                            etCorporate = Mapper.Map<CorporateViewModel, DataLayer.DataContext.Corporate>(etCorporateViewModel);

                            etCorporate.ENTRY_DATE = DateTimeOffset.Now;

                            if (SessionContent.Container.Login.UserID.ToString() != string.Empty)
                            {
                                etCorporate.ENTRY_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());
                            }

                            db.Corporates.Add(etCorporate);
                            db.SaveChanges();
                            

                        }

                        messages[0] = "s";
                        messages[1] = etCorporate.ID.ToString();
                        //   savedSuccessfully = true;
                        scope.Complete();

                    }
                }

                catch (Exception ex)
                {
                    messages[0] = "e";
                    messages[1] = ex.Message;
                }

            }
            return messages;

        }

        public string DeleteCorporateBuisnessLogic(CorporateViewModel etCorporate)
        {
            string message = string.Empty;
            if (etCorporate.ID == -1)
            {
                message = "there is no record";
            }
            return message;
        }

        public string GetListCorporateBuisnessLogic(CorporateContentViewModel etCorporateContentViewModel)
        {
            string message = string.Empty;
            return message;
        }

        public string SaveCorporateBuisnessLogic(CorporateViewModel model)
        {
            string message = string.Empty;
            return message;
        }

        #region Private Methods

        public List<NavigationViewModel> GetNavigationList(string type, string title)
        {
            var model = new List<NavigationViewModel>();

            if (type == "list")
            {
                model.Add(new NavigationViewModel() { NavigationName = "Setup", NavigationUrl = "" });
                model.Add(new NavigationViewModel() { NavigationName = title });
            }
            else if (type == "update")
            {
                model.Add(new NavigationViewModel() { NavigationName = "Setup", NavigationUrl = "" });
                model.Add(new NavigationViewModel() { NavigationName = "Corporate", NavigationUrl = Utilities.GetUrl("Corporate", "Corporate", SessionContent.Current.Corporate.IsSecure) });
                model.Add(new NavigationViewModel() { NavigationName = title });
            }

            return model;
        }

        public override void InitializeMapper()
        {
            #region Database To View

            Mapper.CreateMap<DataLayer.DataContext.Corporate, CorporateViewModel>()
                  .IgnoreAllNonExisting();

            Mapper.CreateMap<USP_GET_Corporate_Result, CorporateGridViewModel>()
                 .IgnoreAllNonExisting();

            #endregion

            #region view to database

            Mapper.CreateMap<CorporateViewModel, DataLayer.DataContext.Corporate>()
                  .IgnoreAllNonExisting();
            Mapper.CreateMap<CorporateGridViewModel, USP_GET_Corporate_Result>()
               .IgnoreAllNonExisting();

            #endregion
            base.InitializeMapper();
        }
        #endregion

    }
}
