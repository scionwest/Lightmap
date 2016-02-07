using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lightmap.Modeling
{
    public struct SqlStatements
    {
        public struct Constraints
        {
            public const string PrimaryKey = "PRIMARY KEY";
            public const string CompositeKey = "__CompositeKey__";
            public const string ForeignKey = "FOREIGN KEY";
            public const string CascadeDelete = "ON DELETE CASCADE";
            public const string Unique = "UNIQUE";
            public const string NotNull = "NOT NULL";
            public const string ReferencesTable = "REFERENCES";
            public const string ReferencesColumn = "REFERENCES";
            public const string AutoIncrement = "AUTOINCREMENT";
        }
    }
}
