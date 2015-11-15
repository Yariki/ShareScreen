///////////////////////////////////////////////////////////
//  ISSPluginSystem.cs
//  Implementation of the Interface ISSPluginSystem
//  Generated by Enterprise Architect
//  Created on:      15-Nov-2015 22:31:24
//  Original author: Yariki
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Drawing;
using SS.PluginCore.Interfaces;

namespace SS.ShareScreen.Interfaces.System
{
    public interface ISSPluginSystem : ISSSystem
    {
        IEnumerable<ISSSharePlugin> GetPlugins();

        ///
        /// <param name="image"></param>
        /// <param name="pluginId"></param>
        ISSSharingUrlResult ShareScreenshot(Bitmap image, Guid pluginId);

        int ShowPluginSettings();
    }//end ISSPluginSystem
}//end namespace System