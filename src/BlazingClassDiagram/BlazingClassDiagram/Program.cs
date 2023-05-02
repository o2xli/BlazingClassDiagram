using BlazingClassDiagram;
using BlazingClassDiagram.Mermaid;
using BlazingClassDiagram.Models;
using BlazingClassDiagram.Syntax;
using CommandLine;

CommandLine.Parser.Default//.ParseArguments<Options>(args);
//new CommandLine.Parser(settings =>
//{
//    settings.AutoVersion = true;
//    settings.AutoHelp = true;
//    settings.CaseInsensitiveEnumValues = true;
//    settings.EnableDashDash = true;
//})
.ParseArguments<Options>(args)
        .WithParsed<Options>(options =>
        {
            var fi = new FileInfo(options.Path);
            var di = new DirectoryInfo(options.Path);

            var root = new Root();
            if (fi.Exists)
                root.Parse(fi, options);
            else if (di.Exists)
                root.Parse(di, options);
            else
            {
                Console.WriteLine("Directory or File dosn't exists!");
                Environment.Exit(1);
            }

            var mermaid = Renderer.Render(root, options);
            if (File.Exists(options.Output))
            {
                Console.WriteLine($"Overwrite {options.Output} Yes / No ?");
                var key = Console.ReadKey();
                if (key.KeyChar.ToString().ToLower() == "y")
                    File.Delete(options.Output);
                else
                    Environment.Exit(0);
            }
            File.WriteAllText(options.Output, mermaid);
            Console.WriteLine($"Output written to {options.Output}");
        });



