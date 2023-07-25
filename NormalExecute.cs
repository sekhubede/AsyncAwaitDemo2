using System.Diagnostics;
using System.Net;

internal class NormalExecute
{
    public static void ExecuteSync()
    {
        var watch = Stopwatch.StartNew();

        RunDownloadSync();

        watch.Stop();

        var elapsedMs = watch.ElapsedMilliseconds;

        Console.WriteLine($"Total execution time: {elapsedMs}");

    }

    private static void RunDownloadSync()
    {
        List<string> websites = PrepData();

        foreach ( string site in websites )
        {
            WebsiteDataModel results = DownloadWebsite(site);
            ReportWebsiteInfo(results);
        }
    }

    private static void ReportWebsiteInfo(WebsiteDataModel data)
    {
        Console.WriteLine($"{data.WebsiteUrl} downloaded: {data.WebsiteData.Length} characters long");
    }

    private static WebsiteDataModel DownloadWebsite(string websiteURL)
    {
        WebsiteDataModel output = new WebsiteDataModel();
        WebClient client = new WebClient();

        output.WebsiteUrl = websiteURL;
        output.WebsiteData = client.DownloadString(websiteURL);

        return output;
    }

    private static List<string> PrepData()
    {
        List<string> output = new List<string>();

        Console.Clear();

        output.Add("https://www.yahoo.com");
        output.Add("https://www.google.com");
        output.Add("https://www.codeproject.com");
        output.Add("https://www.documentwarehouse.com.na");
        //output.Add("https://www.mfiles.com");

        return output;
    }
}
