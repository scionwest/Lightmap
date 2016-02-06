using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lightmap.Core.Tests.Models;
using Lightmap.Modeling2;

namespace Lightmap.Core.Tests
{
    public class DatabaseModel : IMigration
    {
        public Task Apply()
        {
            throw new NotImplementedException();
        }

        public void Configure(IDatabaseModeler modeler)
        {
            modeler.Create()
                .Table<AspNetRoles>()
                    .UsePrimaryKey(table => table.Id);

            

            //modeler.Create()
            //    .Table<AspNetRoleClaims>()
            //        .UsePrimaryKey(table => table.Id)
            //        .UseForeignKey(nameof(AspNetRoles), table => new { );
        }

        public Task Rollback()
        {
            throw new NotImplementedException();
        }
    }
}
