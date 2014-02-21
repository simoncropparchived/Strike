using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

class Program
{
    static List<Result> results = new List<Result>();
    static List<string> testTexts;
    static void Main()
    {
        testTexts = ReadFilesToText().ToList();
        testTexts.AddRange(testTexts);
        testTexts.AddRange(testTexts);
        testTexts.AddRange(testTexts);
        testTexts.AddRange(testTexts);


        Console.WriteLine("Bulk run is {0} documents", testTexts.Count);

        TestMarkdownSharp();
        TestMarkdownDeep();
        TestStrikeIE();
        TestStrikeV8();
        var output = new StringBuilder(); 
        output .AppendFormat(
@"
| Engine | Warm up | Construction |  Bulk ({0} docs) | Average per doc |
|:-------|:-------:|:------------:|:---------------:|:---------------:|
", testTexts.Count);
        foreach (var result in results)
        {
            output.AppendFormat("|{0}|{1} ms|{2} ms|{3} ms|**{4} ms**|\r\n", result.Name, result.Construction, result.FirstRun, result.BulkRun, result.Average);
        }

        ClipBoardHelper.SetClipboard(output.ToString());
    }

    static void TestStrikeIE()
    {
        var result = new Result {Name = "Strike"};
        var stopwatch = Stopwatch.StartNew();
        using (var warmup = new Strike.IE.Markdownify())
        {
            warmup.Transform(testTexts.First());
        }
        stopwatch.Stop();
        result.WarmUp = stopwatch.ElapsedMilliseconds;

        stopwatch = Stopwatch.StartNew();
        using (var markdownify = new Strike.IE.Markdownify())
        {
            stopwatch.Stop();
            result.Construction = stopwatch.ElapsedMilliseconds;

            stopwatch = Stopwatch.StartNew();
            markdownify.Transform(testTexts.First());
            stopwatch.Stop();
            result.FirstRun = stopwatch.ElapsedMilliseconds;

            stopwatch = Stopwatch.StartNew();
            foreach (var text in testTexts)
            {
                markdownify.Transform(text);
            }
            stopwatch.Stop();
            result.BulkRun = stopwatch.ElapsedMilliseconds;
            result.Average = GetAverage(stopwatch.ElapsedMilliseconds);
        }
        results.Add(result);
    }
    static void TestStrikeV8()
    {
        var result = new Result {Name = "Strike"};
        var stopwatch = Stopwatch.StartNew();
        using (var warmup = new Strike.V8.Markdownify())
        {
            warmup.Transform(testTexts.First());
        }
        stopwatch.Stop();
        result.WarmUp = stopwatch.ElapsedMilliseconds;

        stopwatch = Stopwatch.StartNew();
        using (var markdownify = new Strike.V8.Markdownify())
        {
            stopwatch.Stop();
            result.Construction = stopwatch.ElapsedMilliseconds;

            stopwatch = Stopwatch.StartNew();
            markdownify.Transform(testTexts.First());
            stopwatch.Stop();
            result.FirstRun = stopwatch.ElapsedMilliseconds;

            stopwatch = Stopwatch.StartNew();
            foreach (var text in testTexts)
            {
                markdownify.Transform(text);
            }
            stopwatch.Stop();
            result.BulkRun = stopwatch.ElapsedMilliseconds;
            result.Average = GetAverage(stopwatch.ElapsedMilliseconds);
        }
        results.Add(result);
    }

    static void TestMarkdownDeep()
    {

        var result = new Result {Name = "MarkdownDeep"};


        var stopwatch = Stopwatch.StartNew();
        var warmup = new MarkdownDeep.Markdown
        {
            ExtraMode = true,
            MarkdownInHtml = true,
            SafeMode = false,
        };
        stopwatch.Stop();
        result.WarmUp = stopwatch.ElapsedMilliseconds;

        warmup.Transform(testTexts.First());

        stopwatch = Stopwatch.StartNew();
        var markdownify = new MarkdownDeep.Markdown
        {
            ExtraMode = true, 
            MarkdownInHtml = true,
            SafeMode = false,
        };
        stopwatch.Stop();
        result.Construction = stopwatch.ElapsedMilliseconds;

        stopwatch = Stopwatch.StartNew();
        markdownify.Transform(testTexts.First());
        stopwatch.Stop();
        result.FirstRun = stopwatch.ElapsedMilliseconds;

        stopwatch = Stopwatch.StartNew();
        foreach (var text in testTexts)
        {
            markdownify.Transform(text);
        }
        stopwatch.Stop();
        result.BulkRun = stopwatch.ElapsedMilliseconds;
        result.Average = GetAverage(stopwatch.ElapsedMilliseconds);
        results.Add(result);
    }

    static void TestMarkdownSharp()
    {
        var result = new Result { Name = "MarkdownSharp" };
        var stopwatch = Stopwatch.StartNew();
        var warmup = new MarkdownSharp.Markdown();
        warmup.Transform(testTexts.First());
        stopwatch.Stop();
        result.WarmUp = stopwatch.ElapsedMilliseconds;

        stopwatch = Stopwatch.StartNew();
        var markdownify = new MarkdownSharp.Markdown();

        stopwatch.Stop();
        result.Construction = stopwatch.ElapsedMilliseconds;

        stopwatch = Stopwatch.StartNew();
        markdownify.Transform(testTexts.First());
        stopwatch.Stop();
        result.FirstRun = stopwatch.ElapsedMilliseconds;

        stopwatch = Stopwatch.StartNew();
        foreach (var text in testTexts)
        {
            markdownify.Transform(text);
        }
        stopwatch.Stop();
        result.BulkRun = stopwatch.ElapsedMilliseconds;
        result.Average = GetAverage(stopwatch.ElapsedMilliseconds);
        results.Add(result);
    }

    static IEnumerable<string> ReadFilesToText()
    {
        var testFileDir = Path.Combine(Path.GetDirectoryName(typeof (Program).Assembly.Location), "MarkdownTest");
        return Directory.EnumerateFiles(testFileDir, "*.text").Select(File.ReadAllText);
    }


    static decimal GetAverage(long elapsedMilliseconds)
    {
        return Math.Round((decimal)elapsedMilliseconds / testTexts.Count, 2);
    }
}