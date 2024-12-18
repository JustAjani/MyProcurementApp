using Autofac;
using DatabaseCodeBase.DatabaseCode;
using DatabaseCodeBase.Model;
using HelperFunctions.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MyProcurementApp
{
    public partial class Default : System.Web.UI.Page
    {
        private IContainer container;
        private UserDB userdb;
        protected async void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected async void OnSubmitUser(object sender, EventArgs e)
        {
            //Calling on Container and User Database Service
            container = (IContainer)Application["AutofacContainer"];
            userdb = container.Resolve<UserDB>();

            //Validating string with a string Extenstion Method to see if it's valid or not
            string validatedName = userName.Text.ValidateString();

            // Instantiating our model to hold the data
            var user = new UserModel()
            {
                Name = validatedName,
            };

            //Data Should be transferred to Database
            string isCreated = await userdb.CreateUser("insertName", user);
            if (isCreated.Contains("Successfully"))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"Swal.fire('Success', '{isCreated}', 'success');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"Swal.fire('Error', '{isCreated}', 'error');", true);
            }
           
        }
    }
}