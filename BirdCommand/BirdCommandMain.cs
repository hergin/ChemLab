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

        public BirdCommandMain()
        {
            InitializeComponent();

            LoadLevel1();
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
    }
}
