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
        private int _myNumber;
        private bool _isMyTurn;

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

            _myNumber = int.Parse(response.JsonData);

            buttonConnectToServer.Enabled = false;

            if (_myNumber == 1)
            {
                MessageBox.Show("Вы успешно подключились к серверу. Вы играете за 1 игрока. Ожидаем подключение 2 игрока.....");
                _isMyTurn = true;
            }
            else
            {
                MessageBox.Show($"Вы успешно подключились к серверу. Вы играете за 2 игрока");
                _isMyTurn = false;
            }

            labelMyNumber.Text = $"Вы играете за {_myNumber} игрока";

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

        private void pictureBoxShootField_MouseDown(object sender, MouseEventArgs e)
        {
            if (_isMyTurn == false)
            {
                MessageBox.Show("Дождитесь хода другого игрока");
                return;
            }

            ShootCoords shootCoords = _pictureboxesManager.GetShootCoordsFromShootField(e.X, e.Y);

            Request request = new Request()
            {
                Command = Commands.Shoot,
                JsonData = JsonSerializer.Serialize(shootCoords)
            };

            _clientEngine.SendRequest(request);
            Response response = _clientEngine.ReceiveResponse();

            if (response.Status == Statuses.Ok)
            {
                request = new Request()
                {
                    Command = Commands.GetFields
                };

                _clientEngine.SendRequest(request);
                response = _clientEngine.ReceiveResponse();

                PlayerFields playerFields = JsonSerializer.Deserialize<PlayerFields>(response.JsonData);

                _fieldsManager.ReadFields(playerFields);

                _pictureboxesManager.DrawFields(_fieldsManager.MyField, _fieldsManager.ShootField);

            }
            else if(response.Status == Statuses.Error)
            {
                MessageBox.Show(response.JsonData);
            }

        }
    }
}
