using System;
using System.Linq;
using Lightmap.Modeling;

namespace Lightmap.Sqlite.Tests.Migrations
{
    [MigrationVersion(0)]
    public class Untyped_MultipleTable_WithForeignKey : IMigration
    {
        public Untyped_MultipleTable_WithForeignKey(IDataModel model) => this.DataModel = model;

        public IDataModel DataModel { get; }

        public void Apply()
        {
            this.DataModel.AddTable("User")
                .AddColumn(typeof(int), "Id")
                    .GetOwner()
                .AddColumn(typeof(string), "Name")
                    .GetOwner()
                .AddColumn(typeof(DateTime), "CreatedOn");

            this.DataModel.AddTable("Account")
                .AddColumn(typeof(int), "Id").IsPrimaryKey().GetOwner()
                // Barf. Need lookup methods to simplify finding tables and columns.
                .AddColumn(typeof(int), "UserId")
                    .WithForeignKey(this.DataModel
                        .GetTables()
                            .FirstOrDefault(table => table.TableName == "User")
                        .GetColumns()
                            .FirstOrDefault(column => column.ColumnName == "Id").GetModel());
        }

        public void Revert()
        {
            this.DataModel.DropTable("main", "User");
        }
    }
}
