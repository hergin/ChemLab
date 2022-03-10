﻿using BirdCommand.Custom;
using Dalssoft.DiagramNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BirdCommand
{
    public partial class BirdCommandMain : Form
    {
        public static int CELL_SIZE = 50;
        BirdCell theBird;
        StartCell theStart;
        bool addBird = false, addEmpty = false;

        public BirdCommandMain()
        {
            InitializeComponent();

            theStart = new StartCell();
            designer_trafo.Document.AddElement(theStart);

            designer_trafo.ElementClick += Designer_trafo_ElementClick;
            designer_trafo.MouseUp += Designer_trafo_MouseUp;
            designer_trafo.ElementMoved += Designer_trafo_ElementMoved;
            designer_trafo.ElementMoving += Designer_trafo_ElementMoving;

            //designer_trafo.Document.AddElement(new SnapCell(0, 0));

            // LoadLevel1();
        }

        private void Designer_trafo_ElementMoved(object sender, ElementEventArgs e)
        {
            if(e.Element is RuleCell)
            {
                // TODO put the current rule right after the highlighted element and move the rest accordingly.
            }
        }

        private void Designer_trafo_ElementMoving(object sender, ElementEventArgs e)
        {
            if(e.Element is RuleCell)
            {
                // TODO highlight the closest rule that this can snap (this can be done by adding another cell, like HighlightCell)
            }
        }

        private void Designer_trafo_MouseUp(object sender, MouseEventArgs e)
        {
            if (addBird)
            {
                // TODO if there is a cell underneath, auto-center the bird on the cell.
                designer_trafo.Document.AddElement(new BirdCell(e.X, e.Y));
                addBird = false;
            }
            if (addEmpty)
            {
                // TODO if there is a cell around, auto-move this one to it.
                designer_trafo.Document.AddElement(new EmptyCell(e.X, e.Y));
                addEmpty = false;
            }
        }

        private void Designer_trafo_ElementClick(object sender, ElementEventArgs e)
        {
            if (e.Element is RuleCell)
            {
                List<BaseElement> list = FindElementsWithin(designer_trafo,e.Element);
                designer_trafo.Document.SelectElements(list.ToArray());
            }
        }

        List<BaseElement> FindElementsWithin(Designer designer, BaseElement parentElement)
        {
            List<BaseElement> result = new List<BaseElement>();
            foreach (var element in designer.Document.Elements)
            {
                BaseElement casted = element as BaseElement;
                if (casted.Location.Y >= parentElement.Location.Y && casted.Location.Y <= parentElement.Location.Y + parentElement.Size.Height
                    && casted.Location.Y + casted.Size.Height <= parentElement.Location.Y + parentElement.Size.Height
                    && casted.Location.X >= parentElement.Location.X && casted.Location.X <= parentElement.Location.X + parentElement.Size.Width
                    && casted.Location.X + casted.Size.Width <= parentElement.Location.X + parentElement.Size.Width)
                {
                    result.Add(casted);
                }
            }
            return result;
        }

        void LoadLevel1()
        {
            designer_board.Document.SelectAllElements();
            designer_board.Document.DeleteSelectedElements();

            LevelDesigner.Level1(designer_board);

            theBird = (BirdCell)designer_board.Document.Elements.GetArray().Where(e => e is BirdCell).First();
        }

        void LoadLevel2()
        {
            designer_board.Document.SelectAllElements();
            designer_board.Document.DeleteSelectedElements();

            LevelDesigner.Level2(designer_board);

            theBird = (BirdCell)designer_board.Document.Elements.GetArray().Where(e => e is BirdCell).First();
        }

        void LoadLevel3()
        {
            designer_board.Document.SelectAllElements();
            designer_board.Document.DeleteSelectedElements();

            LevelDesigner.Level3(designer_board);

            theBird = (BirdCell)designer_board.Document.Elements.GetArray().Where(e => e is BirdCell).First();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            theBird.MoveDown();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            theBird.MoveUp();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            theBird.MoveRight();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            theBird.MoveLeft();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            theBird.TurnRight();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            theBird.TurnLeft();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LoadLevel1();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LoadLevel2();
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LoadLevel3();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var highestY = 0;
            foreach (var element in designer_trafo.Document.Elements)
            {
                if(((BaseElement)element).Location.Y + ((BaseElement)element).Size.Height > highestY)
                {
                    highestY = ((BaseElement)element).Location.Y + ((BaseElement)element).Size.Height;
                }
            }
            designer_trafo.Document.AddElement(new RuleCell(21, highestY-4));
        }

        private void button9_Click(object sender, EventArgs e)
        {
            designer_trafo.Document.Action = DesignerAction.Add;
            addBird = true;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            designer_trafo.Document.Action = DesignerAction.Add;
            addEmpty = true;
        }
    }
}
