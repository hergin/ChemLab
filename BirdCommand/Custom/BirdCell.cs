﻿using BirdCommand.Properties;
using Dalssoft.DiagramNet;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCommand.Custom
{
    public enum Direction
    {
        Up, Down, Right, Left
    }
    [Serializable]
    public class BirdCell : RectangleElement
    {
        Direction Direction;
        public BirdCell(int x, int y) : base(x, y, BirdCommandMain.CELL_SIZE, BirdCommandMain.CELL_SIZE)
        {
            Background = Resources.BirdDown;
            Direction = Direction.Down;
            FillColor1 = Color.Transparent;
            FillColor2 = Color.Transparent;
        }

        public BirdCell(int x, int y, Direction direction) : base(x, y, BirdCommandMain.CELL_SIZE, BirdCommandMain.CELL_SIZE)
        {
            Direction = direction;
            FillColor1 = Color.Transparent;
            FillColor2 = Color.Transparent;
            switch(direction)
            {
                case Direction.Up:
                    Background = Resources.BirdUp;
                    break;
                case Direction.Down:
                    Background = Resources.BirdDown;
                    break;
                case Direction.Left:
                    Background = Resources.BirdLeft;
                    break;
                case Direction.Right:
                    Background = Resources.BirdRight;
                    break;
            }
        }

        public void TurnRight()
        {
            if (Direction.Equals(Direction.Down))
            {
                Background = Resources.BirdLeft;
                Direction = Direction.Left;
            }
            else if (Direction.Equals(Direction.Left))
            {
                Background = Resources.BirdUp;
                Direction = Direction.Up;
            }
            else if (Direction.Equals(Direction.Up))
            {
                Background = Resources.BirdRight;
                Direction = Direction.Right;
            }
            else if (Direction.Equals(Direction.Right))
            {
                Background = Resources.BirdDown;
                Direction = Direction.Down;
            }
        }

        public void TurnLeft()
        {
            if (Direction.Equals(Direction.Down))
            {
                Background = Resources.BirdRight;
                Direction = Direction.Right;
            }
            else if (Direction.Equals(Direction.Left))
            {
                Background = Resources.BirdDown;
                Direction = Direction.Down;
            }
            else if (Direction.Equals(Direction.Up))
            {
                Background = Resources.BirdLeft;
                Direction = Direction.Left;
            }
            else if (Direction.Equals(Direction.Right))
            {
                Background = Resources.BirdUp;
                Direction = Direction.Up;
            }
        }

        public void MoveDown()
        {
            Location = new Point(location.X, location.Y + BirdCommandMain.CELL_SIZE);
        }

        public void MoveUp()
        {
            Location = new Point(location.X, location.Y - BirdCommandMain.CELL_SIZE);
        }

        public void MoveRight()
        {
            Location = new Point(location.X + BirdCommandMain.CELL_SIZE, location.Y);
        }

        public void MoveLeft()
        {
            Location = new Point(location.X - BirdCommandMain.CELL_SIZE, location.Y);
        }
    }
}
