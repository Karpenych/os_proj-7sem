using Microsoft.Win32;

namespace os_lr_1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RegistryKey[] regKeyArr = new RegistryKey[]
            {
                Registry.ClassesRoot,
                Registry.CurrentUser,
                Registry.LocalMachine,
                Registry.Users,
                Registry.CurrentConfig
            };

            textBox1.Text = "";
            textBox2.Text = "";

            foreach (var regKey in regKeyArr)
            {
                textBox1.Text += regKey.Name + "\r\n";
                textBox2.Text += regKey.SubKeyCount + "\r\n";
            }
        }
    }
}