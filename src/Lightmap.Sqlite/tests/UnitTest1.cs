using System;
using Lightmap;
using Lightmap.Modeling;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace tests
{
    [MigrationVersion(0)]
    public class TestMigration : IMigration
    {
        private string schemaName;

        public TestMigration(IDataModel currentModel, string defaultSchema)
        {
            this.DataModel = currentModel;
            this.schemaName = defaultSchema;
        }

        public IDataModel DataModel { get; }

        public void Apply()
        {
            DataModel.AddTable(this.schemaName, "Foo").AddColumn(typeof(int), "Id");
        }

        public void Revert()
        {
            throw new NotImplementedException();
        }
    }

    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            // Arrange
            string databaseName = Guid.NewGuid().ToString();
            IDataModel dataModel = new DataModel();
            var databaseManager = new SqliteDatabaseManager(databaseName, $"DATA SOURCe={databaseName}.sqlite");
            IMigration testMigration = new TestMigration(dataModel, "main");
            var migrator = new SqliteMigrator(testMigration);

            // Assert
            migrator.Apply(databaseManager);
        }
    }
}
