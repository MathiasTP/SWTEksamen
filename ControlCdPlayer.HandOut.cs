using System;
using SWTEksamen;
using SWTEksamen.Interfaces;

namespace SWTEksamen
{
    public class ControlCdPlayer
    {
        private enum State
        {
            Ready,
            TrayOpen,
            Playing,
            Paused,
        }

        private State state;
        private ITrayInterface Tray;
        private IDriverinterface Driver;
        private IDisplay Display;

        // Her mangler constructoren
        public ControlCdPlayer(IButton start, IButton stop, IButton eject, IDriverinterface driver, IDisplay display, ITrayInterface tray)
        {
            state = State.Ready;

            this.Driver = driver;
            this.Tray = tray;
            this.Display = display;

            Display.Clear();

            eject.ButtonPressedEvent += EjectPressed;
            start.ButtonPressedEvent += StartPausePressed;
            stop.ButtonPressedEvent += StopPressed;
            driver.EndOfCd += EndOfCd;
            driver.NewCd += NewCdTrack;
        }

        private void EjectPressed(object o, EventArgs e)
        {
            // Her mangler kode til at behandle denne trigger
            // Lad dig inspirere af de nedenstående implementationer for andre triggere

            switch (state)
            {
                case State.Ready:
                    Tray.Open();
                    Display.Clear();
                    state = State.TrayOpen;
                    break;

                case State.TrayOpen:
                    Tray.Close();
                    state = State.Ready;
                    Display.Write("Ready");
                    break;

                case State.Playing:
                    Driver.Stop();
                    Tray.Open();
                    Display.Clear();
                    break;

                case State.Paused:
                    Tray.Open();
                    Driver.Stop();
                    Display.Clear();
                    state = State.TrayOpen;
                    break;
            }
        }

        private void StartPausePressed(object o, EventArgs e)
        {
            // Her mangler kode til at behandle denne trigger
            // Lad dig inspirere af de nedenstående implementationer for andre triggere
            switch (state)
            {
                case State.Ready:
                    Driver.Start();
                    state = State.Playing;
                    break;

                case State.TrayOpen:
                    // Ignore
                    break;

                case State.Playing:
                    Driver.Pause();
                    state = State.Paused;
                    Display.Write("Pause");
                    break;

                case State.Paused:
                    Driver.Start();
                    state = State.Playing;
                    break;
            }

        }

        private void StopPressed(object o, EventArgs e)
        {
            // Implementation af denne trigger ifølge tilstandsdiagrammet

            switch (state)
            {
                case State.Ready:
                    // Ignore
                    break;

                case State.TrayOpen:
                    // Ignore
                    break;

                case State.Playing:
                    Driver.Stop();
                    Display.Write("Stopped");
                    state = State.Ready;
                    break;

                case State.Paused:
                    Driver.Stop();
                    Display.Write("Stopped");
                    state = State.Ready;
                    break;
            }
        }

        private void EndOfCd(object o, EventArgs e)
        {
            // Implementation af denne trigger ifølge tilstandsdiagrammet

            switch (state)
            {
                case State.Ready:
                    // Ignore, not possible
                    break;

                case State.TrayOpen:
                    // Ignore, not possible
                    break;

                case State.Playing:
                    Display.Write("End");
                    state = State.Ready;
                    break;

                case State.Paused:
                    // Ignore, not possible
                    break;
            }
        }

        private void NewCdTrack(object sender, NewCdTrackEventArg e)
        {
            // Implementation af denne trigger ifølge tilstandsdiagrammet
            // NewCdTrackEventArg kan bruges som data for en event fra DriveIF
            // Definitionen for NewCdTrackEventArg findes i en anden fil

            switch (state)
            {
                case State.Ready:
                    // Ignore, not possible
                    break;

                case State.TrayOpen:
                    // Ignore, not possible
                    break;

                case State.Playing:
                    Display.Show(e.track);
                    break;

                case State.Paused:
                    // Ignore, not possible
                    break;
            }
        }
    }
}
