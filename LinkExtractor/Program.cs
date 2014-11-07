using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkExtractor
{
    class Program
    {
        static void Main(string[] args)
        {
            string sourceTextFile = ( args.Length >= 1 ) ? args[0] : Console.ReadLine();

            if ( !File.Exists(sourceTextFile) )
            {
                Console.WriteLine("Oops! File {0} not found.", sourceTextFile);
                return;
            }

            Console.WriteLine("Working on file {0}", sourceTextFile);

            using ( StreamReader sr = new StreamReader(sourceTextFile) )
            {
                string targetTextFile = String.Format("linksextractedon{0}.txt",DateTime.Now.ToString("yyyyMMddHHmmss"));

                using (StreamWriter sw = new StreamWriter(targetTextFile, false, ASCIIEncoding.ASCII))
                {
                    string line;
                    string[] s;
                    int i = 0;

                    try
                    {
                        line = sr.ReadLine();

                        while ( !sr.EndOfStream )
                        {
                            if (String.IsNullOrEmpty(line))
                            {
                                line = sr.ReadLine();
                                continue;
                            }

                            s = line.Split(" ".ToCharArray());

                            foreach (string token in s)
                            {
                                if (Uri.IsWellFormedUriString(token, UriKind.Absolute))
                                {
                                    sw.WriteLine(token);

                                    Console.WriteLine("Adding url {0}", token);
                                    i++;
                                }
                            }

                            line = sr.ReadLine();
                        }

                        Console.WriteLine("{0} URLs proccessed.", i);
                        Console.ReadKey();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
        }
    }
}
