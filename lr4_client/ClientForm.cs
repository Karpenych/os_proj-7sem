using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lr4_client
{
    enum Command
    {
        Login,      //Вход на сервер
        Logout,     //Выход с сервера
        Message,    //Послать сообщение в чат
        List,       //Получить список участников чата
        Null        //Нет комманды
    }

    public partial class ClientForm : Form
    {

        public Socket clientSocket; //Сокет клиента
        public string strName;      //Логин клиента
        private byte[] byteData = new byte[1024];

        public ClientForm()
        {
            InitializeComponent();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                Data msgToSend = new() //Информация для отправки
                {
                    strName = strName,
                    strMessage = txtMessage.Text,
                    cmdCommand = Command.Message
                };

                byte[] byteData = msgToSend.ToByte();
                clientSocket.BeginSend(byteData, 0, byteData.Length, SocketFlags.None, new AsyncCallback(OnSend), null); //Отправка на сервер
                txtMessage.Text = null;
            }
            catch (Exception)
            {
                MessageBox.Show("Не получилось отправить сообщение.", "ОШИБКА отправки: " + strName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OnSend(IAsyncResult ar)
        {
            try
            {
                clientSocket.EndSend(ar);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ОШИБКА отправки: " + strName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OnReceive(IAsyncResult ar)
        {
            try
            {
                clientSocket.EndReceive(ar);
                Data msgReceived = new(byteData);

                switch (msgReceived.cmdCommand) //Обработка полученного сообщения
                {
                    case Command.Login:
                        lstChatters.Items.Add(msgReceived.strName);
                        break;

                    case Command.Logout:
                        lstChatters.Items.Remove(msgReceived.strName);
                        break;

                    case Command.Message:
                        break;

                    case Command.List:
                        lstChatters.Items.AddRange(msgReceived.strMessage.Split('*'));
                        lstChatters.Items.RemoveAt(lstChatters.Items.Count - 1);
                        txtChatBox.Invoke(delegate ()
                        {
                            txtChatBox.Text += "<<< " + strName + " connected >>>\r\n";

                        });
                        break;
                }

                if (msgReceived.strMessage != null && msgReceived.cmdCommand != Command.List)
                    txtChatBox.Text += msgReceived.strMessage + "\r\n";

                byteData = new byte[1024];

                clientSocket.BeginReceive(byteData, 0, byteData.Length, SocketFlags.None, new AsyncCallback(OnReceive), null);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ОШИБКА RECEIVE: " + strName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClientForm_Load(object sender, EventArgs e)
        {
            Text += ": " + strName;

            //Делаем запрос List к серверу
            Data msgToSend = new()
            {
                cmdCommand = Command.List,
                strName = strName,
                strMessage = null
            };

            byteData = msgToSend.ToByte();
            clientSocket.BeginSend(byteData, 0, byteData.Length, SocketFlags.None, new AsyncCallback(OnSend), null);
            byteData = new byte[1024];
            //Начинаем прослушку сервера асинхронно
            clientSocket.BeginReceive(byteData, 0, byteData.Length, SocketFlags.None, new AsyncCallback(OnReceive), null);
        }

        private void txtMessage_TextChanged(object sender, EventArgs e)
        {
            btnSend.Enabled = txtMessage.Text.Length > 0;
        }

        private void ClientForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Вы точно хотите выйти?", "Закрытие: " + strName, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
            {
                e.Cancel = true;
                return;
            }

            try
            {
                Data msgToSend = new() //Отправляем на сервер Logout
                {
                    cmdCommand = Command.Logout,
                    strName = strName,
                    strMessage = null
                };

                byte[] b = msgToSend.ToByte();
                clientSocket.Send(b, 0, b.Length, SocketFlags.None);
                clientSocket.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ОШИБКА ЗАКРЫТИЯ ФОРМЫ: " + strName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSend_Click(sender, null);
            }
        }
    }

    class Data
    {
        public string strName;     //Имя под которым зашел клиент  
        public string strMessage;  //Сообщение  
        public Command cmdCommand; //Команда

        public Data() //Обычный конструктор
        {
            cmdCommand = Command.Null;
            strMessage = null;
            strName = null;
        }

        public Data(byte[] data) //Конструктор преобразовывающий массив байт в структуру Data
        {
            cmdCommand = (Command)BitConverter.ToInt32(data, 0); //Команда
            int nameLen = BitConverter.ToInt32(data, 4);         //Длинна имени
            int msgLen = BitConverter.ToInt32(data, 8);          //Длинна сообщения

            //Если имя задано, то преобразовываем байты имени в текст
            strName = nameLen > 0 ? Encoding.UTF8.GetString(data, 12, nameLen) : null;

            //Если сообщение задано, то преобразовываем байты сообщения в текст
            strMessage = msgLen > 0 ? Encoding.UTF8.GetString(data, 12 + nameLen, msgLen) : null;
        }

        public byte[] ToByte() //Преобразовывает структуру Data в массив байт
        {
            List<byte> result = new();

            result.AddRange(BitConverter.GetBytes((int)cmdCommand)); //Команда

            if (strName != null) //Если имя задано, то добавляем длинну
                result.AddRange(BitConverter.GetBytes(strName.Length));
            else //Или добавляем 0
                result.AddRange(BitConverter.GetBytes(0));

            if (strMessage != null) //Если сообщение задано, то добавляем длинну
                result.AddRange(BitConverter.GetBytes(strMessage.Length));
            else //Или добавляем 0
                result.AddRange(BitConverter.GetBytes(0));

            if (strName != null) //Если имя задано, то добавляем его в байтах
                result.AddRange(Encoding.UTF8.GetBytes(strName));

            if (strMessage != null) //Если сообщение задано, то добавляем его в байтах
                result.AddRange(Encoding.UTF8.GetBytes(strMessage));

            return result.ToArray(); //Из List в массив
        }
    }
}
