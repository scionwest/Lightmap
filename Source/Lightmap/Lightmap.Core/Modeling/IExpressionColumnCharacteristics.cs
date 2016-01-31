using System;
using System.Linq.Expressions;

namespace Lightmap.Modeling
{
    public interface IExpressionColumnCharacteristics<TTableDefiniton> : IEntityBuilder
    {
        IExpressionColumnCharacteristics<TTableDefiniton> UsePrimaryKey<TColumn>(Expression<Func<TTableDefiniton, TColumn>> columnSelector);

        // TODO: Add overload of UseForeignKey<TTable, TForeignKey> for an alternative to the relatedTable string.
        IExpressionColumnCharacteristics<TTableDefiniton> UseForeignKey<TForeignKey>(string relatedTable, Expression<Func<TTableDefiniton, TForeignKey>> constraint);

        //IColumnDefinitionResult WithIndex();

        //IColumnDefinitionResult WithIndex(string name);

        //IColumnDefinitionResult NotNull();

        //IColumnDefinitionResult IsUnique();
    }
}
