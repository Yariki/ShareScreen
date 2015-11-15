///////////////////////////////////////////////////////////
//  ISSMenuItemViewModel.cs
//  Implementation of the Interface ISSMenuItemViewModel
//  Generated by Enterprise Architect
//  Created on:      15-Nov-2015 22:31:24
//  Original author: Yariki
///////////////////////////////////////////////////////////

using System.Collections.Generic;
using SS.ShareScreen.Enums;

namespace SS.ShareScreen.Interfaces.Core
{
    public interface ISSMenuItemViewModel
    {
        eSSMenuCommand Command
        {
            get;
        }

        string Icon
        {
            get;
        }

        bool IsParent
        {
            get;
        }

        IEnumerable<ISSMenuItemViewModel> SubItems
        {
            get;
        }

        string UIName
        {
            get;
        }
    }//end ISSMenuItemViewModel
}//end namespace Core