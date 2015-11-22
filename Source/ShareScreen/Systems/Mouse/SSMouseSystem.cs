using System;
using System.ComponentModel.Composition;
using System.Windows;
using SS.ShareScreen.Interfaces.System;

namespace SS.ShareScreen.Systems.Mouse
{
    [Export(typeof(ISSMouseSystem))]
    public class SSMouseSystem : ISSMouseSystem
    {
        public void StartSystem()
        {
            throw new NotImplementedException();
        }

        public void StopSystem()
        {
            throw new NotImplementedException();
        }

        public Tuple<Point, Point> GetSelectedArea()
        {
            throw new NotImplementedException();
        }

        public IntPtr GetSelectedWindow()
        {
            throw new NotImplementedException();
        }
    }
}