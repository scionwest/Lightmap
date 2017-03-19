using System.Linq;

namespace Lightmap.Modeling
{
    internal struct TableModel : ITableModel
    {
        private ISchemaModel schemaModel;
        private ITableBuilder owningBuilder;

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
    }
}
