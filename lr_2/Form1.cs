using System.Linq;

namespace lr_2
{
    public partial class Form1 : Form
    {
        private Mutex MUTEX = new();
        private List<string> FILE_PATHS = new();

        public Form1()
        {
            InitializeComponent();
            openFileDialog1.Multiselect = true;
            openFileDialog1.Filter = "Текстовые файлы (*.txt;*.csv)|*.txt;*.csv";
            listBox1.Items.Clear();
            listBox2.Items.Clear();
        }

        private void btFiles_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            FILE_PATHS.Clear();
            openFileDialog1.ShowDialog();
            foreach (string pathFile in openFileDialog1.FileNames)
            {
                FILE_PATHS.Add(pathFile);
                listBox1.Items.Add(pathFile);
            }

            openFileDialog1.Dispose();
        }

        private void btFind_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            foreach (string _pathFile in FILE_PATHS)
            {
                new Thread(delegate ()
                {
                    string[] lines = File.ReadAllLines(_pathFile);
                    for (int i = 0; i < lines.Length; i++)
                    {
                        string line = lines[i];
                        if (line.Contains(textBox1.Text))
                        {
                            MUTEX.WaitOne();
                            var str = $"{_pathFile}) [{i + 1}] {line}";
                            listBox2.Invoke(new MethodInvoker(delegate ()
                            {
                                listBox2.Items.Add(str);
                            }));
                            MUTEX.ReleaseMutex();
                        }
                    }
                }).Start();
            }
        }
    }
}
