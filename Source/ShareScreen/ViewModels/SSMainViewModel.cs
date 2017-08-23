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
using System.Drawing;
using System.Windows;
using MahApps.Metro.Controls;
using ShareScreen.Core.Core.MVVM;
using ShareScreen.Core.Core.Payload;
using ShareScreen.Core.Enums;
using ShareScreen.Core.Extensions;
using ShareScreen.Core.InteractionProviders;
using ShareScreen.Core.Interfaces.Core;
using ShareScreen.Core.Interfaces.InteractionManager;
using ShareScreen.Core.Interfaces.Main;
using ShareScreen.Core.Interfaces.System;
using ShareScreen.Core.Payloads;

namespace SS.ShareScreen.ViewModels
{
    [Export(typeof(ISSMainViewModel))]
    public class SSMainViewModel : SSUIBaseViewModel<ISSMainView>, ISSMainViewModel
    {
        private List<ISSScreenShotViewModel> _screenShotList;

        private ISSSubscribeToken _keyboardToken;
        private ISSSubscribeToken _normalizeToken;
        private ISSSubscribeToken _maximazeToken;
        private ISSSubscribeToken _minimazeToken;
        private ISSSubscribeToken _selectionWindowToken;
        private ISSSubscribeToken _selectionAreaToken;
        private static string ScreenshotName = "Screenshot";
        private int SaveNumber = 1;


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
                //KeyboardSystem?.StopSystem();
                //MouseSystem?.StopSystem();
                InteractionManager.GetCommand<SSNormalizeMainWindowProvider>().Unsubscribe(_normalizeToken);
                InteractionManager.GetCommand<SSSelectedWindowProvider>().Unsubscribe(_selectionWindowToken);
                InteractionManager.GetCommand<SSSelectionRegionFinished>().Unsubscribe(_selectionAreaToken);
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
            //KeyboardSystem?.StartSystem();
            //MouseSystem?.StartSystem();
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


        public ISSScreenShotViewModel SelectedScreenShot
        {
            set { Set(() => SelectedScreenShot, value); }
            get { return Get(() => SelectedScreenShot); }
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
                case eSSMenuCommand.Save:
                    if (SelectedScreenShot.IsNotNull())
                    {
                        SelectedScreenShot.Save($"{ScreenshotName}{SaveNumber++}");
                    }
                    break;
                case eSSMenuCommand.SaveAs:
                    if (SelectedScreenShot.IsNotNull())
                    {
                        SelectedScreenShot.SaveAs();
                    }
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
            _selectionAreaToken = InteractionManager.GetCommand<SSSelectionRegionFinished>().Subscribe(OnSelectionArea);
        }

        private void OnSelectionWindow(SSPayload<bool> ssPayload)
        {
            MouseSystem.ResetCurrentAction();
            if (ssPayload.Value)
            {
                var selectedWindow = MouseSystem.GetSelectedWindow();
                System.Diagnostics.Debug.WriteLine(string.Format("SelectedWindow: {0}", selectedWindow));
                var activeWnd = ScreenShotSystem.GetScreenshtOfSelectedWindow(selectedWindow);
                AddScreenShot(activeWnd);

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
                    MakeScreenShotOfSelectedArea();
                    break;
                case eScreenshotType.SelectedWindow:
                    MakeScreenShotOfSelectedWindow();
                    break;
            }
        }

        private void MakeScreenShot()
        {
            try
            {
                InternalWindow.WindowState = WindowState.Minimized;
                var screen = ScreenShotSystem.GetScreenshot();
                InternalWindow.WindowState = WindowState.Normal;
                AddScreenShot(screen);
                MouseSystem.ResetCurrentAction();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
            }
        }

        private void MakeScreenShotOfSelectedWindow()
        {
            InternalWindow.WindowState = WindowState.Minimized;
            MouseSystem.RunSelectingWindow(IntPtr.Zero);
        }

        private void MakeScreenShotOfSelectedArea()
        {
            InternalWindow.WindowState = WindowState.Minimized;
            MouseSystem.RunSelectingArea();
        }

        private void OnSelectionArea(SSPayload<bool> result)
        {
            try
            {
                if (result.Value)
                {
                    var region = MouseSystem.GetSelectedArea();
                    var selectedArea = ScreenShotSystem.GetScreenshotOfSelectedArea(
                        new System.Drawing.Point((int)region.Item1.X, (int)region.Item1.Y),
                        new System.Drawing.Point((int)region.Item2.X, (int)region.Item2.Y));
                    AddScreenShot(selectedArea);

                }
                InternalWindow.WindowState = WindowState.Normal;
                MouseSystem.ResetCurrentAction();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.ToString());
            }
        }


        private void AddScreenShot(Bitmap screenshot)
        {
            Contract.Requires(screenshot != null);
            var shot = Container.GetExportedValue<ISSScreenShotViewModel>();
            if (shot.IsNotNull())
            {
                shot.Header = ScreenshotName;
                shot.SetScreenShot(screenshot);
                ScreenShots.Add(shot);
                SelectedScreenShot = shot;
            }
        }



    }//end SSMainViewModel
}//end namespace ViewModels