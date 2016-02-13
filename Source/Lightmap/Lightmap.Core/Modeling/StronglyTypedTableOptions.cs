using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Lightmap.Modeling
{
    public class StronglyTypedTableOptions<TTable>
    {
        private Table<TTable> owner;

        public StronglyTypedTableOptions(Table<TTable> owner)
        {
            this.owner = owner;
        }

        public ITable<TTable> GetTable()
        {
            return this.owner;
        }

        public StronglyTypedTableOptions<TTable> WithPrimaryKey<TColumn>(Expression<Func<TTable, TColumn>> columnSelector)
        {
            var memberExpression = columnSelector.Body as MemberExpression;
            if (memberExpression == null)
            {
                throw new NotSupportedException($"The selector provided is not supported. You must return a property that represents a column from the table definiton provided to you.");
            }

            string columnName = memberExpression.Member.Name;
            IColumn column = this.owner.GetColumn(columnName);
            column.GetColumnModeler().AddDefinition(SqlStatements.Constraints.PrimaryKey, columnName);
            return this;
        }

        public ForeignKeyConstraints<StronglyTypedTableOptions<TTable>, TTable> WithForeignKey<TReferenceTable, TConstraint>(ITable<TReferenceTable> referenceTable, Expression<Func<TTable, TReferenceTable, TConstraint>> constraint)
        {
            var equalsExpression = constraint.Body as BinaryExpression;
            if (equalsExpression == null)
            {
                throw new NotSupportedException($"The {equalsExpression.NodeType.GetType().Name} expression usage is not supported. You must map a property off the current table, to your reference table using the equality operator. An example of this is 'table.Id == referenceTable.UserId");
            }

            var leftExpression = equalsExpression.Left as MemberExpression;
            var rightExpression = equalsExpression.Right as MemberExpression;

            if (leftExpression == null || rightExpression == null)
            {
                throw new NotSupportedException("You must map a property off both the current table and the reference table to represent the foreign key column constraints.");
            }

            Type owningTableType = TypeCache.GetGenericParameters(this.owner.GetType()).FirstOrDefault();
            if (owningTableType == null)
            {
                throw new InvalidOperationException("The owning table was not defined using a generic Type or an expression.");
            }

            if (leftExpression.Member.DeclaringType == owningTableType)
            {
                this.owner.AddDefinition(SqlStatements.Constraints.ForeignKey, leftExpression.Member.Name);
            }
            else if (rightExpression.Member.DeclaringType == owningTableType)
            {
                this.owner.AddDefinition(SqlStatements.Constraints.ForeignKey, rightExpression.Member.Name);
            }
            else
            {
                throw new InvalidOperationException("You can not map a foreign key constraint to a member of an object that does not belong to the table currently being modeled.");
            }

            // Determine which expression is for the reference table
            Type referenceDefinition = TypeCache.GetGenericParameters(referenceTable.GetType()).FirstOrDefault();
            if (leftExpression.Member.DeclaringType.Name == referenceDefinition.Name)
            {
                this.owner.AddDefinition(SqlStatements.Constraints.ReferencesTable, referenceTable.Name);
                this.owner.AddDefinition(SqlStatements.Constraints.ReferencesColumn, leftExpression.Member.Name);
            }
            if (rightExpression.Member.DeclaringType.Name == referenceDefinition.Name)
            {
                this.owner.AddDefinition(SqlStatements.Constraints.ReferencesTable, referenceTable.Name);
                this.owner.AddDefinition(SqlStatements.Constraints.ReferencesColumn, rightExpression.Member.Name);
            }
            else
            {
                throw new InvalidOperationException("You can not reference a table/column with a foreign key constraint if the table has yet to be modeled as part of the database model.");
            }

            return new ForeignKeyConstraints<StronglyTypedTableOptions<TTable>, TTable>(this.owner);
        }

        public StronglyTypedTableOptions<TTable> WithUniqueColumn<TColumn>(Expression<Func<TTable, TColumn>> columnSelector)
        {
            throw new NotImplementedException();
        }

        public StronglyTypedTableOptions<TTable> WithDefaultColumnValue<TColumn, TValue>(TValue value, Expression<Func<TTable, TColumn>> columnSelector)
        {
            return this;
        }

        public StronglyTypedTableOptions<TTable> DisallowNullsOnColumn()
        {
            this.owner.AddDefinition(SqlStatements.Constraints.NotNull, this.owner.Name);
            return this;
        }

        public StronglyTypedTableOptions<TTable> AllowNullsOnColumn()
        {
            if (string.IsNullOrEmpty(this.owner.GetTableModeler().GetDefinition(SqlStatements.Constraints.NotNull)))
            {
                return this;
            }

            this.owner.GetTableModeler().RemoveDefinition(SqlStatements.Constraints.NotNull);
            return this;
        }

        public StronglyTypedTableOptions<TTable> CheckValueOnColumn<TColumn>(Expression<Func<TTable, TColumn>> columnSelector, Expression<Func<TTable, bool>> condition)
        {
            return this;
        }
    }
}
