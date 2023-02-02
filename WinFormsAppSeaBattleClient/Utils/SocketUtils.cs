﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsAppSeaBattleClient.Utils
{
    internal class SocketUtils
    {
        public static void SendMessage(Socket clientSocket, string messageToServer)
        {
            byte[] outputBytes = Encoding.Unicode.GetBytes(messageToServer);
            clientSocket.Send(outputBytes);
        }

        public static string ReceiveMessage(Socket clientSocket)
        {
            StringBuilder messageBuilder = new StringBuilder();
            do
            {
                byte[] inputBytes = new byte[1024];
                int countBytes = clientSocket.Receive(inputBytes);
                messageBuilder.Append(Encoding.Unicode.GetString(inputBytes, 0, countBytes));
            } while (clientSocket.Available > 0);

            string messageFromServer = messageBuilder.ToString();

            return messageFromServer;
        }

    }
}
