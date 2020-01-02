using System;
using System.Collections.Generic;
using System.Text;
using SWTEksamen.Interfaces;

namespace SWTEksamen
{
    public class PlayPauseButton : IButton
    {
        public event EventHandler ButtonPressedEvent;

        public virtual void OnButtonPressedEvent()
        {
            ButtonPressedEvent?.Invoke(this, EventArgs.Empty);
        }
    }
}
