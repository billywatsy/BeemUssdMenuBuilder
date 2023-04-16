using BeemAfricaSDK.USSD.Builder.v1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TreeNodeSample.Controllers
{
    public class DataForm
    {
        public static string Showprofile(USSDEngine uSSDSessionModel)
        {
            return "John Doe , GoodBye";
        }
        public static string End(USSDEngine uSSDSessionModel)
        {
            return "GoodBye";
        }
        public static List<USSDMenuModel> GetAppForm()
        {
            List<USSDMenuModel> uSSDMenuModels = new List<USSDMenuModel>();

            uSSDMenuModels.Add(
                new USSDMenuModel()
                {
                    Code = "profilename",
                    Text = "Profile Name",
                    OnFormEnd = Showprofile
                }
           );
            uSSDMenuModels.Add(
                 new USSDMenuModel()
                 {
                     Code = "parentone",
                     Text = "Parent Level One",
                     ChildMenus = new List<USSDMenuModel>()
                     {
                         new USSDMenuModel()
                         {
                             Code = "parenttwo1",
                             Text = "Parent LevelTwo 1",
                             ChildMenus = new List<USSDMenuModel>()
                             {
                                 new USSDMenuModel()
                                 {
                                     Code = "parentthree1",
                                     Text = "Parent LevelThree 1", 
                                     ChildMenus = new List<USSDMenuModel>()
                                     {
                                         new USSDMenuModel()
                                         {
                                             Code = "parenttfour1",
                                             Text = "Parent LevelFour 1",
                                              OnFormEnd = End
                                         }
                                     }
                                 }
                             }
                         },
                         new USSDMenuModel()
                         {
                             Code = "parenttwo2",
                             Text = "Parent LevelTwo 2",
                             Forms = new List<USSDFormModel>()
                             {
                                 new USSDFormModel()
                                 {
                                     Code = "firstname" ,
                                     DisplayText = "Enter firstname" ,
                                      FormParameterName = "firstname" ,
                                      FormType = FormType.TEXT
                                 },
                                 new USSDFormModel()
                                 {
                                     Code = "surname" ,
                                     DisplayText = "Enter surname" ,
                                      FormParameterName = "surname" ,
                                      FormType = FormType.TEXT
                                 }
                             },
                             OnFormEnd = End
                         }
                     }
                 }
           );

            return uSSDMenuModels;
        }
    }
}