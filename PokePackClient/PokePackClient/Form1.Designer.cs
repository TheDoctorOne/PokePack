namespace PokePackClient
{
    partial class Form1
    {
        /// <summary>
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer üretilen kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.usernameBox = new System.Windows.Forms.TextBox();
            this.passBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.loginButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.consoleWanted = new System.Windows.Forms.CheckBox();
            this.forceDownloadJava = new System.Windows.Forms.CheckBox();
            this.forceUpdateBox = new System.Windows.Forms.CheckBox();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ramLabel = new System.Windows.Forms.Label();
            this.serverLabel = new System.Windows.Forms.Label();
            this.statusLabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.findJavaPath = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // usernameBox
            // 
            this.usernameBox.Location = new System.Drawing.Point(20, 37);
            this.usernameBox.Name = "usernameBox";
            this.usernameBox.Size = new System.Drawing.Size(160, 20);
            this.usernameBox.TabIndex = 0;
            // 
            // passBox
            // 
            this.passBox.Location = new System.Drawing.Point(20, 82);
            this.passBox.Name = "passBox";
            this.passBox.PasswordChar = '●';
            this.passBox.Size = new System.Drawing.Size(160, 20);
            this.passBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Username";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(17, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Password";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // loginButton
            // 
            this.loginButton.BackColor = System.Drawing.SystemColors.Desktop;
            this.loginButton.Location = new System.Drawing.Point(107, 108);
            this.loginButton.Name = "loginButton";
            this.loginButton.Size = new System.Drawing.Size(75, 24);
            this.loginButton.TabIndex = 4;
            this.loginButton.Text = "Login";
            this.loginButton.UseVisualStyleBackColor = false;
            this.loginButton.Click += new System.EventHandler(this.loginButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.consoleWanted);
            this.groupBox1.Controls.Add(this.forceDownloadJava);
            this.groupBox1.Controls.Add(this.forceUpdateBox);
            this.groupBox1.Controls.Add(this.usernameBox);
            this.groupBox1.Controls.Add(this.loginButton);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.passBox);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(547, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(195, 186);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Login";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // consoleWanted
            // 
            this.consoleWanted.AutoSize = true;
            this.consoleWanted.Location = new System.Drawing.Point(10, 161);
            this.consoleWanted.Name = "consoleWanted";
            this.consoleWanted.Size = new System.Drawing.Size(94, 17);
            this.consoleWanted.TabIndex = 11;
            this.consoleWanted.Text = "Show Console";
            this.consoleWanted.UseVisualStyleBackColor = true;
            this.consoleWanted.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // forceDownloadJava
            // 
            this.forceDownloadJava.AutoSize = true;
            this.forceDownloadJava.Location = new System.Drawing.Point(10, 138);
            this.forceDownloadJava.Name = "forceDownloadJava";
            this.forceDownloadJava.Size = new System.Drawing.Size(130, 17);
            this.forceDownloadJava.TabIndex = 11;
            this.forceDownloadJava.Text = "Force Download Java";
            this.forceDownloadJava.UseVisualStyleBackColor = true;
            this.forceDownloadJava.CheckedChanged += new System.EventHandler(this.forceDownloadJava_CheckedChanged);
            // 
            // forceUpdateBox
            // 
            this.forceUpdateBox.AutoSize = true;
            this.forceUpdateBox.Location = new System.Drawing.Point(10, 113);
            this.forceUpdateBox.Name = "forceUpdateBox";
            this.forceUpdateBox.Size = new System.Drawing.Size(91, 17);
            this.forceUpdateBox.TabIndex = 5;
            this.forceUpdateBox.Text = "Force Update";
            this.forceUpdateBox.UseVisualStyleBackColor = true;
            this.forceUpdateBox.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // webBrowser1
            // 
            this.webBrowser1.Location = new System.Drawing.Point(12, 12);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.ScrollBarsEnabled = false;
            this.webBrowser1.Size = new System.Drawing.Size(529, 374);
            this.webBrowser1.TabIndex = 6;
            this.webBrowser1.Url = new System.Uri("http://pixelmon.mahmutkocas.me/updates.html", System.UriKind.Absolute);
            this.webBrowser1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted);
            // 
            // trackBar1
            // 
            this.trackBar1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.trackBar1.Location = new System.Drawing.Point(6, 19);
            this.trackBar1.Maximum = 16000;
            this.trackBar1.Minimum = 1000;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(183, 45);
            this.trackBar1.TabIndex = 7;
            this.trackBar1.Value = 3000;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.ramLabel);
            this.groupBox2.Controls.Add(this.trackBar1);
            this.groupBox2.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.groupBox2.Location = new System.Drawing.Point(547, 204);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(195, 95);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Ram Options";
            // 
            // ramLabel
            // 
            this.ramLabel.AutoSize = true;
            this.ramLabel.Location = new System.Drawing.Point(69, 51);
            this.ramLabel.Name = "ramLabel";
            this.ramLabel.Size = new System.Drawing.Size(50, 13);
            this.ramLabel.TabIndex = 8;
            this.ramLabel.Text = "2300 MB";
            // 
            // serverLabel
            // 
            this.serverLabel.AutoSize = true;
            this.serverLabel.BackColor = System.Drawing.Color.Transparent;
            this.serverLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.serverLabel.Location = new System.Drawing.Point(584, 302);
            this.serverLabel.Name = "serverLabel";
            this.serverLabel.Size = new System.Drawing.Size(114, 16);
            this.serverLabel.TabIndex = 7;
            this.serverLabel.Text = "Waiting for Server";
            this.serverLabel.Click += new System.EventHandler(this.label3_Click);
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.statusLabel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.statusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.statusLabel.ForeColor = System.Drawing.Color.Aqua;
            this.statusLabel.Location = new System.Drawing.Point(208, 373);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.statusLabel.Size = new System.Drawing.Size(177, 13);
            this.statusLabel.TabIndex = 8;
            this.statusLabel.Text = "Pack Status: Waiting for login";
            this.statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(423, 389);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(319, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "By using this software, you are accepting everything it comes with.";
            // 
            // findJavaPath
            // 
            this.findJavaPath.BackColor = System.Drawing.Color.Black;
            this.findJavaPath.ForeColor = System.Drawing.Color.Transparent;
            this.findJavaPath.Location = new System.Drawing.Point(243, 389);
            this.findJavaPath.Name = "findJavaPath";
            this.findJavaPath.Size = new System.Drawing.Size(110, 22);
            this.findJavaPath.TabIndex = 10;
            this.findJavaPath.Text = "Find Java!";
            this.findJavaPath.UseVisualStyleBackColor = false;
            this.findJavaPath.Visible = false;
            this.findJavaPath.Click += new System.EventHandler(this.findJavaPath_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(754, 411);
            this.Controls.Add(this.findJavaPath);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.serverLabel);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.groupBox1);
            this.ForeColor = System.Drawing.Color.Transparent;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(770, 450);
            this.MinimumSize = new System.Drawing.Size(770, 450);
            this.Name = "Form1";
            this.Text = "PokePack Launcher";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox usernameBox;
        private System.Windows.Forms.TextBox passBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button loginButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label ramLabel;
        private System.Windows.Forms.Label serverLabel;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button findJavaPath;
        private System.Windows.Forms.CheckBox forceDownloadJava;
        private System.Windows.Forms.CheckBox forceUpdateBox;
        private System.Windows.Forms.CheckBox consoleWanted;
    }
}

