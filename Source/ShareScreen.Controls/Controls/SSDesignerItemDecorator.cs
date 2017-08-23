using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using ShareScreen.Controls.Controls.Adorners;
using ShareScreen.Controls.EventArguments;
using ShareScreen.Core.Extensions;

namespace ShareScreen.Controls.Controls
{
    public class SSDesignerItemDecorator : Control
    {
        public static readonly DependencyProperty ShowDecoratorProperty = DependencyProperty.Register("ShowDecorator", typeof(bool), typeof(SSDesignerItemDecorator), (PropertyMetadata)new FrameworkPropertyMetadata((object)false, new PropertyChangedCallback(SSDesignerItemDecorator.ShowDecoratorProperty_Changed)));
        private Adorner adorner;

        public bool ShowDecorator
        {
            get
            {
                return (bool)this.GetValue(SSDesignerItemDecorator.ShowDecoratorProperty);
            }
            set
            {
                this.SetValue(SSDesignerItemDecorator.ShowDecoratorProperty, (object)value);
            }
        }

        public SSDesignerItemDecorator()
        {
            this.Unloaded += new RoutedEventHandler(this.DesignerItemDecorator_Unloaded);
        }

        public event EventHandler<SSSelectionSizeChangedArgs> SelectionSizeChanged; 

        private void HideAdorner()
        {
            if (this.adorner == null)
                return;
            this.adorner.Visibility = Visibility.Hidden;
        }

        private void ShowAdorner()
        {
            if (this.adorner == null)
            {
                AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer((Visual)this);
                if (adornerLayer == null)
                    return;
                ContentControl dataContext = this.DataContext as ContentControl;
                VisualTreeHelper.GetParent((DependencyObject)dataContext);
                this.adorner = (Adorner)new SSResizeAdorner(dataContext);
                this.adorner.SizeChanged += AdornerOnSizeChanged;
                adornerLayer.Add(this.adorner);
                if (this.ShowDecorator)
                    this.adorner.Visibility = Visibility.Visible;
                else
                    this.adorner.Visibility = Visibility.Hidden;
            }
            else
                this.adorner.Visibility = Visibility.Visible;
        }

        private void AdornerOnSizeChanged(object sender, SizeChangedEventArgs sizeChangedEventArgs)
        {
            var temp = SelectionSizeChanged;
            if (temp.IsNotNull())
            {
                temp(this,new SSSelectionSizeChangedArgs(sizeChangedEventArgs.NewSize.Width,sizeChangedEventArgs.NewSize.Height));
            }
        }

        private void DesignerItemDecorator_Unloaded(object sender, RoutedEventArgs e)
        {
            if (this.adorner == null)
                return;
            AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer((Visual)this);
            if (adornerLayer != null)
                adornerLayer.Remove(this.adorner);
            this.adorner.SizeChanged -= AdornerOnSizeChanged;
            this.adorner = (Adorner)null;
        }

        private static void ShowDecoratorProperty_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            SSDesignerItemDecorator designerItemDecorator = (SSDesignerItemDecorator)d;
            if ((bool)e.NewValue)
                designerItemDecorator.ShowAdorner();
            else
                designerItemDecorator.HideAdorner();
        }

    }
}