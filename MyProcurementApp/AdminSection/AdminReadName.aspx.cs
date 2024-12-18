using Autofac;
using DatabaseCodeBase.DatabaseCode;
using DatabaseCodeBase.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MyProcurementApp.AdminSection
{
    public partial class AdminReadName : System.Web.UI.Page
    {
        private IContainer container;
        private UserDB userDB;
        protected List<UserModel> users;

        protected async void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                await AddUsersToList();
                BindUsers();
            }
        }

        private async Task AddUsersToList()
        {
            container = (IContainer)Application["AutofacContainer"];
            userDB = container.Resolve<UserDB>();
            users = await userDB.ReadUser("selectUsers"); //Stores the users in list
        }

        private void BindUsers()
        {
            gvUsers.DataSource = users;
            gvUsers.DataBind();
        }
    }
}