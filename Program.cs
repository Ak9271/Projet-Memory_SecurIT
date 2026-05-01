using System;
using System.Windows.Forms;
using MemorySecurIT.Forms;

namespace MemorySecurIT
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MenuForm());
        }
    }
}