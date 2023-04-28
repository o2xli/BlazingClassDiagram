namespace BlazingClassDiagram.Models
{
    internal class Namespace
    {
        public required string Name { get; set; }
        public List<Class> Classes { set; get; } = new();
        public List<Struct> Structs { set; get; } = new();
        public List<Interface> Interfaces { set; get; } = new();
    }
}
