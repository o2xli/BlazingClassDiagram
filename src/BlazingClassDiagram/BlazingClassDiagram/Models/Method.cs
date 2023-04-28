using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazingClassDiagram.Models
{
    internal class Method
    {
        public required string Name { get; set; }    
        public required Type ReturnType { get; set; }
        public List<Parameter> Parameters { get; set; } = new();
        public required AccessModifier AccessModifier { get; set; }
        public required Classifiers Classifiers { get; set; }
        public List<Type> GenericTypes { get; set; } = new();

    }
}
