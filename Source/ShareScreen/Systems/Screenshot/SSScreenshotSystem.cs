using System;
using System.ComponentModel.Composition;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using SS.ShareScreen.Interfaces.System;

namespace SS.ShareScreen.Systems.Screenshot
{
    [Export(typeof(ISSScreenshotSystem))]
    public class SSScreenshotSystem : ISSScreenshotSystem
    {
        public void StartSystem()
        {
        }

        public void StopSystem()
        {
        }

        public Bitmap GetScreenshot()
        {
            Bitmap screenShot = new Bitmap(SystemInformation.VirtualScreen.Width,
                SystemInformation.VirtualScreen.Height,
                PixelFormat.Format32bppArgb);
            Graphics screen = Graphics.FromImage(screenShot);
            screen.CopyFromScreen(SystemInformation.VirtualScreen.X,SystemInformation.VirtualScreen.Y,
                0,
                0,
                SystemInformation.VirtualScreen.Size,
                CopyPixelOperation.SourceCopy);
            return screenShot;
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