///////////////////////////////////////////////////////////
//  SSMainViewModel.cs
//  Implementation of the Class SSMainViewModel
//  Generated by Enterprise Architect
//  Created on:      15-Nov-2015 22:31:25
//  Original author: Yariki
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Diagnostics.Contracts;
using System.Windows;
using MahApps.Metro.Controls;
using SS.ShareScreen.Core.InteractionManager;
using SS.ShareScreen.Core.MVVM;
using SS.ShareScreen.Core.Payload;
using SS.ShareScreen.Enums;
using SS.ShareScreen.Extensions;
using SS.ShareScreen.InteractionProviders;
using SS.ShareScreen.Interfaces.Controls;
using SS.ShareScreen.Interfaces.Core;
using SS.ShareScreen.Interfaces.InteractionManager;
using SS.ShareScreen.Interfaces.Main;
using SS.ShareScreen.Interfaces.System;
using SS.ShareScreen.Logger;
using SS.ShareScreen.Payloads;

namespace SS.ShareScreen.ViewModels
{
    [Export(typeof(ISSMainViewModel))]
    public class SSMainViewModel : SSUIBaseViewModel<ISSMainView>, ISSMainViewModel
    {
        private List<ISSScreenShotViewModel> _screenShotList;
        private ISSSelectionWindow _selectionWindow;
        private ISSSubscribeToken _keyboardToken;
        private ISSSubscribeToken _normalizeToken;
        private ISSSubscribeToken _maximazeToken;
        private ISSSubscribeToken _minimazeToken;
        private ISSSubscribeToken _selectionWindowToken;
        private ISSSubscribeToken _selectionAreaToken;
        

        public SSMainViewModel()
        {

        }

