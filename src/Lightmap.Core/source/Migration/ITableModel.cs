namespace Lightmap.Migration
{
    public interface ITableModel
    {
        string Name { get; }

        ISchemaModel Schema { get;  }

        string FullyQualifiedName { get; }

        IColumnModel[] Columns { get; }
    }
}