namespace YoutubeDownloader_GUI
{
    partial class SettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.ffmpegPathLabel = new System.Windows.Forms.Label();
            this.setFFmpegPath = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(108, 51);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(541, 22);
            this.textBox1.TabIndex = 0;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // ffmpegPathLabel
            // 
            this.ffmpegPathLabel.AutoSize = true;
            this.ffmpegPathLabel.Location = new System.Drawing.Point(108, 28);
            this.ffmpegPathLabel.Name = "ffmpegPathLabel";
            this.ffmpegPathLabel.Size = new System.Drawing.Size(91, 17);
            this.ffmpegPathLabel.TabIndex = 1;
            this.ffmpegPathLabel.Text = "FFMpeg path";
            // 
            // setFFmpegPath
            // 
            this.setFFmpegPath.Location = new System.Drawing.Point(108, 118);
            this.setFFmpegPath.Name = "setFFmpegPath";
            this.setFFmpegPath.Size = new System.Drawing.Size(167, 23);
            this.setFFmpegPath.TabIndex = 2;
            this.setFFmpegPath.Text = "Set ffmpeg path";
            this.setFFmpegPath.UseVisualStyleBackColor = true;
            this.setFFmpegPath.Click += new System.EventHandler(this.setFFmpegPath_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(712, 253);
            this.Controls.Add(this.setFFmpegPath);
            this.Controls.Add(this.ffmpegPathLabel);
            this.Controls.Add(this.textBox1);
            this.Name = "SettingsForm";
            this.Text = "SettingsForm";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label ffmpegPathLabel;
        private System.Windows.Forms.Button setFFmpegPath;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}