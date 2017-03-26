namespace Lightmap.Modeling
{
    public interface IDatabaseModelingManager
    {
        IDatabaseMigrator DataModelMigration { get; }

        IDataModel DataModel { get; set; }
    }
}