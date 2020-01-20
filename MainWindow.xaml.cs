using System.Windows;
using System.Windows.Input;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Threading;
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
                        OS.Content = (string) o;
                    }), wmi.GetOS());
                while(true) {
                    var ul = wmi.GetTotalCPUUsage();
                    ctx.Post(new SendOrPostCallback(o => {
                        CPUUsage.Content = (string) o;
                    }), ul.ToString());
                }
            };
            worker.RunWorkerAsync();
        }
    }
}
 