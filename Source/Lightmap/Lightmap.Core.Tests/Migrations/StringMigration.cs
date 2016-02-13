using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lightmap.Modeling;

namespace Lightmap.Core.Tests.Migrations
{
    [MigrationVersion(1)]
    public class StringMigration : IMigration
    {
        public Task Apply(IDataProvider provider)
        {
            return provider.ProcessMigration(this);
        }

        public void Configure(IDatabaseModeler modeler)
        {
            var rolesTable = modeler.Create().Table("AspNetRoles");
            rolesTable.WithColumn<string>("Id").AsPrimaryKey();
            rolesTable.WithColumn<string>("ConcurrencyStamp");
            rolesTable.WithColumn<string>("Name");
            rolesTable.WithColumn<string>("NormalizedName");

            var roleClaimsTable = modeler.Create().Table("AspNetRoleClaims");
            roleClaimsTable.WithColumn<int>("Id").AsPrimaryKey();
            roleClaimsTable.WithColumn<string>("ClaimType");
            roleClaimsTable.WithColumn<string>("ClaimValue");
            roleClaimsTable.WithColumn<string>("RoleId").WithForeignKey(rolesTable, "Id");

            var userTable = modeler.Create().Table("AspNetUsers");
            userTable.WithColumn<string>("Id").AsPrimaryKey();
            userTable.WithColumn<int>("AccessFailedCount");
            userTable.WithColumn<string>("ConcurrencyStamp");
            userTable.WithColumn<string>("Email");
            userTable.WithColumn<int>("EmailConfirmed");
            userTable.WithColumn<int>("LockoutEnabled");
            userTable.WithColumn<string>("LockoutEnd");
            userTable.WithColumn<string>("NormalizedEmail");
            userTable.WithColumn<string>("NormalizedUserName");
            userTable.WithColumn<string>("PasswordHash");
            userTable.WithColumn<string>("PhoneNumber");
            userTable.WithColumn<int>("PhoneNumberConfirmed");
            userTable.WithColumn<string>("SecurityStamp");
            userTable.WithColumn<int>("TwoFactorEnabled");
            userTable.WithColumn<string>("UserName");

            var userRolesTable = modeler.Create().Table("AspNetUserRoles");
            userRolesTable.WithColumn<string>("UserId").WithForeignKey(userTable, "Id");
            userRolesTable.WithColumn<string>("RoleId").WithForeignKey(rolesTable, "Id");

            var userLoginsTable = modeler.Create().Table("AspNetUserLogins");
            userLoginsTable.WithColumn<string>("LoginProvider");
            userLoginsTable.WithColumn<string>("ProviderKey");
            userLoginsTable.WithColumn<string>("ProviderDisplayName");
            userLoginsTable.WithColumn<string>("UserId").WithForeignKey(userTable, "Id");
            // userLoginsTable.HasCompositeKey("LoginProvider", "ProviderKey");

            var userClaimsTable = modeler.Create().Table("AspNetUserClaims");
            userClaimsTable.WithColumn<int>("Id").AsPrimaryKey();
            userClaimsTable.WithColumn<string>("ClaimType");
            userClaimsTable.WithColumn<string>("ClaimValue");
            userClaimsTable.WithColumn<string>("UserId").WithForeignKey(userTable, "Id");
        }

        public Task Rollback(IDataProvider provider)
        {
            throw new NotImplementedException();
        }
    }
}
