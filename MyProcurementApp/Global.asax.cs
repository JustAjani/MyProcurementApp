using Autofac;
using DatabaseCodeBase.DatabaseCode;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace MyProcurementApp
{
    public class Global : System.Web.HttpApplication
    {
        protected async void Application_Start(object sender, EventArgs e)
        {
            //Creates a builder
            var builder = new ContainerBuilder();
            string dbConfigString = ConfigurationManager.ConnectionStrings["ProcurementDb"].ConnectionString;

            // Register database as Service and single instance so it can be used for the entire life cycle
            builder.RegisterInstance(new BaseDatabase(dbConfigString)).As<BaseDatabase>().SingleInstance();
            builder.RegisterInstance(new UserDB(dbConfigString)).As<UserDB>().SingleInstance();
            builder.RegisterInstance(new ProcurementTypeDB(dbConfigString)).As<ProcurementTypeDB>().SingleInstance();
            builder.RegisterInstance(new RoleDB(dbConfigString)).As<RoleDB>().SingleInstance();
            builder.RegisterInstance(new ProcurementDB(dbConfigString)).As<ProcurementDB>().SingleInstance();
            builder.RegisterInstance(new CostCenterDB(dbConfigString)).As<CostCenterDB>().SingleInstance();
            builder.RegisterInstance(new SupplierDB(dbConfigString)).As<SupplierDB>().SingleInstance();

            // Build the container
            var container = builder.Build();
            Application["AutofacContainer"] = container;

            //Builds Basic Tables
            var db = container.Resolve<BaseDatabase>();
            await db.MakeDatabaseTables("CreateTables");        
            db.TableCreationFail += Db_TableCreationFail;
        }

        // Display Error
        private void Db_TableCreationFail(object sender, string e)
        {
            string script = @"<script>
                                Swal.fire({
                                  title: 'Error!',
                                  text: 'Connection Error Try Again Later!',
                                  icon: 'error',
                                  confirmButtonText: 'Okay'
                                });
                              </script>";

            HttpContext.Current.Response.Write(script);
        }
    }
}