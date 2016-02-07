using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lightmap.Modeling;

namespace Lightmap.Core.Tests.Migrations
{
    [MigrationVersion(1)]
    public class ExpressionMigrations : IMigration
    {
        public Task Apply(IDataProvider provider)
        {
            return provider.ProcessMigration(this);
        }

        public void Configure(DatabaseModeler modeler)
        {
            var rolesTable = modeler.Create()
                .Table(
                    "AspNetRoles",
                    () => new
                    {
                        Id = default(string),
                        ConcurrencyStamp = default(string),
                        Name = default(string),
                        NormalizedName = default(string),
                    })
                .GetTable();

            var roleClaimsTable = modeler.Create()
                .Table(
                    "AspNetRoleClaims",
                    () => new
                    {
                        Id = default(int),
                        ClaimType = default(string),
                        ClaimValue = default(string),
                        RoleId = default(string),
                    })
                .WithPrimaryKey(table => table.Id)
                .WithForeignKey(rolesTable, (table, referenceTable) => table.RoleId == referenceTable.Id)
                .GetTable();

            var userTable = modeler.Create()
                .Table(
                "AspNetUsers",
                () => new
                {
                    Id = default(string),
                    AccessFailedCount = default(int),
                    ConcurrencyStamp = default(string),
                    Email = default(string),
                    EmailConfirmed = default(int),
                    LockoutEnabled = default(int),
                    LockoutEnd = default(string),
                    NormalizedEmail = default(string),
                    NormalizedUserName = default(string),
                    PasswordHash = default(string),
                    PhoneNumber = default(string),
                    PhoneNumberConfirmed = default(int),
                    SecurityStamp = default(string),
                    TwoFactorEnabled = default(int),
                    UserName = default(string),
                })
                .WithPrimaryKey(table => table.Id)
                .GetTable();

            var userRolesTable = modeler.Create()
                .Table(
                "AspNetUserRoles",
                () => new
                {
                    UserId = default(string),
                    RoleId = default(string)
                })
                .WithForeignKey(userTable, (table, referenceTable) => table.RoleId == referenceTable.Id)
                .WithForeignKey(rolesTable, (table, referenceTable) => table.UserId == referenceTable.Id);

            var userLoginsTable = modeler.Create()
                .Table(
                    "AspNetUserLogins",
                    () => new
                    {
                        LoginProvider = default(string),
                        ProviderKey = default(string),
                        ProviderDisplayName = default(string),
                        UserId = default(string),
                    })
                    //.WithCompositeKey(table => table.LoginProvider, table => table.ProviderKey)
                    .WithForeignKey(userTable, (table, referenceTable) => table.UserId == referenceTable.Id);

            var userClaimsTable = modeler.Create()
                .Table(
                "AspNetUserClaims",
                () => new
                {
                    Id = default(int),
                    ClaimType = default(string),
                    ClaimValue = default(string),
                    UserId = default(string),
                })
                .WithPrimaryKey(table => table.Id)
                .WithForeignKey(userTable, (table, referenceTable) => table.UserId == referenceTable.Id);
        }

        public Task Rollback(IDataProvider provider)
        {
            throw new NotImplementedException();
        }
    }
}
