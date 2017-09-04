using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DotNetBasicWindowsFormsApp
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private async void btnStart_Click(object sender, EventArgs e)
        {
            var exePath = "DotNetBasicConsoleApp.exe";
            IProgress<int> prgs = new Progress<int>(p =>
            {
                this.progressBar1.Value = p;
                this.Text = $"{p}/{100}";
            });

            this.btnStart.Enabled = false;
            await Task.Run(() =>
            {
                using (var p = Process.Start(new ProcessStartInfo()
                {
                    FileName = exePath, //要执行的.exe文件
                    CreateNoWindow = true, //无窗口
                    UseShellExecute = false, //非Shell行为
                    RedirectStandardOutput = true, //必须，否则连毛都不出来
                }))
                {
                    using (var reader = new BinaryReader(p.StandardOutput.BaseStream))
                    {
                        while (true)
                        {
                            var prs = reader.ReadByte();
                            prgs.Report(prs);
                            if (prs == 100) { break; }
                        }
                        reader.Close();
                    }
                    p.WaitForExit();
                    p.Close();
                }
            });
            this.btnStart.Enabled = true;
        }

        private CancellationTokenSource tokenSource;
        //取得网页的内容
        private void btnGetURL_Click(object sender, EventArgs e)
        {
            this.tokenSource = new CancellationTokenSource();
            this.btnGetURL.Enabled = false;
            this.btnCancel.Enabled = true;

            var url = this.textBoxURL.Text;
            //让被Call的程序从下一秒开始执行
            Task<string> waitTask = Task.Factory.StartNew(() =>
            {
                var client = new WebClient() { Encoding = Encoding.UTF8 };
                //Thread.Sleep(1000);
                return client.DownloadString(url);
            }, tokenSource.Token);

            waitTask.ContinueWith(action =>
            {
                //将内容传写到TextBox上
                this.textBoxBody.Text = action.Result;
                this.btnGetURL.Enabled = true;
                this.btnCancel.Enabled = false;
            }, tokenSource.Token, TaskContinuationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());
        }
        //取消取得网页的内容
        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (this.tokenSource != null)
            {
                this.tokenSource.Cancel();
                this.btnGetURL.Enabled = true;
                this.btnCancel.Enabled = false;
                this.textBoxBody.Text = "使用者取消！";
            }
        }
    }
}
