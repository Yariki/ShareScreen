﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Drawing;
using SS.PluginCore.Interfaces;
using SS.ShareScreen.Interfaces.System;

namespace SS.ShareScreen.Systems.Plugins
{
    [Export(typeof(ISSPluginSystem))]
    public class SSPluginSystem  : ISSPluginSystem
    {
        public void StartSystem()
        {
            throw new NotImplementedException();
        }

        public void StopSystem()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ISSSharePlugin> GetPlugins()
        {
            throw new NotImplementedException();
        }

        public ISSSharingUrlResult ShareScreenshot(Bitmap image, Guid pluginId)
        {
            throw new NotImplementedException();
        }

        public int ShowPluginSettings()
        {
            throw new NotImplementedException();
        }
    }
}