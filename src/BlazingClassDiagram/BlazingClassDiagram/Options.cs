using BlazingClassDiagram.Models;
using CommandLine;

namespace BlazingClassDiagram
{
    internal class Options
    {
        [Option('p', "path", Required = true, HelpText = "Set input path. (can be .cs file or an directory)")]
        public string Path { get; set; }

        [Option('o', "output", Required = false, Default = "output.mmd", HelpText = "Set output file path.")]
        public string Output { get; set; }

        [Option('m', "modifier", Required = false, Default = AccessModifier.Internal | AccessModifier.Public | AccessModifier.Private | AccessModifier.Protected, HelpText = "Set include modifier.")]
        public AccessModifier IncludeModifier { get; set; }

        [Option('i', "include", Required = false, Default = ObjectTypes.Class | ObjectTypes.Record | ObjectTypes.Interface | ObjectTypes.Struct, HelpText = "Set include types.")]
        public ObjectTypes IncludeTypes { get; set; }

        [Option('r', "recursive", Required = false, Default = true, HelpText = "Scan directory recursive for *.cs files.")]
        public bool Recursive { get; set; }

        [Option("verbose", Required = false, HelpText = "Set output to verbose messages.")]
        public bool Verbose { get; set; }

    }
}
