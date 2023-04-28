// See https://aka.ms/new-console-template for more information

using BlazingClassDiagram.Models;
using BlazingClassDiagram.Syntax;

var content = File.ReadAllText(@"C:\work\GitHub\CloudAdoptionFramework\ready\AzNamingTool\Models\PolicyDefinition\PolicyRule.cs");
var root = new Root();
root.Parse(content);
