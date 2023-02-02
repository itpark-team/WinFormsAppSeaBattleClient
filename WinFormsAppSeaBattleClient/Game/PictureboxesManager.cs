using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsAppSeaBattleClient.NetModel
{
    internal class PictureboxesManager
    {
        private PictureboxManager _pictureBoxManagerMyField;
        private PictureboxManager _pictureBoxManagerShootField;

        public PictureboxesManager(PictureBox pictureBoxMyField, PictureBox pictureBoxShootField)
        {
            _pictureBoxManagerMyField = new PictureboxManager(pictureBoxMyField);

            _pictureBoxManagerShootField = new PictureboxManager(pictureBoxShootField);
        }

        public void DrawFields(char[,] myField, char[,] shootField)
        {
            _pictureBoxManagerMyField.DrawField(myField);
            _pictureBoxManagerShootField.DrawField(shootField);
        }

        public ShootCoords GetShootCoordsFromShootField(int x, int y)
        {
            return _pictureBoxManagerShootField.GetShootCoords(x, y);
        }
    }
}