        ///
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                MenuViewModel?.Dispose();
                KeyboardSystem?.StopSystem();
                MouseSystem?.StopSystem();
                InteractionManager.GetCommand<SSNormalizeMainWindowProvider>().Unsubscribe(_normalizeToken);
                InteractionManager.GetCommand<SSSelectedWindowProvider>().Unsubscribe(_selectionWindowToken);
                InteractionManager.GetCommand<SSSelectionRegionProvider>().Unsubscribe(_selectionAreaToken);
            }
            base.Dispose(disposing);
        }

        ///
        /// <param name="view"></param>
        public override void HideDialog(ISSView view)
        {
        }

        ///
        /// <param name="parentModel"></param>
        public override void Initialize(ISSUIViewModel parentModel)
        {
            MenuViewModel?.InitializeMenu(ExecuteMenuCommand, CanExecuteManuCommand);
            KeyboardSystem?.StartSystem();
            MouseSystem?.StartSystem();
            //PluginSystem?.StartSystem();
            ScreenShotSystem?.StartSystem();
            ScreenShots = new ObservableCollection<ISSScreenShotViewModel>();
            SubscribeInteractionProviders();
        }

        public void ShowMainWindow()
        {
            InternalWindow.Show();
        }

        [Import]
        private ISSKeyboardSystem KeyboardSystem
        {
            get; set;
        }

        [Import]
        public ISSMainMenuViewModel MenuViewModel
        {
            get; set;
        }

        [Import]
        private ISSMouseSystem MouseSystem
        {
            get; set;
        }

        [Import]
        private ISSPluginSystem PluginSystem
        {
            get; set;
        }

        [Import]
        private ISSScreenshotSystem ScreenShotSystem
        {
            get; set;
        }

        [Import]
        public ISSMainStatusBarViewModel StatusBarViewModel
        {
            get; set;
        }

        private Window InternalWindow
        {
            get { return View as Window; }
        }

        public ObservableCollection<ISSScreenShotViewModel> ScreenShots
        {
            get { return Get(() => ScreenShots); }
            set { Set(() => ScreenShots, value); }
        }


        private void ExecuteMenuCommand(object arg)
        {
            Contract.Ensures(arg is SSMenuItemViewModel);

            var menuItem = arg as SSMenuItemViewModel;
            switch (menuItem.MenuCommand)
            {
                case eSSMenuCommand.MakeAllScreen:
                    MakeScreenShot();
                    break;
                case eSSMenuCommand.MakeActiveWindow:
                    MakeScreenShotOfSelectedWindow();
                    break;
                case eSSMenuCommand.MakeRegionScreen:
                    MakeScreenShotOfSelectedArea();
                    break;
            }
        }
        
        private bool CanExecuteManuCommand(object arg)
        {
            return true;
        }


        private void SubscribeInteractionProviders()
        {
            _keyboardToken = InteractionManager.GetCommand<SSKeyboardProvider>().Subscribe(OnKeyboardAction);
            _normalizeToken = InteractionManager.GetCommand<SSNormalizeMainWindowProvider>()
                .Subscribe(OnNormalizeWindow);
            _maximazeToken = InteractionManager.GetCommand<SSMaximazeMainWindowProvider>().Subscribe(OnMaximazeWindow);
            _minimazeToken = InteractionManager.GetCommand<SSMinimizeMainWindowProvider>().Subscribe(OnMinimazeWindow);
            _selectionWindowToken =
                InteractionManager.GetCommand<SSSelectedWindowProvider>().Subscribe(OnSelectionWindow);
            _selectionAreaToken = InteractionManager.GetCommand<SSSelectionRegionProvider>().Subscribe(OnSelectionArea);
        }

        private void OnSelectionWindow(SSPayload<bool> ssPayload)
        {
            MouseSystem.ResetCurrentAction();
            if (ssPayload.Value)
            {
                var selectedWindow = MouseSystem.GetSelectedWindow();
                System.Diagnostics.Debug.WriteLine(string.Format("SelectedWindow: {0}", selectedWindow));
                var activeWnd = ScreenShotSystem.GetScreenshtOfSelectedWindow(selectedWindow);
                var activeShot = Container.GetExportedValue<ISSScreenShotViewModel>();
                if (activeShot.IsNotNull())
                {
                    activeShot.Header = "Selected Window";
                    activeShot.SetScreenShot(activeWnd);
                    ScreenShots.Add(activeShot);
                }

            }
            else
            {
                System.Diagnostics.Debug.WriteLine(string.Format("SelectedWindow was canceled..."));
            }
            InternalWindow.WindowState = WindowState.Normal;
        }

        private void OnMinimazeWindow(SSPayload<object> ssPayload)
        {
            InternalWindow.WindowState = WindowState.Minimized;
        }

        private void OnMaximazeWindow(SSPayload<object> ssPayload)
        {
            InternalWindow.WindowState = WindowState.Maximized;
        }

        private void OnNormalizeWindow(SSPayload<object> ssPayload)
        {
            InternalWindow.WindowState = WindowState.Normal;
        }


        private void OnKeyboardAction(SSKeyboardPayload ssKeyboardPayload)
        {
            Contract.Requires(ssKeyboardPayload != null);
            Logger.Debug(Enum.GetName(typeof(eScreenshotType), ssKeyboardPayload.Value));
            switch (ssKeyboardPayload.Value)
            {
                case eScreenshotType.Screen:
                    MakeScreenShot();
                    break;
                case eScreenshotType.SelectedArea:
                    break;
                case eScreenshotType.SelectedWindow:
                    MakeScreenShotOfSelectedWindow();
                    break;
            }
        }

        private void MakeScreenShot()
        {
            InternalWindow.WindowState = WindowState.Minimized;
            var screen = ScreenShotSystem.GetScreenshot();
            InternalWindow.WindowState = WindowState.Normal;
            var shot = Container.GetExportedValue<ISSScreenShotViewModel>();
            if (shot.IsNotNull())
            {
                shot.Header = "Screen Shot";
                shot.SetScreenShot(screen);
                ScreenShots.Add(shot);
            }
        }

        private void MakeScreenShotOfSelectedWindow()
        {
            InternalWindow.WindowState = WindowState.Minimized;
            MouseSystem.RunSelectingWindow();
        }

        private void MakeScreenShotOfSelectedArea()
        {
            InternalWindow.WindowState = WindowState.Minimized;
            _selectionWindow = Container.GetExportedValue<ISSSelectionWindow>();
            _selectionWindow?.Show();
        }

        private void OnSelectionArea(SSPayload<Tuple<bool, Point, Point>> result)
        {
            if (result.Value.Item1)
            {
                _selectionWindow?.Close();
                var selectedArea = ScreenShotSystem.GetScreenshotOfSelectedArea(
                    new System.Drawing.Point((int)result.Value.Item2.X, (int)result.Value.Item2.Y),
                    new System.Drawing.Point((int)result.Value.Item3.X, (int)result.Value.Item3.Y));
                var shot = Container.GetExportedValue<ISSScreenShotViewModel>();
                if (shot.IsNotNull())
                {
                    shot.Header = "Selected Area";
                    shot.SetScreenShot(selectedArea);
                    ScreenShots.Add(shot);
                }
            }
            InternalWindow.WindowState = WindowState.Normal;
        }




    }//end SSMainViewModel
}//end namespace ViewModels