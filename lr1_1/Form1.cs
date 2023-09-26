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
            catch (Exception ex)
            {
                listBox1.Items.Add(ex.Message);
            }


        }

        //”далить слово €рлык

        private void button5_Click(object sender, EventArgs e)
        {
            RegistryKey rKey1 = Registry.CurrentUser;
            string key2 = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\";
            RegistryKey rKey2 = rKey1.OpenSubKey(key2, true);
            rKey2.SetValue("link", 0);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            RegistryKey rKey1 = Registry.CurrentUser;
            string key2 = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\";
            RegistryKey rKey2 = rKey1.OpenSubKey(key2, true);
            rKey2.SetValue("link", 16);
        }

        //”далить стрелку с €рлыка

        private void button6_Click(object sender, EventArgs e)
        {
            RegistryKey rKey1 = Registry.LocalMachine;
            string key2 = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\";
            RegistryKey rKey2 = rKey1.OpenSubKey(key2, true);
            RegistryKey rKey3 = rKey2.CreateSubKey("Shell Icons");
            rKey3.SetValue("29", "");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            RegistryKey rKey1 = Registry.LocalMachine;
            string key2 = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Shell Icons\\";
            RegistryKey rKey2 = rKey1.OpenSubKey(key2, true);
            rKey2.DeleteValue("29");
            rKey2.Close();
            string key3 = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\";
            RegistryKey rKey3 = rKey1.OpenSubKey(key3, true);
            rKey3.DeleteSubKey("Shell Icons");
        }

        //ќтключить диспейчер задач

        private void button7_Click(object sender, EventArgs e)
        {
            RegistryKey rKey1 = Registry.CurrentUser;
            string key2 = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\";
            RegistryKey rKey2 = rKey1.OpenSubKey(key2, true);
            RegistryKey rKey3 = rKey2.CreateSubKey("System");
            rKey3.SetValue("DisableTaskMgr", 1);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            RegistryKey rKey1 = Registry.CurrentUser;
            string key2 = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\System\\";
            RegistryKey rKey2 = rKey1.OpenSubKey(key2, true);
            rKey2.DeleteValue("DisableTaskMgr");
            rKey2.Close();
            string key3 = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\";
            RegistryKey rKey3 = rKey1.OpenSubKey(key3, true);
            rKey3.DeleteSubKey("System");
        }

        // омандна€ строка

        private void button8_Click(object sender, EventArgs e)
        {
            RegistryKey rKey1 = Registry.ClassesRoot;
            string key2 = "Directory\\shell\\";
            RegistryKey rKey2 = rKey1.OpenSubKey(key2, true);
            RegistryKey rKey3 = rKey2.CreateSubKey("Command Prompt");
            rKey3.SetValue(string.Empty, "ќткрыть в командной строке");
            RegistryKey rKey4 = rKey3.CreateSubKey("Command");
            rKey4.SetValue(string.Empty, "cmd.exe /s /k pushd \"%L\"");
        }

        private void button11_Click(object sender, EventArgs e)
        {
            RegistryKey rKey1 = Registry.ClassesRoot;
            string key2 = "Directory\\shell\\";
            RegistryKey rKey2 = rKey1.OpenSubKey(key2, true);
            rKey2.DeleteSubKeyTree("Command Prompt");
        }
    }
}