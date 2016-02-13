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

        public StronglyTypedTableOptions<TTable> WithPrimaryKey<TColumn>(Expression<Func<TTable, TColumn>> keySelector)
        {
            var memberExpression = keySelector.Body as MemberExpression;
            if (memberExpression == null)
            {
                throw new NotSupportedException($"The selector provided is not supported. You must return a property that represents a column from the table definiton provided to you.");
            }

            string columnName = memberExpression.Member.Name;
            Column column = this.owner.GetColumn(columnName);
            column.AddDefinition(SqlStatements.Constraints.PrimaryKey, columnName);
            return this;
        }

        public StronglyTypedTableOptions<TTable> WithForeignKey<TReferenceTable, TConstraint>(ITable<TReferenceTable> referenceTable, Expression<Func<TTable, TReferenceTable, TConstraint>> constraint)
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
            
            // Determine which expression is for the owning table.
            if (leftExpression.Member.DeclaringType.Name == this.owner.Name)
            {
                // Add the Property/Column name as the Foreign Key for this table.
                this.owner.AddDefiniton(SqlStatements.Constraints.ForeignKey, leftExpression.Member.Name);
            }
            else if (rightExpression.Member.DeclaringType.Name == this.owner.Name)
            {
                this.owner.AddDefiniton(SqlStatements.Constraints.ForeignKey, rightExpression.Member.Name);
            }
            else
            {
                throw new InvalidOperationException("You can not map a foreign key constraint to a member of an object that does not belong to the table currently being modeled.");
            }

            // Determine which expression is for the reference table
            if (leftExpression.Member.DeclaringType.Name == referenceTable.Name)
            {
                this.owner.AddDefiniton(SqlStatements.Constraints.ReferencesTable, referenceTable.Name);
                this.owner.AddDefiniton(SqlStatements.Constraints.ReferencesColumn, leftExpression.Member.Name);
            }
            if (rightExpression.Member.DeclaringType.Name == referenceTable.Name)
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

        public StronglyTypedTableOptions<TTable> WithUniqueColumn<TColumn>(Expression<Func<TTable, TColumn>> columnSelector)
        {
            throw new NotImplementedException();
        }

        public StronglyTypedTableOptions<TTable> DisallowNulls()
        {
            throw new NotImplementedException();
        }

        public StronglyTypedTableOptions<TTable> AllowNulls()
        {
            throw new NotImplementedException();
        }
    }
}
