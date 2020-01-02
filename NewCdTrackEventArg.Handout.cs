using System;

namespace SWTEksamen
{
    public class NewCdTrackEventArg : EventArgs
    {
        public NewCdTrackEventArg(int track_)
        {
            track = track_;
        }
        public int track;
    }

}
