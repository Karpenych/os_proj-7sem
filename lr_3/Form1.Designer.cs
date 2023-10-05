namespace lr_3
{
    partial class Form1
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
            listBox1 = new ListBox();
            btCPU = new Button();
            btGPU = new Button();
            SuspendLayout();
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 20;
            listBox1.Location = new Point(12, 12);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(630, 424);
            listBox1.TabIndex = 0;
            // 
            // btCPU
            // 
            btCPU.Location = new Point(683, 12);
            btCPU.Name = "btCPU";
            btCPU.Size = new Size(150, 191);
            btCPU.TabIndex = 1;
            btCPU.Text = "Процессор";
            btCPU.UseVisualStyleBackColor = true;
            btCPU.Click += btCPU_Click;
            // 
            // btGPU
            // 
            btGPU.Location = new Point(683, 246);
            btGPU.Name = "btGPU";
            btGPU.Size = new Size(150, 190);
            btGPU.TabIndex = 2;
            btGPU.Text = "Видеокарта";
            btGPU.UseVisualStyleBackColor = true;
            btGPU.Click += btGPU_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(849, 450);
            Controls.Add(btGPU);
            Controls.Add(btCPU);
            Controls.Add(listBox1);
            Name = "Form1";
            Text = "Карпенко ЛР3";
            ResumeLayout(false);
        }

        #endregion

        private ListBox listBox1;
        private Button btCPU;
        private Button btGPU;
    }
}