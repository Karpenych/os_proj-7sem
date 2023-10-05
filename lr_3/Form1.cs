using System.Management;

namespace lr_3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            listBox1.Items.Clear();
        }

        private void btCPU_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();

            OutputResult("Процессор:",          GetHardwareInfo("Win32_Processor", "Name"),             listBox1);
            OutputResult("Описание:",           GetHardwareInfo("Win32_Processor", "Description"),      listBox1);
            OutputResult("Характеристика:",     GetHardwareInfo("Win32_Processor", "Characteristics"),  listBox1);
            OutputResult("Разрядность:",        GetHardwareInfo("Win32_Processor", "DataWidth"),        listBox1);
            OutputResult("Макс такт частота:",  GetHardwareInfo("Win32_Processor", "MaxClockSpeed"),    listBox1);
        }

        private void btGPU_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();

            OutputResult("Видеокарта:", GetHardwareInfo("Win32_VideoController", "Name"), listBox1);
            OutputResult("Описание:", GetHardwareInfo("Win32_VideoController", "Description"), listBox1);
            OutputResult("Макс частота обновления:", GetHardwareInfo("Win32_VideoController", "MaxRefreshRate"), listBox1);
            OutputResult("Мин частота обновления:", GetHardwareInfo("Win32_VideoController", "MinRefreshRate"), listBox1);
            OutputResult("Видеопроцессор:", GetHardwareInfo("Win32_VideoController", "VideoProcessor"), listBox1);
            OutputResult("Версия драйвера:", GetHardwareInfo("Win32_VideoController", "DriverVersion"), listBox1);
            OutputResult("Видеоархитектура:", GetHardwareInfo("Win32_VideoController", "VideoArchitecture"), listBox1);
            OutputResult("Тип видеопамяти:", GetHardwareInfo("Win32_VideoController", "VideoMemoryType"), listBox1);
        }

        private static List<string> GetHardwareInfo(string Win32_class, string ClassItemField)
        {
            List<string> result = new();
            var searcher = new ManagementObjectSearcher("SELECT * FROM " + Win32_class);
            try
            {
                foreach (ManagementObject obj in searcher.Get().Cast<ManagementObject>())
                    result.Add(obj[ClassItemField].ToString().Trim());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка получения данных: " + ex.Message);
            }
            return result;
        }

        private static void OutputResult(string info, List<string> result, ListBox listbox)
        {
            if (result.Count > 0) 
            {
                for (int i = 0; i < result.Count; ++i)
                    listbox.Items.Add(info + " " + result[i]);
            }
        }
    }
}