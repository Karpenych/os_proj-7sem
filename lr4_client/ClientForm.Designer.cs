namespace lr4_client
{
    partial class ClientForm
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
            txtChatBox = new TextBox();
            txtMessage = new TextBox();
            btnSend = new Button();
            lstChatters = new ListBox();
            SuspendLayout();
            // 
            // txtChatBox
            // 
            txtChatBox.Location = new Point(12, 12);
            txtChatBox.Multiline = true;
            txtChatBox.Name = "txtChatBox";
            txtChatBox.Size = new Size(632, 406);
            txtChatBox.TabIndex = 0;
            // 
            // txtMessage
            // 
            txtMessage.Location = new Point(12, 442);
            txtMessage.Name = "txtMessage";
            txtMessage.Size = new Size(495, 27);
            txtMessage.TabIndex = 1;
            txtMessage.TextChanged += txtMessage_TextChanged;
            txtMessage.KeyDown += txtMessage_KeyDown;
            // 
            // btnSend
            // 
            btnSend.Enabled = false;
            btnSend.Location = new Point(522, 435);
            btnSend.Name = "btnSend";
            btnSend.Size = new Size(122, 41);
            btnSend.TabIndex = 2;
            btnSend.Text = "Отправить";
            btnSend.UseVisualStyleBackColor = true;
            btnSend.Click += btnSend_Click;
            // 
            // lstChatters
            // 
            lstChatters.FormattingEnabled = true;
            lstChatters.ItemHeight = 20;
            lstChatters.Location = new Point(650, 12);
            lstChatters.Name = "lstChatters";
            lstChatters.Size = new Size(223, 464);
            lstChatters.TabIndex = 3;
            // 
            // ClientForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(885, 484);
            Controls.Add(lstChatters);
            Controls.Add(btnSend);
            Controls.Add(txtMessage);
            Controls.Add(txtChatBox);
            Name = "ClientForm";
            Text = "Чат";
            FormClosing += ClientForm_FormClosing;
            Load += ClientForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtChatBox;
        private TextBox txtMessage;
        private Button btnSend;
        private ListBox lstChatters;
    }
}