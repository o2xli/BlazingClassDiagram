using BlazingClassDiagram.Models;

namespace BlazingClassDiagram.Syntax
{
    internal static class RelationshipBuilder
    {
        internal static void BuildInheritance(List<Relationship> relationships, Root root)
        {
            var allClasses = root.Namespaces.SelectMany(c => c.Classes).Concat(root.Classes);

            foreach (var item in allClasses)
            {
                foreach (var child in item.BaseTypes)
                {
                    var relationShip = new Relationship()
                    {
                        TypeA = new Models.Type { Name = item.Name },
                        TypeB = child,
                        Label = "implements",
                        RelationType = RelationType.Inheritance
                    };
                    relationships.Add(relationShip);
                }
            }
        }
    }
}
