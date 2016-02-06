using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DatabaseManagerSqliteExtensionsTests;
using Lightmap.Core.Tests;
using Lightmap.Modeling2;
using Xunit;

namespace Lightmap.Provider.Sqlite.Tests
{
    public class InitialDatabase : IMigration
    {
        public Task Apply()
        {
            throw new NotImplementedException();
        }

        public void Configure(IDatabaseModeler modeler)
        {
            // Model a table using strings and generic Type arguments for data-types.
            modeler.Create().Table("User")
                .WithColumn<int>("UserId")
                    .AsPrimaryKey()
                    .IsUnique()
                .WithColumn<string>("Email")
                    .IsUnique()
                    .NotNull();

            // Model a table based off the properties in an existing model.
            modeler.Create().Table<Bank>()
                .UsePrimaryKey(bank => bank.BankId)
                .IgnoreColumn(bank => bank.IsDirty)
                .UseForeignKey("User", bank => new { BankId = bank.BankId });

            // Model a table using anonymous types
            modeler.Create().Table("Account")
                .WithColumns(() => new { UserId = default(int), AccountId = default(int), BankId = default(int) })
                    .UsePrimaryKey(table => table.AccountId)
                    .UseForeignKey("User", accountTable => new { UserId = accountTable.UserId })
                    .UseForeignKey("Bank", accountTable => new { BankId = accountTable.BankId });

            modeler.Create().Table("Product")
                .WithColumn<int>("ProductId").AsPrimaryKey()
                .WithColumn<string>("Name").NotNull();

            modeler.Create().Table("Purchase")
                .WithColumns(() => new { PurchaseId = default(int), UserId = default(int), ProductId = default(int), AccountId = default(int) })
                    .UsePrimaryKey(purchaseTable => purchaseTable.PurchaseId)
                    .UseForeignKey("Account", purchaseTable => new { AccountId = purchaseTable.AccountId })
                    .UseForeignKey("User", purchaseTable => new { UserId = purchaseTable.UserId })
                    .UseForeignKey("Product", purchaseTable => new { ProductId = purchaseTable.ProductId });
        }

        public Task Rollback()
        {
            throw new NotImplementedException();
        }
    }

    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    public class DatabaseManagerSqliteExtensionsTests
    {
        [Fact]
        public void DatabaseModeler_provides_table_modeler()
        {
            // Arrange
            int count = 100000;
            var time = new List<double>();

            // Act
            for (int c = 0; c < count; c++)
            {
                var watch = new Stopwatch();
                var model = new StronglyTypedMigration();
                watch.Start();
                model.Configure(new DatabaseModeler("Foo.sql"));
                watch.Stop();
                time.Add(watch.Elapsed.TotalMilliseconds);
                watch.Reset();
            }

            double average = time.Average();
        }

        [Fact]
        public void Test()
        {
            var database = new Lightmap.Modeling.DatabaseModeler("test");
            var testTable = database.Create()
                .Table("Account", () => new { AccountId = default(int), Name = default(string) })
                    .WithPrimaryKey(table => table.AccountId).GetTable();

            database.Create()
                .Table("User", () => new { UserId = default(int), FirstName = default(string), BankId = default(int) })
                    .WithPrimaryKey(table => table.UserId)
                    .WithForeignKey(testTable, (table, referenceTable) => table.BankId == referenceTable.AccountId);
        }
    }
}