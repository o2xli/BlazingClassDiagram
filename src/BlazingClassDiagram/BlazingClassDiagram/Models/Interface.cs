namespace BlazingClassDiagram.Models
{
    internal class Interface
    {
        public required string Name { get; set; }
        public required string FullName { get; set; }
        public List<Method>? Methods { get; set; }
        public List<Property>? Properties { get; set; }

        public List<Type> GenericTypes { get; set; }
    }
}
