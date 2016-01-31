using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DatabaseManagerSqliteExtensionsTests;
using Lightmap.Modeling;
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
            modeler.Create()
                .Table("User")
                    .WithColumn<int>("UserId")
                    .AsPrimaryKey()
                    .IsUnique()
                .WithColumn<string>("Email")
                    .IsUnique()
                    .NotNull();

            modeler.Create()
                .Table("Account")
                .WithColumns(() => new { UserId = default(int), AccountId = default(int) })
                    .UsePrimaryKey(table => table.AccountId)
                    .UseForeignKey("User", accountTable => accountTable.UserId);
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
            // Act
            int count = 100000;
            var time = new List<double>();

            for (int c = 0; c < count; c++)
            {
                var watch = new Stopwatch();
                watch.Start();
                //this.ModelDatabase();
                watch.Stop();
                time.Add(watch.Elapsed.TotalMilliseconds);
                watch.Reset();
            }

            double average = time.Average();
        }

        //private void ModelDatabase()
        //{
        //    var modeler = new DatabaseModeler();

        //    // Create the user table with 4 columns, with UserId being a primary key.
        //    // The table name is defined by the anonymous type that contains a single property that maps to the name of the table "User".
        //    modeler.Create()
        //        .Table(
        //            () => new { User = default(int) },
        //            () => new
        //            {
        //                UserId = default(int),
        //                FirstName = default(string),
        //                LastName = default(string),
        //                MiddleInitial = default(string),
        //            })
        //        .ModifyColumn((table, column) => column.Name == nameof(table.UserId))
        //            .NotNull()
        //            .AsPrimaryKey()
        //            .IsUnique();

        //    // Create a bank table based off the properties in the Bank Type, with BankId being the PK.
        //    modeler.Create().Table<Bank>()
        //            .ModifyColumn((bankTable, column) => column.Name == nameof(bankTable.BankId))
        //            .AsPrimaryKey()
        //            .IsUnique();

        //    // Create an Account table based with 4 columns. The AccountId is the PK and OwnerId and BankId are foreign keys to the Bank and User tables.
        //    modeler.Create()
        //        .Table(
        //            "Account",
        //            () => new
        //            {
        //                AccountId = default(int),
        //                Number = default(string),
        //                OwnerId = default(int),
        //                BankId = default(int),
        //            })
        //        .ModifyColumn((accountTable, column) => column.Name == nameof(accountTable.AccountId))
        //            .IsUnique()
        //            .AsPrimaryKey()
        //        .ModifyColumn((accountTable, column) => column.Name == nameof(accountTable.OwnerId))
        //            .AsForeignKey(modeler.GetTable("User"), accountTable => new { UserId = accountTable.OwnerId })
        //        .ModifyColumn((accountTable, column) => column.Name == nameof(accountTable.BankId))
        //            .AsForeignKey(modeler.GetTable<Bank>(), accountTable => new { BankId = accountTable.BankId });
        //}
    }
}