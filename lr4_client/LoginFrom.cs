using System.Net;
using System.Net.Sockets;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml.Linq;

namespace lr4_client
{
    public partial class LoginForm : Form
    {
        public Socket clientSocket;
        public string strName;

        public LoginForm()
        {
            InitializeComponent();
        }

        private void btOk_Click(object sender, EventArgs e)
        {
            try
            {
                clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint ipEndPoint = new(IPAddress.Parse(txtServerIP.Text), 1000);

                clientSocket.BeginConnect(ipEndPoint, new AsyncCallback(OnConnect), null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка btOK", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OnConnect(IAsyncResult ar)
        {
            try
            {
                clientSocket.EndConnect(ar);

                //Так как входим на сервер, нужно отправить на него команду Login
                Data msgToSend = new()
                {
                    cmdCommand = Command.Login,
                    strName = txtLogin.Text,
                    strMessage = null
                };

                byte[] b = msgToSend.ToByte();

                //Отправляю сообщение на сервер
                clientSocket.BeginSend(b, 0, b.Length, SocketFlags.None, new AsyncCallback(OnSend), null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка Connect", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OnSend(IAsyncResult ar)
        {
            try
            {
                clientSocket.EndSend(ar);
                strName = txtLogin.Text;
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка Send", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
        }

        private void txtLogin_TextChanged(object sender, EventArgs e)
        {
            btOk.Enabled = txtLogin.Text.Length > 0 && txtServerIP.Text.Length > 0;
        }

        private void txtServerIP_TextChanged(object sender, EventArgs e)
        {
            btOk.Enabled = txtLogin.Text.Length > 0 && txtServerIP.Text.Length > 0;
        }
    }
}