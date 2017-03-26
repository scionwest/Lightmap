namespace Lightmap.Modeling
{
    public class ColumnDefinitions
    {
        public static readonly string PrimaryKey = "PRIMARY KEY";
        public static readonly string CompositeKey = "__CompositeKey__";
        public static readonly string ForeignKey = "FOREIGN KEY";
        public static readonly string CascadeDelete = "ON DELETE CASCADE";
        public static readonly string Unique = "UNIQUE";
        public static readonly string NotNull = "NOT NULL";
        public static readonly string ReferencesSchema = "REFERENCES SCHEMA";
        public static readonly string ReferencesTable = "REFERENCES TABLE";
        public static readonly string ReferencesColumn = "REFERENCES COLUMN";
        public static readonly string AutoIncrement = "AUTOINCREMENT";
    }
}
