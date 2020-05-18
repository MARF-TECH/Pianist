using System;
using System.IO;
using System.Collections.Generic;

using CommandLine;
using CommandLine.Text;

using Microsoft.Extensions.DependencyInjection;

namespace PianistAnalyser.Desktop
{
    class ProgramUsage
    {
        [Option('f', "read", Required = true, HelpText = "Partition csv file")]
        public string filename { get; set; }

        [Usage(ApplicationAlias = "Pianist Analyser")]
        public static IEnumerable<Example> Examples => new List<Example>() {
                  new Example("Exemple", new ProgramUsage { filename = "partition.csv" })
        };
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Parser.Default.ParseArguments<ProgramUsage>(args)
                      .WithParsed(o =>
                      {
                          // - file exists ?
                          if (!File.Exists(o.filename))
                          {
                              Console.WriteLine("Error: partition file does not exist");
                              return;
                          }

                          // - register dependency
                          var services = DependencyInjection.ConfigureServices();

                          // - run application
                          var serviceProvider = services.BuildServiceProvider();
                          serviceProvider.GetService<ConsoleApplication>().Run(o.filename);

                      });
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: partition file contains errors");
                Console.WriteLine(e.Message);
            }
        }
    }
}
