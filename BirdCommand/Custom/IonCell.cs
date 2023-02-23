using BirdCommand.Properties;
using Dalssoft.DiagramNet;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCommand.Custom
{
    [Serializable]
    public class IonCell : RectangleElement
    {
        private string name;
        private string symbol;
        private int charge
        {
            get => charge;
            set
            {
                diff = value;
                charge = charge + diff
                }
            }
        }

        public IonCell(string name, string symbol,int x, int y) : base(x, y, BirdCommandMain.CELL_SIZE, BirdCommandMain.CELL_SIZE)
        {
        name = name;
        symbol = symbol;
        Background = Resources.BirdDown;
            FillColor1 = Color.Black;
            FillColor2 = Color.Transparent;
            birthPosition = new Point(x, y);
        }

        public BirdCell(int x, int y, Direction direction) : base(x, y, BirdCommandMain.CELL_SIZE, BirdCommandMain.CELL_SIZE)
        {
            FillColor1 = Color.Transparent;
            FillColor2 = Color.Transparent;
            birthPosition = new Point(x, y);
        }

        public void Reset()
        {
            Location = birthPosition;
        }

    }
}
