///////////////////////////////////////////////////////////
//  SSBootstraper.cs
//  Implementation of the Class SSBootstraper
//  Generated by Enterprise Architect
//  Created on:      15-Nov-2015 22:31:25
//  Original author: Yariki
///////////////////////////////////////////////////////////

using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Windows;
using SS.ShareScreen.Interfaces.Main;
using SS.ShareScreen.Logger;

namespace SS.ShareScreen
{

    public class SSBootstraper
    {
        private CompositionContainer _container;
        private ISSMainViewModel _mainViewModel;

        ~SSBootstraper()
        {

        }

        public SSBootstraper()
        {
            var aggregateCatalog = new AggregateCatalog();
            string currentPath = Directory.GetCurrentDirectory();
            aggregateCatalog.Catalogs.Add(new DirectoryCatalog(currentPath));
            aggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(SSBootstraper).Assembly));
            _container = new CompositionContainer(aggregateCatalog);
            var logger = new SSLogger();
            var composition = new CompositionBatch();
            composition.AddExportedValue(_container);
            composition.AddExportedValue(logger);
            _container.Compose(composition);
            _mainViewModel = _container.GetExportedValue<ISSMainViewModel>();
        }

        public void Exit()
        {
            _mainViewModel?.Dispose();
            _mainViewModel = null;
        }
        
        public void Run()
        {
            _mainViewModel.Initialize(null);
            _mainViewModel.ShowMainWindow();
        }
    }
    
//end SSBootstraper
}
