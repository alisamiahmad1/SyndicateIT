using SyndicateIT.Model.ViewModel.SetupManagement.Region;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SyndicateIT.DataLayer.DataContext;
using AutoMapper;

using SyndicateIT.UtilityComponent.Enum;
using SyndicateIT.Model.UtilityModel.Session;
using System.Transactions;
using SyndicateIT.UtilityComponent;
using SyndicateIT.Model.ViewModel.Shared;

namespace SyndicateIT.Model.DomainModel.SetupManagement.Region
{
    public class RegionDomainModel : DomainModelBase
    {

        public RegionContentViewModel GetRegionContent()
        {
            RegionContentViewModel model = new RegionContentViewModel();
            model.Navigation = GetNavigationList("list", "Region");
            model.Title = "Region";
            model.ClassTitle = "fa fa-lg fa-fw fa-inbox";
            model.Alert = new AlertViewModel { HasAlert = false };
            return model;
        }

        public RegionViewModel GetRegionViewModel(String Region_ID)
        {
            RegionViewModel model = new RegionViewModel();
            string title = string.Empty;
            if (Region_ID != null && Region_ID != "")
            {
                using (var db = new SyndicatDataEntities())
                {
                    var etRegion = db.Regions.Where(p => p.Region_ID.ToString() == Region_ID).FirstOrDefault();
                    model = Mapper.Map<DataLayer.DataContext.Region, RegionViewModel>(etRegion);
                }
                title = "Edit Region";
            }
            else
            {
                title = "New Region";
            }
            model.Navigation = GetNavigationList("update", title);
            model.Title = title;
            model.Alert = new AlertViewModel() { HasAlert = false };
            model.ClassTitle = "fa fa-lg fa-fw fa-inbox";
            return model;
        }


        public RegionViewModel GetRegionViewModel(RegionViewModel model)
        {
            string title = string.Empty;
            if (model.Region_ID != null && model.Region_ID > 0)
            {
                title = "Edit Region";
            }
            else
            {
                title = "New Region";
            }
            model.Navigation = GetNavigationList("update", title);
            model.Title = title;
            model.Alert = new AlertViewModel() { HasAlert = false };
            model.ClassTitle = "fa fa-lg fa-fw fa-inbox";
            return model;
        }


        public List<RegionGridViewModel> GetListRegion(RegionContentViewModel etRegionContentViewModel)
        {
            string[] messages = new string[2];
            messages[1] = GetListRegionBuisnessLogic(etRegionContentViewModel);

            List<RegionGridViewModel> lst = new List<RegionGridViewModel>();

            SyndicateIT.UtilityComponent.Encryption.Encrypt(etRegionContentViewModel.Region_ID.ToString());

            using (var db = new SyndicatDataEntities())
            {


                var listdb = db.USP_GET_Region(etRegionContentViewModel.Region_ID, etRegionContentViewModel.Region_Name, etRegionContentViewModel.Region_Description, etRegionContentViewModel.Country_ID, etRegionContentViewModel.LANGUAGE_ID, etRegionContentViewModel.IS_ACTIVE, etRegionContentViewModel.START_ROW, etRegionContentViewModel.END_ROW, etRegionContentViewModel.TOP, etRegionContentViewModel.ENTRY_DATE, etRegionContentViewModel.ENTRY_USER_ID, etRegionContentViewModel.MODIFICATION_USER_ID, etRegionContentViewModel.Modification_Date).ToList();

                List<RegionGridViewModel> Regions = Mapper.Map<List<USP_GET_Region_Result>, List<RegionGridViewModel>>(listdb);

                lst = Regions;
            };

            return lst;

        }

