namespace Lightmap.Migration
{
    public interface ISchemaBuilder
    {
        string Name { get; set; }

        // TODO: Add GetTables() so we can browse all tables owned by this schema.
        ISchemaModel GetSchemaModel();
    }
}