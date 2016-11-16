using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PracticalLab.UI;
using System.IO;
using System.Reflection;

namespace PracticalLab.BLL
{
    static class MainApp
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new MainForm());

            string appFolderPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            string str1 = Path.Combine(Directory.GetParent(appFolderPath).Parent.Parent.FullName, "PracticalLabTest\\Resources\\BunnyLandscape.png");
            string str = @""+str1;
            Console.WriteLine(str);
        }
    }
}
