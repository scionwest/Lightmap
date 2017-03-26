using System;
using System.Linq.Expressions;

namespace Lightmap.Modeling
{
    internal class ColumnBuilderStronglyTyped<TTableType> : ColumnBuilderBase, IColumnBuilderStronglyTyped<TTableType>
    {
        private readonly TableBuilder<TTableType> tableBuilder;
        private readonly Type tableType;

        public ColumnBuilderStronglyTyped(string columnName, Type dataType, TableBuilder<TTableType> tableBuilder)
            : base(columnName, dataType, tableBuilder)
        {
            this.tableBuilder = tableBuilder;
            this.tableType = typeof(TTableType);
            base.AddColumnDefinition(ColumnDefinitions.NotNull, columnName);
        }

        public ITableBuilder<TTableType> GetOwner() => this.tableBuilder;

        public IColumnBuilderStronglyTyped<TTableType> IsNullable()
        {
            base.GetColumnDefinition().Remove(ColumnDefinitions.NotNull);
            return this;
        }

        public IColumnBuilderStronglyTyped<TTableType> IsPrimaryKey()
        {
            base.AddColumnDefinition(ColumnDefinitions.PrimaryKey, this.ColumnName);
            return this;
        }

        public IColumnBuilderStronglyTyped<TTableType> Unique()
        {
            base.AddColumnDefinition(ColumnDefinitions.Unique, this.ColumnName);
            return this;
        }

        public IColumnBuilderStronglyTyped<TTableType> WithForeignKey(IColumnBuilder column)
        {
            base.AddColumnDefinition(ColumnDefinitions.ForeignKey, this.ColumnName);
            base.AddColumnDefinition(ColumnDefinitions.ReferencesSchema, column.TableBuilder.Schema?.Name);
            base.AddColumnDefinition(ColumnDefinitions.ReferencesTable, column.TableBuilder.TableName);
            base.AddColumnDefinition(ColumnDefinitions.ReferencesColumn, column.ColumnName);
            return this;
        }

        public IColumnBuilderStronglyTyped<TTableType> WithForeignKey<TReferenceTable>(Expression<Func<TTableType, TReferenceTable, bool>> columnSelector, ISchemaModel schema = null)
        {
            var selectorBody = columnSelector.Body as BinaryExpression;
            var leftExpression = selectorBody?.Left as MemberExpression;
            var rightExpression = selectorBody?.Right as MemberExpression;
            if (leftExpression == null || rightExpression == null)
            {
                throw new NotSupportedException($"The expression given as the column selector is not supported. You must do a comparison expression between the two column properties you which to use as foreign key references.");
            }

            // Find the property on the TTableType to ensure we're associating the same model property as what was 
            // defined for the column currently being altered. For instance, you can't call
            // .AlterColumn(model => model.Id) and then use .WithForeignKey to associate a property other than Id as the FK.
            if ((leftExpression.Member.DeclaringType == this.tableType && leftExpression.Member.Name == this.ColumnName) ||
                (rightExpression.Member.DeclaringType == this.tableType && rightExpression.Member.Name == this.ColumnName))
            {
                base.AddColumnDefinition(ColumnDefinitions.ForeignKey, this.ColumnName);
            }
            else
            {
                throw new InvalidOperationException("You must map a foreign key reference to the current table and column being altered.");
            }

            Type referencedType = typeof(TReferenceTable);
            if (leftExpression.Member.DeclaringType == referencedType)
            {
                base.AddColumnDefinition(ColumnDefinitions.ReferencesColumn, leftExpression.Member.Name);
            }
            else if (rightExpression.Member.DeclaringType == referencedType)
            {
                base.AddColumnDefinition(ColumnDefinitions.ReferencesColumn, rightExpression.Member.Name);
            }
            else
            {
                throw new InvalidOperationException("You must reference a property off of the generic argument given to this method call.");
            }

            base.AddColumnDefinition(ColumnDefinitions.ReferencesTable, referencedType.Name);
            if (schema != null)
            {
                base.AddColumnDefinition(ColumnDefinitions.ReferencesSchema, schema.Name);
            }

            return this;
        }

        public IColumnBuilderStronglyTyped<TTableType> WithForeignKey<TReferenceTable>(Expression<Func<TTableType, TReferenceTable, bool>> constraint, ITableBuilder<TReferenceTable> referenceTable, ISchemaModel schema = null)
        {
            this.WithForeignKey<TReferenceTable>(constraint, schema);

            // In the event the table has a custom name, we don't rely only on the generic argument Type name.
            base.GetColumnDefinition()[ColumnDefinitions.ReferencesTable] = referenceTable.TableName;
            return this;
        }
    }
}
