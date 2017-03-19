using System;

namespace Lightmap.Modeling
{
    public class ModelingManager : IDatabaseModelingManager
    {
        public ModelingManager(IDatabaseMigrator migrator, IDataModel model)
        {
            this.DataModelMigration = migrator ?? throw new ArgumentNullException(nameof(migrator), "You can not provide the data model with a null migrator.");
            this.DataModel = model ?? throw new ArgumentNullException(nameof(model), "You must provide a valid data model for use with the modeling manager.");
        }

        public IDatabaseMigrator DataModelMigration { get; }

        public IDataModel DataModel { get; set; }
    }
}
