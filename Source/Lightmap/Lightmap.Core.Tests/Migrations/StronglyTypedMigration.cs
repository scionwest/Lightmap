using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lightmap.Core.Tests.Models;
using Lightmap.Modeling;

namespace Lightmap.Core.Tests
{
    public class StronglyTypedMigration : IMigration
    {
        public Task Apply()
        {
            throw new NotImplementedException();
        }

        public void Configure(Lightmap.Modeling2.IDatabaseModeler modeler)
        {
            throw new NotImplementedException();
        }

        public void Configure(DatabaseModeler modeler)
        {
            var rolesTable = modeler.Create().Table<AspNetRoles>()
                .WithPrimaryKey(table => table.Id)
                .GetTable();

            modeler.Create().Table<AspNetRoleClaims>()
                .WithPrimaryKey(table => table.Id)
                .WithForeignKey(rolesTable, (table, referenceTable) => table.RoleId == referenceTable.Id);

            var usersTable = modeler.Create().Table<AspNetUsers>()
                .WithPrimaryKey(table => table.Id)
                .GetTable();

            var userRolesTable = modeler.Create().Table<AspNetUserRoles>()
                .WithForeignKey(rolesTable, (table, referenceTable) => table.RoleId == referenceTable.Id)
                .WithForeignKey(usersTable, (table, referenceTable) => table.UserId == referenceTable.Id);

            modeler.Create().Table<AspNetUserLogins>()
                //.WithClusteredPrimaryKey(table => table.ProviderKey, table => table.LoginProvider)
                .WithForeignKey(usersTable, (table, referenceTable) => table.UserId == referenceTable.Id);

            modeler.Create().Table<AspNetUserClaims>()
                .WithPrimaryKey(table => table.Id)
                .WithForeignKey(usersTable, (table, referenceTable) => table.UserId == referenceTable.Id);
        }

        public Task Rollback()
        {
            throw new NotImplementedException();
        }
    }
}
