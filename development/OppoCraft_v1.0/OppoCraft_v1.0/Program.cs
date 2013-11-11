using System;
using System.Windows.Forms;

namespace OppoCraft
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 
        public static NetworkModule network;

        static void Main(string[] args)
        {
            /*
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new StartForm());
            /**/
            NetworkModule net = new NetworkModule("127.0.0.1");
            while (net.buffer.Count == 0) ;//wait for client id
            using (Game1 game = new Game1(net))
            {
                game.Run();
            }
             /**/
        }
    }
#endif
}

