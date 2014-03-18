using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using HS201_FinalAssignment.Controllers;
using HS201_FinalAssignment.Domain.Entities;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Mapping.ByCode;

namespace HS201_FinalAssignment.Data_Access
{
    public class NhConfiguration
    {
        private Configuration Configure()
        {
            var config = new Configuration();

            config.SessionFactoryName("HS201");

            config.DataBaseIntegration(x =>
            {
                x.Dialect<MsSql2008Dialect>();
                x.Driver<SqlClientDriver>();
                x.ConnectionStringName = "HS201_ConnectionString";
                x.Timeout = 10;
                x.BatchSize = 100;
                x.KeywordsAutoImport = Hbm2DDLKeyWords.AutoQuote;
                x.LogFormattedSql = true;
            });

            return config;
        }

        private HbmMapping GenerateMapping()
        {
            var map = new ModelMapper();

            map.AddMappings(Assembly.GetAssembly(typeof (Conference)).GetExportedTypes());

            return map.CompileMappingForAllExplicitlyAddedEntities();
        }

        public Configuration CreateConfiguration()
        {
            var configuration = Configure();
            var mapping = GenerateMapping();

            configuration.AddDeserializedMapping(mapping, "HS201");

            return configuration;
        }
    }
}