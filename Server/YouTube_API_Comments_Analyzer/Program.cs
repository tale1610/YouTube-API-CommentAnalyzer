using OpenNLP.Tools.NameFind;
using OpenNLP.Tools.Tokenize;
using System.Xml.Linq;

namespace YouTube_API_Comments_Analyzer;
public class Program
{
    public static void Main()
    {
        //var modelPath = "C:\\Users\\tasko\\Downloads\\ner\\en-ner-";
        //var nameFinder = new CustomEnglishNameFinder(modelPath);
        //var sentence = "Mr. & Mrs. Smith is a 2005 American romantic comedy action film.";
        //// specify which types of entities you want to detect
        //string[] models = ["date", "location", "money", "organization", "percentage", "person", "time"];
        //var ner = nameFinder.GetNames(models, sentence);
        //// ner = Mr. & Mrs. <person>Smith</person> is a <date>2005</date> American romantic comedy action film.
        //Console.WriteLine(string.Join(",", ner));

        //govno usrano mrtvo jebem mu sve da mu jebem u picku

        var webServer = new WebServer();
        webServer.Start();
        Console.ReadLine();
        webServer.Stop();
    }
}
