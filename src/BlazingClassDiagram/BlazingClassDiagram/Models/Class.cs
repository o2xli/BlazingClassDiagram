using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazingClassDiagram.Models
{
    internal class Class 
    {
        public required string Name { get; set; }
        public required string FullName { get; set; }
        public required AccessModifier AccessModifier { get; set; }
        public required Classifiers Classifiers { get; set; }        
        public Constructor? Constructor { get; set; }
        
        public List<Method>? Methods { get; set; }
        public List<Property>? Properties { get; set; }
        
        public Namespace? Namespace { get; set; }
        public List<Type> GenericTypes { get; set; }
        
    }
}
