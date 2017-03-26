using System.Linq;

namespace Lightmap.Modeling
{
    internal struct TableModel : ITableModel
    {
        private readonly ISchemaModel schemaModel;
        private readonly ITableBuilder owningBuilder;

        public TableModel(ISchemaModel schemaModel, string tableName, ITableBuilder owningBuilder)
        {
            this.owningBuilder = owningBuilder;
            this.schemaModel = schemaModel;
            this.Name = tableName;
        }

        public string Name { get; }

        public string FullyQualifiedName => $"{this.Schema.Name}.{this.Name}";

        public IColumnModel[] GetColumns() => this.owningBuilder.GetColumns().Select(columnBuilder => columnBuilder.GetModel()).ToArray();

        public ISchemaModel Schema => this.schemaModel;

        public override int GetHashCode() => this.Name.GetHashCode() ^ (this.Schema?.GetHashCode() ?? 0) ^ this.FullyQualifiedName.GetHashCode() ^ this.owningBuilder.GetHashCode();

        public override bool Equals(object obj)
        {
            if (!(obj is TableModel))
            {
                return false;
            }

            TableModel model = (TableModel)obj;
            return model.Name == this.Name;
        }

        public static bool operator ==(TableModel model1, TableModel model2)
        {
            return model1.Equals(model2);
        }

        public static bool operator !=(TableModel model1, TableModel model2)
        {
            return !model1.Equals(model2);
        }
    }
}
