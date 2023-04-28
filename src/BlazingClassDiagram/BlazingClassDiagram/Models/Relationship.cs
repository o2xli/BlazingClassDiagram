namespace BlazingClassDiagram.Models
{
    internal class Relationship
    {
        public Type TypeA { get; set; }
        public Type TypeB { get; set; }
        public Cardinality? CardinalityA { get; set; }
        public Cardinality? CardinalityB { get; set; }
        public string? Label { get; set; }
        public required RelationType RelationType { get; set; }
        public bool IsTwoWay { get; set; } = false;
        public bool IsSolid { get; set; } = true;
    }
}
