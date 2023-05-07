using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SyndicateIT.Model.ViewModel.SetupManagement.Shift;
using SyndicateIT.DataLayer.DataContext;
using AutoMapper;

using SyndicateIT.UtilityComponent.Enum;
using SyndicateIT.Model.UtilityModel.Session;
using System.Transactions;
using SyndicateIT.UtilityComponent;
using SyndicateIT.Model.ViewModel.Shared;

namespace SyndicateIT.Model.DomainModel.SetupManagement.Shift
{
    public class ShiftDomainModel : DomainModelBase
    {
        public ShiftContentViewModel GetShiftContent()
        {
            ShiftContentViewModel model = new ShiftContentViewModel();
            model.Navigation = GetNavigationList("list", "Shift");
            model.Title = "Shift";
            model.ClassTitle = "fa fa-lg fa-fw fa-inbox";
            model.Alert = new AlertViewModel { HasAlert = false };
            return model;
        }

        public ShiftViewModel GetShiftViewModel(String Shift_ID)
        {
            ShiftViewModel model = new ShiftViewModel();
            string title = string.Empty;
            if (Shift_ID != null && Shift_ID != "")
            {
                using (var db = new SyndicatDataEntities())
                {
                    var etShift = db.SHIFTs.Where(p => p.SHIFT_ID.ToString() == Shift_ID).FirstOrDefault();
                    model = Mapper.Map<SHIFT, ShiftViewModel>(etShift);
                }
                title = "Edit Shift";
            }
            else
            {
                title = "New Shift";
            }
            model.Navigation = GetNavigationList("update", title);
            model.Title = title;
            model.Alert = new AlertViewModel() { HasAlert = false };
            model.ClassTitle = "fa fa-lg fa-fw fa-inbox";
            return model;
        }


        public ShiftViewModel GetShiftViewModel(ShiftViewModel model)
        {
            string title = string.Empty;
            if (model.SHIFT_ID != null && model.SHIFT_ID > 0)
            {
                title = "Edit Shift";
            }
            else
            {
                title = "New Shift";
            }
            model.Navigation = GetNavigationList("update", title);
            model.Title = title;
            model.Alert = new AlertViewModel() { HasAlert = false };
            model.ClassTitle = "fa fa-lg fa-fw fa-inbox";
            return model;
        }


            public List<ShiftGridViewModel> GetListShift(ShiftContentViewModel etShiftContentViewModel, bool isAPI = true)
            {
                string[] messages = new string[2];
                messages[1] = GetListShiftBuisnessLogic(etShiftContentViewModel);

                List<ShiftGridViewModel> lst = new List<ShiftGridViewModel>();

                SyndicateIT.UtilityComponent.Encryption.Encrypt(etShiftContentViewModel.SHIFT_ID.ToString());

                using (var db = new SyndicatDataEntities())
                {


                    var listdb = db.USP_GET_SHIFT(
                       etShiftContentViewModel.SHIFT_ID,
                        etShiftContentViewModel.SHIFT_NAME,
                        etShiftContentViewModel.LANGUAGE_ID,
                        etShiftContentViewModel.IS_ACTIVE,
                        etShiftContentViewModel.START_ROW,
                        etShiftContentViewModel.END_ROW,
                        etShiftContentViewModel.TOP,
                        etShiftContentViewModel.ENTRY_DATE,
                        etShiftContentViewModel.ENTRY_USER_ID,
                        etShiftContentViewModel.MODIFICATION_USER_ID,
                        etShiftContentViewModel.Modification_Date
                        ).ToList();

                    List<ShiftGridViewModel> Shifts = Mapper.Map<List<USP_GET_SHIFT_Result>, List<ShiftGridViewModel>>(listdb);

                    lst = Shifts;
                };

                return lst;

            }

