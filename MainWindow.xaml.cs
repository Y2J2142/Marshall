using System.Windows;
using System.Windows.Input;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using System.ComponentModel;
using System;
namespace Marshall
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private WMI wmi;
        SynchronizationContext ctx;
        private BackgroundWorker worker;
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            this.DragMove();
        }
        private async Task<string> UpdateCpu() {
            var ul = await Task.Run(() => {
                return wmi.GetTotalCPUUsage();
            });
            return ul.ToString();
        }
        public MainWindow()
        {   
            InitializeComponent();
            ctx = SynchronizationContext.Current;
            worker = new BackgroundWorker();
            worker.DoWork += (obj, ea) => {
                wmi = new WMI();
                ctx.Post(new SendOrPostCallback(o => {
                        var l = (List<string>)o;
                        OS.Content = l[0];
                        TotalMem.Content = l[1];
                        MemV.Content = l[2];
                        MemClck.Content = l[3];
                        MemType.Content = l[4];
                    }), new List<string>(){
                        wmi.GetOS(),
                        wmi.GetTotalMemory() + "MB",
                        wmi.GetMemVoltage().ToString() + "mV",
                        wmi.GetMemClockSpeed().ToString() + "Hz",
                        wmi.GetMemType().ToString()
                    });
                while(true) {
                    var ul = wmi.GetTotalCPUUsage();
                    ctx.Post(new SendOrPostCallback(o => {
                        var l = (List<string>)o;
                        CPUUsage.Content = l[0];
                        AvailableMem.Content = l[1];
                    }),new List<string>() {
                        wmi.GetTotalCPUUsage().ToString() + "%",
                        wmi.GetAvailableMemory() + "MB"
                    });
                }
            };
            worker.RunWorkerAsync();
        }
    }
}
 