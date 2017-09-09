using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using YoutubeExtractor;

namespace YoutubeDownloader_GUI
{
    public partial class Form1 : Form
    {   
        public Form1()
        {
            InitializeComponent();
        }

        private  void button1_Click(object sender, EventArgs e)
        {

            try
            {
                string link = textBox1.Text;
                IEnumerable<VideoInfo> videoInfo = DownloadUrlResolver.GetDownloadUrls(link);
                VideoInfo video = videoInfo.FirstOrDefault();
                    //.First(info => info.VideoType == VideoType.Flash && info.Resolution == 480);

                if (video.RequiresDecryption)
                {
                    DownloadUrlResolver.DecryptDownloadUrl(video);
                }
                var videoDownloader = new VideoDownloader(video, Path.Combine(@"C:\Users\cosmi\Desktop\youtube downlaoder gui test", video.Title + video.VideoExtension));
                videoDownloader.Execute();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }
    }
}
