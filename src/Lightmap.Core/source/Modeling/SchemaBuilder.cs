namespace Lightmap.Modeling
{
    public class SchemaBuilder : ISchemaBuilder
    {
        private readonly IDataModel dataModel;

        public SchemaBuilder(IDataModel currentModel)
        {
            this.dataModel = currentModel;
        }

        public string Name { get; set; }

        public ISchemaModel GetSchemaModel() => new SchemaModel(this.Name);
    }
}
