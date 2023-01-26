using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsAppSeaBattleClient.Game
{
    internal class FieldsManager
    {
        private const int Rows = 10;
        private const int Columns = 10;

        public char[,] MyField { get; private set; }
        public char[,] ShootField { get; private set; }

        public FieldsManager()
        {
            MyField = new char[Rows, Columns];
            ShootField = new char[Rows, Columns];
        }

        public void ReadFields(PlayerFields playerFields)
        {
            int index = 0;

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    MyField[i, j] = playerFields.MyField[index];
                    ShootField[i, j] = playerFields.ShootField[index];

                    index++;
                }
            }
        }
    }
}
