namespace Time_to_burn_fear
{
    partial class AddThing
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
            this.lBxDress = new System.Windows.Forms.ListBox();
            this.cBxType = new System.Windows.Forms.ComboBox();
            this.tBxName = new System.Windows.Forms.TextBox();
            this.nUDFirstParametr = new System.Windows.Forms.NumericUpDown();
            this.nUDSecondParametr = new System.Windows.Forms.NumericUpDown();
            this.lblName = new System.Windows.Forms.Label();
            this.lblType = new System.Windows.Forms.Label();
            this.lblFirstParametr = new System.Windows.Forms.Label();
            this.lblSecondParamer = new System.Windows.Forms.Label();
            this.btnAdd_Save = new System.Windows.Forms.Button();
            this.btnChange_Cancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nUDFirstParametr)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUDSecondParametr)).BeginInit();
            this.SuspendLayout();
            // 
            // lBxDress
            // 
            this.lBxDress.FormattingEnabled = true;
            this.lBxDress.Location = new System.Drawing.Point(138, 50);
            this.lBxDress.Name = "lBxDress";
            this.lBxDress.Size = new System.Drawing.Size(122, 82);
            this.lBxDress.TabIndex = 0;
            this.lBxDress.SelectedValueChanged += new System.EventHandler(this.lBxDress_SelectedValueChanged);
            // 
            // cBxType
            // 
            this.cBxType.FormattingEnabled = true;
            this.cBxType.Location = new System.Drawing.Point(138, 24);
            this.cBxType.Name = "cBxType";
            this.cBxType.Size = new System.Drawing.Size(121, 21);
            this.cBxType.TabIndex = 1;
            this.cBxType.SelectedIndexChanged += new System.EventHandler(this.cBxType_SelectedIndexChanged);
            // 
            // tBxName
            // 
            this.tBxName.Location = new System.Drawing.Point(12, 25);
            this.tBxName.Name = "tBxName";
            this.tBxName.Size = new System.Drawing.Size(120, 20);
            this.tBxName.TabIndex = 2;
            // 
            // nUDFirstParametr
            // 
            this.nUDFirstParametr.Location = new System.Drawing.Point(12, 66);
            this.nUDFirstParametr.Name = "nUDFirstParametr";
            this.nUDFirstParametr.Size = new System.Drawing.Size(120, 20);
            this.nUDFirstParametr.TabIndex = 3;
            // 
            // nUDSecondParametr
            // 
            this.nUDSecondParametr.Location = new System.Drawing.Point(12, 112);
            this.nUDSecondParametr.Name = "nUDSecondParametr";
            this.nUDSecondParametr.Size = new System.Drawing.Size(120, 20);
            this.nUDSecondParametr.TabIndex = 3;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(12, 8);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(57, 13);
            this.lblName.TabIndex = 4;
            this.lblName.Text = "Название";
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(135, 8);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(26, 13);
            this.lblType.TabIndex = 4;
            this.lblType.Text = "Тип";
            // 
            // lblFirstParametr
            // 
            this.lblFirstParametr.AutoSize = true;
            this.lblFirstParametr.Location = new System.Drawing.Point(12, 49);
            this.lblFirstParametr.Name = "lblFirstParametr";
            this.lblFirstParametr.Size = new System.Drawing.Size(35, 13);
            this.lblFirstParametr.TabIndex = 4;
            this.lblFirstParametr.Text = "label1";
            // 
            // lblSecondParamer
            // 
            this.lblSecondParamer.AutoSize = true;
            this.lblSecondParamer.Location = new System.Drawing.Point(12, 95);
            this.lblSecondParamer.Name = "lblSecondParamer";
            this.lblSecondParamer.Size = new System.Drawing.Size(35, 13);
            this.lblSecondParamer.TabIndex = 4;
            this.lblSecondParamer.Text = "label1";
            // 
            // btnAdd_Save
            // 
            this.btnAdd_Save.Location = new System.Drawing.Point(12, 150);
            this.btnAdd_Save.Name = "btnAdd_Save";
            this.btnAdd_Save.Size = new System.Drawing.Size(120, 23);
            this.btnAdd_Save.TabIndex = 5;
            this.btnAdd_Save.Text = "button1";
            this.btnAdd_Save.UseVisualStyleBackColor = true;
            this.btnAdd_Save.Click += new System.EventHandler(this.btnAdd_Save_Click);
            // 
            // btnChange_Cancel
            // 
            this.btnChange_Cancel.Location = new System.Drawing.Point(138, 150);
            this.btnChange_Cancel.Name = "btnChange_Cancel";
            this.btnChange_Cancel.Size = new System.Drawing.Size(120, 23);
            this.btnChange_Cancel.TabIndex = 5;
            this.btnChange_Cancel.Text = "button1";
            this.btnChange_Cancel.UseVisualStyleBackColor = true;
            this.btnChange_Cancel.Click += new System.EventHandler(this.btnChange_Cancel_Click);
            // 
            // AddThing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(275, 190);
            this.Controls.Add(this.btnChange_Cancel);
            this.Controls.Add(this.btnAdd_Save);
            this.Controls.Add(this.lblType);
            this.Controls.Add(this.lblSecondParamer);
            this.Controls.Add(this.lblFirstParametr);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.nUDSecondParametr);
            this.Controls.Add(this.nUDFirstParametr);
            this.Controls.Add(this.tBxName);
            this.Controls.Add(this.cBxType);
            this.Controls.Add(this.lBxDress);
            this.Name = "AddThing";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AddThing";
            this.Load += new System.EventHandler(this.AddThing_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nUDFirstParametr)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUDSecondParametr)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lBxDress;
        private System.Windows.Forms.ComboBox cBxType;
        private System.Windows.Forms.TextBox tBxName;
        private System.Windows.Forms.NumericUpDown nUDFirstParametr;
        private System.Windows.Forms.NumericUpDown nUDSecondParametr;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.Label lblFirstParametr;
        private System.Windows.Forms.Label lblSecondParamer;
        private System.Windows.Forms.Button btnAdd_Save;
        private System.Windows.Forms.Button btnChange_Cancel;
    }
}