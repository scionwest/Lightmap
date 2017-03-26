using Lightmap.Modeling;

namespace Lightmap.Sqlite.Tests.Migrations
{
    [MigrationVersion(0)]
    public class Untyped_SingleTable_SingleColumn_NoConstraints : IMigration
    {
        public Untyped_SingleTable_SingleColumn_NoConstraints(IDataModel model) => this.DataModel = model;

        public IDataModel DataModel { get; }

        public void Apply()
        {
            this.DataModel.AddTable("User")
                .AddColumn(typeof(int), "Id");
        }

        public void Revert()
        {
        }
    }
}
