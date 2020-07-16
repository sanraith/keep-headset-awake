# keep-headset-awake

Keeps a wireless headset awake by periodically playing sounds inaudible to the human ear.  
I made this because my HyperX Cloud Flight headset automatically turns off after ~15 minutes of inactivity.

Requirements:

- .NET Core 3.1 SDK

Usage:

- Edit `appsettings.json` to customize the beep length, frequency and interval.
- Start with `dotnet run`
- Press CTRL+C to stop.
