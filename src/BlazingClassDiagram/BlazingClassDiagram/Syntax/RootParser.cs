using BlazingClassDiagram.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Reflection.Metadata;
using Parameter = BlazingClassDiagram.Models.Parameter;
using System.Collections;
using Type = BlazingClassDiagram.Models.Type;

namespace BlazingClassDiagram.Syntax
{
    internal static class RootParser
    {
        internal static void ParseFile(this Root root, string content)            
        {
            if (String.IsNullOrWhiteSpace(content))
                return;

            var syntaxTree = CSharpSyntaxTree.ParseText(content);

            var compilationUnitSyntax = syntaxTree.GetRoot() as CompilationUnitSyntax;

            compilationUnitSyntax?.Members
                .OfType<NamespaceDeclarationSyntax>()
                .ToList().ForEach(n=> root.Namespaces.Parse(n));

            compilationUnitSyntax?.Members
                .OfType<FileScopedNamespaceDeclarationSyntax>()
                .ToList().ForEach(n => root.Namespaces.Parse(n));

            compilationUnitSyntax?.Members
                .OfType<ClassDeclarationSyntax>()
                .ToList().ForEach(c => root.Classes.Parse(c));

            
        }

        internal static void Parse(this List<Namespace> list, NamespaceDeclarationSyntax declarationSyntax)
        {
            if (declarationSyntax is null)
                return;

            Namespace item = new()
            {
                Name = declarationSyntax.Name.ToString(),                
            };
            declarationSyntax.Members
                .OfType<ClassDeclarationSyntax>()
                .ToList().ForEach(m => item.Classes.Parse(m));

            declarationSyntax.Members
                .OfType<InterfaceDeclarationSyntax>()
                .ToList().ForEach(m => item.Interfaces.Parse(m));


            list.Add(item);
        }

        internal static void Parse(this List<Namespace> list, FileScopedNamespaceDeclarationSyntax declarationSyntax)
        {
            if (declarationSyntax is null)
                return;

            Namespace item = new()
            {
                Name = declarationSyntax.Name.ToString(),
            };
            declarationSyntax.Members
                .OfType<ClassDeclarationSyntax>()
                .ToList().ForEach(m => item.Classes.Parse(m));

            declarationSyntax.Members
                .OfType<InterfaceDeclarationSyntax>()
                .ToList().ForEach(m => item.Interfaces.Parse(m));

            list.Add(item);
        }

        internal static void Parse(this List<Interface> list, InterfaceDeclarationSyntax declarationSyntax)
        {
            if (declarationSyntax is null)
                return;

            Interface item = new()
            {
                Name = declarationSyntax.Identifier.ValueText,
            };

            declarationSyntax.Members
                .OfType<MethodDeclarationSyntax>()
                .ToList().ForEach(m => item.Methods.Parse(m));

            declarationSyntax.Members
               .OfType<PropertyDeclarationSyntax>()
               .ToList().ForEach(m => item.Properties.Parse(m));

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
            
            declarationSyntax.Members
                .OfType<ConstructorDeclarationSyntax>()
                .ToList().ForEach(m => item.Constructors.Parse(m));

            declarationSyntax.Members
                .OfType<MethodDeclarationSyntax>()
                .ToList().ForEach(m => item.Methods.Parse(m));

            declarationSyntax.Members
               .OfType<PropertyDeclarationSyntax>()
               .ToList().ForEach(m => item.Properties.Parse(m));

            item.GenericTypes.Parse(declarationSyntax.TypeParameterList);

            list.Add(item);
        }

        internal static void Parse(this List<Constructor> list, ConstructorDeclarationSyntax declarationSyntax)
        {
            if (declarationSyntax is null)
                return;

            Constructor item = new()
            {
                Name = declarationSyntax.Identifier.ValueText,
                AccessModifier = declarationSyntax.Modifiers.ParseModifier(),
            };
            item.Parameters.Parse(declarationSyntax.ParameterList);

            list.Add(item);
        }

        internal static void Parse(this List<Parameter> list, ParameterListSyntax declarationSyntax)
        {
            if (declarationSyntax is null)
                return;

            declarationSyntax.Parameters
                .OfType<ParameterSyntax>()
                .ToList().ForEach(m => list.Parse(m));           
        }

        internal static void Parse(this List<Parameter> list, ParameterSyntax declarationSyntax)
        {
            if (declarationSyntax is null)
                return;

            Parameter item = new()
            {
                Name = declarationSyntax.Identifier.ValueText,
                Type = declarationSyntax?.Type?.ToString()
            };
            list.Add(item);
        }

        internal static void Parse(this List<Type> list, TypeParameterListSyntax declarationSyntax)
        {
            if (declarationSyntax is null)
                return;

            declarationSyntax.Parameters
                .OfType<TypeParameterSyntax>()
                .ToList().ForEach(m => list.Parse(m));
        }

        internal static void Parse(this List<Type> list, TypeParameterSyntax declarationSyntax)
        {
            if (declarationSyntax is null)
                return;

            Type item = new()
            {
                Name = declarationSyntax.Identifier.ValueText,
            };
            list.Add(item);
        }

        internal static void Parse(this List<Method> list, MethodDeclarationSyntax declarationSyntax)
        {
            if (declarationSyntax is null)
                return;

            Method item = new()
            {
                Name = declarationSyntax.Identifier.ValueText,
                AccessModifier = declarationSyntax.Modifiers.ParseModifier(),
                Classifiers = Classifiers.None,
                ReturnType = new Models.Type { Name =  declarationSyntax.ReturnType.ToString() }
            };
            item.Parameters.Parse(declarationSyntax.ParameterList);

            item.GenericTypes.Parse(declarationSyntax.TypeParameterList);

            list.Add(item);
        }

        internal static void Parse(this List<Property> list, PropertyDeclarationSyntax declarationSyntax)
        {
            if (declarationSyntax is null)
                return;

            Property item = new()
            {
                Name = declarationSyntax.Identifier.ValueText,
                AccessModifier = declarationSyntax.Modifiers.ParseModifier(),
                Classifiers = Classifiers.None,
                Type = new Models.Type { Name = declarationSyntax.Type.ToString() }
            };

            list.Add(item);
        }

        public static AccessModifier ParseModifier(this IEnumerable modifier)
        {
            AccessModifier item = AccessModifier.None;
            
            if (modifier is null)
                return item;

            foreach (var m in modifier)
            {
                switch (m.ToString())
                {
                    case "public": item |= AccessModifier.Public; break;
                    case "private": item |= AccessModifier.Private;  break;
                    case "internal": item |= AccessModifier.Internal; break;
                    case "protected": item |= AccessModifier.Protected;  break;
                }
            }
            return item;
        }
    }
}
