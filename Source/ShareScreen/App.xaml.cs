using System.Windows;

namespace SS.ShareScreen
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private SSBootstraper _bootstraper;

        protected override void OnStartup(StartupEventArgs e)
        {
            _bootstraper = new SSBootstraper();
            _bootstraper.Run();
            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _bootstraper?.Exit();
            base.OnExit(e);
        }
    }
}
