namespace ChatServiceProject
{
    partial class PickUsernameForm
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
            this.username = new System.Windows.Forms.TextBox();
            this.okbutton = new System.Windows.Forms.Button();
            this.language = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.themes = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // username
            // 
            this.username.Location = new System.Drawing.Point(13, 13);
            this.username.Name = "username";
            this.username.Size = new System.Drawing.Size(259, 20);
            this.username.TabIndex = 0;
            // 
            // okbutton
            // 
            this.okbutton.Location = new System.Drawing.Point(197, 123);
            this.okbutton.Name = "okbutton";
            this.okbutton.Size = new System.Drawing.Size(75, 23);
            this.okbutton.TabIndex = 1;
            this.okbutton.Text = "OK";
            this.okbutton.UseVisualStyleBackColor = true;
            this.okbutton.Click += new System.EventHandler(this.okbutton_Click);
            // 
            // language
            // 
            this.language.FormattingEnabled = true;
            this.language.Items.AddRange(new object[] {
            "English (en)",
            "Portuguese (pt)"});
            this.language.Location = new System.Drawing.Point(114, 86);
            this.language.Name = "language";
            this.language.Size = new System.Drawing.Size(158, 21);
            this.language.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 86);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Wanted Language";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Theme";
            // 
            // themes
            // 
            this.themes.FormattingEnabled = true;
            this.themes.Location = new System.Drawing.Point(114, 47);
            this.themes.Name = "themes";
            this.themes.Size = new System.Drawing.Size(158, 21);
            this.themes.TabIndex = 5;
            // 
            // PickUsernameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 158);
            this.Controls.Add(this.themes);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.language);
            this.Controls.Add(this.okbutton);
            this.Controls.Add(this.username);
            this.Name = "PickUsernameForm";
            this.Text = "PickUsernameForm";
            this.Load += new System.EventHandler(this.PickUsernameForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox username;
        private System.Windows.Forms.Button okbutton;
        private System.Windows.Forms.ComboBox language;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox themes;
    }
}