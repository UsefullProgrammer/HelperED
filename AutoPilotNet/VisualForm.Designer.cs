using System.Windows.Forms;
namespace AutoPilotNet
{
    partial class VisualForm
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
            this.components = new System.ComponentModel.Container();
            this.ibVisual = new Emgu.CV.UI.ImageBox();
            this.ibGravidar = new Emgu.CV.UI.ImageBox();
            this.IMTEMP = new Emgu.CV.UI.ImageBox();
            this.txtrefresh = new System.Windows.Forms.TextBox();
            this.IBScreen1 = new Emgu.CV.UI.ImageBox();
            this.IBScreen2 = new Emgu.CV.UI.ImageBox();
            this.IBScreen3 = new Emgu.CV.UI.ImageBox();
            this.IBScreen4 = new Emgu.CV.UI.ImageBox();
            this.IBSource = new Emgu.CV.UI.ImageBox();
            this.IBTemplate = new Emgu.CV.UI.ImageBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lTime = new System.Windows.Forms.Label();
            this.txtinfo = new System.Windows.Forms.TextBox();
            this.cbPause = new System.Windows.Forms.CheckBox();
            this.cbOptimize = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbAutoScan = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.ibVisual)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ibGravidar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IMTEMP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IBScreen1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IBScreen2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IBScreen3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IBScreen4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IBSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IBTemplate)).BeginInit();
            this.SuspendLayout();
            // 
            // ibVisual
            // 
            this.ibVisual.FunctionalMode = Emgu.CV.UI.ImageBox.FunctionalModeOption.Minimum;
            this.ibVisual.Location = new System.Drawing.Point(2, 3);
            this.ibVisual.MinimumSize = new System.Drawing.Size(500, 450);
            this.ibVisual.Name = "ibVisual";
            this.ibVisual.Size = new System.Drawing.Size(808, 450);
            this.ibVisual.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ibVisual.TabIndex = 2;
            this.ibVisual.TabStop = false;
            // 
            // ibGravidar
            // 
            this.ibGravidar.FunctionalMode = Emgu.CV.UI.ImageBox.FunctionalModeOption.Minimum;
            this.ibGravidar.Location = new System.Drawing.Point(12, 459);
            this.ibGravidar.Name = "ibGravidar";
            this.ibGravidar.Size = new System.Drawing.Size(149, 131);
            this.ibGravidar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ibGravidar.TabIndex = 2;
            this.ibGravidar.TabStop = false;
            // 
            // IMTEMP
            // 
            this.IMTEMP.Location = new System.Drawing.Point(186, 459);
            this.IMTEMP.Name = "IMTEMP";
            this.IMTEMP.Size = new System.Drawing.Size(144, 131);
            this.IMTEMP.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.IMTEMP.TabIndex = 3;
            this.IMTEMP.TabStop = false;
            this.IMTEMP.Click += new System.EventHandler(this.imageBox2_Click);
            // 
            // txtrefresh
            // 
            this.txtrefresh.Location = new System.Drawing.Point(12, 611);
            this.txtrefresh.Name = "txtrefresh";
            this.txtrefresh.Size = new System.Drawing.Size(100, 20);
            this.txtrefresh.TabIndex = 4;
            this.txtrefresh.Text = "1";
            this.txtrefresh.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtrefresh_KeyPress);
            // 
            // IBScreen1
            // 
            this.IBScreen1.FunctionalMode = Emgu.CV.UI.ImageBox.FunctionalModeOption.Minimum;
            this.IBScreen1.Location = new System.Drawing.Point(336, 459);
            this.IBScreen1.Name = "IBScreen1";
            this.IBScreen1.Size = new System.Drawing.Size(149, 131);
            this.IBScreen1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.IBScreen1.TabIndex = 5;
            this.IBScreen1.TabStop = false;
            // 
            // IBScreen2
            // 
            this.IBScreen2.FunctionalMode = Emgu.CV.UI.ImageBox.FunctionalModeOption.Minimum;
            this.IBScreen2.Location = new System.Drawing.Point(491, 459);
            this.IBScreen2.Name = "IBScreen2";
            this.IBScreen2.Size = new System.Drawing.Size(149, 131);
            this.IBScreen2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.IBScreen2.TabIndex = 6;
            this.IBScreen2.TabStop = false;
            // 
            // IBScreen3
            // 
            this.IBScreen3.FunctionalMode = Emgu.CV.UI.ImageBox.FunctionalModeOption.Minimum;
            this.IBScreen3.Location = new System.Drawing.Point(646, 459);
            this.IBScreen3.Name = "IBScreen3";
            this.IBScreen3.Size = new System.Drawing.Size(149, 131);
            this.IBScreen3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.IBScreen3.TabIndex = 7;
            this.IBScreen3.TabStop = false;
            // 
            // IBScreen4
            // 
            this.IBScreen4.FunctionalMode = Emgu.CV.UI.ImageBox.FunctionalModeOption.Minimum;
            this.IBScreen4.Location = new System.Drawing.Point(806, 459);
            this.IBScreen4.Name = "IBScreen4";
            this.IBScreen4.Size = new System.Drawing.Size(149, 131);
            this.IBScreen4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.IBScreen4.TabIndex = 8;
            this.IBScreen4.TabStop = false;
            // 
            // IBSource
            // 
            this.IBSource.FunctionalMode = Emgu.CV.UI.ImageBox.FunctionalModeOption.Minimum;
            this.IBSource.Location = new System.Drawing.Point(816, 188);
            this.IBSource.Name = "IBSource";
            this.IBSource.Size = new System.Drawing.Size(400, 250);
            this.IBSource.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.IBSource.TabIndex = 9;
            this.IBSource.TabStop = false;
            // 
            // IBTemplate
            // 
            this.IBTemplate.FunctionalMode = Emgu.CV.UI.ImageBox.FunctionalModeOption.Minimum;
            this.IBTemplate.Location = new System.Drawing.Point(999, 444);
            this.IBTemplate.Name = "IBTemplate";
            this.IBTemplate.Size = new System.Drawing.Size(200, 200);
            this.IBTemplate.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.IBTemplate.TabIndex = 10;
            this.IBTemplate.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(816, 9);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(109, 23);
            this.button1.TabIndex = 11;
            this.button1.Text = "bLoadImage";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(931, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Media ms";
            // 
            // lTime
            // 
            this.lTime.AutoSize = true;
            this.lTime.Location = new System.Drawing.Point(996, 14);
            this.lTime.Name = "lTime";
            this.lTime.Size = new System.Drawing.Size(13, 13);
            this.lTime.TabIndex = 13;
            this.lTime.Text = "0";
            // 
            // txtinfo
            // 
            this.txtinfo.BackColor = System.Drawing.SystemColors.InfoText;
            this.txtinfo.ForeColor = System.Drawing.SystemColors.Info;
            this.txtinfo.Location = new System.Drawing.Point(816, 38);
            this.txtinfo.Multiline = true;
            this.txtinfo.Name = "txtinfo";
            this.txtinfo.Size = new System.Drawing.Size(383, 144);
            this.txtinfo.TabIndex = 14;
            // 
            // cbPause
            // 
            this.cbPause.AutoSize = true;
            this.cbPause.Location = new System.Drawing.Point(118, 611);
            this.cbPause.Name = "cbPause";
            this.cbPause.Size = new System.Drawing.Size(163, 17);
            this.cbPause.TabIndex = 15;
            this.cbPause.Text = "Pause for 15 min (ctrl destro) ";
            this.cbPause.UseVisualStyleBackColor = true;
            this.cbPause.CheckedChanged += new System.EventHandler(this.cbPause_CheckedChanged);
            // 
            // cbOptimize
            // 
            this.cbOptimize.AutoSize = true;
            this.cbOptimize.Location = new System.Drawing.Point(287, 611);
            this.cbOptimize.Name = "cbOptimize";
            this.cbOptimize.Size = new System.Drawing.Size(68, 17);
            this.cbOptimize.TabIndex = 16;
            this.cbOptimize.Text = "Ottimizza";
            this.cbOptimize.UseVisualStyleBackColor = true;
            this.cbOptimize.CheckedChanged += new System.EventHandler(this.cbOptimize_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 595);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(250, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Refresh ms (non aumentare troppo se usi autoscan)";
            // 
            // cbAutoScan
            // 
            this.cbAutoScan.AutoSize = true;
            this.cbAutoScan.Location = new System.Drawing.Point(118, 627);
            this.cbAutoScan.Name = "cbAutoScan";
            this.cbAutoScan.Size = new System.Drawing.Size(308, 17);
            this.cbAutoScan.TabIndex = 18;
            this.cbAutoScan.Text = "Auto scan quindo esci da un sistema parte autoscan (inizio) ";
            this.cbAutoScan.UseVisualStyleBackColor = true;
            this.cbAutoScan.CheckedChanged += new System.EventHandler(this.cbAutoScan_CheckedChanged);
            // 
            // VisualForm
            // 
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1211, 653);
            this.Controls.Add(this.cbAutoScan);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbOptimize);
            this.Controls.Add(this.cbPause);
            this.Controls.Add(this.txtinfo);
            this.Controls.Add(this.lTime);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.IBTemplate);
            this.Controls.Add(this.IBSource);
            this.Controls.Add(this.IBScreen4);
            this.Controls.Add(this.IBScreen3);
            this.Controls.Add(this.IBScreen2);
            this.Controls.Add(this.IBScreen1);
            this.Controls.Add(this.txtrefresh);
            this.Controls.Add(this.IMTEMP);
            this.Controls.Add(this.ibGravidar);
            this.Controls.Add(this.ibVisual);
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(1000, 649);
            this.Name = "VisualForm";
            this.Text = "Auto Pilot";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.VisualForm_FormClosing);
            this.Load += new System.EventHandler(this.VisualForm_Load);
            this.Shown += new System.EventHandler(this.VisualForm_Shown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.VisualForm_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.ibVisual)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ibGravidar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IMTEMP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IBScreen1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IBScreen2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IBScreen3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IBScreen4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IBSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IBTemplate)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Emgu.CV.UI.ImageBox ibVisual;
        private Emgu.CV.UI.ImageBox ibGravidar;
        private Emgu.CV.UI.ImageBox IMTEMP;
        private TextBox txtrefresh;
        private Emgu.CV.UI.ImageBox IBScreen1;
        private Emgu.CV.UI.ImageBox IBScreen2;
        private Emgu.CV.UI.ImageBox IBScreen3;
        private Emgu.CV.UI.ImageBox IBScreen4;
        private Emgu.CV.UI.ImageBox IBSource;
        private Emgu.CV.UI.ImageBox IBTemplate;
        private Button button1;
        private Label label1;
        private Label lTime;
        public TextBox txtinfo;
        private CheckBox cbPause;
        private CheckBox cbOptimize;
        private Label label2;
        private CheckBox cbAutoScan;
    }
}