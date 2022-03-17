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
    public enum BlockType
    {
        Glass, HalfStone, HalfWood, Stone, TNT, Wood
    }

    [Serializable]
    public class BlockCell : RectangleElement
    {
        private BlockType type;
        public BlockType BlockType { get => type; }

        public BlockCell(int x, int y) : base(x, y, BirdCommandMain.CELL_SIZE, BirdCommandMain.CELL_SIZE)
        {
            Background = Resources.Wood;
        }

        public BlockCell(int x, int y, BlockType type) : base(x, y, BirdCommandMain.CELL_SIZE, BirdCommandMain.CELL_SIZE)
        {
            this.type = type;
            switch (type)
            {
                case BlockType.Glass:
                    Background = Resources.Glass;
                    break;
                case BlockType.HalfStone:
                    Background = Resources.HalfStone;
                    break;
                case BlockType.HalfWood:
                    Background = Resources.HalfWood;
                    break;
                case BlockType.Stone:
                    Background = Resources.Stone;
                    break;
                case BlockType.TNT:
                    Background = Resources.TNT;
                    break;
                case BlockType.Wood:
                    Background = Resources.Wood;
                    break;
            }
        }
    }
}
