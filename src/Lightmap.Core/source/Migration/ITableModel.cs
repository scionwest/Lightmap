namespace Lightmap.Migration
{
    public interface ITableModel
    {
        string Name { get; }

        ISchemaModel Schema { get;  }

        ITableColumn[] Columns { get; }
    }
}