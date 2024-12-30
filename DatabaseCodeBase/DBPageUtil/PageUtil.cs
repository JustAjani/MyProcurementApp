﻿using Autofac;
using DatabaseCodeBase.DatabaseCode;
using DatabaseCodeBase.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace DatabaseCodeBase.DBPageUtil
{
    public abstract class PageUtil : Page
    {
        // database Container
        protected IContainer container;
        protected UserDB userDB;
        protected ProcurementDB procurementDB;
        protected RoleDB roleDB;
        protected ProcurementTypeDB procurementTypeDB;
        protected ProcurementModel Aprocurement {  get; set; } = new ProcurementModel();

        // model list
        protected List<UserModel> UserList { get; set; } = new List<UserModel>();
        protected List<ProcurementTypeModel> ProcurementTypeList { get; set; } = new List<ProcurementTypeModel>();
        protected List<RoleModel> RoleList { get; set; } = new List<RoleModel>();
        protected List<ProcurementModel> ProcurementList { get; set; } = new List<ProcurementModel>();

        // dependency Initialization function
        protected virtual async Task InitializeDependecy()
        {
            await Task.CompletedTask;
        }
    }
}
