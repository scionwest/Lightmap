namespace Lightmap.Modeling
{
    public class Column
    {
        public Column AsPrimaryKey()
        {
            return this;
        }

        public Column WithForeignKey(Table referenceTable, string constrainedColumn)
        {
            return this;
        }

        public Column WithForeignKey(Table referenceTable, Column constrainedColumn)
        {
            return this;
        }
    }
}