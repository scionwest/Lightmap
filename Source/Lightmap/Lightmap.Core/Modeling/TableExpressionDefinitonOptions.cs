using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Lightmap.Modeling
{
    public class TableExpressionDefinitonOptions<TTableDefinition>
    {
        private Table<TTableDefinition> owner;

        public TableExpressionDefinitonOptions(Table<TTableDefinition> owningTable)
        {
            this.owner = owningTable;
        }

        public Table<TTableDefinition> GetTable()
        {
            return owner;
        }

        public TableExpressionDefinitonOptions<TTableDefinition> WithPrimaryKey<TColumn>(Expression<Func<TTableDefinition, TColumn>> columnSelector)
        {
            MemberExpression columnExpression = columnSelector.Body as MemberExpression;
            Column column = this.owner.GetColumn(columnExpression.Member.Name);
            column.AddDefinition(SqlStatements.Constraints.PrimaryKey, column.Name);
            return this;
        }

        public TableExpressionDefinitonOptions<TTableDefinition> WithForeignKey<TReferenceTable, TColumnMapping>(Table<TReferenceTable> referenceTable, Expression<Func<TTableDefinition, TReferenceTable, TColumnMapping>> constraint)
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
                this.owner.AddDefiniton(SqlStatements.Constraints.ForeignKey, leftExpression.Member.Name);
            }
            else if (rightExpression.Member.DeclaringType == owningTableType)
            {
                this.owner.AddDefiniton(SqlStatements.Constraints.ForeignKey, rightExpression.Member.Name);
            }
            else
            {
                throw new InvalidOperationException("You can not map a foreign key constraint to a member of an object that does not belong to the table currently being modeled.");
            }

            // Determine which expression is for the reference table
            Type referenceDefinition = TypeCache.GetGenericParameters(referenceTable.GetType()).FirstOrDefault();
            if (leftExpression.Member.DeclaringType.Name == referenceDefinition.Name)
            {
                this.owner.AddDefiniton(SqlStatements.Constraints.ReferencesTable, referenceTable.Name);
                this.owner.AddDefiniton(SqlStatements.Constraints.ReferencesColumn, leftExpression.Member.Name);
            }
            if (rightExpression.Member.DeclaringType.Name == referenceDefinition.Name)
            {
                this.owner.AddDefiniton(SqlStatements.Constraints.ReferencesTable, referenceTable.Name);
                this.owner.AddDefiniton(SqlStatements.Constraints.ReferencesColumn, rightExpression.Member.Name);
            }
            else
            {
                throw new InvalidOperationException("You can not reference a table/column with a foreign key constraint if the table has yet to be modeled as part of the database model.");
            }

            return this;
        }
    }
}
