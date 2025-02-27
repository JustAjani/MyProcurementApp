﻿using Autofac;
using DatabaseCodeBase.DatabaseCode;
using DatabaseCodeBase.DBPageUtil;
using DatabaseCodeBase.Model;
using HelperFunctions.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MyProcurementApp.AdminSection
{
    public partial class AdminCreateRole : PageUtil
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected async void OnRoleSubmit(object sender, EventArgs e)
        {
            container = (IContainer)Application["AutofacContainer"];
            roleDB = container.Resolve<RoleDB>();

            string userRole = Role.Text.ValidateString(this);

            var RoleToDb = new RoleModel()
            {
                RoleName = userRole
            };

            string isRoleCreated = await roleDB.CreateRoles("insertRole", RoleToDb);
            isRoleCreated.AlertSuccessORFail(this);
        }
    }
}