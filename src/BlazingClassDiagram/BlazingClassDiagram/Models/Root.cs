using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazingClassDiagram.Models
{
    internal class Root
    {
        public List<Namespace>? Namespaces { set; get; }
        public List<Class>? Classes { set; get; }
        public List<Struct>? Structs { set; get; }
        public List<Interface>? Interfaces { set; get; }
        public List<Relationship>? Relationships { set; get; }
    }
}
