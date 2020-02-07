namespace WindowsFormsApp2
{
    partial class FormFind
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
            this.labelFindText = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.checkBoxCaseSensetive = new System.Windows.Forms.CheckBox();
            this.checkBoxWholeWordsOnly = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // labelFindText
            // 
            this.labelFindText.AutoSize = true;
            this.labelFindText.Location = new System.Drawing.Point(8, 8);
            this.labelFindText.Name = "labelFindText";
            this.labelFindText.Size = new System.Drawing.Size(107, 13);
            this.labelFindText.TabIndex = 0;
            this.labelFindText.Text = "Type here text to find";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(8, 29);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(306, 20);
            this.textBox1.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(348, 8);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Find";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(348, 39);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // checkBoxCaseSensetive
            // 
            this.checkBoxCaseSensetive.AutoSize = true;
            this.checkBoxCaseSensetive.Location = new System.Drawing.Point(13, 56);
            this.checkBoxCaseSensetive.Name = "checkBoxCaseSensetive";
            this.checkBoxCaseSensetive.Size = new System.Drawing.Size(100, 17);
            this.checkBoxCaseSensetive.TabIndex = 4;
            this.checkBoxCaseSensetive.Text = "Case Sensetive";
            this.checkBoxCaseSensetive.UseVisualStyleBackColor = true;
            this.checkBoxCaseSensetive.CheckedChanged += new System.EventHandler(this.checkBoxCaseSensetive_CheckedChanged);
            // 
            // checkBoxWholeWordsOnly
            // 
            this.checkBoxWholeWordsOnly.AutoSize = true;
            this.checkBoxWholeWordsOnly.Location = new System.Drawing.Point(130, 56);
            this.checkBoxWholeWordsOnly.Name = "checkBoxWholeWordsOnly";
            this.checkBoxWholeWordsOnly.Size = new System.Drawing.Size(115, 17);
            this.checkBoxWholeWordsOnly.TabIndex = 5;
            this.checkBoxWholeWordsOnly.Text = "Whole Words Only";
            this.checkBoxWholeWordsOnly.UseVisualStyleBackColor = true;
            this.checkBoxWholeWordsOnly.CheckedChanged += new System.EventHandler(this.checkBoxWholeWordsOnly_CheckedChanged);
            // 
            // FormFind
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(461, 75);
            this.Controls.Add(this.checkBoxWholeWordsOnly);
            this.Controls.Add(this.checkBoxCaseSensetive);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.labelFindText);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormFind";
            this.Text = "FormFind";
            this.Load += new System.EventHandler(this.FormFind_Load);
            this.Shown += new System.EventHandler(this.FormFind_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelFindText;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.CheckBox checkBoxCaseSensetive;
        private System.Windows.Forms.CheckBox checkBoxWholeWordsOnly;
    }
}