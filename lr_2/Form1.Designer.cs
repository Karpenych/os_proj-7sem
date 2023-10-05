namespace lr_2
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
            label1 = new Label();
            textBox1 = new TextBox();
            btFind = new Button();
            openFileDialog1 = new OpenFileDialog();
            btFiles = new Button();
            label2 = new Label();
            listBox1 = new ListBox();
            listBox2 = new ListBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(10, 16);
            label1.Name = "label1";
            label1.Size = new Size(150, 15);
            label1.TabIndex = 0;
            label1.Text = "Введите слово для поиска";
            // 
            // textBox1
            // 
            textBox1.Cursor = Cursors.IBeam;
            textBox1.Location = new Point(10, 34);
            textBox1.Margin = new Padding(3, 2, 3, 2);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(168, 23);
            textBox1.TabIndex = 1;
            // 
            // btFind
            // 
            btFind.Cursor = Cursors.Hand;
            btFind.Location = new Point(10, 154);
            btFind.Margin = new Padding(3, 2, 3, 2);
            btFind.Name = "btFind";
            btFind.Size = new Size(1070, 32);
            btFind.TabIndex = 2;
            btFind.Text = "Найти";
            btFind.UseVisualStyleBackColor = true;
            btFind.Click += btFind_Click;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // btFiles
            // 
            btFiles.Cursor = Cursors.Hand;
            btFiles.Location = new Point(10, 70);
            btFiles.Margin = new Padding(3, 2, 3, 2);
            btFiles.Name = "btFiles";
            btFiles.Size = new Size(167, 71);
            btFiles.TabIndex = 3;
            btFiles.Text = "Выбрать файлы";
            btFiles.UseVisualStyleBackColor = true;
            btFiles.Click += btFiles_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(635, 16);
            label2.Name = "label2";
            label2.Size = new Size(113, 15);
            label2.TabIndex = 4;
            label2.Text = "Выбранные файлы";
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.HorizontalScrollbar = true;
            listBox1.ItemHeight = 15;
            listBox1.Location = new Point(196, 34);
            listBox1.Margin = new Padding(3, 2, 3, 2);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(885, 109);
            listBox1.TabIndex = 5;
            // 
            // listBox2
            // 
            listBox2.FormattingEnabled = true;
            listBox2.HorizontalScrollbar = true;
            listBox2.ItemHeight = 15;
            listBox2.Location = new Point(10, 206);
            listBox2.Margin = new Padding(3, 2, 3, 2);
            listBox2.Name = "listBox2";
            listBox2.Size = new Size(1071, 259);
            listBox2.TabIndex = 6;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1099, 474);
            Controls.Add(listBox2);
            Controls.Add(listBox1);
            Controls.Add(label2);
            Controls.Add(btFiles);
            Controls.Add(btFind);
            Controls.Add(textBox1);
            Controls.Add(label1);
            Margin = new Padding(3, 2, 3, 2);
            Name = "Form1";
            Text = "Карпенко ЛР2";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox textBox1;
        private Button btFind;
        private OpenFileDialog openFileDialog1;
        private Button btFiles;
        private Label label2;
        private ListBox listBox1;
        private ListBox listBox2;
    }
}