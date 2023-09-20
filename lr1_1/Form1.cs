using Microsoft.Win32;
using System.Windows.Forms;

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

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            RegistryKey rKey1 = Registry.LocalMachine;
            string key2 = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced\\Folder\\";
            RegistryKey rKey2 = rKey1.OpenSubKey(key2);
            string[] vNames = rKey2.GetValueNames();

            foreach (var name in vNames)
                listBox1.Items.Add(name + "\t\t" + rKey2.GetValue(name).ToString());

        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            RegistryKey rKey1 = Registry.CurrentUser;
            string key2 = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced\\";
            RegistryKey rKey2 = rKey1.OpenSubKey(key2);
            string[] vNames = rKey2.GetValueNames();

            foreach (var name in vNames)
                listBox1.Items.Add(name + "\t\t\t" + rKey2.GetValue(name).ToString());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            RegistryKey rKey1 = Registry.CurrentUser;
            string key2 = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\";
            RegistryKey rKey2 = rKey1.OpenSubKey(key2);
            string key3 = comboBox1.Text;
            try
            {
                RegistryKey rKey3 = rKey2.OpenSubKey(key3);
                string[] vNames = rKey3.GetValueNames();

                foreach (var name in vNames)
                    listBox1.Items.Add(name + "\t\t\t" + rKey2.GetValue(name).ToString());
            }
            catch(Exception ex)
            {
                listBox1.Items.Add(ex.Message);
            }
            

        }
    }
}