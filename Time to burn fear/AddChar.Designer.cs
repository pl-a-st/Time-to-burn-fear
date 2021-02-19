namespace Time_to_burn_fear
{
    partial class AddChar
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnAddChar = new System.Windows.Forms.Button();
            this.tBxName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cBxRace = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(148, 65);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnAddChar
            // 
            this.btnAddChar.Location = new System.Drawing.Point(148, 28);
            this.btnAddChar.Name = "btnAddChar";
            this.btnAddChar.Size = new System.Drawing.Size(75, 23);
            this.btnAddChar.TabIndex = 6;
            this.btnAddChar.Text = "Добавить";
            this.btnAddChar.UseVisualStyleBackColor = true;
            this.btnAddChar.Click += new System.EventHandler(this.btnAddChar_Click);
            // 
            // tBxName
            // 
            this.tBxName.Location = new System.Drawing.Point(12, 28);
            this.tBxName.Name = "tBxName";
            this.tBxName.Size = new System.Drawing.Size(130, 20);
            this.tBxName.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Имя";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // cBxRace
            // 
            this.cBxRace.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBxRace.FormattingEnabled = true;
            this.cBxRace.Location = new System.Drawing.Point(12, 67);
            this.cBxRace.Name = "cBxRace";
            this.cBxRace.Size = new System.Drawing.Size(130, 21);
            this.cBxRace.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Раса";
            // 
            // AddChar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(252, 107);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cBxRace);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAddChar);
            this.Controls.Add(this.tBxName);
            this.Controls.Add(this.label1);
            this.Name = "AddChar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Таверна  ";
            this.Load += new System.EventHandler(this.AddChar_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button btnCancel;
        public System.Windows.Forms.Button btnAddChar;
        public System.Windows.Forms.TextBox tBxName;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.ComboBox cBxRace;
    }
}