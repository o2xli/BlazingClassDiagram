using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazingClassDiagram.Models
{
    internal class Constructor 
    {
        public required string Name { get; set; }
        public List<Parameter> Parameters { get; set; } = new();
        public required AccessModifier AccessModifier { get; set; }

    }
}
