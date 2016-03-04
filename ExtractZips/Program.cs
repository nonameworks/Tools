using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;
using System.IO;

namespace ExtractZips
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Open zip using this application or pass it as an agument");
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
                return;
            }

            try
            {
                var source = args[0];
                var destination = extractFiles(source);

                while (true)
                {
                    var files = Directory.GetFiles(destination, "*.zip");
                    if (files.Length == 0) break;
                    foreach (var file in files)
                    {
                        extractFiles(file);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Open zip using this application or pass it as an agument");
                Console.WriteLine(ex.Message);
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
                return;
            }


        }

        private static string extractFiles(string source)
        {
            string destination = source.Replace(".zip", "");
            Directory.CreateDirectory(destination);
            using (ZipArchive archive = ZipFile.OpenRead(source))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    var newName = Path.Combine(destination, entry.Name);
                    if (!File.Exists(newName) && entry.Length > 0)
                    {                        
                        entry.ExtractToFile(newName);
                    }
                }
            }

            File.Move(source, source + "D");

            return destination;
        }
    }
}
