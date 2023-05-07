using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

using SyndicateIT.UtilityComponent.Enum;
using SyndicateIT.Model.UtilityModel.Session;
using System.Transactions;
using SyndicateIT.UtilityComponent;
using SyndicateIT.Model.ViewModel.Shared;
using SyndicateIT.Model.ViewModel.SetupManagement.Place;
using SyndicateIT.DataLayer.DataContext;

namespace SyndicateIT.Model.DomainModel.SetupManagement.Place
{
    public class PlaceDomainModel : DomainModelBase
    {

     

            public PlaceContentViewModel GetPlaceContent()
            {
                PlaceContentViewModel model = new PlaceContentViewModel();
                model.Navigation = GetNavigationList("list", "Place");
                model.Title = "Place";
                model.ClassTitle = "fa fa-lg fa-fw fa-inbox";
                model.Alert = new AlertViewModel { HasAlert = false };
                return model;
            }

            public PlaceViewModel GetPlaceViewModel(String Place_ID)
            {
                PlaceViewModel model = new PlaceViewModel();
                string title = string.Empty;
                if (Place_ID != null && Place_ID != "")
                {
                    using (var db = new SyndicatDataEntities())
                    {
                        var etPlace = db.Places.Where(p => p.Place_ID.ToString() == Place_ID).FirstOrDefault();
                        model = Mapper.Map<DataLayer.DataContext.Place, PlaceViewModel>(etPlace);
                    }
                    title = "Edit Place";
                }
                else
                {
                    title = "New Place";
                }
                model.Navigation = GetNavigationList("update", title);
                model.Title = title;
                model.Alert = new AlertViewModel() { HasAlert = false };
                model.ClassTitle = "fa fa-lg fa-fw fa-inbox";
                return model;
            }


            public PlaceViewModel GetPlaceViewModel(PlaceViewModel model)
            {
                string title = string.Empty;
                if (model.Place_ID != null && model.Place_ID > 0)
                {
                    title = "Edit Place";
                }
                else
                {
                    title = "New Place";
                }
                model.Navigation = GetNavigationList("update", title);
                model.Title = title;
                model.Alert = new AlertViewModel() { HasAlert = false };
                model.ClassTitle = "fa fa-lg fa-fw fa-inbox";
                return model;
            }


            public List<PlaceGridViewModel> GetListPlace(PlaceContentViewModel etPlaceContentViewModel)
            {
                string[] messages = new string[2];
                messages[1] = GetListPlaceBuisnessLogic(etPlaceContentViewModel);

                List<PlaceGridViewModel> lst = new List<PlaceGridViewModel>();

                SyndicateIT.UtilityComponent.Encryption.Encrypt(etPlaceContentViewModel.Place_ID.ToString());

                using (var db = new SyndicatDataEntities())
                {


                var listdb = db.USP_GET_Place(etPlaceContentViewModel.Place_ID, etPlaceContentViewModel.Place_Name, etPlaceContentViewModel.Place_Description, etPlaceContentViewModel.Country_ID,etPlaceContentViewModel.Kaddaa_ID, etPlaceContentViewModel.LANGUAGE_ID, etPlaceContentViewModel.IS_ACTIVE, etPlaceContentViewModel.START_ROW, etPlaceContentViewModel.END_ROW, etPlaceContentViewModel.TOP, etPlaceContentViewModel.ENTRY_DATE, etPlaceContentViewModel.ENTRY_USER_ID, etPlaceContentViewModel.MODIFICATION_USER_ID, etPlaceContentViewModel.MODIFICATION_DATE).ToList();
                    List<PlaceGridViewModel> Places = Mapper.Map<List<USP_GET_Place_Result>, List<PlaceGridViewModel>>(listdb);

                    lst = Places;
                };

                return lst;

            }

