namespace BlazingClassDiagram.Models
{
    internal class Root
    {
        public List<Namespace> Namespaces { set; get; } = new();
        public List<Class> Classes { set; get; } = new();
        public List<Struct> Structs { set; get; } = new();
        public List<Interface> Interfaces { set; get; } = new();
        public List<Relationship> Relationships { set; get; } = new();

    }
}
