namespace BlazingClassDiagram.Models
{
    internal class Constructor
    {
        public required string Name { get; set; }
        public List<Parameter> Parameters { get; set; } = new();
        public required AccessModifier AccessModifier { get; set; }

    }
}
