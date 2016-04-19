using System;
using System.ComponentModel.Composition;
using System.Drawing;
using SS.ShareScreen.Interfaces.System;

namespace SS.ShareScreen.Systems.Screenshot
{
    [Export(typeof(ISSScreenshotSystem))]
    public class SSScreenshotSystem : ISSScreenshotSystem
    {
        public void StartSystem()
        {
            throw new NotImplementedException();
        }

        public void StopSystem()
        {
            throw new NotImplementedException();
        }

        public Bitmap GetScreenshot()
        {
            return null;
        }

        public Bitmap GetScreenshotOfSelectedArea(Point leftTop, Point bottomRight)
        {
            throw new NotImplementedException();
        }

        public Bitmap GetScreenshtOfSelectedWindow(IntPtr handle)
        {
            throw new NotImplementedException();
        }
    }
}