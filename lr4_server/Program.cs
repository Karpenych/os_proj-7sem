using System.Net.Sockets;
using System.Net;
using System.Collections;
using System.Text;

namespace lr4_server
{
    enum Command
    {
        Login,      //Вход на сервер
        Logout,     //Выход с сервера
        Message,    //Послать сообщение в чат
        List,       //Получить список участников чата
        Null        //Нет комманды
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
            strName    = nameLen > 0 ? Encoding.UTF8.GetString(data, 12,           nameLen) : null;

            //Если сообщение задано, то преобразовываем байты сообщения в текст
            strMessage = msgLen  > 0 ? Encoding.UTF8.GetString(data, 12 + nameLen, msgLen)  : null;
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

    internal class Program
    {
        struct ClientInfo
        {
            public Socket socket; 
            public string strName;
        }

        private static ArrayList clientList;
        private static Socket serverSocket;
        private static byte[] byteData = new byte[1024];

        private static EventWaitHandle _waitHandle = new AutoResetEvent(false);


        static void Main(string[] args)
        {
            clientList = new();

            try
            {
                serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint ipEndPoint = new(IPAddress.Any, 1000);
                serverSocket.Bind(ipEndPoint);
                serverSocket.Listen(10);
                serverSocket.BeginAccept(new AsyncCallback(OnAccept), null);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("SERVER ERROR: " + ex.Message);
                Console.ForegroundColor = ConsoleColor.White;
            }
           
            _waitHandle.WaitOne();          
        }

        private static void OnAccept(IAsyncResult ar)
        {
            try
            {
                Socket clientSocket = serverSocket.EndAccept(ar);
                serverSocket.BeginAccept(new AsyncCallback(OnAccept), null);

                clientSocket.BeginReceive(byteData, 0, byteData.Length, SocketFlags.None, new AsyncCallback(OnReceive), clientSocket);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ACCEPT ERROR: " + ex.Message);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        private static void OnReceive(IAsyncResult ar)
        {
            try
            {
                Socket clientSocket = (Socket)ar.AsyncState;
                clientSocket.EndReceive(ar);
                Data msgReceived = new(byteData);
                Data msgToSend = new();
                byte[] message;

                msgToSend.cmdCommand = msgReceived.cmdCommand;
                msgToSend.strName = msgReceived.strName;

                switch (msgReceived.cmdCommand)
                {
                    case Command.Login:

                        ClientInfo clientInfo = new()
                        {
                            socket = clientSocket,
                            strName = msgReceived.strName
                        };

                        clientList.Add(clientInfo);

                        msgToSend.strMessage = "<<< " + msgReceived.strName + " connected >>>";
                        break;

                    case Command.Logout:

                        int nIndex = 0;
                        foreach (ClientInfo client in clientList)
                        {
                            if (client.socket == clientSocket)
                            {
                                clientList.RemoveAt(nIndex);
                                break;
                            }
                            ++nIndex;
                        }

                        clientSocket.Close();

                        msgToSend.strMessage = "<<< " + msgReceived.strName + " leave >>>";
                        break;

                    case Command.Message:

                        msgToSend.strMessage = msgReceived.strName + ": " + msgReceived.strMessage;
                        break;

                    case Command.List:

                        //Send the names of all users in the chat room to the new user
                        msgToSend.cmdCommand = Command.List;
                        msgToSend.strName = null;
                        msgToSend.strMessage = null;

                        //Collect the names of the user in the chat room
                        foreach (ClientInfo client in clientList)
                        {
                            msgToSend.strMessage += client.strName + "*";
                        }

                        message = msgToSend.ToByte();

                        //Send the name of the users in the chat room
                        clientSocket.BeginSend(message, 0, message.Length, SocketFlags.None, new AsyncCallback(OnSend), clientSocket);
                        break;
                }

                if (msgToSend.cmdCommand != Command.List)
                {
                    message = msgToSend.ToByte();

                    foreach (ClientInfo clientInfo in clientList)
                    {
                        if (clientInfo.socket != clientSocket || msgToSend.cmdCommand != Command.Login)
                        {
                            clientInfo.socket.BeginSend(message, 0, message.Length, SocketFlags.None, new AsyncCallback(OnSend), clientInfo.socket);
                        }
                    }

                    Console.WriteLine(msgToSend.strMessage); //Logging
                }

                if (msgReceived.cmdCommand != Command.Logout)
                {
                    clientSocket.BeginReceive(byteData, 0, byteData.Length, SocketFlags.None, new AsyncCallback(OnReceive), clientSocket);
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("RECEIVE ERROR: " + ex.Message);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        public static void OnSend(IAsyncResult ar)
        {
            try
            {
                Socket client = (Socket)ar.AsyncState;
                client.EndSend(ar);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("SEND ERROR: " + ex.Message);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }
}