        public String[] DeleteRegion(RegionViewModel etRegionViewModel)
        {
            String[] message = new string[2];
            message[1] = DeleteRegionBuisnessLogic(etRegionViewModel);
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
                        var etRegionByLanguage = db.Region_BY_LANGUAGE.Where(p => p.Region_ID == etRegionViewModel.Region_ID).ToList();
                        if (etRegionByLanguage.Count > 0)
                        {
                            db.Region_BY_LANGUAGE.RemoveRange(etRegionByLanguage);
                        }
                        db.SaveChanges();
                        var etRegion = db.Regions.Where(p => p.Region_ID == etRegionViewModel.Region_ID).FirstOrDefault();
                        if (etRegion != null)
                        {
                            db.Regions.Remove(etRegion);
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

        public string[] SaveRegion(RegionViewModel etRegionViewModel)
        {
            string[] messages = new string[2];
            messages[1] = SaveRegionBuisnessLogic(etRegionViewModel);
            if (messages[1] != string.Empty)
            {
                messages[0] = "error";
                return messages;
            }


            DataLayer.DataContext.Region etRegion = new DataLayer.DataContext.Region();
            Region_BY_LANGUAGE etRegionByLanguageDb = new Region_BY_LANGUAGE();
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (var db = new SyndicatDataEntities())
                    {

                        if (etRegionViewModel.Region_ID != 0 && etRegionViewModel.Region_ID.ToString() != "")
                        {   // Update Table and Default row in Table_By_language
                            if (SessionContent.Container.Login.UserID.ToString() != string.Empty)
                            {
                                // Set the entry user id and the modification user id.

                                etRegionViewModel.ENTRY_USER_ID = etRegionViewModel.MODIFICATION_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());

                            }
                            var etRegiondb = db.Regions.Where(p => p.Region_ID.ToString() == etRegionViewModel.Region_ID.ToString()).FirstOrDefault();
                            etRegion = Mapper.Map<RegionViewModel, DataLayer.DataContext.Region>(etRegionViewModel);

                            if (etRegion != null)
                            {



                                if (SessionContent.Container.Login.UserID.ToString() != string.Empty)
                                {
                                    etRegionViewModel.MODIFICATION_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());
                                }

                                string[] properties = new string[8];

                                int count = -1;

                                if (etRegionViewModel.Region_Name != null)
                                {
                                    count++;
                                    properties[count] = "Region_Name";
                                }
                                if (etRegionViewModel.Region_Description != null)
                                {
                                    count++;
                                    properties[count] = "Region_Description";
                                }
                                if (etRegionViewModel.Country_ID != null)
                                {
                                    count++;
                                    properties[count] = "Country_ID";
                                }

                                count++;
                                properties[count] = "IS_ACTIVE";

                                count++;
                                properties[count] = "MODIFICATION_USER_ID";
                                count++;
                                properties[count] = "MODIFICATION_DATE";


                                UtilityComponent.Utilities.MergeObject(etRegiondb, etRegion, true,
                                      properties
                                     );
                                etRegiondb.MODIFICATION_DATE = DateTimeOffset.Now;

                                etRegionByLanguageDb = db.Region_BY_LANGUAGE.Where(p => p.Region_ID == etRegion.Region_ID && p.Language_ID == 1).FirstOrDefault();
                                Region_BY_LANGUAGE etRegionByLanguage = new Region_BY_LANGUAGE();
                                etRegionByLanguage = etRegionByLanguageDb;
                                etRegionByLanguage.Region_Name = etRegion.Region_Name;
                                etRegionByLanguage.IS_ACTIVE = etRegion.IS_ACTIVE;
                                UtilityComponent.Utilities.MergeObject(etRegionByLanguageDb, etRegionByLanguage, true,
                                                                      properties
                                                                     );
                                db.SaveChanges();
                            }
                        }
                        else
                        {
                            //Insert new row to Table and Table_By_Language

                            // Need to add ENTRY_USER_ID 
                            etRegion = Mapper.Map<RegionViewModel, DataLayer.DataContext.Region>(etRegionViewModel);

                            etRegion.ENTRY_DATE = DateTimeOffset.Now;

                            if (SessionContent.Container.Login.UserID.ToString() != string.Empty)
                            {
                                etRegion.ENTRY_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());
                            }

                            db.Regions.Add(etRegion);
                            db.SaveChanges();
                            Region_BY_LANGUAGE erTranslation = new Region_BY_LANGUAGE();
                            erTranslation.Region_Name = etRegion.Region_Name;
                            erTranslation.Region_ID = etRegion.Region_ID;
                            erTranslation.Language_ID = (int)Languages.English;
                            erTranslation.ENTRY_DATE = DateTime.Now;
                            erTranslation.ENTRY_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());
                            erTranslation.IS_ACTIVE = etRegion.IS_ACTIVE;
                            db.Region_BY_LANGUAGE.Add(erTranslation);
                            db.SaveChanges();

                        }

                        messages[0] = "s";
                        messages[1] = etRegion.Region_ID.ToString();
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

        public string DeleteRegionBuisnessLogic(RegionViewModel etRegion)
        {
            string message = string.Empty;
            if (etRegion.Region_ID == -1)
            {
                message = "there is no record";
            }
            return message;
        }

        public string GetListRegionBuisnessLogic(RegionContentViewModel etRegionContentViewModel)
        {
            string message = string.Empty;
            return message;
        }

        public string SaveRegionBuisnessLogic(RegionViewModel model)
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
                model.Add(new NavigationViewModel() { NavigationName = "Region", NavigationUrl = Utilities.GetUrl("index", "Region", SessionContent.Current.Corporate.IsSecure) });
                model.Add(new NavigationViewModel() { NavigationName = title });
            }

            return model;
        }

        public override void InitializeMapper()
        {
            #region Database To View

            Mapper.CreateMap<DataLayer.DataContext.Region, RegionViewModel>()
                  .IgnoreAllNonExisting();

            Mapper.CreateMap<USP_GET_Region_Result, RegionGridViewModel>()
                 .IgnoreAllNonExisting();

            #endregion

            #region view to database

            Mapper.CreateMap<RegionViewModel, DataLayer.DataContext.Region>()
                  .IgnoreAllNonExisting();
            Mapper.CreateMap<RegionGridViewModel, USP_GET_Region_Result>()
               .IgnoreAllNonExisting();

            #endregion
            base.InitializeMapper();
        }
        #endregion

    }
}
