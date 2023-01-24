using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsAppSeaBattleClient.Game;
using WinFormsAppSeaBattleClient.Net;
using WinFormsAppSeaBattleClient.NetProtocol;

namespace WinFormsAppSeaBattleClient.Forms
{
    public partial class FormGame : Form
    {
        private ClientEngine _clientEngine;

        public FormGame()
        {
            InitializeComponent();
        }

        private void FormGame_Load(object sender, EventArgs e)
        {
            _clientEngine = new ClientEngine("127.0.0.1", 34536);
        }

        private void buttonConnectToServer_Click(object sender, EventArgs e)
        {
            _clientEngine.ConnectToServer();

            buttonConnectToServer.Enabled = false;

            MessageBox.Show("Вы успешно подключились к серверу");
        }

        private void buttonStartGame_Click(object sender, EventArgs e)
        {
            Request request = new Request()
            {
                Command = Commands.GetFields
            };
            _clientEngine.SendRequest(request);

            Response response = _clientEngine.ReceiveResponse();

            if (response.Status == Statuses.Ok)
            {
                PlayerFields playerFields = JsonSerializer.Deserialize<PlayerFields>(response.JsonData);

                int a = 5;
            }
            else
            {
                MessageBox.Show("ОШИБКА!!! " + response.JsonData);
            }
        }
    }
}
