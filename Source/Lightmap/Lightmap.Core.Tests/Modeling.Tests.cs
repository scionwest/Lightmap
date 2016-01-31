using DatabaseManagerSqliteExtensionsTests;
using Lightmap.Modeling;
using Xunit;

namespace Lightmap.Provider.Sqlite.Tests
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    public class DatabaseManagerSqliteExtensionsTests
    {
        [Fact]
        public void DatabaseModeler_provides_table_modeler()
        {
            // Act
            var modeler = new DatabaseModeler();

            // Create the user table with 4 columns, with UserId being a primary key.
            modeler.Create()
                .Table("User", () => new
                    {
                        UserId = default(int),
                        FirstName = default(string),
                        LastName = default(string),
                        MiddleInitial = default(string),
                    })
                .ModifyColumn((table, column) => column.Name == nameof(table.UserId))
                    .NotNull()
                    .AsPrimaryKey()
                    .IsUnique();

            // Create a bank table based off the properties in the Bank Type, with BankId being the PK.
            modeler.Create().Table<Bank>()
                    .ModifyColumn((bankTable, column) => column.Name == nameof(bankTable.BankId))
                    .AsPrimaryKey()
                    .IsUnique();

            // Create an Account table based with 4 columns. The AccountId is the PK and OwnerId and BankId are foreign keys to the Bank and User tables.
            modeler.Create()
                .Table(
                    () => new
                    {
                        Account = default(string)
                    },
                    () => new
                    {
                        AccountId = default(int),
                        Number = default(string),
                        OwnerId = default(int),
                        BankId = default(int),
                    })
                .ModifyColumn((accountTable, column) => column.Name == nameof(accountTable.AccountId))
                    .IsUnique()
                    .AsPrimaryKey()
                .ModifyColumn((accountTable, column) => column.Name == nameof(accountTable.OwnerId))
                    .AsForeignKey(modeler.GetTable("User"), accountTable => new { UserId = accountTable.OwnerId })
                .ModifyColumn((accountTable, column) => column.Name == nameof(accountTable.BankId))
                    .AsForeignKey(modeler.GetTable<Bank>(), accountTable => new { BankId = accountTable.BankId });
        }
    }
}