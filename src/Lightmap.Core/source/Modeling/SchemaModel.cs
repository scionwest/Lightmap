namespace Lightmap.Modeling
{
    public struct SchemaModel : ISchemaModel
    {
        public SchemaModel(string name) => this.Name = name;

        public string Name { get; }

        public override bool Equals(object obj)
        {
            if (!(obj is SchemaModel))
            {
                return false;
            }

            SchemaModel model = (SchemaModel)obj;
            return model.Name == this.Name;
        }

        public static bool operator ==(SchemaModel model1, SchemaModel model2)
        {
            return model1.Equals(model2);
        }

        public static bool operator !=(SchemaModel model1, SchemaModel model2)
        {
            return !model1.Equals(model2);
        }
    }
}
