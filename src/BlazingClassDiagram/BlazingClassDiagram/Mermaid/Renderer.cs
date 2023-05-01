using BlazingClassDiagram.Models;
using System.Text;

namespace BlazingClassDiagram.Mermaid
{
    internal class Renderer
    {
        const string ident = "    ";
        public static string Render(Root root)
        {
            var sb = new StringBuilder();
            sb.AppendLine("classDiagram");

            RenderRelationShips(sb, root.Relationships);
            RenderNamespaces(sb, root.Namespaces);
            RenderClasses(sb, root.Classes);
            RenderInterfaces(sb, root.Interfaces);
            RenderStructs(sb, root.Structs);

            return sb.ToString();
        }

        private static void RenderRelationShips(StringBuilder sb, List<Relationship> list)
        {
            foreach (var item in list)
            {
                sb.AppendLine(item.Render());
            }
        }

        private static void RenderNamespaces(StringBuilder sb, List<Namespace> list)
        {
            foreach (var item in list)
            {
                RenderClasses(sb, item.Classes);
                RenderInterfaces(sb, item.Interfaces);
                RenderStructs(sb, item.Structs);
            }
        }

        private static void RenderInterfaces(StringBuilder sb, List<Interface> list)
        {
            foreach (var item in list)
            {
                sb.AppendLine($"class {item.Render()}{{");
                sb.AppendLine("<<Interface>>");
                foreach (var member in item.Members)
                {
                    sb.AppendLine($"{ident}{member.Render()}");
                }
                foreach (var method in item.Methods)
                {
                    sb.AppendLine($"{ident}{method.Render()}");
                }

                sb.AppendLine("}");
            }
        }

        private static void RenderStructs(StringBuilder sb, List<Struct> list)
        {
            foreach (var item in list)
            {
                sb.AppendLine($"class {item.Render()}{{");
                sb.AppendLine("<<Struct>>");
                foreach (var member in item.Members)
                {
                    sb.AppendLine($"{ident}{member.Render()}");
                }
                foreach (var method in item.Methods)
                {
                    sb.AppendLine($"{ident}{method.Render()}");
                }

                sb.AppendLine("}");
            }
        }

        private static void RenderClasses(StringBuilder sb, List<Class> list)
        {
            foreach (var item in list)
            {
                sb.AppendLine($"class {item.Render()}{{");
                if (item.IsRecord)
                    sb.AppendLine("<<Record>>");
                foreach (var member in item.Members)
                {
                    sb.AppendLine($"{ident}{member.Render()}");
                }
                foreach (var constructor in item.Constructors)
                {
                    sb.AppendLine($"{ident}{constructor.Render()}");
                }
                foreach (var method in item.Methods)
                {
                    sb.AppendLine($"{ident}{method.Render()}");
                }

                sb.AppendLine("}");
            }
        }
    }
}
