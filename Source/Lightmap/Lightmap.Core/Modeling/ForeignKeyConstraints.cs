using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lightmap.Modeling
{
    public class ForeignKeyConstraints<TTableOptions, TTable> : StronglyTypedTableOptions<TTable> where TTableOptions : StronglyTypedTableOptions<TTable>
    {
        private Table owner;

        public ForeignKeyConstraints(Table<TTable> owner) :base(owner)
        {
            this.owner = owner;
        }

        public ConstraintOptions<TTableOptions> OnDelete()
        {
            return new ConstraintOptions<TTableOptions>();
        }

        public ConstraintOptions<TTableOptions> OnUpdate()
        {
            throw new NotImplementedException();
        }
    }

    public class ConstraintOptions<TTableOptions>
    {
        private TTableOptions tableOptions;

        public TTableOptions NoAction()
        {
            return tableOptions;
        }

        public TTableOptions Restrict()
        {
            return tableOptions;
        }

        public TTableOptions SetNull()
        {
            return tableOptions;
        }

        public TTableOptions SetDefault()
        {
            return tableOptions;
        }

        public TTableOptions Cascade()
        {
            return tableOptions;
        }
    }
}
