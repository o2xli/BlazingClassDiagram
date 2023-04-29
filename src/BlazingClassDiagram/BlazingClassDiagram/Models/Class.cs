namespace BlazingClassDiagram.Models
{
    internal class Class
    {
        public required string Name { get; set; }
        public required AccessModifier AccessModifier { get; set; }
        public required Classifiers Classifiers { get; set; }
        public List<Constructor> Constructors { get; set; } = new();

        public List<Method> Methods { get; set; } = new();
        public List<Member> Members { get; set; } = new();

        public Namespace? Namespace { get; set; }
        public List<Type> GenericTypes { get; set; } = new();

        public List<Type> BaseTypes { get; set; } = new();

        public bool IsRecord { get; set; } = false;
    }
}
