using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using FluentValidation;
using FluentValidation.Mvc;
using HS201_FinalAssignment.Helpers;
using Microsoft.Practices.ServiceLocation;
using NHibernate;

namespace HS201_FinalAssignment
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            DataAnnotationsModelValidatorProvider.AddImplicitRequiredAttributeForValueTypes = false;
            FluentValidationModelValidatorProvider.Configure();
            ValidatorOptions.ResourceProviderType = typeof(FluentValidationConfigResource);
            
            AutoMapperBootstrapper.Initialize();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_BeginRequest(Object sender, EventArgs e)
        {
            var currentSession = ServiceLocator.Current.GetInstance<ISession>();
            currentSession.BeginTransaction();
        }

        protected void Application_EndRequest(Object sender, EventArgs e)
        {
            var currentSession = ServiceLocator.Current.GetInstance<ISession>();
            var currentTransaction = currentSession.Transaction;

            try
            {
                if (currentTransaction.IsActive)
                    currentTransaction.Commit();
            }
            catch (Exception ex)
            {
                if (currentTransaction.IsActive)
                    currentTransaction.Rollback();
                throw ex;
            }
            finally
            {
                currentSession.Dispose();
            }
        }

        public class FluentValidationConfigResource
        {
            public static string notempty_error
            {
                get { return "{PropertyName} is Required"; }
            }
        }
    }
}