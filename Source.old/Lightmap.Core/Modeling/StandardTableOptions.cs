using System;
using System.Linq.Expressions;

namespace Lightmap.Modeling
{
    public class StandardTableOptions
    {
        private Column owner;

        public StandardTableOptions(Column owner)
        {
            this.owner = owner;
        }

        public ITable GetTable()
        {
            return this.owner.Owner;
        }

        public StandardTableOptions AsPrimaryKey()
        {
            this.owner.AddDefinition(SqlStatements.Constraints.PrimaryKey, this.owner.Name);
            return this;
        }

        public StandardTableOptions WithForeignKey(ITable referenceTable, string constrainedColumn)
        {
            this.owner.Owner.GetTableModeler().AddDefinition(SqlStatements.Constraints.ForeignKey, this.owner.Name);
            this.owner.Owner.GetTableModeler().AddDefinition(SqlStatements.Constraints.ReferencesTable, referenceTable.Name);
            this.owner.Owner.GetTableModeler().AddDefinition(SqlStatements.Constraints.ReferencesColumn, constrainedColumn);
            return this;
        }

        public StandardTableOptions WithForeignKey(ITable referenceTable, Column constrainedColumn)
        {
            this.owner.Owner.GetTableModeler().AddDefinition(SqlStatements.Constraints.ForeignKey, this.owner.Name);
            this.owner.Owner.GetTableModeler().AddDefinition(SqlStatements.Constraints.ReferencesTable, referenceTable.Name);
            this.owner.Owner.GetTableModeler().AddDefinition(SqlStatements.Constraints.ReferencesColumn, constrainedColumn.Name);
            return this;
        }

        public StandardTableOptions IsUnique()
        {
            this.owner.AddDefinition(SqlStatements.Constraints.Unique, this.owner.Name);
            return this;
        }

        public StandardTableOptions WithDefaultValue<TValue>(TValue value)
        {
            throw new NotImplementedException();
        }

        public StandardTableOptions WithValueCheck(Expression<Func<string, bool>> condition)
        {
            throw new NotImplementedException();
        }

        public StandardTableOptions DisallowNulls()
        {
            this.owner.AddDefinition(SqlStatements.Constraints.NotNull, this.owner.Name);
            return this;
        }

        public StandardTableOptions AllowNulls()
        {
            if (string.IsNullOrEmpty(this.owner.GetDefinition(SqlStatements.Constraints.NotNull)))
            {
                return this;
            }

            this.owner.RemoveDefinition(SqlStatements.Constraints.NotNull);
            return this;
        }
    }
}
