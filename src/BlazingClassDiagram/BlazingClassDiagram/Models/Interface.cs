namespace BlazingClassDiagram.Models
{
    internal class Interface
    {
        public required string Name { get; set; }
        public List<Method> Methods { get; set; } = new();
        public List<Property> Properties { get; set; } = new();

        public List<Type> GenericTypes { get; set; } = new();
    }
}
