using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Configuration;
using System.IO;

namespace YoutubeDownloader_GUI
{
    public partial class SettingsForm : Form
    {
        Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            textBox1.ReadOnly = true;
            textBox1.Text = ConfigurationManager.AppSettings["ffmpegPath"] ?? "FFMpeg path not set";  //check if ffpmeg path is present in app.config.;
        }

        private void setFFmpegPath_Click(object sender, EventArgs e)
        {
            SetSettings();
            textBox1_TextChanged(sender, e);
        }

        private void SetSettings()
        {
            openFileDialog1.Title = "Select ffmpeg executable file";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string ffmpegPath = openFileDialog1.FileName;
                configuration.AppSettings.Settings.Remove("ffmpegPath");
                configuration.AppSettings.Settings.Add("ffmpegPath", Path.GetFullPath(ffmpegPath));
                configuration.Save(ConfigurationSaveMode.Full, true);
                ConfigurationManager.RefreshSection("appSettings");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox1.ReadOnly = false;
            textBox1.Text = ConfigurationManager.AppSettings["ffmpegPath"] ?? "FFMpeg path not set";
            textBox1.ReadOnly = true;
        }
    }
}
