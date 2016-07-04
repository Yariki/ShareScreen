using System;
using System.Windows;
using SS.ShareScreen.Core.InteractionManager;
using SS.ShareScreen.Core.Payload;

namespace SS.ShareScreen.InteractionProviders
{
    public class SSSelectionRegionProvider : SSCommandProvider<SSPayload<Tuple<Point,Point>>>
    {
    }
}