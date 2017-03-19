using System;
using System.Collections.Generic;
using System.Text;

namespace Lightmap.Migration
{
    public class SchemaBuilder : ISchemaBuilder
    {
        public string Name { get; set; }

        public ISchemaModel GetSchemaModel() => new SchemaModel(this.Name);
    }
}
