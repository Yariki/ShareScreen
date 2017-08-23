using System;

namespace ShareScreen.Controls.EventArguments
{
    public class SSSelectionSizeChangedArgs : EventArgs
    {

        public SSSelectionSizeChangedArgs(double Width, double Height)
        {
            this.Height = Height;
            this.Width = Width;
        }

        public double Width { get; set; }

        public double Height { get; set; }
    }
}