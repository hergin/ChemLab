using BirdCommand.Custom;
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
            designer_trafo.ElementMouseUp += Designer_trafo_ElementMouseUp;
            designer_trafo.ElementMoved += Designer_trafo_ElementMoved;
            designer_trafo.ElementMoving += Designer_trafo_ElementMoving;

            //designer_trafo.Document.AddElement(new SnapCell(0, 0));

            // LoadLevel1();
        }

        private void Designer_trafo_ElementMouseUp(object sender, ElementMouseEventArgs e)
        {
            if (e.Element is RuleCell rule)
            {
                // TODO put the current rule right after the highlighted element and move the rest accordingly.
            }
            else if (e.Element is BirdCell bird)
            {
                var cellUnderneath = DesignerUtil.FindCellUnderneath(designer_trafo, bird);
                if (cellUnderneath != null)
                    bird.Location = cellUnderneath.Location;
            }
        }

        private void Designer_trafo_ElementMoved(object sender, ElementEventArgs e)
        {
            
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
                // Add the bird and align it with the empty cell underneath (if there is one)
                var bird = new BirdCell(e.X, e.Y);
                var cellUnderneath = DesignerUtil.FindCellUnderneath(designer_trafo, bird);
                if (cellUnderneath != null)
                    bird.Location = cellUnderneath.Location;
                designer_trafo.Document.AddElement(bird);
                addBird = false;
            }
            if (addEmpty)
            {
                // if there is a cell around, snap this one to it.
                var empty = new EmptyCell(e.X, e.Y);
                var newPosition = DesignerUtil.FindPositionOfAnotherEmptyAround(designer_trafo, empty);
                empty.Location = newPosition;
                designer_trafo.Document.AddElement(empty);
                addEmpty = false;
            }
            DesignerUtil.ArrangeTheOrder(designer_trafo);
        }

        

        private void Designer_trafo_ElementClick(object sender, ElementEventArgs e)
        {
            if (e.Element is RuleCell)
            {
                List<BaseElement> list = DesignerUtil.FindElementsWithin(designer_trafo,e.Element);
                designer_trafo.Document.SelectElements(list.ToArray());
            }
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
                if (element is RuleCell || element is StartCell)
                {
                    if (((BaseElement)element).Location.Y + ((BaseElement)element).Size.Height > highestY)
                    {
                        highestY = ((BaseElement)element).Location.Y + ((BaseElement)element).Size.Height;
                    }
                }
            }
            designer_trafo.Document.AddElement(new RuleCell(21, highestY-4));
            DesignerUtil.ArrangeTheOrder(designer_trafo);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            designer_trafo.Document.Action = DesignerAction.Add;
            addBird = true;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if(designer_trafo.Document.SelectedElements.Count == 1
                && designer_trafo.Document.SelectedElements[0] is BirdCell bird)
            {
                bird.TurnRight();
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (designer_trafo.Document.SelectedElements.Count == 1
                && designer_trafo.Document.SelectedElements[0] is BirdCell bird)
            {
                bird.TurnLeft();
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Result: "+
                TrafoUtil.DoesPatternExist(
                    designer_board.Document.Elements.GetArray().ToList(),
                    DesignerUtil.FindElementsWithin(
                        designer_trafo,
                        designer_trafo.Document.Elements.GetArray().Where(s=>s is RuleCell).First())));
        }

        private void button8_Click(object sender, EventArgs e)
        {
            designer_trafo.Document.Action = DesignerAction.Add;
            addEmpty = true;
        }
    }
}
