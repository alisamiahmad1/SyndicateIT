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
using SyndicateIT.Model.ViewModel.SetupManagement.PhoneType;
using SyndicateIT.DataLayer.DataContext;

namespace SyndicateIT.Model.DomainModel.SetupManagement.PhoneType
{
    public class PhoneTypeDomainModel : DomainModelBase
    {

     
            public PhoneTypeContentViewModel GetPhoneTypeContent()
            {
                PhoneTypeContentViewModel model = new PhoneTypeContentViewModel();
                model.Navigation = GetNavigationList("list", "Phone Type");
                model.Title = "Phone Type";
                model.ClassTitle = "fa fa-lg fa-fw fa-inbox";
                model.Alert = new AlertViewModel { HasAlert = false };
                return model;
            }

            public PhoneTypeViewModel GetPhoneTypeViewModel(String PhoneType_ID)
            {
                PhoneTypeViewModel model = new PhoneTypeViewModel();
                string title = string.Empty;
                if (PhoneType_ID != null && PhoneType_ID != "")
                {
                    using (var db = new SyndicatDataEntities())
                    {
                        var etPhoneType = db.Phone_Type.Where(p => p.Phone_Type_ID.ToString() == PhoneType_ID).FirstOrDefault();
                        model = Mapper.Map<Phone_Type, PhoneTypeViewModel>(etPhoneType);
                    }
                    title = "Edit Phone Type";
                }
                else
                {
                    title = "New Phone Type";
                }
                model.Navigation = GetNavigationList("update", title);
                model.Title = title;
                model.Alert = new AlertViewModel() { HasAlert = false };
                model.ClassTitle = "fa fa-lg fa-fw fa-inbox";
                return model;
            }


            public PhoneTypeViewModel GetPhoneTypeViewModel(PhoneTypeViewModel model)
            {
                string title = string.Empty;
                if (model.Phone_Type_ID != null && model.Phone_Type_ID > 0)
                {
                    title = "Edit Phone Type";
                }
                else
                {
                    title = "New Phone Type";
                }
                model.Navigation = GetNavigationList("update", title);
                model.Title = title;
                model.Alert = new AlertViewModel() { HasAlert = false };
                model.ClassTitle = "fa fa-lg fa-fw fa-inbox";
                return model;
            }


            public List<PhoneTypeGridViewModel> GetListPhoneType(PhoneTypeContentViewModel etPhoneTypeContentViewModel)
            {
                string[] messages = new string[2];
                messages[1] = GetListPhoneTypeBuisnessLogic(etPhoneTypeContentViewModel);

                List<PhoneTypeGridViewModel> lst = new List<PhoneTypeGridViewModel>();

                SyndicateIT.UtilityComponent.Encryption.Encrypt(etPhoneTypeContentViewModel.Phone_Type_ID.ToString());

                using (var db = new SyndicatDataEntities())
                {


                    var listdb = db.USP_GET_Phone_Type(etPhoneTypeContentViewModel.Phone_Type_ID, etPhoneTypeContentViewModel.Phone_Type_Title, etPhoneTypeContentViewModel.Phone_Type_Description, etPhoneTypeContentViewModel.LANGUAGE_ID, etPhoneTypeContentViewModel.IS_ACTIVE, etPhoneTypeContentViewModel.START_ROW, etPhoneTypeContentViewModel.END_ROW, etPhoneTypeContentViewModel.TOP, etPhoneTypeContentViewModel.ENTRY_DATE, etPhoneTypeContentViewModel.ENTRY_USER_ID, etPhoneTypeContentViewModel.MODIFICATION_USER_ID, etPhoneTypeContentViewModel.MODIFICATION_DATE).ToList();

                    List<PhoneTypeGridViewModel> PhoneTypes = Mapper.Map<List<USP_GET_Phone_Type_Result>, List<PhoneTypeGridViewModel>>(listdb);

                    lst = PhoneTypes;
                };

                return lst;

            }

