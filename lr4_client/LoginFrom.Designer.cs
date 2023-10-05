namespace lr4_client
{
    partial class LoginForm
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
            btOk = new Button();
            btClose = new Button();
            txtLogin = new TextBox();
            txtServerIP = new TextBox();
            label1 = new Label();
            label2 = new Label();
            SuspendLayout();
            // 
            // btOk
            // 
            btOk.Enabled = false;
            btOk.Location = new Point(12, 119);
            btOk.Name = "btOk";
            btOk.Size = new Size(212, 54);
            btOk.TabIndex = 0;
            btOk.Text = "Вход";
            btOk.UseVisualStyleBackColor = true;
            btOk.Click += btOk_Click;
            // 
            // btClose
            // 
            btClose.Location = new Point(243, 141);
            btClose.Name = "btClose";
            btClose.Size = new Size(80, 32);
            btClose.TabIndex = 1;
            btClose.Text = "Отмена";
            btClose.UseVisualStyleBackColor = true;
            btClose.Click += btClose_Click;
            // 
            // txtLogin
            // 
            txtLogin.Location = new Point(117, 12);
            txtLogin.Name = "txtLogin";
            txtLogin.Size = new Size(206, 27);
            txtLogin.TabIndex = 2;
            txtLogin.TextChanged += txtLogin_TextChanged;
            // 
            // txtServerIP
            // 
            txtServerIP.Location = new Point(117, 58);
            txtServerIP.Name = "txtServerIP";
            txtServerIP.Size = new Size(206, 27);
            txtServerIP.TabIndex = 3;
            txtServerIP.TextChanged += txtServerIP_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 19);
            label1.Name = "label1";
            label1.Size = new Size(42, 20);
            label1.TabIndex = 4;
            label1.Text = "Имя:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 65);
            label2.Name = "label2";
            label2.Size = new Size(85, 20);
            label2.TabIndex = 5;
            label2.Text = "IP сервера:";
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(339, 188);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtServerIP);
            Controls.Add(txtLogin);
            Controls.Add(btClose);
            Controls.Add(btOk);
            Name = "LoginForm";
            Text = "Вход";
            Load += LoginForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btOk;
        private Button btClose;
        private TextBox txtLogin;
        private TextBox txtServerIP;
        private Label label1;
        private Label label2;
    }
}