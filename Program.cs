using System;

using SWTEksamen.Interfaces;

namespace SWTEksamen
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            IButton start = new PlayPauseButton();
            IButton stop = new StopButton();
            IButton eject = new EjectButton();
            IDriverinterface driver = new DriverInterface();
            PlayPauseButton play = new PlayPauseButton();

        }
    }
}
