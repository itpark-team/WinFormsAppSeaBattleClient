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
using WinFormsAppSeaBattleClient.NetModel;
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

        private void ReceiveAndDrawFields()
        {
            Request request = new Request()
            {
                Command = Commands.GetFields
            };
            _clientEngine.SendRequest(request);

            Response response = _clientEngine.ReceiveResponse();

            PlayerFields playerFields = JsonSerializer.Deserialize<PlayerFields>(response.JsonData);

            _fieldsManager.ReadFields(playerFields);

            _pictureboxesManager.DrawFields(_fieldsManager.MyField, _fieldsManager.ShootField);
        }

        private Response MakeShootAndGetResponse(int x, int y)
        {
            ShootCoords shootCoords = _pictureboxesManager.GetShootCoordsFromShootField(x, y);

            Request request = new Request()
            {
                Command = Commands.Shoot,
                JsonData = JsonSerializer.Serialize(shootCoords)
            };

            _clientEngine.SendRequest(request);

            return _clientEngine.ReceiveResponse();
        }

        private int ReceiveMyNumber()
        {
            Response response = _clientEngine.ReceiveResponse();

            return int.Parse(response.JsonData);
        }

        private string ReceiveGameResult()
        {
            Request request = new Request()
            {
                Command = Commands.GetGameResult
            };

            _clientEngine.SendRequest(request);

            Response response = _clientEngine.ReceiveResponse();

            return response.JsonData;
        }

        private void ExitGame()
        {
            Request request = new Request()
            {
                Command = Commands.ExitGame
            };

            _clientEngine.SendRequest(request);
        }

        public void WaitTurnOtherPlayer()
        {
            bool isWait = true;
            while (isWait)
            {
                ReceiveAndDrawFields();

                string gameResult = ReceiveGameResult();

                if (gameResult.StartsWith("Turn"))
                {
                    int currentTurn = int.Parse(gameResult.Remove(0, 4));

                    if (currentTurn == _myNumber)
                    {
                        _isMyTurn = true;

                        isWait=false;
                    }
                }
                else if (gameResult.StartsWith("Win"))
                {
                    int win = int.Parse(gameResult.Remove(0, 3));

                    _isMyTurn = false;

                    isWait = false;

                    MessageBox.Show("Вы проиграли!");
                }

                Thread.Sleep(500);
            }
        }

       

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

            _myNumber = ReceiveMyNumber();

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

            MessageBox.Show("Игроки успешно подключены");

            buttonStartGame.Enabled = true;
        }

        private void buttonStartGame_Click(object sender, EventArgs e)
        {
            ReceiveAndDrawFields();
            buttonStartGame.Enabled = false;

            if (_myNumber == 2)
            {
                Task.Run(() =>
                {
                    WaitTurnOtherPlayer();
                });
            }
        }

        private void pictureBoxShootField_MouseDown(object sender, MouseEventArgs e)
        {
            if (_isMyTurn == false)
            {
                MessageBox.Show("Дождитесь хода другого игрока");
                return;
            }

            Response response = MakeShootAndGetResponse(e.X, e.Y);

            if (response.Status == Statuses.Ok)
            {
                ReceiveAndDrawFields();
                string gameResult = ReceiveGameResult();

                if (gameResult.StartsWith("Turn"))
                {
                    int currentTurn = int.Parse(gameResult.Remove(0, 4));

                    if (currentTurn != _myNumber)
                    {
                        _isMyTurn = false;

                        Task.Run(() =>
                        {
                            WaitTurnOtherPlayer();
                        });
                    }
                } 
                else if (gameResult.StartsWith("Win"))
                {
                    int win = int.Parse(gameResult.Remove(0, 3));

                    _isMyTurn = false;

                    MessageBox.Show("Поздравляем вы победили!");
                }

            }
            else if (response.Status == Statuses.Error)
            {
                MessageBox.Show(response.JsonData);
            }

        }

        private void buttonExitGame_Click(object sender, EventArgs e)
        {
            ExitGame();
            _clientEngine.CloseClientSocket();

            Application.Exit();
        }
    }
}
