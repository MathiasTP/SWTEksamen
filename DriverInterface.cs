using System;
using System.Collections.Generic;
using System.Text;

using SWTEksamen.Interfaces;

namespace SWTEksamen
{
    class DriverInterface : IDriverinterface
    {
        public event EventHandler<NewCdTrackEventArg> NewCd;
        public event EventHandler EndOfCd;

        public void Start()
        {
            
        }

        public void Stop()
        {
            
        }

        public void Pause()
        {
           
        }
    }
}
