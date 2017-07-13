using System;
using System.Windows;
using ShareScreen.Core.Core.InteractionManager;
using ShareScreen.Core.Core.Payload;

namespace ShareScreen.Core.InteractionProviders
{
    public class SSSelectionRegionProvider : SSCommandProvider<SSPayload<Tuple<bool, Point, Point>>>
    {
    }
}