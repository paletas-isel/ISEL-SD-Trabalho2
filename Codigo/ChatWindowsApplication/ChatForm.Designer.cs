namespace ChatWindowsApplication
{
    partial class ChatForm
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
            this.chat = new System.Windows.Forms.ListBox();
            this.message = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // chat
            // 
            this.chat.FormattingEnabled = true;
            this.chat.Location = new System.Drawing.Point(9, 12);
            this.chat.Name = "chat";
            this.chat.Size = new System.Drawing.Size(418, 316);
            this.chat.TabIndex = 2;
            // 
            // message
            // 
            this.message.Location = new System.Drawing.Point(12, 334);
            this.message.Name = "message";
            this.message.Size = new System.Drawing.Size(337, 20);
            this.message.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(355, 334);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Send";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ChatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(439, 363);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.message);
            this.Controls.Add(this.chat);
            this.Name = "ChatForm";
            this.Text = "Chat";
            this.Load += new System.EventHandler(this.ChatForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox chat;
        private System.Windows.Forms.TextBox message;
        private System.Windows.Forms.Button button1;
    }
}

