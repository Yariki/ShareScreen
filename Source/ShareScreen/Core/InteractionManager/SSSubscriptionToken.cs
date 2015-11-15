///////////////////////////////////////////////////////////
//  SSSubscriptionToken.cs
//  Implementation of the Class SSSubscriptionToken
//  Generated by Enterprise Architect
//  Created on:      15-Nov-2015 22:31:26
//  Original author: Yariki
///////////////////////////////////////////////////////////

using System;
using SS.ShareScreen.Interfaces.InteractionManager;

namespace SS.ShareScreen.Core.InteractionManager
{
    public class SSSubscriptionToken : ISSSubscribeToken
    {
        public SSSubscriptionToken()
        {
        }

        ~SSSubscriptionToken()
        {
        }

        public Guid Id
        {
            get
            {
                return Id;
            }
            set
            {
                Id = value;
            }
        }
    }//end SSSubscriptionToken
}//end namespace InteractionManager