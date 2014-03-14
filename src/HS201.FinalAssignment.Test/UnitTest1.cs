using System;
using System.Collections.Generic;
using System.IO;
using HS201_FinalAssignment.Data_Access;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate.Tool.hbm2ddl;

namespace HS201.FinalAssignment.Test
{
    [TestClass]
    public class UnitTest1
    {
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
            
        }
    }
}
