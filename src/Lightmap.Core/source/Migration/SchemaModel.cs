using System;
using System.Collections.Generic;
using System.Text;

namespace Lightmap.Migration
{
    public struct SchemaModel : ISchemaModel
    {
        public SchemaModel(string name) => this.Name = name;

        public string Name { get; }
    }
}
