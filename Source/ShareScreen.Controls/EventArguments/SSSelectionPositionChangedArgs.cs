using System;

namespace ShareScreen.Controls.EventArguments
{
    public class SSSelectionPositionChangedArgs : EventArgs
    {

        public SSSelectionPositionChangedArgs(double vert, double horz)
        {
            HorizontalDelta = horz;
            VerticalDelta = vert;
        }

        public double HorizontalDelta { get; private set; }

        public double VerticalDelta { get; private set; }

    }
}