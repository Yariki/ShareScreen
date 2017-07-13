///////////////////////////////////////////////////////////
//  SSStatusBarViewModel.cs
//  Implementation of the Class SSStatusBarViewModel
//  Generated by Enterprise Architect
//  Created on:      15-Nov-2015 22:31:26
//  Original author: Yariki
///////////////////////////////////////////////////////////

using System.ComponentModel.Composition;
using ShareScreen.Core.Core.MVVM;
using ShareScreen.Core.Interfaces.Main;

namespace SS.ShareScreen.ViewModels
{
    [Export(typeof(ISSMainStatusBarViewModel))]
    public class SSStatusBarViewModel : SSUIBaseViewModel<ISSStatusBarView>, ISSMainStatusBarViewModel
    {
        public SSStatusBarViewModel()
        {
        }

        ~SSStatusBarViewModel()
        {
        }

        ///
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
        }

    }//end SSStatusBarViewModel
}//end namespace ViewModels