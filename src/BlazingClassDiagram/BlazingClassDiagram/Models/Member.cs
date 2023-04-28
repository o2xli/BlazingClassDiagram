namespace BlazingClassDiagram.Models
{
    internal class Member
    {
        public required string Name { get; set; }
        public required Type Type { get; set; }
        public required AccessModifier AccessModifier { get; set; }
        public required Classifiers Classifiers { get; set; }
    }
}
