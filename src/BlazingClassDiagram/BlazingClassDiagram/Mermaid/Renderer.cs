using BlazingClassDiagram.Models;
using System.Text;

namespace BlazingClassDiagram.Mermaid
{
    internal static class Renderer
    {
        const string ident = "    ";
        private static StringBuilder _sb;
        private static Root _root;
        private static Options _options;
        public static string Render(Root root, Options options)
        {
            _options = options;
            _sb = new StringBuilder();
            _sb.AppendLine("classDiagram");
            _root = root;

            _root.Relationships.Render();
            _root.Namespaces.Render();
            _root.Classes.Render();
            _root.Interfaces.Render();
            _root.Structs.Render();

            return _sb.ToString();
        }

        private static void Render(this List<Relationship> list)
        {
            foreach (var item in list)
            {
                _sb.AppendLine(item.Render());
            }
        }

        private static void Render(this List<Namespace> list)
        {
            foreach (var item in list)
            {
                item.Classes.Render();
                item.Interfaces.Render();
                item.Structs.Render();
            }
        }

        private static void Render(this List<Interface> list)
        {
            foreach (var item in list.Where(i => _options.IncludeModifier.HasFlag(i.AccessModifier)))
            {
                _sb.AppendLine($"class {item.Render()}{{");
                _sb.AppendLine("<<Interface>>");
                foreach (var member in item.Members)
                {
                    _sb.AppendLine($"{ident}{member.Render()}");
                }
                foreach (var method in item.Methods)
                {
                    _sb.AppendLine($"{ident}{method.Render()}");
                }

                _sb.AppendLine("}");
            }
        }

        private static void Render(this List<Struct> list)
        {
            foreach (var item in list.Where(i => _options.IncludeModifier.HasFlag(i.AccessModifier)))
            {
                _sb.AppendLine($"class {item.Render()}{{");
                _sb.AppendLine("<<Struct>>");
                foreach (var member in item.Members.Where(i => _options.IncludeModifier.HasFlag(i.AccessModifier)))
                {
                    _sb.AppendLine($"{ident}{member.Render()}");
                }
                foreach (var method in item.Methods.Where(i => _options.IncludeModifier.HasFlag(i.AccessModifier)))
                {
                    _sb.AppendLine($"{ident}{method.Render()}");
                }

                _sb.AppendLine("}");
            }
        }

        private static void Render(this List<Class> list)
        {
            foreach (var item in list.Where(i => _options.IncludeModifier.HasFlag(i.AccessModifier)))
            {
                _sb.AppendLine($"class {item.Render()}{{");
                if (item.IsRecord)
                    _sb.AppendLine("<<Record>>");
                foreach (var member in item.Members.Where(i => _options.IncludeModifier.HasFlag(i.AccessModifier)))
                {
                    _sb.AppendLine($"{ident}{member.Render()}");
                }
                foreach (var constructor in item.Constructors.Where(i => _options.IncludeModifier.HasFlag(i.AccessModifier)))
                {
                    _sb.AppendLine($"{ident}{constructor.Render()}");
                }
                foreach (var method in item.Methods.Where(i => _options.IncludeModifier.HasFlag(i.AccessModifier)))
                {
                    _sb.AppendLine($"{ident}{method.Render()}");
                }

                _sb.AppendLine("}");
            }
        }
    }
}
