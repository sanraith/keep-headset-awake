using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace keep_headset_awake
{
    public sealed class Program
    {
        async static Task Main(string[] args)
        {
            var configFilePath = Path.Join(Directory.GetParent(AppContext.BaseDirectory).FullName, "appsettings.json");
            var config = new BeepConfig();
            try
            {
                var configContent = File.ReadAllText(configFilePath, Encoding.UTF8);
                config = JsonSerializer.Deserialize<BeepConfig>(configContent);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.WriteLine("Could not load appsettings.json!");
            }
            await new Program { myConfig = config }.RunForever();
        }

        private async Task RunForever()
        {
            var beepFrequency = myConfig.BeepFrequencyHz;
            var beepLength = TimeSpan.FromSeconds(myConfig.BeepDurationSeconds);
            var beepInterval = TimeSpan.FromSeconds(myConfig.BeepIntervalSeconds);

            Console.WriteLine($"Started: {DateTime.Now}");
            Console.WriteLine($"Frequency: {beepFrequency} Hz, Duration: {beepLength}, Interval: {beepInterval}");
            Console.WriteLine();

            while (true)
            {
                WriteStatus($"Currently beeping until {DateTime.Now + beepLength:HH:mm:ss}.");
                Console.Beep(beepFrequency, (int)beepLength.TotalMilliseconds);
                WriteStatus($"Currently not beeping until {DateTime.Now + beepInterval:HH:mm:ss}.");
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
        public int BeepFrequencyHz { get; set; } = 18000;
        public int BeepDurationSeconds { get; set; } = 60;
        public int BeepIntervalSeconds { get; set; } = 240;
    }
}
