using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazingClassDiagram.Models
{
    internal class Struct 
    {
        public required string Name { get; set; }
        public required string FullName { get; set; }
        public List<Method>? Methods { get; set; }
        public List<Property>? Properties { get; set; }
        
    }
    
}
