using opennlp.tools.namefind;
using OpenNLP.Tools.NameFind;
using OpenNLP.Tools.Tokenize;
using System.Xml.Linq;
using java.io;
using FileNotFoundException = System.IO.FileNotFoundException;
using System;
using opennlp.tools.util;
using javax.swing.table;
using NReco.NLQuery.Table;
using NReco.NLQuery;

namespace YouTube_API_Comments_Analyzer;
public class Program
{
    public static void Main()
    {
        //var modelPath = "C:\\Users\\tasko\\Downloads\\ner\\en-ner-";
        //var nameFinder = new EnglishNameFinder(modelPath);
        //var sentence = "Mr. & Mrs. Smith is a 2005 American romantic comedy action film.";
        //string[] models = ["date", "location", "money", "organization", "percentage", "person", "time"];
        //var ner = nameFinder.GetNames(models, sentence);
        //System.Console.WriteLine(string.Join(", ", ner));



        //String inputFileName = "en-ner-person.bin";
        //String[] inputString = new String[] { "Mike", "and", "Smiths", "are", "Joe", "Frazier", "Howard" };
        //var sentence = "Joe Frazier was the heavyweight champion of the world after he beat Muhammad Ali, but then he lost the title to Joe Forman.";

        //var tokenizer = new EnglishRuleBasedTokenizer(true);
        //var tokens = tokenizer.Tokenize(sentence);

        //NameFinderME model = null;
        //InputStream modelFile = null;
        //TokenNameFinderModel modelStream = null;

        //try
        //{
        //    modelFile = new FileInputStream("C:\\Users\\tasko\\Downloads\\ner\\en-ner-person.bin");
        //    modelStream = new TokenNameFinderModel(modelFile);

        //}
        //catch (FileNotFoundException e)
        //{
        //    System.Console.WriteLine(e.StackTrace.ToString());
        //}
        //catch (System.IO.IOException e)
        //{
        //    System.Console.WriteLine(e.StackTrace.ToString());
        //}
        //finally
        //{
        //    try
        //    {
        //        modelFile.close();
        //    }
        //    catch (System.IO.IOException e)
        //    {
        //        // TODO Auto-generated catch block
        //        System.Console.WriteLine(e.StackTrace.ToString());
        //    }
        //}

        //model = new NameFinderME(modelStream);
        //opennlp.tools.util.Span[] spans = model.find(tokens);

        //System.Console.WriteLine("span length: " + spans.Length);

        //foreach (Span span in spans)
        //{
        //    System.Console.WriteLine(span.toString());
        //}



        //govno usrano mrtvo jebem mu sve da mu jebem u picku

        var webServer = new WebServer();
        webServer.Start();
        System.Console.ReadLine();
        webServer.Stop();
    }
}
