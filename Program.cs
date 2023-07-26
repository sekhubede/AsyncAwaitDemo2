bool running = true;

Initialize();

while (running)
{
    Console.Write("> ");
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
