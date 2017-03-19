namespace Lightmap.Migration
{
    internal struct TableModel : ITableModel
    {
        private ITableColumn[] columns;
        private ISchemaModel schemaModel;

        public TableModel(ISchemaModel schemaModel, string tableName, ITableColumn[] columns)
        {
            this.columns = columns;
            this.schemaModel = schemaModel;
            this.Name = tableName;
        }

        public string Name { get; }

        public ITableColumn[] Columns => this.columns;

        public ISchemaModel Schema => this.schemaModel;
    }
}
