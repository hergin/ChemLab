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
    public enum Direction
    {
        Up, Right, Down, Left
    }
    [Serializable]
    public class BirdCell : RectangleElement
    {
        Point birthPosition;
        Direction birthDirection;

        private Direction direction;
        public Direction Direction { get => direction; }

        public BirdCell(int x, int y) : base(x, y, BirdCommandMain.CELL_SIZE, BirdCommandMain.CELL_SIZE)
        {
            Background = Resources.BirdDown;
            direction = Direction.Down;
            birthDirection = Direction.Down;
            FillColor1 = Color.Transparent;
            FillColor2 = Color.Transparent;
            birthPosition = new Point(x, y);
        }

        public BirdCell(int x, int y, Direction direction) : base(x, y, BirdCommandMain.CELL_SIZE, BirdCommandMain.CELL_SIZE)
        {
            this.direction = direction;
            birthDirection = direction;
            FillColor1 = Color.Transparent;
            FillColor2 = Color.Transparent;
            birthPosition = new Point(x, y);
            switch (direction)
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

        public void Reset()
        {
            direction = birthDirection;
            Location = birthPosition;
            switch (direction)
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

        public void Turn180()
        {
            if (Direction.Equals(Direction.Down))
            {
                Background = Resources.BirdUp;
                direction = Direction.Up;
            }
            else if (Direction.Equals(Direction.Left))
            {
                Background = Resources.BirdRight;
                direction = Direction.Right;
            }
            else if (Direction.Equals(Direction.Up))
            {
                Background = Resources.BirdDown;
                direction = Direction.Down;
            }
            else if (Direction.Equals(Direction.Right))
            {
                Background = Resources.BirdLeft;
                direction = Direction.Left;
            }
        }

        public void TurnRight()
        {
            if (Direction.Equals(Direction.Down))
            {
                Background = Resources.BirdLeft;
                direction = Direction.Left;
            }
            else if (Direction.Equals(Direction.Left))
            {
                Background = Resources.BirdUp;
                direction = Direction.Up;
            }
            else if (Direction.Equals(Direction.Up))
            {
                Background = Resources.BirdRight;
                direction = Direction.Right;
            }
            else if (Direction.Equals(Direction.Right))
            {
                Background = Resources.BirdDown;
                direction = Direction.Down;
            }
        }

        public void TurnLeft()
        {
            if (Direction.Equals(Direction.Down))
            {
                Background = Resources.BirdRight;
                direction = Direction.Right;
            }
            else if (Direction.Equals(Direction.Left))
            {
                Background = Resources.BirdDown;
                direction = Direction.Down;
            }
            else if (Direction.Equals(Direction.Up))
            {
                Background = Resources.BirdLeft;
                direction = Direction.Left;
            }
            else if (Direction.Equals(Direction.Right))
            {
                Background = Resources.BirdUp;
                direction = Direction.Up;
            }
        }

        public void MoveForward()
        {
            if (Direction.Equals(Direction.Down))
            {
                MoveDown();
            }
            else if (Direction.Equals(Direction.Left))
            {
                MoveLeft();
            }
            else if (Direction.Equals(Direction.Up))
            {
                MoveUp();
            }
            else if (Direction.Equals(Direction.Right))
            {
                MoveRight();
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
