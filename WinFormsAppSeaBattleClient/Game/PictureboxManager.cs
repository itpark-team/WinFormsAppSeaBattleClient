using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsAppSeaBattleClient.Game
{
    internal class PictureboxManager
    {
        private Bitmap _bitmap;
        private Graphics _graphics;
        private PictureBox _pictureBox;

        private int _cellWidth;
        private int _cellHeight;

        public PictureboxManager(PictureBox pictureBox)
        {
            _pictureBox = pictureBox;
            _bitmap = new Bitmap(pictureBox.Width, pictureBox.Height);
            _graphics = Graphics.FromImage(_bitmap);

            _cellWidth = _cellHeight = 25;
        }

        public void DrawField(char[,] field)
        {
            _graphics.Clear(Color.White);

            Pen blackPen = new Pen(Color.Black, 1);

            _graphics.DrawRectangle(blackPen, 0, 0, _bitmap.Width - 1, _bitmap.Height - 1);

            for (int i = 0; i < field.GetLength(0); i++)
            {
                _graphics.DrawLine(blackPen, 0, i * _cellHeight, _bitmap.Width, i * _cellHeight);
            }

            for (int j = 0; j < field.GetLength(1); j++)
            {
                _graphics.DrawLine(blackPen, j * _cellWidth, 0, j * _cellWidth, _bitmap.Height);
            }

            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    int x = j * _cellWidth;
                    int y = i * _cellHeight;

                    _graphics.DrawString(field[i, j].ToString(), SystemFonts.DefaultFont, Brushes.Black, x + 5, y + 5);
                }
            }

            _pictureBox.Image = _bitmap;
        }

        public ShootCoords GetShootCoords(int x, int y)
        {
            return new ShootCoords()
            {
                I = y / _cellHeight,
                J = x / _cellWidth
            };
        }
    }
}
