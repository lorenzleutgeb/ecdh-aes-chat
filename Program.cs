using System;
using System.Windows.Forms;
using System.Threading;

namespace LeutgebAes
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "AES";
            Thread gui = new Thread(new ThreadStart(delegate
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new ToolForm());
            }));
            gui.SetApartmentState(ApartmentState.STA);
            gui.Start();
        }
    }
}
