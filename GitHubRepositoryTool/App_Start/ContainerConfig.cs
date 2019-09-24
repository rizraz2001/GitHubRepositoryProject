using Autofac;
using Autofac.Integration.Mvc;
using GitHubRepositoryTool.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GitHubRepositoryTool
{
    public class ContainerConfig
    {
        internal static void RegisterContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterType<BookmarkService>().As<IBookmarkService>().SingleInstance();
            builder.RegisterType<LogService>().As<ILogService>().SingleInstance();
            builder.RegisterType<RequestService>().As<IRequestService>().SingleInstance();


            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}