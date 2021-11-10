using Autofac;
using Autofac.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using System.Configuration;
using System.Reflection;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using isRock.LineBot.Dialog;

namespace linebotluis
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        private const string EndpointUrl = "https://webdb.documents.azure.com:443/";
        private const string PrimaryKey = "yKg5PPPcZRyPPeSnEdja1NjVWjrJ97EGdeEoPvc1WBBl4W7Im8jj4NCIubrXe6BEfGL360TbhLWBYZQkVJkfUQ==";

        protected void Application_Start()
        {
          
            GlobalConfiguration.Configure(WebApiConfig.Register);
            Uri docDbServiceEndpoint = new Uri(ConfigurationManager.AppSettings["DocumentDbServiceEndpoint"]);
            string docDbEmulatorKey = ConfigurationManager.AppSettings["DocumentDbAuthKey"];
            var builder = new ContainerBuilder();
            builder.RegisterModule(new AzureModule(Assembly.GetExecutingAssembly()));
            var store = GetStore(docDbServiceEndpoint, docDbEmulatorKey);
            builder.Register(c => store).Keyed<IBotDataStore<IBotDataStore>>(AzureModule.Key_DataStore)
                 .AsSelf()
                .SingleInstance();
            builder.Update(Conversation.Container);
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }

        private static DocumentDbDataStore GetStore(Uri docDbServiceEndpoint, string docDbEmulatorKey)
        {
            DocumentDbDataStore documentDbDataStore = new DocumentDbDataStore(docDbServiceEndpoint, docDbEmulatorKey);
            return documentDbDataStore;
        }

        private interface IBotDataStore<T>
        {
        }

        private class AzureModule : IModule
        {
            internal static readonly object Key_DataStore;
            private Assembly assembly;

            public AzureModule(Assembly assembly)
            {
                this.assembly = assembly;
            }
        }

        private interface IBotDataStore
        {
        }
    }
}
