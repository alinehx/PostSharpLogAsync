using System.Threading.Tasks;

namespace PostSharpLogAsync
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync().Wait();
        }

        static async Task MainAsync()
        {
            SampleAsync sample = new SampleAsync();
            await sample.GetExceptionAsync(value: 1, name: "teste").ConfigureAwait(false);
        }
    }
}

