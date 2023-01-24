﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsAppSeaBattleClient.NetProtocol
{
    internal class Request
    {
        public string Command { get; set; }
        public string JsonData { get; set; }

        public override string ToString()
        {
            return $"Command:{Command} JsonData:{JsonData}";
        }
    }
}
