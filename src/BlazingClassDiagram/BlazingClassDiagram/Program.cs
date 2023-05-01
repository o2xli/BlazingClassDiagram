// See https://aka.ms/new-console-template for more information

using BlazingClassDiagram.Mermaid;
using BlazingClassDiagram.Models;
using BlazingClassDiagram.Syntax;
using CommandLine.Text;
using CommandLine;
using System.ComponentModel.DataAnnotations;

CommandLine.Parser.Default.ParseArguments<Options>(args)
    .WithParsed<Options>(o =>
    {
        var content = File.ReadAllText(@"C:\work\GitHub\CloudAdoptionFramework\ready\AzNamingTool\Models\PolicyDefinition\PolicyRule.cs");
        var root = new Root();
        root.Parse(content,o);        
        var mermaid = Renderer.Render(root);
    });


public class Options
{
    [Option('p', "path", Required = true, HelpText = "Set input path. (can be .cs file or an directory)")]
    public string Path { get; set; }

    [Option('o', "output", Required = false,Default = "output.mmd",  HelpText = "Set output file path.")]
    public string Output { get; set; }

    [Option('i', "include", Separator=',', Required = false, Default = new string[] { "class,record,interface,struct" }, HelpText = "Set include types.")]
    public IEnumerable<string>? IncludeTypes { get; set; }

    [Option('m',"modifier", Separator = ',', Required = false, Default = new string[] { "private,public,protected,internal" }, HelpText = "Set include modifier.")]
    public IEnumerable<string>? IncludeModifier { get; set; }

    [Option("verbose", Required = false, HelpText = "Set output to verbose messages.")]
    public bool Verbose { get; set; }

}