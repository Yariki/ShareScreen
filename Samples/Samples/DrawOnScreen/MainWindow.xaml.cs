using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DrawOnScreen
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Point? _startPoint;
        private Point? _endPoint;

        public MainWindow()
        {
            InitializeComponent();
            this.KeyDown += OnKeyDown;
            this.MouseLeftButtonDown += OnMouseLeftButtonDown;
            this.MouseLeftButtonUp += OnMouseLeftButtonUp;
            this.MouseMove += OnMouseMove;
            this.Topmost = true;
        }

        private void OnMouseMove(object sender, MouseEventArgs mouseEventArgs)
        {
            _endPoint = mouseEventArgs.GetPosition(this);
            System.Diagnostics.Debug.WriteLine("OnMouseMove");
            if (_startPoint.HasValue && _endPoint.HasValue)
            {
                this.InvalidateVisual();
            }
        }

        private void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            _startPoint = mouseButtonEventArgs.GetPosition(this);
            System.Diagnostics.Debug.WriteLine("OnMouseLeftButtonUp");
        }

        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            _startPoint = null;
            _endPoint = null;
            System.Diagnostics.Debug.WriteLine("OnMouseLeftButtonDown");
        }

        private void OnKeyDown(object sender, KeyEventArgs keyEventArgs)
        {
            switch (keyEventArgs.Key)
            {
                case Key.Escape:
                    Close();
                    break;
            }
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            if (_startPoint.HasValue && _endPoint.HasValue)
            {
                drawingContext.DrawRectangle(Brushes.Aqua,new Pen(Brushes.Blue,1),new Rect(_startPoint.Value,_endPoint.Value));
                System.Diagnostics.Debug.WriteLine("Render");
                System.Diagnostics.Debug.WriteLine(_startPoint.Value.ToString() +" : " + _endPoint.Value.ToString());
            }
            
        }
    }
}
