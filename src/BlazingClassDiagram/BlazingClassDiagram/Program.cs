// See https://aka.ms/new-console-template for more information

using BlazingClassDiagram.Mermaid;
using BlazingClassDiagram.Models;
using BlazingClassDiagram.Syntax;
using CommandLine.Text;
using CommandLine;

CommandLine.Parser.Default.ParseArguments<Options>(args)
    .WithParsed<Options>(o =>
    {
        var content = File.ReadAllText(@"C:\work\GitHub\CloudAdoptionFramework\ready\AzNamingTool\Models\PolicyDefinition\PolicyRule.cs");
        var root = new Root();
        root.Parse(content);
        var mermaid = Renderer.Render(root);
    });


public class Options
{
    [Option('v', "verbose", Required = false, HelpText = "Set output to verbose messages.")]
    public bool Verbose { get; set; }

    [Option('p', "path", Required = false, HelpText = "Set input path. (can be .cs file or an directory)")]
    public string Path { get; set; }


}