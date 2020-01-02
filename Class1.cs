using System;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using NUnit.Framework;
using NUnit.Framework.Internal;
using SWTEksamen;
using SWTEksamen.Interfaces;

namespace TestProjektEksamen
{
    [TestFixture]
    public class ControlCdPlayerTest
    {
        private IButton startpausButton;
        private IButton ejectButton;
        private IButton stopButton;
        private IDriverinterface driver;
        private IDisplay display;
        private ITrayInterface tray;
        private ControlCdPlayer uut;
        private NewCdTrackEventArg newCd;


        [SetUp]
        public void SetUp()
        {
            //Arrange
            startpausButton = Substitute.For<IButton>();
            ejectButton = Substitute.For<IButton>();
            stopButton = Substitute.For<IButton>();

            driver = Substitute.For<IDriverinterface>();
            tray = Substitute.For<ITrayInterface>();
            display = Substitute.For<IDisplay>();

            

            uut = new ControlCdPlayer(startpausButton, stopButton, ejectButton, driver, display, tray);
        }


        [Test]
        public void StartButtonPressed()
        {
            //Act
            startpausButton.ButtonPressedEvent += Raise.EventWith(this, EventArgs.Empty);
            //Assert
            driver.Received().Start();

            //Act
            startpausButton.ButtonPressedEvent += Raise.EventWith(this, EventArgs.Empty);
            //Assert
            driver.Received(1).Pause();

            //Act
            startpausButton.ButtonPressedEvent += Raise.EventWith(this, EventArgs.Empty);
            //Assert
            driver.Received().Start();

        }


        [Test]
        public void StopButtonPressed()
        {
            startpausButton.ButtonPressedEvent += Raise.EventWith(this, EventArgs.Empty); // Kaldes for at få cdplayer i playing state
            stopButton.ButtonPressedEvent += Raise.EventWith(this, EventArgs.Empty);

            driver.Received(1).Stop();
            display.Received().Write("Stopped");

            startpausButton.ButtonPressedEvent += Raise.EventWith(this, EventArgs.Empty);
            startpausButton.ButtonPressedEvent += Raise.EventWith(this, EventArgs.Empty); // Pause state
            stopButton.ButtonPressedEvent += Raise.EventWith(this, EventArgs.Empty);

            driver.Received().Stop();
            display.Received().Write("Stopped");

        }
        [Test]
        public void EjectButtonPressed()
        {
            ejectButton.ButtonPressedEvent += Raise.EventWith(this, EventArgs.Empty); // ready state

            tray.Received(1).Open();
            display.Received().Clear();

            ejectButton.ButtonPressedEvent += Raise.EventWith(this, EventArgs.Empty); // Trayopen state

            tray.Received(1).Close();
            display.Received().Write("Ready");
        }

        [Test]
        public void EjectButtonPressedPlayingAndPauseState()
        {
            startpausButton.ButtonPressedEvent += Raise.EventWith(this, EventArgs.Empty); // playing state 
            ejectButton.ButtonPressedEvent += Raise.EventWith(this, EventArgs.Empty);

            driver.Received().Stop();
            tray.Received().Open();
            display.Received().Clear();

            startpausButton.ButtonPressedEvent += Raise.EventWith(this, EventArgs.Empty); // pause state
            ejectButton.ButtonPressedEvent += Raise.EventWith(this, EventArgs.Empty);

            driver.Received().Stop();
            tray.Received().Open();
            display.Received().Clear();
        }

        [Test]
        public void EndOfCDtest()
        {
            startpausButton.ButtonPressedEvent += Raise.EventWith(this, EventArgs.Empty);
            driver.EndOfCd += Raise.EventWith(this, EventArgs.Empty);

            display.Received().Write("End");
        }

        [Test]
        public void NewCdTrackTest()
        {
            //Arrange
            newCd = new NewCdTrackEventArg(2);
            //Act
            startpausButton.ButtonPressedEvent += Raise.EventWith(this, EventArgs.Empty);
            driver.NewCd += Raise.EventWith(this, newCd);
            //Assert
            display.Received().Show(2);
        }
    }
}
