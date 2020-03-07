using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using DiseasesData;

namespace Population
{
    class Program
    {
        static string uline(string text)
        {
            string underline = "\x1B[4m";
            string reset = "\x1B[0m";
            return $"{underline}{text}{reset}";
        }

        static void Main(string[] args)
        {
            DiseasesContext ctx = new DiseasesContext();
            // clear DB
            ctx.Diseases.RemoveRange(ctx.Diseases);
            ctx.Names.RemoveRange(ctx.Names);
            ctx.Causes.RemoveRange(ctx.Causes);
            ctx.SaveChanges();

            // dir must be in bin/Debug/netcoreapp3.0
            string dirPath = "files";
            Regex re = new Regex($@"{dirPath}\\([a-zA-Z ]+)\.txt");
            foreach (string filePath in Directory.GetFiles(dirPath, "*.txt"))
            {
                string category = re.Match(filePath).Groups[1].Value;
                Console.WriteLine(category);
                Console.WriteLine(string.Concat(Enumerable.Repeat("=", category.Length + 4)));

                List<Disease> diseases = new List<Disease>();
                foreach (string line in File.ReadAllText(filePath).Split('\n'))
                {
                    Disease disease = new Disease();
                    Name name = new Name(disease, line.Trim());
                    ctx.Diseases.Add(disease);
                    ctx.Names.Add(name);
                    Console.WriteLine($"Added {uline(disease.StringName)}");

                    diseases.Add(disease);
                }
                Console.WriteLine();

                Random rand = new Random();
                for (int i = 0; i < diseases.Count; i += 1)
                {
                    for (int j = 0; j < rand.Next(1, 5); j += 1)
                    {
                        int di = -1;
                        do
                        {
                            di = rand.Next(diseases.Count);
                        } while (i == di);

                        Cause cause = new Cause(diseases[i], diseases[di]);
                        try
                        {
                            ctx.Causes.Add(cause);
                            ctx.SaveChanges();
                            Console.WriteLine($"{uline(diseases[i].StringName)} is comorbid with {uline(diseases[di].StringName)}");
                        }
                        catch
                        {
                            ctx.Causes.Remove(cause);
                            j -= 1;
                        }
                    }
                }
            }
        }
    }
}
