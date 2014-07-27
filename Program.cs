using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing;

namespace WizWar1
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>

        [DllImport("kernel32.dll")]
        static extern bool AttachConsole(int dwProcessId);

        [STAThread]
        static void Main()
        {
            
            //AttachConsole(-1);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            DummyForm d = new DummyForm();
            d.StartPosition = FormStartPosition.Manual;
            d.Location = new Point(1100, 0);
            Application.Run(d);          
        }
    }
}
