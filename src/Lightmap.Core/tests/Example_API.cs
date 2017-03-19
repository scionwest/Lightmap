using System;
using Lightmap.Migration;
using Lightmap.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Lightmap
{
    public class DataModelTests
    {
        public void Example_of_API()
        {
            // Arrange
            var dataModel = new DataModel(Mock.Of<IDatabaseMigrator>());

            // String based
            dataModel.AddTable("dbo", "Foo")
                .AddColumn(typeof(bool), "Id").IsNullable();

            // Type based
            ITableBuilder<AspNetRoles> rolesTable = dataModel.AddTable<AspNetRoles>("dbo");
            rolesTable.AlterColumn(model => model.Id)
                .AsPrimaryKey()
                .IsUniquenessRequired();

            rolesTable.AlterColumn(model => model.Name)
                .IsNullable();

            // Anonymous Type based
            dataModel.AddTable("dbo", "Foo", () => new { Id = default(Guid), RoleId = default(string) })
                .AlterColumn(model => model.Id).AsPrimaryKey();
            
            // Mix string based, Type based and Anonymous Type based modeling.
            dataModel.AddTable("dbo", "Foo", () => new { Id = default(Guid), RoleId = default(string), Name = default(string) })
                .AlterColumn(model => model.RoleId).WithForeignKey(rolesTable, (userTable, roleTable) => userTable.RoleId == roleTable.Id).GetOwner()
                .AddColumn(typeof(bool), "CreatedOn").IsNullable();
        }
    }
}
