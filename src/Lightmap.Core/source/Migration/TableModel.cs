namespace Lightmap.Migration
{
    internal struct TableModel : ITableModel
    {
        private IColumnModel[] columns;
        private ISchemaModel schemaModel;

        public TableModel(ISchemaModel schemaModel, string tableName, IColumnModel[] columns)
        {
            this.columns = columns;
            this.schemaModel = schemaModel;
            this.Name = tableName;
        }

        public string Name { get; }

        public string FullyQualifiedName => $"{this.Schema.Name}.{this.Name}";

        public IColumnModel[] Columns => this.columns;

        public ISchemaModel Schema => this.schemaModel;
    }
}
