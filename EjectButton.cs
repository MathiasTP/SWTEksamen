using System;
using System.Collections.Generic;
using System.Text;
using SWTEksamen.Interfaces;

namespace SWTEksamen
{
   public class EjectButton : IButton
    {
        public event EventHandler ButtonPressedEvent;

        public void OnButtonPressedEvent()
        {
            ButtonPressedEvent?.Invoke(this,EventArgs.Empty);
        }
    }
}
