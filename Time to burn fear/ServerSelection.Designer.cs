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
            this.button1 = new System.Windows.Forms.Button();
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
            this.cBxServers.Text = "Необходимо зачитать сервера ->";
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
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(163, 39);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(133, 25);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(266, 11);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(30, 20);
            this.button1.TabIndex = 4;
            this.button1.Text = "Ѻ";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
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
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.cBxServers);
            this.Name = "ServerSelection";
            this.Text = "Выбор сервера";
            this.Load += new System.EventHandler(this.ServerSelection_Load);
            this.Shown += new System.EventHandler(this.ServerSelection_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cBxServers;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ProgressBar pBLoad;
    }
}