using Lightmap.Modeling;

namespace Lightmap.Sqlite.Tests.Migrations
{
    [MigrationVersion(0)]
    public class Untyped_SingleTable_SingleColumn_PrimaryKey : IMigration
    {
        public Untyped_SingleTable_SingleColumn_PrimaryKey(IDataModel model) => this.DataModel = model;

        public IDataModel DataModel { get; }

        public void Apply()
        {
            this.DataModel.AddTable("User")
                .AddColumn(typeof(int), "Id")
                .IsPrimaryKey();
        }

        public void Revert()
        {
        }
    }
}
