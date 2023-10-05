namespace lr4_client
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            LoginForm loginForm = new();
            Application.Run(loginForm);

            if (loginForm.DialogResult == DialogResult.OK)
            {
                ClientForm clientForm = new()
                {
                    clientSocket = loginForm.clientSocket,
                    strName = loginForm.strName
                };

                clientForm.ShowDialog();
            }
        }
    }
}