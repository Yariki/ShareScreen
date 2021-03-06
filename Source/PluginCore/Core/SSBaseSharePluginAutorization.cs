///////////////////////////////////////////////////////////
//  SSBaseSharePluginAutorization.cs
//  Implementation of the Class SSBaseSharePluginAutorization
//  Generated by Enterprise Architect
//  Created on:      16-Nov-2015 00:19:54
//  Original author: Yariki
///////////////////////////////////////////////////////////

using SS.PluginCore.Interfaces;

namespace SS.PluginCore.Core
{
    public class SSBaseSharePluginAutorization : ISSSharePluginAuthorization
    {
        public SSBaseSharePluginAutorization()
        {
        }

        ~SSBaseSharePluginAutorization()
        {
        }

        public bool Authorize()
        {
            return false;
        }

        protected virtual bool DoAuthorize()
        {
            return false;
        }

        public bool IsAuthorized()
        {
            return false;
        }
    }//end SSBaseSharePluginAutorization
}//end namespace Core