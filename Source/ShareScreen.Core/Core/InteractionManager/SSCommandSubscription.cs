///////////////////////////////////////////////////////////
//  SSCommandSubscription.cs
//  Implementation of the Class SSCommandSubscription
//  Generated by Enterprise Architect
//  Created on:      15-Nov-2015 22:31:25
//  Original author: Yariki
///////////////////////////////////////////////////////////

using System;
using System.Diagnostics.Contracts;
using ShareScreen.Core.Interfaces.InteractionManager;

namespace ShareScreen.Core.Core.InteractionManager
{
    public class SSCommandSubscription<TPayload> : ISSSubscription
    {
        private Interfaces.InteractionManager.ISSReferenceDelegate _actionReference;

        public SSCommandSubscription(ISSReferenceDelegate reference)
        {
            Contract.Requires(reference.Target != null);
            _actionReference = reference;
        }

        private Action<TPayload> Action => (Action<TPayload>) _actionReference.Target;

        ~SSCommandSubscription()
        {
        }

        ///
        /// <param name="arg"></param>
        public void Publish(object arg)
        {
            Action((TPayload)arg);
        }

        public ISSSubscribeToken Token { get; set; }
    }//end SSCommandSubscription
}//end namespace InteractionManager