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
            return new Table<TTableDefinition>(this.owner.GetDatabaseModeler());
        }

        public TableExpressionDefinitonOptions<TTableDefinition> WithPrimaryKey<TColumn>(Expression<Func<TTableDefinition, TColumn>> columnSelector)
        {
            return this;
        }

        public TableExpressionDefinitonOptions<TTableDefinition> WithForeignKey<TReferenceTable, TColumnMapping>(Table<TReferenceTable> referenceTable, Expression<Func<TTableDefinition, TReferenceTable, TColumnMapping>> constraint)
        {
            var binaryExpression = constraint as BinaryExpression;
            var leftLeaf = binaryExpression.Left as MemberExpression;
            var rightLeaf = binaryExpression.Right as MemberExpression;


            // referenceTable parameter will always be null. We must rely on the generic argument instead.
            if (leftLeaf.Member.DeclaringType == typeof(TReferenceTable))
            {
                // store reference
            }

            if (rightLeaf.Member.DeclaringType == typeof(TReferenceTable))
            {
                // store reference
            }

            if (leftLeaf.Member.DeclaringType == typeof(TTableDefinition))
            {
                // store constraint
            }

            if (rightLeaf.Member.DeclaringType == typeof(TTableDefinition))
            {
                // store
            }

            return this;
        }
    }
}
