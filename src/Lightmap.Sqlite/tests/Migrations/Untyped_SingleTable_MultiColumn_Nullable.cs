using System;
using Lightmap.Modeling;

namespace Lightmap.Sqlite.Tests.Migrations
{
    [MigrationVersion(0)]
    public class Untyped_SingleTable_MultiColumn_Nullable : IMigration
    {
        public Untyped_SingleTable_MultiColumn_Nullable(IDataModel model) => this.DataModel = model;

        public IDataModel DataModel { get; }

        public void Apply()
        {
            this.DataModel.AddTable("main", "User")
                .AddColumn(typeof(int), "Id")
                    .GetOwner()
                .AddColumn(typeof(string), "Name")
                    .IsNullable()
                    .GetOwner()
                .AddColumn(typeof(DateTime), "CreatedOn");
        }

        public void Revert()
        {
            this.DataModel.DropTable("main", "User");
        }
    }
}
