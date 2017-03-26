using System;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Dapper;
using Lightmap.Modeling;
using Lightmap.Sqlite.Tests.Migrations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lightmap.Sqlite.Tests
{
    [TestClass]
    public class UntypedMigrationTests
    {
        private IDataModel dataModel;

        [TestInitialize]
        public void Setup()
        {
            this.dataModel = new DataModel();
        }

        [TestMethod]
        public void SingleTable_SingleColumn_NoConstraints_Created()
        {
            // Arrange
            IDatabaseManager databaseManager = this.GetDatabaseManager();
            IMigration migration = new Untyped_SingleTable_SingleColumn_NoConstraints(this.dataModel);
            var migrator = new SqliteMigrator(migration);

            // Act
            migrator.Apply(databaseManager);

            // Assert
            string tableName = migration.DataModel.GetTables().FirstOrDefault().TableName;
            using (var connection = databaseManager.OpenConnection())
            {
                int count = connection.QueryFirstOrDefault<int>($"SELECT count(*) FROM sqlite_master WHERE type='table' AND name='{tableName}';");
                Assert.AreEqual(1, count);
            }

            this.Cleanup(databaseManager);
        }

        [TestMethod]
        public void SingleTable_SingleColumn_PrimaryKey_Created()
        {
            // Arrange
            IDatabaseManager databaseManager = this.GetDatabaseManager();
            IMigration migration = new Untyped_SingleTable_SingleColumn_PrimaryKey(this.dataModel);
            var migrator = new SqliteMigrator(migration);

            // Act
            migrator.Apply(databaseManager);
            this.Cleanup(databaseManager);
        }

        [TestMethod]
        public void SingleTable_MultiColumn_Created()
        {
            // Arrange
            IDatabaseManager databaseManager = this.GetDatabaseManager();
            IMigration migration = new Untyped_SingleTable_MultiColumn(this.dataModel);
            var migrator = new SqliteMigrator(migration);

            // Act
            migrator.Apply(databaseManager);
            this.Cleanup(databaseManager);
        }

        [TestMethod]
        public void SingleTable_MultiColumn_WithNullableColumn_Created()
        {
            // Arrange
            IDatabaseManager databaseManager = this.GetDatabaseManager();
            IMigration migration = new Untyped_SingleTable_MultiColumn_Nullable(this.dataModel);
            var migrator = new SqliteMigrator(migration);

            // Act
            migrator.Apply(databaseManager);
            this.Cleanup(databaseManager);
        }

        [TestMethod]
        public void SingleTable_MultipleTable_WithForeignKey()
        {
            // Arrange
            IDatabaseManager databaseManager = this.GetDatabaseManager();
            IMigration migration = new Untyped_MultipleTable_WithForeignKey(this.dataModel);
            var migrator = new SqliteMigrator(migration);

            // Act
            migrator.Apply(databaseManager);
            this.Cleanup(databaseManager);
        }

        private void Cleanup(IDatabaseManager manager)
        {
            File.Delete(AppContext.BaseDirectory + "\\databases\\" + manager.Database + ".sqlite");
        }

        private IDatabaseManager GetDatabaseManager([CallerMemberName] string methodName = "")
        {
            if (!Directory.Exists(AppContext.BaseDirectory + "\\databases"))
            {
                Directory.CreateDirectory(AppContext.BaseDirectory + "\\databases");
            }

            var databaseName = methodName + "_" + DateTime.Now.ToString("yyyyMMdd_HHMMss.ms");
            return new SqliteDatabaseManager(databaseName, $"DATA SOURCe={AppContext.BaseDirectory}\\databases\\{databaseName}.sqlite");
        }
    }
}
