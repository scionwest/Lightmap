namespace Lightmap.Modeling
{
    public interface IColumnBuilderUntyped : IColumnBuilder
    {
        ITableBuilder GetOwner();

        IColumnBuilderUntyped IsPrimaryKey();

        IColumnBuilderUntyped WithForeignKey(string table, string columnName, ISchemaModel schema = null);

        IColumnBuilderUntyped WithForeignKey(IColumnModel referenceColumn);

        IColumnBuilderUntyped Unique();

        IColumnBuilderUntyped IsNullable();
    }
}
