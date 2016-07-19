using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using PPI.Core;

namespace PPI.Hogan.Service
{
    public partial class Feed : ServiceBase
    {
        protected System.IO.FileSystemWatcher fileWatcher;
        
        public Feed()
        {
            InitializeComponent();            
        }
        [Log]
        protected override void OnStart(string[] args)
        {
            var Watcher = new PPI.Hogan.Service.Utility.Util();
            Watcher.StartWatching(fileWatcher);
        }       
        [Log]
        protected override void OnStop()
        {
            if (fileWatcher != null)
            {
                fileWatcher.EnableRaisingEvents = false;
                fileWatcher.Dispose();
            }            
        }        
    }
}
