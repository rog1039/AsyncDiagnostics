using System;
using System.Threading;
using System.Threading.Tasks;
using Nito.AsyncEx.AsyncDiagnostics;
using Xunit;

[assembly: AsyncDiagnosticAspect]

class Program
{
    public static void Main(string[] args)
    {
        MainAsync(args).Wait();
    }

    static async Task MainAsync(string[] args)
    {
        try
        {
            await MyMethodAsync("I'm an asynchronous exception! Locate me if you can!");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToAsyncDiagnosticString());
            //Console.ReadKey();
        }
    }

    static Task MyMethodAsync(string message)
    {
        using (AsyncDiagnosticStack.Enter("  My message is: " + message))
        {
            return MyMethodAsync(message, CancellationToken.None);
        }
    }

    static async Task MyMethodAsync(string message, CancellationToken token)
    {
        var task1 = Task.Delay(1000);
        var task2 = Task.Run(() => { throw new InvalidOperationException(message); });
        await Task.WhenAll(task1, task2);
    }
}

public class MyUnitTests
{
    [Fact]
    public void TestOne()
    {
        Program.Main(null);
        Program.Main(null);
        Program.Main(null);
        Program.Main(null);
        Program.Main(null);
    }

    [Fact]
    public void TestTwo()
    {
        Program.Main(null);
        Program.Main(null);
        Program.Main(null);
        Program.Main(null);
        Program.Main(null);
    }
    [Fact]
    public void TestThree()
    {
        Program.Main(null);
        Program.Main(null);
        Program.Main(null);
        Program.Main(null);
        Program.Main(null);
    }

}