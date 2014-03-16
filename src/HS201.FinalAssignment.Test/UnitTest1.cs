using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using HS201_FinalAssignment.Data_Access;
using HS201_FinalAssignment.DependencyResolution;
using HS201_FinalAssignment.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using StructureMap;

namespace HS201.FinalAssignment.Test
{
    [TestClass]
    public class UnitTest1
    {
        private ISession _session;

        [TestInitialize]
        public void Setup()
        {
        }

        [TestMethod]
        public void TestMethod1()
        {
            var configuration = new NhConfiguration().CreateConfiguration();
            var schemaExport = new SchemaExport(configuration);

            var scripts = new List<string>();

            schemaExport.Create(scripts.Add, false);

            var fullPath = @"..\..\..\Database\HS201FinalAssignment\Up\0001-CreateEntities.sql";

            File.WriteAllLines(fullPath, scripts);
        }

        [TestMethod]
        public void AssertConferenceCanPersist()
        {
            var conf = new Conference();
            conf.Name = "test123";
            conf.StartDate = DateTime.Now;
            conf.EndDate = DateTime.Now;

            _session.SaveOrUpdate(conf);

            Assert.IsNotNull(_session.QueryOver<Conference>().Where(x=>x.Name=="test123"));
        }
    }
}
