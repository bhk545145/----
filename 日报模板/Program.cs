using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;



namespace 日报模板
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            bool bCreatedNew;
            Mutex m = new Mutex(false, "myUniqueName", out bCreatedNew);
            if (bCreatedNew)
            {
                Application.Run(new logon());
                if (logon.ConnectionString == "" || logon.ConnectionString == null)
                {

                }
                else
                {
                    Application.Run(new mainforms());
                }
                
            }

        }
    }
}
