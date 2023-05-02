namespace BlazingClassDiagram.Models
{
    internal class Interface
    {
        public required string Name { get; set; }
        public List<Method> Methods { get; set; } = new();
        public List<Member> Members { get; set; } = new();

        public List<Type> GenericTypes { get; set; } = new();
        public List<Type> BaseTypes { get; set; } = new();
        public required AccessModifier AccessModifier { get; set; }
    }
}
