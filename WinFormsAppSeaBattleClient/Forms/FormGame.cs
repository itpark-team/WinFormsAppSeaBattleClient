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
        private FieldsManager _fieldsManager;
        private PictureboxesManager _pictureboxesManager;

        public FormGame()
        {
            InitializeComponent();
        }

        private void FormGame_Load(object sender, EventArgs e)
        {
            _clientEngine = new ClientEngine("127.0.0.1", 34536);
            _fieldsManager = new FieldsManager();
            _pictureboxesManager = new PictureboxesManager(pictureBoxMyField, pictureBoxShootField);
        }

        private void buttonConnectToServer_Click(object sender, EventArgs e)
        {
            _clientEngine.ConnectToServer();
            Response response = _clientEngine.ReceiveResponse();

            int myNumber = int.Parse(response.JsonData);

            buttonConnectToServer.Enabled = false;

            if (myNumber == 1)
            {
                MessageBox.Show("Вы успешно подключились к серверу. Вы играете за 1 игрока. Ожидаем подключение 2 игрока.....");
            }
            else
            {
                MessageBox.Show($"Вы успешно подключились к серверу. Вы играете за 2 игрока");
            }

            _clientEngine.ReceiveResponse();
            buttonStartGame.Enabled = true;
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

                _fieldsManager.ReadFields(playerFields);

                _pictureboxesManager.DrawFields(_fieldsManager.MyField, _fieldsManager.ShootField);
            }
            else
            {
                MessageBox.Show("ОШИБКА!!! " + response.JsonData);
            }

            buttonStartGame.Enabled = false;
        }
    }
}
