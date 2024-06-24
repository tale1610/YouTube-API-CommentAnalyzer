using java.io;
using opennlp.tools.namefind;
using opennlp.tools.util;
using OpenNLP.Tools.Tokenize;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouTube_API_Comments_Analyzer
{
    public static class NamedEntityRecognition
    {
        private static readonly object lockObj = new object();
        public static string Analyze(string comments)
        {
            string[] tokens;
            lock (lockObj)
            {
                var tokenizer = new EnglishRuleBasedTokenizer(true);
                tokens = tokenizer.Tokenize(comments);
            }
            //int i = 0;
            //foreach (var token in tokens)
            //{
            //    System.Console.WriteLine("Nit" + Thread.CurrentThread.ManagedThreadId.ToString() + " indexReci[" + i.ToString() + "] " + token);
            //    i++;
            //}

            StringBuilder recognizedEntities = new StringBuilder();
            recognizedEntities.Append("Imena u komentarima:\n");

            try
            {
                //using (var modelFile = new FileInputStream("C:\\Users\\tasko\\Downloads\\ner\\en-ner-person.bin"))
                using (var modelFile = new FileInputStream("C:\\Users\\tasko\\Downloads\\ner\\en-ner-person.bin"))
                {
                    var modelStream = new TokenNameFinderModel(modelFile);
                    var model = new NameFinderME(modelStream);
                    model.clearAdaptiveData();//OVO JE RESILO PROBLEM
                    var spans = model.find(tokens);

                    if (spans.Length > 0)
                    {
                        //System.Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} span length: " + spans.Length);

                        foreach (Span span in spans)
                        {
                            //System.Console.WriteLine($"Nit{Thread.CurrentThread.ManagedThreadId} " + span.ToString());
                            for (int j = span.getStart(); j < span.getEnd(); j++)
                            {
                                recognizedEntities.Append(tokens[j] + " ");
                            }
                            recognizedEntities.Append("\n");
                        }
                    }
                    else
                    {
                        recognizedEntities.Append("Na ovom videu nema imena u komentarima\n");
                        //System.Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} Na ovom videu nema imena u komentarima");
                    }
                    //model.clearAdaptiveData();//OVO JE RESILO PROBLEM ne bas ipak ovde, kad sam ga popeo tamo gore to je resilo za kad su 3 zahteva, u sustini to kaze da model odma pocne sa fresh start a ne da mesa sta je naucio dodatno iz prethodnih interakcija
                }
            }
            catch (System.IO.FileNotFoundException e)
            {
                System.Console.WriteLine(e.StackTrace);
            }
            catch (System.IO.IOException e)
            {
                System.Console.WriteLine(e.StackTrace);
            }
            recognizedEntities.Append("================================================");
            return recognizedEntities.ToString().Trim();
        }
    }
}
