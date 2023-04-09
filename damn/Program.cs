using damn.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace damn
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Global.LoadAll<Role>();
            Global.LoadAll<User>();
            Global.LoadAll<MasterService>();
            Global.LoadAll<Service>();
            Global.LoadAll<Record>();

            Application.Run(new FormAuth());
        }
    }
}
