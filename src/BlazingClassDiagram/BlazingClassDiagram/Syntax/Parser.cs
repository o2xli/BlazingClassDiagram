﻿using BlazingClassDiagram.Models;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections;
using Parameter = BlazingClassDiagram.Models.Parameter;
using Type = BlazingClassDiagram.Models.Type;

namespace BlazingClassDiagram.Syntax
{
    internal static class Parser
    {
        private static Options _options;

        internal static void Parse(this Root root, DirectoryInfo directory, Options options)
        {
            _options = options;

            foreach (var file in directory.GetFiles("*.cs"))
            {
                root.Parse(file, options);
            }
            if (options.Recursive)
            {
                foreach (var child in directory.GetDirectories())
                {
                    root.Parse(child, options);
                }
            }
        }

        internal static void Parse(this Root root, FileInfo file, Options options)
        {
            _options = options;

            if (!file.Extension.EndsWith(".cs", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("File is not a csharp file!");
                Environment.Exit(1);
            }
            if (_options.Verbose)
                Console.WriteLine($"Parsing file {file.Name}");

            var content = File.ReadAllText(file.FullName);
            root.Parse(content);
        }

        private static void Parse(this Root item, string content)
        {
            if (String.IsNullOrWhiteSpace(content))
                return;

            var syntaxTree = CSharpSyntaxTree.ParseText(content);

            var declarationSyntax = syntaxTree.GetRoot() as CompilationUnitSyntax;

            declarationSyntax?.Members
                .OfType<BaseNamespaceDeclarationSyntax>()
                .ToList().ForEach(n => item.Namespaces.Parse(n));

            if (_options.IncludeTypes.HasFlag(ObjectTypes.Class))
            {
                declarationSyntax?.Members
                    .OfType<ClassDeclarationSyntax>()
                    .ToList().ForEach(c => item.Classes.Parse(c));
            }

            if (_options.IncludeTypes.HasFlag(ObjectTypes.Record))
            {
                declarationSyntax?.Members
                .OfType<RecordDeclarationSyntax>()
                .ToList().ForEach(c => item.Classes.Parse(c));
            }

            if (_options.IncludeTypes.HasFlag(ObjectTypes.Interface))
            {
                declarationSyntax?.Members
                .OfType<InterfaceDeclarationSyntax>()
                .ToList().ForEach(m => item.Interfaces.Parse(m));
            }

            if (_options.IncludeTypes.HasFlag(ObjectTypes.Struct))
            {
                declarationSyntax?.Members
                .OfType<StructDeclarationSyntax>()
                .ToList().ForEach(m => item.Structs.Parse(m));
            }
            item.Relationships.Parse(item);
        }

        internal static void Parse(this List<Relationship> Relationships, Root root)
        {
            RelationshipBuilder.BuildInheritance(Relationships, root);
        }

        internal static void Parse(this List<Namespace> list, BaseNamespaceDeclarationSyntax? declarationSyntax)
        {
            if (declarationSyntax is null)
                return;

            Namespace item = new()
            {
                Name = declarationSyntax.Name.ToString(),
            };

            if (_options.Verbose)
                Console.WriteLine($"  Found Namespace {item.Name}");

            if (_options.IncludeTypes.HasFlag(ObjectTypes.Class))
            {
                declarationSyntax?.Members
                    .OfType<ClassDeclarationSyntax>()
                    .ToList().ForEach(c => item.Classes.Parse(c));
            }

            if (_options.IncludeTypes.HasFlag(ObjectTypes.Record))
            {
                declarationSyntax?.Members
                .OfType<RecordDeclarationSyntax>()
                .ToList().ForEach(c => item.Classes.Parse(c));
            }

            if (_options.IncludeTypes.HasFlag(ObjectTypes.Interface))
            {
                declarationSyntax?.Members
                .OfType<InterfaceDeclarationSyntax>()
                .ToList().ForEach(m => item.Interfaces.Parse(m));
            }

            if (_options.IncludeTypes.HasFlag(ObjectTypes.Struct))
            {
                declarationSyntax?.Members
                .OfType<StructDeclarationSyntax>()
                .ToList().ForEach(m => item.Structs.Parse(m));
            }

            list.Add(item);
        }

        internal static void Parse(this List<Struct> list, StructDeclarationSyntax declarationSyntax)
        {
            if (declarationSyntax is null)
                return;

            Struct item = new()
            {
                Name = declarationSyntax.Identifier.ValueText,
                AccessModifier = declarationSyntax.Modifiers.ParseModifier(),
            };

            if (_options.Verbose)
                Console.WriteLine($"  Found Struct {item.Name}");

            declarationSyntax.Members
                .OfType<MethodDeclarationSyntax>()
                .ToList().ForEach(m => item.Methods.Parse(m));

            declarationSyntax.Members
               .OfType<PropertyDeclarationSyntax>()
               .ToList().ForEach(m => item.Members.Parse(m));

            declarationSyntax.Members
              .OfType<FieldDeclarationSyntax>()
              .ToList().ForEach(m => item.Members.Parse(m));

            item.GenericTypes.Parse(declarationSyntax.TypeParameterList);

            list.Add(item);
        }

        internal static void Parse(this List<Interface> list, InterfaceDeclarationSyntax declarationSyntax)
        {
            if (declarationSyntax is null)
                return;

            Interface item = new()
            {
                Name = declarationSyntax.Identifier.ValueText,
                AccessModifier = declarationSyntax.Modifiers.ParseModifier(),
            };

            if (_options.Verbose)
                Console.WriteLine($"  Found Interface {item.Name}");

            declarationSyntax.Members
                .OfType<MethodDeclarationSyntax>()
                .ToList().ForEach(m => item.Methods.Parse(m));

            declarationSyntax.Members
               .OfType<PropertyDeclarationSyntax>()
               .ToList().ForEach(m => item.Members.Parse(m));

            declarationSyntax.Members
              .OfType<FieldDeclarationSyntax>()
              .ToList().ForEach(m => item.Members.Parse(m));

            item.GenericTypes.Parse(declarationSyntax.TypeParameterList);
            item.BaseTypes.Parse(declarationSyntax.BaseList);

            list.Add(item);
        }

        internal static void Parse(this List<Class> list, RecordDeclarationSyntax declarationSyntax)
        {
            if (declarationSyntax is null)
                return;

            Class item = new()
            {
                Name = declarationSyntax.Identifier.ValueText,
                AccessModifier = declarationSyntax.Modifiers.ParseModifier(),
                Classifiers = Classifiers.None,
                IsRecord = true
            };

            if (_options.Verbose)
                Console.WriteLine($"  Found Record {item.Name}");

            declarationSyntax.Members
                .OfType<ConstructorDeclarationSyntax>()
                .ToList().ForEach(m => item.Constructors.Parse(m));

            declarationSyntax.Members
                .OfType<MethodDeclarationSyntax>()
                .ToList().ForEach(m => item.Methods.Parse(m));

            declarationSyntax.Members
               .OfType<PropertyDeclarationSyntax>()
               .ToList().ForEach(m => item.Members.Parse(m));

            declarationSyntax.Members
               .OfType<FieldDeclarationSyntax>()
               .ToList().ForEach(m => item.Members.Parse(m));


            item.BaseTypes.Parse(declarationSyntax.BaseList);
            item.GenericTypes.Parse(declarationSyntax.TypeParameterList);

            list.Add(item);
        }

        internal static void Parse(this List<Class> list, ClassDeclarationSyntax declarationSyntax)
        {
            if (declarationSyntax is null)
                return;

            Class item = new()
            {
                Name = declarationSyntax.Identifier.ValueText,
                AccessModifier = declarationSyntax.Modifiers.ParseModifier(),
                Classifiers = Classifiers.None,
            };

            if (_options.Verbose)
                Console.WriteLine($"  Found Class {item.Name}");

            declarationSyntax.Members
                .OfType<ConstructorDeclarationSyntax>()
                .ToList().ForEach(m => item.Constructors.Parse(m));

            declarationSyntax.Members
                .OfType<MethodDeclarationSyntax>()
                .ToList().ForEach(m => item.Methods.Parse(m));

            declarationSyntax.Members
               .OfType<PropertyDeclarationSyntax>()
               .ToList().ForEach(m => item.Members.Parse(m));

            declarationSyntax.Members
               .OfType<FieldDeclarationSyntax>()
               .ToList().ForEach(m => item.Members.Parse(m));


            item.BaseTypes.Parse(declarationSyntax.BaseList);
            item.GenericTypes.Parse(declarationSyntax.TypeParameterList);

            list.Add(item);
        }

        internal static void Parse(this List<Constructor> list, ConstructorDeclarationSyntax? declarationSyntax)
        {
            if (declarationSyntax is null)
                return;

            Constructor item = new()
            {
                Name = declarationSyntax.Identifier.ValueText,
                AccessModifier = declarationSyntax.Modifiers.ParseModifier(),
            };

            if (_options.Verbose)
                Console.WriteLine($"    Found Constructor {item.Name}");

            item.Parameters.Parse(declarationSyntax.ParameterList);

            list.Add(item);
        }

        internal static void Parse(this List<Parameter> list, ParameterListSyntax? declarationSyntax)
        {
            if (declarationSyntax is null)
                return;    

            declarationSyntax.Parameters
                .OfType<ParameterSyntax>()
                .ToList().ForEach(m => list.Parse(m));
        }

        internal static void Parse(this List<Parameter> list, ParameterSyntax? declarationSyntax)
        {
            if (declarationSyntax is null)
                return;

            Parameter item = new()
            {
                Name = declarationSyntax.Identifier.ValueText,
                Type = declarationSyntax.Type?.ToString()
            };

            if (_options.Verbose)
                Console.WriteLine($"      Found Parameter {item.Name}");

            list.Add(item);
        }

        internal static void Parse(this List<Type> list, BaseListSyntax? declarationSyntax)
        {
            if (declarationSyntax is null)
                return;

            declarationSyntax.Types
                .OfType<SimpleBaseTypeSyntax>()
                .ToList().ForEach(m => list.Parse(m));
        }

        internal static void Parse(this List<Type> list, SimpleBaseTypeSyntax? declarationSyntax)
        {
            if (declarationSyntax is null)
                return;

            Type item = new()
            {
                Name = declarationSyntax.Type.ToString(),
            };
            list.Add(item);
        }

        internal static void Parse(this List<Type> list, TypeParameterListSyntax? declarationSyntax)
        {
            if (declarationSyntax is null)
                return;

            declarationSyntax.Parameters
                .OfType<TypeParameterSyntax>()
                .ToList().ForEach(m => list.Parse(m));
        }

        internal static void Parse(this List<Type> list, TypeParameterSyntax? declarationSyntax)
        {
            if (declarationSyntax is null)
                return;

            Type item = new()
            {
                Name = declarationSyntax.Identifier.ValueText,
            };
            list.Add(item);
        }

        internal static void Parse(this List<Method> list, MethodDeclarationSyntax? declarationSyntax)
        {
            if (declarationSyntax is null)
                return;

            Method item = new()
            {
                Name = declarationSyntax.Identifier.ValueText,
                AccessModifier = declarationSyntax.Modifiers.ParseModifier(),
                Classifiers = Classifiers.None,
                ReturnType = new Models.Type { Name = declarationSyntax.ReturnType.ToString() }
            };

            if (_options.Verbose)
                Console.WriteLine($"    Found Method {item.Name}");

            item.Parameters.Parse(declarationSyntax.ParameterList);

            item.GenericTypes.Parse(declarationSyntax.TypeParameterList);

            list.Add(item);
        }

        internal static void Parse(this List<Member> list, PropertyDeclarationSyntax? declarationSyntax)
        {
            if (declarationSyntax is null)
                return;

            Member item = new()
            {
                Name = declarationSyntax.Identifier.ValueText,
                AccessModifier = declarationSyntax.Modifiers.ParseModifier(),
                Classifiers = Classifiers.None,
                Type = new Models.Type { Name = declarationSyntax.Type.ToString() }
            };

            if (_options.Verbose)
                Console.WriteLine($"    Found Property {item.Name}");

            list.Add(item);
        }

        internal static void Parse(this List<Member> list, FieldDeclarationSyntax? declarationSyntax)
        {
            if (declarationSyntax is null)
                return;

            var variable = declarationSyntax.Declaration.Variables.FirstOrDefault();
            if (variable is null)
                return;

            Member item = new()
            {
                Name = variable.Identifier.ToString(),
                AccessModifier = declarationSyntax.Modifiers.ParseModifier(),
                Classifiers = Classifiers.None,
                Type = new Models.Type { Name = declarationSyntax.Declaration.Type.ToString() }
            };

            if (_options.Verbose)
                Console.WriteLine($"    Found Field {item.Name}");

            list.Add(item);
        }


        public static AccessModifier ParseModifier(this IEnumerable? modifier)
        {
            AccessModifier item = AccessModifier.None;

            if (modifier is null)
                return item;

            foreach (var m in modifier)
            {
                switch (m.ToString())
                {
                    case "public": item |= AccessModifier.Public; break;
                    case "private": item |= AccessModifier.Private; break;
                    case "internal": item |= AccessModifier.Internal; break;
                    case "protected": item |= AccessModifier.Protected; break;
                }
            }
            return item;
        }
    }
}
