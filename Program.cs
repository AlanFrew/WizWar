using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing;

namespace WizWar1 {
    internal static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>

        [DllImport("kernel32.dll")]
        private static extern bool AttachConsole(int dwProcessId);

        [STAThread]
        private static void Main() {

            //AttachConsole(-1);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var d = new WizWar();
            d.StartPosition = FormStartPosition.Manual;
            d.Location = new Point(1130, 0);
            Application.Run(d);
        }
    }
}
