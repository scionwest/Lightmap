namespace Lightmap.Modeling
{
    public interface ITableModel
    {
        string Name { get; }

        ISchemaModel Schema { get;  }

        string FullyQualifiedName { get; }

        IColumnModel[] GetColumns();
    }
}