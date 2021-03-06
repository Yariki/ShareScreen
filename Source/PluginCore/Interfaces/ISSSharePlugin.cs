///////////////////////////////////////////////////////////
//  ISSSharePlugin.cs
//  Implementation of the Interface ISSSharePlugin
//  Generated by Enterprise Architect
//  Created on:      16-Nov-2015 00:19:53
//  Original author: Yariki
///////////////////////////////////////////////////////////

using System;
using System.Drawing;

namespace SS.PluginCore.Interfaces
{
    public interface ISSSharePlugin
    {
        ISSSharePluginAuthorization Authorize
        {
            get;
        }

        ISSSharePluginBrowser Browser
        {
            get;
        }

        Bitmap Icon
        {
            get;
        }

        Guid Id
        {
            get;
        }

        string Name
        {
            get;
        }

        ISSSharePluginSharing Sharing
        {
            get;
        }

        string UIName
        {
            get;
        }
    }//end ISSSharePlugin
}//end namespace Interfaces