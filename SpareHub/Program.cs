using System;
using System.Windows.Forms;

namespace SpareHub
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // PERBAIKAN: Fix untuk .NET version yang berbeda
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // PERBAIKAN: Explicit constructor call untuk fix CS0121
            Application.Run(new SistemPemesanan());
        }
    }
}