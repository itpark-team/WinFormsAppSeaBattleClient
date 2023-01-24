using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WinFormsAppSeaBattleClient.NetProtocol;
using WinFormsAppSeaBattleClient.Utils;

namespace WinFormsAppSeaBattleClient.Net
{
    internal class ClientEngine
    {
        private Socket _clientSocket;
        private IPEndPoint _ipEndPoint;

        public ClientEngine(string ip, int port)
        {
            _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _ipEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
        }

        public void ConnectToServer()
        {
            _clientSocket.Connect(_ipEndPoint);

            LogUtils.Log($"CONNECTED TO SERVER {_clientSocket.RemoteEndPoint}");
        }

        public Response ReceiveResponse()
        {
            string messageFromServer = SocketUtils.ReceiveMessage(_clientSocket);

            Response response = JsonSerializer.Deserialize<Response>(messageFromServer);

            LogUtils.Log($"MESSAGE FROM SERVER RECEIVED: {response}");

            return response;
        }

        public void SendRequest(Request request)
        {
            string messageToServer = JsonSerializer.Serialize(request);

            SocketUtils.SendMessage(_clientSocket, messageToServer);

            LogUtils.Log($"MESSAGE TO SERVER SENT: {request}");
        }


    }
}
