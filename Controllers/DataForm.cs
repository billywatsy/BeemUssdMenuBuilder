using BeemSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using USSDFormBuilder.USSD.Builder.v1;

namespace TreeNodeSample.Controllers
{
    public class DataForm
    {
        public static string AddAddress(USSDEngine uSSDSessionModel)
        {
            try
            {
                BeemContactHelper beemContactHelper = new BeemContactHelper("626cfd2f3166dd65", "M2JjMmZjYjg2YzNjY2VjM2RkNTQ5M2JmMmFmZTRlMWNjMjBlOGQwOGQ2Y2E3MDZkMWY4OTQyMWRlZjI1ZWFiYQ==");
                
                var add = beemContactHelper.AddAddress(new BeemSDK.Model.Address()
                {
                    Addressbook = uSSDSessionModel.MetaData.UssdGetJsonObject("form").UssdGetJsonObject("add_address").UssdGetJsonObjectStringValue("address_form_name") ,
                    Description = uSSDSessionModel.MetaData.UssdGetJsonObject("form").UssdGetJsonObject("add_address").UssdGetJsonObjectStringValue("address_form_description")
                }) ;

                if (!string.IsNullOrEmpty(add.Key))
                    return add.Key;
                return add.Value?.CommonResponseMessage?.Message ?? "Failed processing request";
            }
            catch (Exception er)
            {
                return "Error processing request";
            } 
        }
        public static string UpdateAddress(USSDEngine uSSDSessionModel)
        {
            try
            {
                BeemContactHelper beemContactHelper = new BeemContactHelper("626cfd2f3166dd65", "M2JjMmZjYjg2YzNjY2VjM2RkNTQ5M2JmMmFmZTRlMWNjMjBlOGQwOGQ2Y2E3MDZkMWY4OTQyMWRlZjI1ZWFiYQ==");
                
                var add = beemContactHelper.UpdateAddress(new BeemSDK.Model.Address()
                {
                    Id = uSSDSessionModel.MetaData.UssdGetJsonObject("form").UssdGetJsonObject("view_address").UssdGetJsonObjectStringValue("address_id_select"),
                    Addressbook = uSSDSessionModel.MetaData.UssdGetJsonObject("form").UssdGetJsonObject("view_address").UssdGetJsonObjectStringValue("address_form_name_edit") ,
                    Description = uSSDSessionModel.MetaData.UssdGetJsonObject("form").UssdGetJsonObject("view_address").UssdGetJsonObjectStringValue("address_form_description_edit")
                }) ;

                if (!string.IsNullOrEmpty(add.Key))
                    return add.Key;
                return add.Value?.CommonResponseMessage?.Message ?? "Failed processing request";
            }
            catch (Exception er)
            {
                return "Error processing request";
            } 
        }
        public static HookResponse ShowUserProfile(USSDEngine uSSDSessionModel)
        {
            return new HookResponse()
            {
                Success = "Userid : 626cfd2f3166dd65"
            };
        }
        public static string Showprofile(USSDEngine uSSDSessionModel)
        {
            return "Userid : 626cfd2f3166dd65" ;
        }
        public static string End(USSDEngine uSSDSessionModel)
        {
            return "GoodBye";
        }
        public static HookResponse ViewAddress(USSDEngine uSSDSessionModel)
        {
            var response = new HookResponse();
            try
            {
                BeemContactHelper beemContactHelper = new BeemContactHelper("626cfd2f3166dd65", "M2JjMmZjYjg2YzNjY2VjM2RkNTQ5M2JmMmFmZTRlMWNjMjBlOGQwOGQ2Y2E3MDZkMWY4OTQyMWRlZjI1ZWFiYQ==");

                var address = beemContactHelper.GetPagedAddress(null);

                if (!string.IsNullOrEmpty(address.Key)) {
                    response.ErrorMessage = address.Key;
                    return response; 
                }

                response.DataLookUp = new List<DataLookUp>();

                if(address.Value?.Data == null || address.Value?.Data?.Count() <= 0)
                {
                    response.ErrorMessage = "No Address found";
                    return response;
                }

                int index = -1;
                foreach (var item in address.Value.Data)
                {
                    index++;
                    response.DataLookUp.Add(new DataLookUp()
                    {
                        Code = item.Id  ,
                        Description = item.Description
                    }) ;
                }

                return response;
            }
            catch (Exception er)
            {
                response.ErrorMessage = "Error processing request";
                return response;
            }
        }
        public static List<USSDMenuModel> GetAppForm()
        {
            List<USSDMenuModel> uSSDMenuModels = new List<USSDMenuModel>();

            uSSDMenuModels.Add(
                new USSDMenuModel()
                {
                    Code = "profilename",
                    Text = "Profile Name",
                    Forms = new List<USSDFormModel>()
                    {
                        new USSDFormModel()
                        {
                            Code = "profile",
                            DisplayText = "Hi Profile",
                            FormParameterName = "name" , 
                            PreRenderForm = ShowUserProfile,
                            FormType = FormType.DISPLAYANDCLOSE
                        }
                    },
                    OnFormEnd = Showprofile
                }
           );
            // menu 2
            uSSDMenuModels.Add(
                 new USSDMenuModel()
                 {
                     Code = "address_parent",
                     Text = "Address book",
                     ChildMenus = new List<USSDMenuModel>()
                     {
                         new USSDMenuModel()
                         {
                             Code = "add_address",
                             Text = "Add Address",
                             Forms = new List<USSDFormModel>()
                             {
                                 new USSDFormModel()
                                 {
                                     Code = "address_form_name" ,
                                     DisplayText = "Enter Address Name" ,
                                     FormParameterName = "addressName" ,
                                     FormType = FormType.TEXT
                                 },
                                 new USSDFormModel()
                                 {
                                     Code = "address_form_description" ,
                                     DisplayText = "Enter Address Description" ,
                                     FormParameterName = "addressDescription" ,
                                     FormType = FormType.TEXT
                                 }
                             },
                             OnFormEnd = AddAddress
                         },
                         new USSDMenuModel()
                         {
                             Code = "view_address",
                             Text = "View Address",
                             Forms = new List<USSDFormModel>()
                             {
                                 new USSDFormModel()
                                 {
                                     Code = "address_id_select" ,
                                     DisplayText = "Select Address Book" ,
                                      FormParameterName = "address_id" ,
                                      FormType = FormType.LIST,
                                      PreRenderForm = ViewAddress
                                 },
                                 new USSDFormModel()
                                 {
                                     Code = "address_form_name_edit" ,
                                     DisplayText = "Enter Address Name" ,
                                     FormParameterName = "addressName" ,
                                     FormType = FormType.TEXT
                                 },
                                 new USSDFormModel()
                                 {
                                     Code = "address_form_description_edit" ,
                                     DisplayText = "Enter Address Description" ,
                                     FormParameterName = "addressDescription" ,
                                     FormType = FormType.TEXT
                                 }
                             },
                             OnFormEnd = UpdateAddress
                         }
                     }
                 }
           );

            
            return uSSDMenuModels;
        }
    }
}