            public String[] DeletePlace(PlaceViewModel etPlaceViewModel)
            {
                String[] message = new string[2];
                message[1] = DeletePlaceBuisnessLogic(etPlaceViewModel);
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
                            var etPlaceByLanguage = db.Place_BY_LANGUAGE.Where(p => p.Place_ID == etPlaceViewModel.Place_ID).ToList();
                            if (etPlaceByLanguage.Count > 0)
                            {
                                db.Place_BY_LANGUAGE.RemoveRange(etPlaceByLanguage);
                            }
                            db.SaveChanges();
                            var etPlace = db.Places.Where(p => p.Place_ID == etPlaceViewModel.Place_ID).FirstOrDefault();
                            if (etPlace != null)
                            {
                                db.Places.Remove(etPlace);
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

            public string[] SavePlace(PlaceViewModel etPlaceViewModel)
            {
                string[] messages = new string[2];
                messages[1] = SavePlaceBuisnessLogic(etPlaceViewModel);
                if (messages[1] != string.Empty)
                {
                    messages[0] = "error";
                    return messages;
                }


                DataLayer.DataContext.Place etPlace = new DataLayer.DataContext.Place();
                Place_BY_LANGUAGE etPlaceByLanguageDb = new DataLayer.DataContext.Place_BY_LANGUAGE();
                using (TransactionScope scope = new TransactionScope())
                {
                    try
                    {
                        using (var db = new SyndicatDataEntities())
                        {

                            if (etPlaceViewModel.Place_ID != 0 && etPlaceViewModel.Place_ID.ToString() != "")
                            {   // Update Table and Default row in Table_By_language
                                if (SessionContent.Container.Login.UserID.ToString() != string.Empty)
                                {
                                    // Set the entry user id and the modification user id.

                                    etPlaceViewModel.ENTRY_USER_ID = etPlaceViewModel.MODIFICATION_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());

                                }
                                var etPlacedb = db.Places.Where(p => p.Place_ID.ToString() == etPlaceViewModel.Place_ID.ToString()).FirstOrDefault();
                                etPlace = Mapper.Map<PlaceViewModel, DataLayer.DataContext.Place>(etPlaceViewModel);

                                if (etPlace != null)
                                {



                                    if (SessionContent.Container.Login.UserID.ToString() != string.Empty)
                                    {
                                        etPlaceViewModel.MODIFICATION_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());
                                    }

                                    string[] properties = new string[12];

                                    int count = -1;

                                

                                    if (etPlaceViewModel.Place_Description != null)
                                    {
                                        count++;
                                        properties[count] = "Place_Description";
                                    }

                                if (etPlaceViewModel.Place_Name != null)
                                {
                                    count++;
                                    properties[count] = "Place_Name";
                                }

                                if (etPlaceViewModel.Kaddaa_ID != null)
                                {
                                    count++;
                                    properties[count] = "Kaddaa_ID";
                                }

                                if (etPlaceViewModel.Country_ID != null)
                                {
                                    count++;
                                    properties[count] = "Country_ID";
                                }

                                if (etPlaceViewModel.Region_ID != null)
                                {
                                    count++;
                                    properties[count] = "Region_ID";
                                }


                                count++;
                                    properties[count] = "IS_ACTIVE";

                                    count++;
                                    properties[count] = "MODIFICATION_USER_ID";
                                    count++;
                                    properties[count] = "MODIFICATION_DATE";


                                    UtilityComponent.Utilities.MergeObject(etPlacedb, etPlace, true,
                                          properties
                                         );
                                    etPlacedb.MODIFICATION_DATE = DateTimeOffset.Now;

                                    etPlaceByLanguageDb = db.Place_BY_LANGUAGE.Where(p => p.Place_ID == etPlace.Place_ID && p.Language_ID == 1).FirstOrDefault();
                                    Place_BY_LANGUAGE etPlaceByLanguage = new Place_BY_LANGUAGE();
                                    etPlaceByLanguage = etPlaceByLanguageDb;
                                    etPlaceByLanguage.Place_Name = etPlace.Place_Name;
                                    etPlaceByLanguage.IS_ACTIVE = etPlace.IS_ACTIVE;
                                    UtilityComponent.Utilities.MergeObject(etPlaceByLanguageDb, etPlaceByLanguage, true,
                                                                          properties
                                                                         );
                                    db.SaveChanges();
                                }
                            }
                            else
                            {
                                //Insert new row to Table and Table_By_Language

                                // Need to add ENTRY_USER_ID 
                                etPlace = Mapper.Map<PlaceViewModel, DataLayer.DataContext.Place>(etPlaceViewModel);

                                etPlace.ENTRY_DATE = DateTimeOffset.Now;

                                if (SessionContent.Container.Login.UserID.ToString() != string.Empty)
                                {
                                    etPlace.ENTRY_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());
                                }

                                db.Places.Add(etPlace);
                                db.SaveChanges();
                                Place_BY_LANGUAGE erTranslation = new Place_BY_LANGUAGE();
                                erTranslation.Place_Name = etPlace.Place_Name;
                                erTranslation.Place_ID = etPlace.Place_ID;
                                erTranslation.Language_ID = (int)Languages.English;
                                erTranslation.ENTRY_DATE = DateTime.Now;
                                erTranslation.ENTRY_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());
                                erTranslation.IS_ACTIVE = etPlace.IS_ACTIVE;
                                db.Place_BY_LANGUAGE.Add(erTranslation);
                                db.SaveChanges();

                            }

                            messages[0] = "s";
                            messages[1] = etPlace.Place_ID.ToString();
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

            public string DeletePlaceBuisnessLogic(PlaceViewModel etPlace)
            {
                string message = string.Empty;
                if (etPlace.Place_ID == -1)
                {
                    message = "there is no record";
                }
                return message;
            }

            public string GetListPlaceBuisnessLogic(PlaceContentViewModel etPlaceContentViewModel)
            {
                string message = string.Empty;
                return message;
            }

            public string SavePlaceBuisnessLogic(PlaceViewModel model)
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
                    model.Add(new NavigationViewModel() { NavigationName = "Place", NavigationUrl = Utilities.GetUrl("Index", "Place", SessionContent.Current.Corporate.IsSecure) });
                    model.Add(new NavigationViewModel() { NavigationName = title });
                }

                return model;
            }

            public override void InitializeMapper()
            {
                #region Database To View

                Mapper.CreateMap<DataLayer.DataContext.Place, PlaceViewModel>()
                      .IgnoreAllNonExisting();

                Mapper.CreateMap<USP_GET_Place_Result , PlaceGridViewModel>()
                     .IgnoreAllNonExisting();

                #endregion

                #region view to database

                Mapper.CreateMap<PlaceViewModel, DataLayer.DataContext.Place>()
                      .IgnoreAllNonExisting();
                Mapper.CreateMap<PlaceGridViewModel, USP_GET_Place_Result>()
                   .IgnoreAllNonExisting();

                #endregion
                base.InitializeMapper();
            }
            #endregion
        }
    
}
