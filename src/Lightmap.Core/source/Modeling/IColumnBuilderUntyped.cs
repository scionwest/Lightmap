namespace Lightmap.Modeling
{
    public interface IColumnBuilderUntyped : IColumnBuilder
    {
        ITableBuilder GetOwner();

        IColumnBuilderUntyped IsPrimaryKey();

        IColumnBuilderUntyped WithForeignKey(IColumnModel referenceColumn);

        IColumnBuilderUntyped Unique();

        IColumnBuilderUntyped IsNullable();
    }
}
