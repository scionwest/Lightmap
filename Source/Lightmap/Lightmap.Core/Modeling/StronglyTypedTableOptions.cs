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

        public Table<TTable> GetTable()
        {
            return this.owner;
        }

        public StronglyTypedTableOptions<TTable> WithPrimaryKey<TColumn>(Expression<Func<TTable, TColumn>> keySelector)
        {
            return this;
        }

        public StronglyTypedTableOptions<TTable> WithForeignKey<TReferenceTable, TConstraint>(Table<TReferenceTable> referenceTable, Expression<Func<TTable, TReferenceTable, TConstraint>> constraint)
        {
            return this;
        }
    }
}