        public String[] DeleteShift(ShiftViewModel etShiftViewModel)
        {
            String[] message = new string[2];
            message[1] = DeleteShiftBuisnessLogic(etShiftViewModel);
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
                        var etShiftByLanguage = db.SHIFT_BY_LANGUAGE.Where(p => p.SHIFT_ID == etShiftViewModel.SHIFT_ID).ToList();
                        if (etShiftByLanguage.Count > 0)
                        {
                            db.SHIFT_BY_LANGUAGE.RemoveRange(etShiftByLanguage);
                        }
                        db.SaveChanges();
                        var etShift = db.SHIFTs.Where(p => p.SHIFT_ID == etShiftViewModel.SHIFT_ID).FirstOrDefault();
                        if (etShift != null)
                        {
                            db.SHIFTs.Remove(etShift);
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

        public string[] SaveShift(ShiftViewModel etShiftViewModel, bool isAPI = false)
        {
            string[] messages = new string[2];
            messages[1] = SaveShiftBuisnessLogic(etShiftViewModel);
            if (messages[1] != string.Empty)
            {
                messages[0] = "error";
                return messages;
            }


            SHIFT etShift = new SHIFT();
            SHIFT_BY_LANGUAGE etShiftByLanguageDb = new SHIFT_BY_LANGUAGE();
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    using (var db = new SyndicatDataEntities())
                    {

                        if (etShiftViewModel.SHIFT_ID != 0 && etShiftViewModel.SHIFT_ID.ToString() != "")
                        {   // Update Table and Default row in Table_By_language
                            if (SessionContent.Container.Login.UserID.ToString() != string.Empty)
                            {
                                // Set the entry user id and the modification user id.

                                etShiftViewModel.ENTRY_USER_ID = etShiftViewModel.MODIFICATION_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());

                            }
                            var etShiftdb = db.SHIFTs.Where(p => p.SHIFT_ID.ToString() == etShiftViewModel.SHIFT_ID.ToString()).FirstOrDefault();
                            etShift = Mapper.Map<ShiftViewModel, SHIFT>(etShiftViewModel);

                            if (etShift != null)
                            {



                                if (SessionContent.Container.Login.UserID.ToString() != string.Empty)
                                {
                                    etShiftViewModel.MODIFICATION_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());
                                }

                                string[] properties = new string[8];

                                int count = -1;

                                if (etShiftViewModel.SHIFT_NAME != null)
                                {
                                    count++;
                                    properties[count] = "SHIFT_NAME";
                                }

                                count++;
                                properties[count] = "IS_ACTIVE";

                                count++;
                                properties[count] = "MODIFICATION_USER_ID";
                                count++;
                                properties[count] = "MODIFICATION_DATE";


                                UtilityComponent.Utilities.MergeObject(etShiftdb, etShift, true,
                                      properties
                                     );
                                etShiftdb.MODIFICATION_DATE = DateTimeOffset.Now;

                                etShiftByLanguageDb = db.SHIFT_BY_LANGUAGE.Where(p => p.SHIFT_ID == etShift.SHIFT_ID && p.LANGUAGE_ID == 1).FirstOrDefault();
                                SHIFT_BY_LANGUAGE etShiftByLanguage = new SHIFT_BY_LANGUAGE();
                                etShiftByLanguage = etShiftByLanguageDb;
                                etShiftByLanguage.SHIFT_NAME = etShift.SHIFT_NAME;
                                etShiftByLanguage.IS_ACTIVE = etShift.IS_ACTIVE;
                                UtilityComponent.Utilities.MergeObject(etShiftByLanguageDb, etShiftByLanguage, true,
                                                                      properties
                                                                     );
                                db.SaveChanges();
                            }
                        }
                        else
                        {
                            //Insert new row to Table and Table_By_Language

                            // Need to add ENTRY_USER_ID 
                            etShift = Mapper.Map<ShiftViewModel, SHIFT>(etShiftViewModel);

                            etShift.ENTRY_DATE = DateTimeOffset.Now;

                            if (SessionContent.Container.Login.UserID.ToString() != string.Empty)
                            {
                                etShift.ENTRY_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());
                            }

                            db.SHIFTs.Add(etShift);
                            db.SaveChanges();
                            SHIFT_BY_LANGUAGE erTranslation = new SHIFT_BY_LANGUAGE();
                            erTranslation.SHIFT_NAME = etShift.SHIFT_NAME;
                            erTranslation.SHIFT_ID = etShift.SHIFT_ID;
                            erTranslation.LANGUAGE_ID = (int)Languages.English;
                            erTranslation.ENTRY_DATE = DateTime.Now;
                            erTranslation.ENTRY_USER_ID = new Guid(SessionContent.Container.Login.UserID.ToString());
                            erTranslation.IS_ACTIVE = etShift.IS_ACTIVE;
                            db.SHIFT_BY_LANGUAGE.Add(erTranslation);
                            db.SaveChanges();

                        }

                        messages[0] = "s";
                        messages[1] = etShift.SHIFT_ID.ToString();
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

        public string DeleteShiftBuisnessLogic(ShiftViewModel etShift)
        {
            string message = string.Empty;
            if (etShift.SHIFT_ID == -1)
            {
                message = "there is no record";
            }
            return message;
        }

        public string GetListShiftBuisnessLogic(ShiftContentViewModel etShiftContentViewModel)
        {
            string message = string.Empty;
            return message;
        }

        public string SaveShiftBuisnessLogic(ShiftViewModel model)
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
                model.Add(new NavigationViewModel() { NavigationName = "Shift", NavigationUrl = Utilities.GetUrl("Index", "Shift", SessionContent.Current.Corporate.IsSecure) });
                model.Add(new NavigationViewModel() { NavigationName = title });
            }

            return model;
        }

        public override void InitializeMapper()
        {
            #region Database To View

            Mapper.CreateMap<SHIFT, ShiftViewModel>()
                  .IgnoreAllNonExisting();

            Mapper.CreateMap<USP_GET_SHIFT_Result, ShiftGridViewModel>()
                 .IgnoreAllNonExisting();

            #endregion

            #region view to database

            Mapper.CreateMap<ShiftViewModel, SHIFT>()
                  .IgnoreAllNonExisting();
            Mapper.CreateMap<ShiftGridViewModel, USP_GET_SHIFT_Result>()
               .IgnoreAllNonExisting();

            #endregion
            base.InitializeMapper();
        }
        #endregion


    }

}
