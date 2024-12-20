using Autofac;
using DatabaseCodeBase.DatabaseCode;
using DatabaseCodeBase.Model;
using HelperFunctions.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MyProcurementApp
{
    public partial class Default : Page
    {
        private IContainer container;
        private List<RoleModel> roleList;
        private int roleId;
        private bool isActive;
        protected async void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                await AddRoles();
                BindDropDownData();
            }
        }
        private async Task AddRoles()
        {
            container = (IContainer)Application["AutofacContainer"];
            var rolesdb = container.Resolve<RoleDB>();
            roleList = await rolesdb.ReadRoles("selectRoles");
        }
        
        private void BindDropDownData()
        {
            ddlRoles.DataSource = roleList;
            ddlRoles.DataTextField = "RoleName";
            ddlRoles.DataValueField = "RoleID";
            ddlRoles.DataBind();
            ddlRoles.Items.Insert(0, new ListItem("[Enter A Role]"));
        }

        protected async void OnCheckUserActive(object sender, EventArgs e)
        {

            isActive = isUserActive.Checked;
            
        }

        protected async void OnSubmitUser(object sender, EventArgs e)
        {
            //Calling on Container and User Database Service
            container = (IContainer)Application["AutofacContainer"];
            var userdb = container.Resolve<UserDB>();

            //Validating string with a string Extenstion Method to see if it's valid or not
            string validatedName = userName.Text.ValidateString();
            roleId = ddlRoles.SelectedValue.ValidateString().ConvertStringTo<int>();

            // Instantiating our model to hold the data
            var user = new UserModel()
            {
                Name = validatedName,
                RoleID = roleId,
                Active = isActive
            };

            //Data Should be transferred to Database
            string isCreated = await userdb.CreateUser("insertUser", user);
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