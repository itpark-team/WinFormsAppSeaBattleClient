using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsAppSeaBattleClient.Utils
{
    internal class LogUtils
    {
        public static void Log(string msg)
        {
            Debug.WriteLine($"LOG: {DateTime.Now} --- {msg}");
        }
    }
}
