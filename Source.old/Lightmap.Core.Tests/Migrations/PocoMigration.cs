using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Lightmap.Core.Tests.Models;
using Lightmap.Modeling;

namespace Lightmap.Core.Tests
{
    [MigrationVersion(1)]
    public class PocoMigration : IMigration
    {
        public void Configure(IDatabaseModeler modeler)
        {
            var rolesTableDefinition = modeler.Create().Table<AspNetRoles>()
                .WithPrimaryKey(table => table.Id)
                .GetTable();

            modeler.Create().Table<AspNetRoleClaims>()
                .WithPrimaryKey(table => table.Id)
                .WithForeignKey(rolesTableDefinition, (claimTable, roleTable) => claimTable.RoleId == roleTable.Id)
                    .OnDelete().Cascade();

            var usersTableDefinition = modeler.Create().Table<AspNetUsers>()
                .WithPrimaryKey(userTable => userTable.Id)
                .GetTable();

            var userRolesTable = modeler.Create().Table<AspNetUserRoles>()
                .CheckValueOnColumn(table => table.RoleId, table => table.RoleId != string.Empty)
                .WithForeignKey(rolesTableDefinition, (userRoleTable, roleTable) => userRoleTable.RoleId == roleTable.Id)
                    .OnDelete().Restrict()
                .WithForeignKey(usersTableDefinition, (userRoleTable, userTable) => userRoleTable.UserId == userTable.Id)
                    .OnDelete().Cascade();

            modeler.Create().Table<AspNetUserLogins>()
                //.WithClusteredPrimaryKey(table => table.ProviderKey, table => table.LoginProvider)
                .WithForeignKey(usersTableDefinition, (userLoginTable, userTable) => userLoginTable.UserId == userTable.Id);

            modeler.Create().Table<AspNetUserClaims>()
                .WithPrimaryKey(table => table.Id)
                .WithForeignKey(usersTableDefinition, (userClaimTable, userTable) => userClaimTable.UserId == userTable.Id)
                    .OnDelete().Cascade();
        }
    }
}
