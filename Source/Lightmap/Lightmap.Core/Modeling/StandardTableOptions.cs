namespace Lightmap.Modeling
{
    public class StandardTableOptions
    {
        private Column owner;

        public StandardTableOptions(Column owner)
        {
            this.owner = owner;
        }

        public StandardTableOptions AsPrimaryKey()
        {
            this.owner.AddDefiniton(SqlStatements.Constraints.PrimaryKey, this.owner.Name);
            return this;
        }

        public StandardTableOptions WithForeignKey(Table referenceTable, string constrainedColumn)
        {
            this.owner.AddDefiniton(SqlStatements.Constraints.ForeignKey, constrainedColumn);
            this.owner.AddDefiniton(SqlStatements.Constraints.References, referenceTable.Name);
            return this;
        }

        public StandardTableOptions WithForeignKey(Table referenceTable, Column constrainedColumn)
        {
            this.owner.AddDefiniton(SqlStatements.Constraints.ForeignKey, constrainedColumn.Name);
            this.owner.AddDefiniton(SqlStatements.Constraints.References, referenceTable.Name);
            return this;
        }
    }
}
