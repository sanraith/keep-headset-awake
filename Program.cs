using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace keep_headset_alive
{
    public sealed class Program
    {
        async static Task Main(string[] args)
        {
            var config = new BeepConfig();
            new ConfigurationBuilder()
                .SetBasePath(System.IO.Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json", false)
                .Build()
                .Bind(config);

            await new Program { myConfig = config }.RunForever();
        }

        private async Task RunForever()
        {
            var beepFrequency = myConfig.BeepFrequency;
            var beepLength = TimeSpan.FromSeconds(myConfig.BeepDurationSeconds);
            var beepInterval = TimeSpan.FromSeconds(myConfig.BeepIntervalSeconds);

            Console.WriteLine();
            while (true)
            {
                WriteStatus($"Beeping     until {DateTime.Now + beepLength:HH:mm:ss} at {beepFrequency} Hz.");
                Console.Beep(beepFrequency, (int)beepLength.TotalMilliseconds);
                WriteStatus($"Not beeping until {DateTime.Now + beepLength:HH:mm:ss} at {beepFrequency} Hz.");
                await Task.Delay(beepInterval);
            }
        }

        private void WriteStatus(string text)
        {
            Console.Write($"\r{new string(' ', myPrevStatusWidth)}");
            Console.Write($"\r{text}");
            myPrevStatusWidth = text.Length;
        }

        private BeepConfig myConfig;
        private int myPrevStatusWidth = 0;
    }

    public sealed class BeepConfig
    {
        public int BeepFrequency { get; set; }
        public int BeepDurationSeconds { get; set; }
        public int BeepIntervalSeconds { get; set; }
    }
}
