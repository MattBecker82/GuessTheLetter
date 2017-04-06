using System;

public class ConsoleUi
{
    public void Initialize()
    {
        Console.Clear();
    }

    public void WaitMessage(string text)
    {
        Message("{0} Press Enter to continue.", text);
        Console.ReadLine();
    }

    public void Message(string formatString, params object[] args)
    {
        Console.WriteLine(formatString, args);
    }

    public bool AskYesOrNo(string formatString, params object[] args)
    {
        Message("{0} (Enter Y for yes, N for no)", string.Format(formatString, args));

        var response = Console.ReadLine().ToLowerInvariant();
        while (!(response.StartsWith("y") || response.StartsWith("n")))
        {
            response = Console.ReadLine().ToLowerInvariant();
        }

        return response.StartsWith("y");
    }
}
