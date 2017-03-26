using System;
using System.Linq;
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
            ITableBuilder accountTable = dataModel.AddTable("Account");
            IColumnBuilder aspNetUserIdColumn = accountTable
                .AddColumn(dataType: typeof(int), columnName: "AccountId")
                    .IsPrimaryKey().Unique().GetOwner()
                .AddColumn(typeof(string), "Name").GetOwner()
                .AddColumn(typeof(int), "AspNetUserId");

            // Strongly Typed with Foreign Key to Untyped Table
            dataModel.AddTable<AspNetUsers>("dbo")
                .AlterColumn(model => model.Id).IsPrimaryKey()
                .WithForeignKey(aspNetUserIdColumn);

            // Strongly Typed with Foreign Key to strongly Typed table
            dataModel.AddTable<AspNetUserLogins>("dbo")
                .AlterColumn(model => model.ProviderKey).IsPrimaryKey()
                .WithForeignKey<AspNetUsers>((userLoginTable, userTable) => userLoginTable.UserId == userTable.Id);

            dataModel.AddTable<AspNetRoles>("dbo")
                .AlterColumn(model => model.Id).IsPrimaryKey();

            // Strongly Typed via Anonymous Objects
            var userRoleTable = dataModel.AddTable(() => new { UserId = default(string), RoleId = default(string), }, "UserRoles");
            userRoleTable.AlterColumn(table => table.RoleId)
                .WithForeignKey<AspNetRoles>((linkTable, rolesTable) => linkTable.RoleId == rolesTable.Id).GetOwner()
            .AlterColumn(table => table.UserId)
                .WithForeignKey<AspNetUserLogins>((linkTable, userTable) => linkTable.UserId == userTable.UserId);

            dataModel.AddTable<AspNetRoleClaims>("dbo")
                .AlterColumn(table => table.Id).IsPrimaryKey().GetOwner()
                .AlterColumn(table => table.RoleId).WithForeignKey<AspNetRoles>((linkTable, roleTable) => linkTable.RoleId == roleTable.Id);

            // Foreign Key between Anonymous and Standard Strongly Typed
            dataModel.AddTable<AspNetUserClaims>("dbo")
                .AlterColumn(table => table.UserId)
                .WithForeignKey((claimTable, roleTable) => claimTable.UserId == roleTable.UserId, userRoleTable);
        }
    }
}
