using System;
using System.Collections.Generic;
using System.Text;

namespace Lightmap.Modeling
{
    public struct ColumnDefinitions
    {
        public const string PrimaryKey = "PRIMARY KEY";
        public const string CompositeKey = "__CompositeKey__";
        public const string ForeignKey = "FOREIGN KEY";
        public const string CascadeDelete = "ON DELETE CASCADE";
        public const string Unique = "UNIQUE";
        public const string NotNull = "NOT NULL";
        public const string ReferencesTable = "REFERENCES TABLE";
        public const string ReferencesColumn = "REFERENCES COLUMN";
        public const string AutoIncrement = "AUTOINCREMENT";
    }
}
