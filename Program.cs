

using System.Diagnostics;
using System.Net;
using System.Runtime.InteropServices;

bool running = true;

Initialize();

while (running)
{
    Console.Write(">");
    Int32.TryParse(Console.ReadLine(), out int input);
    ProcessInput(input);
}

Console.WriteLine();
Console.WriteLine("Press any key to exit.");
Console.ReadKey(true);
Environment.Exit(0);

void Initialize()
{
    PrintHeader();
}

void PrintHeader()
{
    Console.WriteLine("----------------------------------");
    Console.WriteLine("Welcome to the Async/Await Demo");
    Console.WriteLine("Type \"0\" for available commands.");
    Console.WriteLine("----------------------------------");
}

void ProcessInput(int input)
{
    switch (input)
    {
        case 0:
            PrintHelp();
            break;
        case 1:
            NormalExecute.ExecuteSync();
            break;
        case 2:
            AsyncExecute.ExecuteAsync();
            break;
        default:
            PrintInvalidCommand();
            break;
    }
}

void PrintHelp()
{
    Console.WriteLine("\tAvailable Commands:");
    Console.WriteLine("\t1 - Normal Execute");
    Console.WriteLine("\t2 - Async Execute");
    Console.WriteLine();
}

void PrintInvalidCommand()
{
    Console.WriteLine();
    Console.WriteLine("Command not recognized, please try again.");
    Console.WriteLine("Type \"0\" for available commands.");
    Console.WriteLine();
}

Console.WriteLine("Starting...");
Console.ReadKey(true);

internal static class AsyncExecute
{
    public static async Task ExecuteAsync()
    {
        var watch = Stopwatch.StartNew();

        await RunDownloadAsync();

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