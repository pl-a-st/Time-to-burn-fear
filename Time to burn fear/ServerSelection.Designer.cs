namespace Time_to_burn_fear
{
    partial class ServerSelection
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
            this.cBxServers = new System.Windows.Forms.ComboBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnLoadServers = new System.Windows.Forms.Button();
            this.pBLoad = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // cBxServers
            // 
            this.cBxServers.Enabled = false;
            this.cBxServers.FormattingEnabled = true;
            this.cBxServers.Location = new System.Drawing.Point(12, 12);
            this.cBxServers.Name = "cBxServers";
            this.cBxServers.Size = new System.Drawing.Size(248, 21);
            this.cBxServers.TabIndex = 0;
            this.cBxServers.Text = "Необходимо загрузить сервера ->";
            this.cBxServers.SelectedIndexChanged += new System.EventHandler(this.cBxServers_SelectedIndexChanged);
            this.cBxServers.Click += new System.EventHandler(this.cBxServers_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(12, 39);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(145, 25);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(163, 39);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(133, 25);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnLoadServers
            // 
            this.btnLoadServers.Location = new System.Drawing.Point(266, 11);
            this.btnLoadServers.Name = "btnLoadServers";
            this.btnLoadServers.Size = new System.Drawing.Size(30, 20);
            this.btnLoadServers.TabIndex = 4;
            this.btnLoadServers.Text = "Ѻ";
            this.btnLoadServers.UseVisualStyleBackColor = true;
            this.btnLoadServers.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // pBLoad
            // 
            this.pBLoad.Location = new System.Drawing.Point(12, 70);
            this.pBLoad.Name = "pBLoad";
            this.pBLoad.Size = new System.Drawing.Size(284, 23);
            this.pBLoad.TabIndex = 5;
            // 
            // ServerSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(321, 104);
            this.Controls.Add(this.pBLoad);
            this.Controls.Add(this.btnLoadServers);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.cBxServers);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "ServerSelection";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Выбор сервера";
            this.Load += new System.EventHandler(this.ServerSelection_Load);
            this.Shown += new System.EventHandler(this.ServerSelection_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cBxServers;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnLoadServers;
        private System.Windows.Forms.ProgressBar pBLoad;
    }
}