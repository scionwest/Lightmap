namespace Lightmap.Modeling
{
    public interface IColumnBuilderUntyped : IColumnBuilder
    {
        ITableBuilder GetOwner();

        IColumnBuilder IsPrimaryKey();

        IColumnBuilder WithForeignKey(IColumnModel referenceColumn);

        IColumnBuilder Unique();

        IColumnBuilder IsNullable();
    }
}
