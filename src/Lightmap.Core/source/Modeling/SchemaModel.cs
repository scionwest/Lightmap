namespace Lightmap.Modeling
{
    public struct SchemaModel : ISchemaModel
    {
        public SchemaModel(string name) => this.Name = name;

        public string Name { get; }
    }
}
