namespace BlazingClassDiagram.Models
{
    internal class Struct
    {
        public required string Name { get; set; }
        public List<Method> Methods { get; set; } = new();
        public List<Member> Members { get; set; } = new();
        public List<Type> GenericTypes { get; set; } = new();
    }

}
