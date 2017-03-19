using System;
using Lightmap.Modeling;
using Lightmap.Models;
using Moq;

namespace Lightmap
{
    public class DataModelTests
    {
        public void Example_of_API()
        {
            // Arrange
            var dataModel = new DataModel();

            // String based
            dataModel.AddTable(schemaName: "dbo", tableName: "Foo")
                .AddColumn(dataType: typeof(int), columnName: "Id")
                .IsNullable();

            // Type based
            ITableBuilder<AspNetRoles> rolesTable = dataModel.AddTable<AspNetRoles>("dbo")
                .AlterColumn(model => model.Id)
                    .IsPrimaryKey()
                    .Unique()
                    .GetOwner()
                .AlterColumn(model => model.Name)
                    .IsNullable()
                    .GetOwner();

            // Anonymous Type based
            dataModel.AddTable("dbo", "Foo", () => new
            {
                Id = default(Guid),
                RoleId = default(string)
            })
            .AlterColumn(model => model.Id)
                .IsPrimaryKey();

            // Mix string based, Type based and Anonymous Type based modeling.
            dataModel.AddTable("dbo", "Foo", () => new
            {
                Id = default(Guid),
                RoleId = default(string),
                Name = default(string)
            })
            .AlterColumn(model => model.RoleId)
                .WithForeignKey(rolesTable, (userTable, roleTable) => userTable.RoleId == roleTable.Id)
                .GetOwner()
            .AlterColumn(model => model.Id)
                .IsPrimaryKey()
                .GetOwner()
            .AddColumn(typeof(bool), "CreatedOn")
                .IsNullable();
        }
    }
}
