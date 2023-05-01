using BlazingClassDiagram.Models;
using System.Text;
using Type = BlazingClassDiagram.Models.Type;

namespace BlazingClassDiagram.Mermaid
{
    internal static class Extentions
    {
        public static string Render(this Class item)
        {
            var sb = new StringBuilder();

            sb.Append(item.Name);
            if (item.GenericTypes.Any())
            {
                sb.Append('~');
                sb.Append(string.Join(",", item.GenericTypes.Select(g => g.Name).ToArray()));
                sb.Append('~');
            }

            return sb.ToString();
        }

        public static string Render(this Interface item)
        {
            var sb = new StringBuilder();

            sb.Append(item.Name);
            if (item.GenericTypes.Any())
            {
                sb.Append('~');
                sb.Append(string.Join(",", item.GenericTypes.Select(g => g.Name).ToArray()));
                sb.Append('~');
            }

            return sb.ToString();
        }

        public static string Render(this Relationship item)
        {
            return $"{item.TypeA.Name} --{item.RelationType.Render()} {item.TypeB.Name}";
        }

        public static string Render(this RelationType item)
        {
            switch (item)
            {
                case RelationType.Inheritance:
                    return "|>";
                case RelationType.Composition:
                    return "*";
                case RelationType.Aggregation:
                    return "o";
                case RelationType.Association:
                    return ">";
                case RelationType.Realization:
                    return "<|";
            }
            return String.Empty;
        }

        public static string Render(this Struct item)
        {
            var sb = new StringBuilder();

            sb.Append(item.Name);
            if (item.GenericTypes.Any())
            {
                sb.Append('~');
                sb.Append(string.Join(",", item.GenericTypes.Select(g => g.Name).ToArray()));
                sb.Append('~');
            }

            return sb.ToString();
        }

        public static string Render(this Type @type)
        {
            return type.Name.Replace('<', '~').Replace('<', '~');
        }

        public static string Render(this Member member)
        {
            return $"{member.AccessModifier.Render()}{member.Type.Render()} {member.Name}";
        }
        public static string Render(this Method method)
        {
            var sb = new StringBuilder();

            sb.Append($"{method.AccessModifier.Render()}{method.Name}");
            //Not supported on mermaid
            //if (method.GenericTypes.Any())
            //{
            //    sb.Append('~');
            //    sb.Append(string.Join(",", method.GenericTypes.Select(g => g.Name).ToArray()));
            //    sb.Append('~');
            //}
            sb.Append('(');
            sb.Append(string.Join(", ", method.Parameters.Select(g => g.Render()).ToArray()));
            sb.Append(')');

            if (method.ReturnType.Name != "void")
            {
                sb.Append($" {method.ReturnType.Name}");
            }

            return sb.ToString();
        }

        public static string Render(this Constructor constructor)
        {
            var sb = new StringBuilder();

            sb.Append($"{constructor.AccessModifier.Render()}{constructor.Name}");

            sb.Append('(');
            sb.Append(string.Join(", ", constructor.Parameters.Select(g => g.Render()).ToArray()));
            sb.Append(')');

            return sb.ToString();
        }

        public static string Render(this Parameter parameter)
        {
            return $"{parameter.Type.Replace('<', '~').Replace('<', '~')} {parameter.Name}";
        }

        public static string Render(this AccessModifier modifier)
        {
            if (modifier.HasFlag(AccessModifier.Public))
                return "+";
            else if (modifier.HasFlag(AccessModifier.Private))
                return "-";
            else if (modifier.HasFlag(AccessModifier.Protected))
                return "#";
            else if (modifier.HasFlag(AccessModifier.Internal))
                return "~";

            return string.Empty;
        }
    }
}
