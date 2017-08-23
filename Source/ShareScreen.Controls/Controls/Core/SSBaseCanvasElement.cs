using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ShareScreen.Controls.Controls.Core
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:SS.ShareScreen.Controls.Core"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:SS.ShareScreen.Controls.Core;assembly=SS.ShareScreen.Controls.Core"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Browse to and select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:SSBaseCanvasElement/>
    ///
    /// </summary>
    public class SSBaseCanvasElement : ContentControl,INotifyPropertyChanged
    {

        static SSBaseCanvasElement()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SSBaseCanvasElement), new FrameworkPropertyMetadata(typeof(SSBaseCanvasElement)));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #region [dependencyu properties]
        

        public static readonly DependencyProperty IsSelectedProperty = DependencyProperty.Register(
            "IsSelected", typeof(bool), typeof(SSBaseCanvasElement), new PropertyMetadata(default(bool),IsSelectedChanged));

        private static void IsSelectedChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var canvasElement = dependencyObject as SSBaseCanvasElement;
            canvasElement.IsSelectedChanged((bool)dependencyPropertyChangedEventArgs.OldValue,(bool)dependencyPropertyChangedEventArgs.NewValue);
        }

        protected virtual void IsSelectedChanged(bool oldValue, bool newValue)
        {

        }

        public bool IsSelected
        {
            get { return (bool) GetValue(IsSelectedProperty); }
            set
            {
                SetValue(IsSelectedProperty, value);
            }
        }

        #endregion



        public double GetAspect()
        {
            return this.GetAspect(false);
        }

        public virtual double GetAspect(bool bCheckShift)
        {
            if (!Keyboard.IsKeyDown(Key.LeftShift) && !Keyboard.IsKeyDown(Key.RightShift) || this.Height <= 0.0)
                return -1.0;
            return this.Width / this.Height;
        }


        protected override void OnPreviewMouseDown(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseDown(e);
            IsSelected = true;
        }
    }
}
