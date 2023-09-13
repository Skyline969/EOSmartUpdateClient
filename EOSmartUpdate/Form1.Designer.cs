
namespace EOSmartUpdate
{
    partial class frmMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.btnPlay = new System.Windows.Forms.Button();
            this.btnForceUpdate = new System.Windows.Forms.Button();
            this.chkPreserveSettings = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // txtStatus
            // 
            this.txtStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
            this.txtStatus.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtStatus.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtStatus.ForeColor = System.Drawing.Color.White;
            this.txtStatus.Location = new System.Drawing.Point(12, 12);
            this.txtStatus.Multiline = true;
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.ReadOnly = true;
            this.txtStatus.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtStatus.Size = new System.Drawing.Size(776, 373);
            this.txtStatus.TabIndex = 4;
            this.txtStatus.TabStop = false;
            // 
            // btnPlay
            // 
            this.btnPlay.BackColor = System.Drawing.Color.Silver;
            this.btnPlay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPlay.Location = new System.Drawing.Point(291, 391);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(75, 47);
            this.btnPlay.TabIndex = 1;
            this.btnPlay.TabStop = false;
            this.btnPlay.Text = "Play";
            this.btnPlay.UseVisualStyleBackColor = false;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // btnForceUpdate
            // 
            this.btnForceUpdate.BackColor = System.Drawing.Color.Silver;
            this.btnForceUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnForceUpdate.Location = new System.Drawing.Point(415, 391);
            this.btnForceUpdate.Name = "btnForceUpdate";
            this.btnForceUpdate.Size = new System.Drawing.Size(100, 47);
            this.btnForceUpdate.TabIndex = 2;
            this.btnForceUpdate.TabStop = false;
            this.btnForceUpdate.Text = "Force Update";
            this.btnForceUpdate.UseVisualStyleBackColor = false;
            this.btnForceUpdate.Click += new System.EventHandler(this.btnForceUpdate_Click);
            // 
            // chkPreserveSettings
            // 
            this.chkPreserveSettings.AutoSize = true;
            this.chkPreserveSettings.Checked = true;
            this.chkPreserveSettings.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPreserveSettings.ForeColor = System.Drawing.Color.White;
            this.chkPreserveSettings.Location = new System.Drawing.Point(12, 406);
            this.chkPreserveSettings.Name = "chkPreserveSettings";
            this.chkPreserveSettings.Size = new System.Drawing.Size(115, 19);
            this.chkPreserveSettings.TabIndex = 3;
            this.chkPreserveSettings.TabStop = false;
            this.chkPreserveSettings.Text = "Preserve Settings";
            this.chkPreserveSettings.UseVisualStyleBackColor = true;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.chkPreserveSettings);
            this.Controls.Add(this.btnForceUpdate);
            this.Controls.Add(this.btnPlay);
            this.Controls.Add(this.txtStatus);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Updater";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtStatus;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Button btnForceUpdate;
        private System.Windows.Forms.CheckBox chkPreserveSettings;
    }
}

