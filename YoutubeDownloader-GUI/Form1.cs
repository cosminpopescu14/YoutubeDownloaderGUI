using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using YoutubeExtractor;
using System.Diagnostics;
using System.Configuration;

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
            backgroundWorker1.RunWorkerAsync();
            button1.Enabled = false;//disable the button during download of the videoclip
        }

        //this method download the video in background. Also makes the GUI responsive
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                string link = textBox1.Text;
                IEnumerable<VideoInfo> videoInfo = DownloadUrlResolver.GetDownloadUrls(link);
                VideoInfo video = videoInfo.FirstOrDefault();//we take the first video from that url.
                //.First(info => info.VideoType == VideoType.Flash && info.Resolution == 480);

                if (video.RequiresDecryption)
                {
                    DownloadUrlResolver.DecryptDownloadUrl(video);
                }
                var videoDownloader = new VideoDownloader(video, Path.Combine(@"C:\Users\cosmi\Desktop\youtube", video.Title.Replace(" ", string.Empty).Replace("/", "") + video.VideoExtension));

                //report the progress for progressbar1
                videoDownloader.DownloadProgressChanged += (y, x) => backgroundWorker1.ReportProgress((int)x.ProgressPercentage); 
                videoDownloader.Execute();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //show the progress
            progressBar1.Value = e.ProgressPercentage;
            label2.Text = e.ProgressPercentage.ToString();
            if (e.ProgressPercentage == 100)
            {
                button1.Enabled = true;//enable it back
            }
        }

        public string SelectFile()
        {
            openFileDialog1.Title = "Select the file you want to convert";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string file = openFileDialog1.FileName;
                return Path.GetFullPath(file);                 
            }
            else
            {
                return "";
            }
        }

        /*Uses ffmpeg for conversion*/
        public void Convert()
        {
            string file = SelectFile();
            StringBuilder sb = new StringBuilder();
            Process process = new Process();
            process.StartInfo.FileName = ConfigurationManager.AppSettings["ffmpegPath"];      //@"C:\Users\cosmi\Documents\Visual Studio 2017\Projects\YoutubeDownloader-GUI\YoutubeDownloader-GUI\bin\ffmpeg-20170904-6cadbb1-win32-static\bin\ffmpeg.exe";
            process.StartInfo.Arguments = "-i" + " " + $"{file}" + " " + $"{file}.mp3";
            process.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.OutputDataReceived += (sender, args) => sb.AppendLine(args.Data);
            process.ErrorDataReceived += (sender, args) => sb.AppendLine(args.Data);
            process.Exited += (sender, args) => MessageBox.Show("Conevrsion finished !");
            process.StartInfo.UseShellExecute = false;
            try
            {                
                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();
                process.WaitForExit();
                richTextBox1.Text = sb.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void convertButton_Click(object sender, EventArgs e)
        {
            Convert();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //formatsComboBox.Enabled = false;
            formatsComboBox.Items.Add(new Formats("MP3", "mp3"));
        }

        private void SetSettings()
        {
            
            foreach (string key in ConfigurationManager.AppSettings)
            {
                string value = ConfigurationManager.AppSettings[key];
                if (value == "")
                {
                    MessageBox.Show("ffpeg path not configured !");
                    openFileDialog1.Title = "Select ffmpeg executable file";
                    if (openFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        string ffmpegPath = openFileDialog1.FileName;
                        Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                        configuration.AppSettings.Settings.Add("ffmpegPath", Path.GetFullPath(ffmpegPath));
                        configuration.Save(ConfigurationSaveMode.Modified);
                        ConfigurationManager.RefreshSection("appSettings");
                    }
                }
                else
                {
                    MessageBox.Show("ffmpeg path already configured !");
                }
            }
        }
    }
}
