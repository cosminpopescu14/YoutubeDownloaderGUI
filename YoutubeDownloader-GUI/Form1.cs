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

                if (video.RequiresDecryption)
                {
                    DownloadUrlResolver.DecryptDownloadUrl(video);
                }
                
                //just a trick to allow method call in background worker control;
                var videoDownloader = new VideoDownloader(video, Path.Combine(Invoke((Func< string>)(() => { return GetPath(""); })).ToString(),
                                                            video.Title.Replace(" ", string.Empty).Replace("/", "") + video.VideoExtension));

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

        public Func<string, string> GetPath = delegate (string s)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                return folderBrowserDialog.SelectedPath;
            }
            return "";
        };

        /*Uses ffmpeg for conversion*/
        public void Convert()
        {
            string file = SelectFile();
            StringBuilder sb = new StringBuilder();
            Process process = new Process();
            process.StartInfo.FileName = ConfigurationManager.AppSettings["ffmpegPath"];      
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

        private void settingsButton_Click(object sender, EventArgs e)
        {
            SettingsForm settingsForm = new SettingsForm();
            settingsForm.Show();
        }
    }
}