            public String[] DeletePhoneType(PhoneTypeViewModel etPhoneTypeViewModel)
            {
                String[] message = new string[2];
                message[1] = DeletePhoneTypeBuisnessLogic(etPhoneTypeViewModel);
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
                            var etPhoneTypeByLanguage = db.Phone_Type_BY_LANGUAGE.Where(p => p.Phone_Type_ID == etPhoneTypeViewModel.Phone_Type_ID).ToList();
                            if (etPhoneTypeByLanguage.Count > 0)
                            {
                                db.Phone_Type_BY_LANGUAGE.RemoveRange(etPhoneTypeByLanguage);
                            }
                            db.SaveChanges();
                            var etPhoneType = db.Phone_Type.Where(p => p.Phone_Type_ID == etPhoneTypeViewModel.Phone_Type_ID).FirstOrDefault();
                            if (etPhoneType != null)
                            {
                                db.Phone_Type.Remove(etPhoneType);
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

            public string[] SavePhoneType(PhoneTypeViewModel etPhoneTypeViewModel)
            {
                string[] messages = new string[2];
                messages[1] = SavePhoneTypeBuisnessLogic(etPhoneTypeViewModel);
                if (messages[1] != string.Empty)
                {
                    messages[0] = "error";
                    return messages;
                }


                Phone_Type etPhoneType = new Phone_Type();
                Phone_Type_BY_LANGUAGE etPhoneTypeByLanguageDb = new Phone_Type_BY_LANGUAGE();
                using (TransactionScope scope = new TransactionScope())
                {
                    try
                    {
                        using (var db = new SyndicatDataEntities())
                        {

                            if (etPhoneTypeViewModel.Phone_Type_ID != 0 && etPhoneTypeViewModel.Phone_Type_ID.ToString() != "")
                            {   // Update Table and Default row in Table_By_language
                                if (SessionContent.Container.Login.UserID.ToString() != string.Empty)
                                {
                                    // Set the entry user id and the modification user id.

                                    etPhoneTypeViewModel.ENTRY_USER_ID = etPhoneTypeViewModel.MODIFICATION_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());

                                }
                                var etPhoneTypedb = db.Phone_Type.Where(p => p.Phone_Type_ID.ToString() == etPhoneTypeViewModel.Phone_Type_ID.ToString()).FirstOrDefault();
                                etPhoneType = Mapper.Map<PhoneTypeViewModel, Phone_Type>(etPhoneTypeViewModel);

                                if (etPhoneType != null)
                                {



                                    if (SessionContent.Container.Login.UserID.ToString() != string.Empty)
                                    {
                                        etPhoneTypeViewModel.MODIFICATION_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());
                                    }

                                    string[] properties = new string[8];

                                    int count = -1;

                                    if (etPhoneTypeViewModel.Phone_Type_Title != null)
                                    {
                                        count++;
                                        properties[count] = "Phone_Type_Title";
                                    }

                                    if (etPhoneTypeViewModel.Phone_Type_Description != null)
                                    {
                                        count++;
                                        properties[count] = "Phone_Type_Description";
                                    }

                                    count++;
                                    properties[count] = "IS_ACTIVE";

                                    count++;
                                    properties[count] = "MODIFICATION_USER_ID";
                                    count++;
                                    properties[count] = "MODIFICATION_DATE";


                                    UtilityComponent.Utilities.MergeObject(etPhoneTypedb, etPhoneType, true,
                                          properties
                                         );
                                    etPhoneTypedb.MODIFICATION_DATE = DateTimeOffset.Now;

                                    etPhoneTypeByLanguageDb = db.Phone_Type_BY_LANGUAGE.Where(p => p.Phone_Type_ID == etPhoneType.Phone_Type_ID && p.LANGUAGE_ID == 1).FirstOrDefault();
                                    Phone_Type_BY_LANGUAGE etPhoneTypeByLanguage = new Phone_Type_BY_LANGUAGE();
                                    etPhoneTypeByLanguage = etPhoneTypeByLanguageDb;
                                    etPhoneTypeByLanguage.Phone_Type_NAME = etPhoneType.Phone_Type_Title;
                                    etPhoneTypeByLanguage.IS_ACTIVE = etPhoneType.IS_ACTIVE;
                                    UtilityComponent.Utilities.MergeObject(etPhoneTypeByLanguageDb, etPhoneTypeByLanguage, true,
                                                                          properties
                                                                         );
                                    db.SaveChanges();
                                }
                            }
                            else
                            {
                                //Insert new row to Table and Table_By_Language

                                // Need to add ENTRY_USER_ID 
                                etPhoneType = Mapper.Map<PhoneTypeViewModel, Phone_Type>(etPhoneTypeViewModel);

                                etPhoneType.ENTRY_DATE = DateTimeOffset.Now;

                                if (SessionContent.Container.Login.UserID.ToString() != string.Empty)
                                {
                                    etPhoneType.ENTRY_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());
                                }

                                db.Phone_Type.Add(etPhoneType);
                                db.SaveChanges();
                                Phone_Type_BY_LANGUAGE erTranslation = new Phone_Type_BY_LANGUAGE();
                                erTranslation.Phone_Type_NAME = etPhoneType.Phone_Type_Title;
                                erTranslation.Phone_Type_ID = etPhoneType.Phone_Type_ID;
                                erTranslation.LANGUAGE_ID = (int)Languages.English;
                                erTranslation.ENTRY_DATE = DateTime.Now;
                                erTranslation.ENTRY_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());
                                erTranslation.IS_ACTIVE = etPhoneType.IS_ACTIVE;
                                db.Phone_Type_BY_LANGUAGE.Add(erTranslation);
                                db.SaveChanges();

                            }

                            messages[0] = "s";
                            messages[1] = etPhoneType.Phone_Type_ID.ToString();
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

            public string DeletePhoneTypeBuisnessLogic(PhoneTypeViewModel etPhoneType)
            {
                string message = string.Empty;
                if (etPhoneType.Phone_Type_ID == -1)
                {
                    message = "there is no record";
                }
                return message;
            }

            public string GetListPhoneTypeBuisnessLogic(PhoneTypeContentViewModel etPhoneTypeContentViewModel)
            {
                string message = string.Empty;
                return message;
            }

            public string SavePhoneTypeBuisnessLogic(PhoneTypeViewModel model)
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
                    model.Add(new NavigationViewModel() { NavigationName = "Phone Type", NavigationUrl = Utilities.GetUrl("Index", "Phone Type", SessionContent.Current.Corporate.IsSecure) });
                    model.Add(new NavigationViewModel() { NavigationName = title });
                }

                return model;
            }

            public override void InitializeMapper()
            {
                #region Database To View

                Mapper.CreateMap<Phone_Type, PhoneTypeViewModel>()
                      .IgnoreAllNonExisting();

                Mapper.CreateMap<USP_GET_Phone_Type_Result, PhoneTypeGridViewModel>()
                     .IgnoreAllNonExisting();

                #endregion

                #region view to database

                Mapper.CreateMap<PhoneTypeViewModel, Phone_Type>()
                      .IgnoreAllNonExisting();
                Mapper.CreateMap<PhoneTypeGridViewModel, USP_GET_Phone_Type_Result>()
                   .IgnoreAllNonExisting();

                #endregion
                base.InitializeMapper();
            }
            #endregion
        }
    
}
