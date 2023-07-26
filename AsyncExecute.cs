

using System.Diagnostics;
using System.Net;

internal static class AsyncExecute
{
    public static async Task ExecuteAsync()
    {
        var watch = Stopwatch.StartNew();

        await RunDownloadParallelAsync();

        watch.Stop();

        var elapsedMs = watch.ElapsedMilliseconds;

        Console.WriteLine($"Total execution time: {elapsedMs}");
    }

    private static async Task RunDownloadParallelAsync()
    {
        List<string> websites = PrepData();

        List<Task<WebsiteDataModel>> tasks = new List<Task<WebsiteDataModel>>();

        foreach (string site in websites)
        {
            tasks.Add(Task.Run(() => DownloadWebsite(site)));
        }

        var results = await Task.WhenAll(tasks);

        foreach(var item in results)
        {
            ReportWebsiteInfo(item);
        }
    }

    private static async Task RunDownloadAsync()
    {
        List<string> websites = PrepData();

        foreach (string site in websites)
        {
            WebsiteDataModel results = await Task.Run(() => DownloadWebsite(site));
            ReportWebsiteInfo(results);
        }
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

    private static WebsiteDataModel DownloadWebsite(string websiteURL)
    {
        WebsiteDataModel output = new WebsiteDataModel();
        WebClient client = new();

        output.WebsiteUrl = websiteURL;
        output.WebsiteData = client.DownloadString(websiteURL);

        return output;
    }

    private static void ReportWebsiteInfo(WebsiteDataModel data)
    {
        Console.WriteLine($"{data.WebsiteUrl} downloaded: {data.WebsiteData.Length} characters long");
    }
}