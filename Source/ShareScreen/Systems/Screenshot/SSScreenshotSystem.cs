using System;
using System.ComponentModel.Composition;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using ShareScreen.Core.Interfaces.System;
using ShareScreen.Core.Windows;

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
            Contract.Requires(leftTop != default(Point));
            Contract.Requires(bottomRight != default(Point));
            var bound = new Rectangle(leftTop.X < bottomRight.X ? leftTop.X : bottomRight.X ,leftTop.Y < bottomRight.Y ? leftTop.Y : bottomRight.Y, Math.Abs(bottomRight.X - leftTop.X), Math.Abs(bottomRight.Y - leftTop.Y));
            var result = new Bitmap(bound.Width,bound.Height);
            using (var graphics = Graphics.FromImage(result))
            {
                graphics.CopyFromScreen(new Point(bound.X,bound.Y), Point.Empty, bound.Size);
            }
            return result;
        }

        public Bitmap GetScreenshtOfSelectedWindow(IntPtr handle)
        {
            Contract.Requires(handle != IntPtr.Zero);

            SSWindowsFunctions.RefreshWindow(handle);

            SSWindowsFunctions.RECT rc = new SSWindowsFunctions.RECT();

            SSWindowsFunctions.GetWindowRect(handle, out rc);
            var bound = new Rectangle(rc.Left,rc.Top,rc.Right - rc.Left,rc.Bottom - rc.Top);
            var result = new Bitmap(bound.Width,bound.Height);

            using (var graphics = Graphics.FromImage(result))
            {
                graphics.CopyFromScreen(new Point(bound.Left,bound.Top),Point.Empty,bound.Size);
            }
            return result;
        }
    }
}