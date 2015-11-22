using System.ComponentModel.Composition;
using SS.ShareScreen.Interfaces.System;

namespace SS.ShareScreen.Systems.Keyboard
{
    [Export(typeof(ISSKeyboardSystem))]
    public class SSKeyboardSystem : ISSKeyboardSystem
    {
        public void StartSystem()
        {
            throw new System.NotImplementedException();
        }

        public void StopSystem()
        {
            throw new System.NotImplementedException();
        }
    }
}