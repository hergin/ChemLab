using BirdCommand.Custom;
using BirdCommand.Properties;
using Dalssoft.DiagramNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BirdCommand
{
    // TODO when you click on the newly slide area to create something or to select, it automatically goes to top.
    // TODO when maze is done, decide what's gonna happen next (going to other maze? etc.)
    // TODO prevent the blockPanel and theStart being selected!
    // TODO get rid of all magical numbers somehow
    // TODO the blocksPanel should be taken into account while getting the rules and such from the designer.
    //             There should be one single place to get elements so we can filter them there.
    public partial class BirdCommandMain : Form
    {
        private const int TimeoutBetweenRuleExecution = 1000;
        public static int CELL_SIZE = 50;
        Point birdButtonLocation = new Point(30, 30),
            emptyCellButtonLocation = new Point(110, 30),
            ruleButtonLocation = new Point(25,120);
        BirdCell theBird;
        PigCell thePig;
        StartCell theStart;
        SnapCell theSnapCell;
        RectangleNode blockPanel;
        bool addBird = false, addEmpty = false;

        public BirdCommandMain()
        {
            InitializeComponent();

            theStart = new StartCell();
            designer_trafo.Document.AddElement(theStart);

            designer_trafo.ElementClick += Designer_trafo_ElementClick;
            designer_trafo.MouseUp += Designer_trafo_MouseUp;
            designer_trafo.MouseDown += Designer_trafo_MouseDown;
            designer_trafo.ElementMouseUp += Designer_trafo_ElementMouseUp;
            designer_trafo.ElementMoved += Designer_trafo_ElementMoved;
            designer_trafo.ElementMoving += Designer_trafo_ElementMoving;
            designer_trafo.Resize += Designer_trafo_Resize;

            toolTip1.SetToolTip(turnLeftButton, "Turn selected bird left");
            toolTip1.SetToolTip(turnRightButton, "Turn selected bird right");
            toolTip1.SetToolTip(increaseRuleCountButton, "Increase the rule count of the selected rule");
            toolTip1.SetToolTip(decreaseRuleCountButton, "Decrease the rule count of the selected rule");
            toolTip1.SetToolTip(copyLhsToRhsButton, "Copy 'Current Pattern' to 'Pattern After'");
            toolTip1.SetToolTip(resetButton, "Move the bird back to the original position in the maze");
            toolTip1.SetToolTip(startOverButton, "This will reset the puzzle to its start state and delete all the blocks you've added or changed.");
            toolTip1.SetToolTip(duplicateButton, "Duplicate the selected rule");
            toolTip1.SetToolTip(maze1button, "Open maze 1");
            toolTip1.SetToolTip(maze2button, "Open maze 2");
            toolTip1.SetToolTip(maze3button, "Open maze 3");

            designer_trafo.Document.GridSize = new System.Drawing.Size(10000, 10000);
            blockPanel = new RectangleNode(0, 0, 200, 220);
            blockPanel.FillColor1 = Color.FromArgb(228, 228, 228);
            blockPanel.FillColor2 = Color.FromArgb(228, 228, 228);
            designer_trafo.Document.AddElement(blockPanel);

            var addBirdButton = new BirdCell(birdButtonLocation.X, birdButtonLocation.Y);
            designer_trafo.Document.AddElement(addBirdButton);
            var addCellButton = new EmptyCell(emptyCellButtonLocation.X, emptyCellButtonLocation.Y);
            designer_trafo.Document.AddElement(addCellButton);
            var addRuleButtonOnCanvas = new RuleCell(ruleButtonLocation.X, ruleButtonLocation.Y, 140, 70);
            designer_trafo.Document.AddElement(addRuleButtonOnCanvas);


            theSnapCell = new SnapCell(0, 0);
            designer_trafo.Document.AddElement(theSnapCell);
            theSnapCell.Visible = false;

            trafoRunner.DoWork += TrafoRunner_DoWork;
            trafoRunner.ProgressChanged += TrafoRunner_ProgressChanged;

            // LoadLevel1();
        }

        private void Designer_trafo_Resize(object sender, EventArgs e)
        {
            blockPanel.Size = new Size(200, designer_trafo.Height);
        }

        private void Designer_trafo_MouseDown(object sender, MouseEventArgs e)
        {
            // TODO if element is not dragged to the canvas, delete the newly created one or the old one! It sure has duplication on the button area.
            if (designer_trafo.Document.FindElement(e.Location) != null
                && designer_trafo.Document.FindElement(e.Location) is BirdCell bird
                && bird.Location == birdButtonLocation)
            {
                var newBird = new BirdCell(birdButtonLocation.X, birdButtonLocation.Y);
                designer_trafo.Document.AddElement(newBird);
                designer_trafo.Document.SendToBackElement(newBird);
                designer_trafo.Document.SendToBackElement(blockPanel);
            }
            else if (designer_trafo.Document.FindElement(e.Location) != null
              && designer_trafo.Document.FindElement(e.Location) is EmptyCell empty
              && empty.Location == emptyCellButtonLocation)
            {
                var newEmptyCell = new EmptyCell(emptyCellButtonLocation.X, emptyCellButtonLocation.Y);
                designer_trafo.Document.AddElement(newEmptyCell);
                designer_trafo.Document.SendToBackElement(newEmptyCell);
                designer_trafo.Document.SendToBackElement(blockPanel);
            }
            else if (designer_trafo.Document.FindElement(e.Location) != null
              && designer_trafo.Document.FindElement(e.Location) is RuleCell rule
              && rule.Location == ruleButtonLocation)
            {
                rule.ResizeToOriginal();
                designer_trafo.Document.BringToFrontElement(rule);
                var newRule = new RuleCell(ruleButtonLocation.X, ruleButtonLocation.Y, 140, 70);
                designer_trafo.Document.AddElement(newRule);
                designer_trafo.Document.SendToBackElement(newRule);
                designer_trafo.Document.SendToBackElement(blockPanel);
            }
        }

        private void TrafoRunner_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            switch (e.ProgressPercentage)
            {
                case (int)TrafoProgress.Highlight:
                    ((RuleCell)e.UserState).Highlight();
                    break;
                case (int)TrafoProgress.Unhighlight:
                    ((RuleCell)e.UserState).Unhighlight();
                    break;
                case (int)TrafoProgress.Error:
                    MessageBox.Show(e.UserState.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case (int)TrafoProgress.Success:
                    MessageBox.Show("You caught the pig!", "Congrats", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case (int)TrafoProgress.Failure:
                    MessageBox.Show("Start again! Something's not quite right yet.", "Pig is not caught", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
            }
        }

        private void TrafoRunner_DoWork(object sender, DoWorkEventArgs e)
        {
            // TODO take empty or other scenarios into account
            var allRules = designer_trafo.Document.Elements.GetArray().Where(el => el is RuleCell && el.Location!=ruleButtonLocation).ToList();
            allRules.Sort((a, b) => { return a.Location.Y - b.Location.Y; });
            foreach (var rule in allRules)
            {
                try
                {
                    trafoRunner.ReportProgress((int)TrafoProgress.Highlight, rule);
                    var ruleType = TrafoUtil.IdentifyRuleType(
                        TrafoUtil.FindPreConditionElements(designer_trafo.Document.Elements.GetArray().ToList(),
                            rule),
                        TrafoUtil.FindPostConditionElements(designer_trafo.Document.Elements.GetArray().ToList(),
                            rule));
                    for (int i = 0; i < ((RuleCell)rule).RuleCount; i++)
                    {
                        if (TrafoUtil.DoesPatternExist(designer_board.Document.Elements.GetArray().ToList(), TrafoUtil.FindPreConditionElements(designer_trafo.Document.Elements.GetArray().ToList(),
                                rule)))
                        {
                            // TODO pattern exists for other move forward or turn rules than the one in the model, should find a solution
                            switch (ruleType)
                            {
                                case RuleType.TurnRight:
                                    theBird.TurnRight();
                                    break;
                                case RuleType.TurnLeft:
                                    theBird.TurnLeft();
                                    break;
                                case RuleType.Turn180:
                                    theBird.Turn180();
                                    break;
                                case RuleType.MoveForward:
                                    theBird.MoveForward();
                                    break;
                            }
                            Thread.Sleep(TimeoutBetweenRuleExecution);
                            if(theBird.Location.Equals(thePig.Location))
                            {
                                trafoRunner.ReportProgress((int)TrafoProgress.Success);
                                trafoRunner.CancelAsync();
                                return;
                            }
                        }
                        else
                        {
                            trafoRunner.ReportProgress((int)TrafoProgress.Error, "Pattern doesn't exist!");
                            trafoRunner.CancelAsync();
                            return;
                        }
                    }
                    trafoRunner.ReportProgress((int)TrafoProgress.Unhighlight, rule);
                }
                catch (Exception exp)
                {
                    trafoRunner.ReportProgress((int)TrafoProgress.Error, exp.Message);
                    trafoRunner.CancelAsync();
                    return;
                }
            }
            trafoRunner.ReportProgress((int)TrafoProgress.Failure);
            trafoRunner.CancelAsync();
            return;
        }

        void MoveRuleAndItsContents(RuleCell rule, int newX, int newY)
        {
            var ruleStarterPosition = rule.Location;

            var ruleContent = DesignerUtil.FindElementsWithin(designer_trafo, rule);
            foreach (var element in ruleContent)
            {
                // move the contents of the rule as well
                var differenceX = Math.Abs(element.Location.X - ruleStarterPosition.X);
                var differenceY = Math.Abs(element.Location.Y - ruleStarterPosition.Y);
                element.Location = new Point(newX + differenceX, newY + differenceY);
            }
        }

        private void Designer_trafo_ElementMouseUp(object sender, ElementMouseEventArgs e)
        {
            theSnapCell.Visible = false;
            if (e.Element is RuleCell rule)
            {
                MoveRuleAndItsContents(rule, theSnapCell.Location.X - 11, theSnapCell.Location.Y);

                // TODO Move the rest of the rules accordingly (if we put rule within two rules)
                foreach (var otherRule in designer_trafo.Document.Elements.GetArray().Where(el=>el is RuleCell && el.Location.X > 200 && el.Location.Y > rule.Location.Y))
                {
                    // this should be investigated more. Because some elements of the newly moved rule might be on top of the existing rule which will mess things up.
                }

                Designer_trafo_ElementClick(sender, e);
            }
            else if (e.Element is BirdCell bird)
            {
                // the snapping is based on mouse position, not the bird origin
                var cellUnderneath = DesignerUtil.FindCellUnderneath(designer_trafo, new Point(e.X, e.Y));
                if (cellUnderneath != null)
                    bird.Location = cellUnderneath.Location;
            }
            else if(e.Element is EmptyCell emptyCell)
            {
                var emptyUnderneathSouth = DesignerUtil.FindCellUnderneath(designer_trafo, emptyCell, new Point(e.X, e.Y+CELL_SIZE));
                if (emptyUnderneathSouth != null)
                {
                    emptyCell.Location = new Point(emptyUnderneathSouth.Location.X, emptyUnderneathSouth.Location.Y - CELL_SIZE);
                    return;
                }
                var emptyUnderneathNorth = DesignerUtil.FindCellUnderneath(designer_trafo, emptyCell, new Point(e.X, e.Y - CELL_SIZE));
                if (emptyUnderneathNorth != null)
                {
                    emptyCell.Location = new Point(emptyUnderneathNorth.Location.X, emptyUnderneathNorth.Location.Y + CELL_SIZE);
                    return;
                }
                var emptyUnderneathEast = DesignerUtil.FindCellUnderneath(designer_trafo, emptyCell, new Point(e.X - CELL_SIZE, e.Y ));
                if (emptyUnderneathEast != null)
                {
                    emptyCell.Location = new Point(emptyUnderneathEast.Location.X + CELL_SIZE, emptyUnderneathEast.Location.Y);
                    return;
                }
                var emptyUnderneathWest = DesignerUtil.FindCellUnderneath(designer_trafo, emptyCell, new Point(e.X + CELL_SIZE, e.Y));
                if (emptyUnderneathWest != null)
                {
                    emptyCell.Location = new Point(emptyUnderneathWest.Location.X - CELL_SIZE, emptyUnderneathWest.Location.Y);
                    return;
                }
            }
        }

        private void Designer_trafo_ElementMoved(object sender, ElementEventArgs e)
        {
            
        }

        private void Designer_trafo_ElementMoving(object sender, ElementEventArgs e)
        {
            if(e.Element is RuleCell)
            {
                var possibleElements = designer_trafo.Document.Elements.GetArray().Where(el => (el is RuleCell || el is StartCell)
                    && el.Location != e.Element.Location
                    && el.Location != ruleButtonLocation);

                var smallest = int.MaxValue;
                BaseElement closestElement = null;
                foreach (var element in possibleElements)
                {
                    if( Math.Abs(element.Location.Y+element.Size.Height - e.Element.Location.Y) < smallest)
                    {
                        smallest = Math.Abs(element.Location.Y + element.Size.Height - e.Element.Location.Y);
                        closestElement = element;
                    }
                }
                theSnapCell.Location = new Point(closestElement.Location.X+11, closestElement.Location.Y + closestElement.Size.Height-5);
                theSnapCell.Visible = true;
                designer_trafo.Document.BringToFrontElement(theSnapCell);
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
            if (e.Element is RuleCell rule)
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
            thePig= (PigCell)designer_board.Document.Elements.GetArray().Where(e => e is PigCell).First();
        }

        void LoadLevel2()
        {
            designer_board.Document.SelectAllElements();
            designer_board.Document.DeleteSelectedElements();

            LevelDesigner.Level2(designer_board);

            theBird = (BirdCell)designer_board.Document.Elements.GetArray().Where(e => e is BirdCell).First();
            thePig = (PigCell)designer_board.Document.Elements.GetArray().Where(e => e is PigCell).First();
        }

        void LoadLevel3()
        {
            designer_board.Document.SelectAllElements();
            designer_board.Document.DeleteSelectedElements();

            LevelDesigner.Level3(designer_board);

            theBird = (BirdCell)designer_board.Document.Elements.GetArray().Where(e => e is BirdCell).First();
            thePig = (PigCell)designer_board.Document.Elements.GetArray().Where(e => e is PigCell).First();
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

        private void button12_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Result: "+
                TrafoUtil.DoesPatternExist(
                    designer_board.Document.Elements.GetArray().ToList(),
                    TrafoUtil.FindPreConditionElements(
                        designer_trafo.Document.Elements.GetArray().ToList(),
                        (RuleCell)designer_trafo.Document.Elements.GetArray().Where(s=>s is RuleCell).First())));
        }

        private void button14_Click(object sender, EventArgs e)
        {
            var ruleType = TrafoUtil.IdentifyRuleType(
                TrafoUtil.FindPreConditionElements(designer_trafo.Document.Elements.GetArray().ToList(),
                    (RuleCell)designer_trafo.Document.SelectedElements.GetArray().Where(el => el is RuleCell).First()),
                TrafoUtil.FindPostConditionElements(designer_trafo.Document.Elements.GetArray().ToList(),
                    (RuleCell)designer_trafo.Document.SelectedElements.GetArray().Where(el => el is RuleCell).First()));
            MessageBox.Show("RuleType: " + ruleType);
        }

        private void toAddBirdButton_Click(object sender, EventArgs e)
        {
            designer_trafo.Document.Action = DesignerAction.Add;
            addBird = true;
        }

        private void toAddEmptyButton_Click(object sender, EventArgs e)
        {
            designer_trafo.Document.Action = DesignerAction.Add;
            addEmpty = true;
        }

        private void addRuleButton_Click(object sender, EventArgs e)
        {
            AddRuleToNextEmptySpot();
        }

        private RuleCell AddRuleToNextEmptySpot()
        {
            var highestY = 0;
            foreach (var element in designer_trafo.Document.Elements)
            {
                if ((element is RuleCell || element is StartCell) && ((BaseElement)element).Location != ruleButtonLocation)
                {
                    if (((BaseElement)element).Location.Y + ((BaseElement)element).Size.Height > highestY)
                    {
                        highestY = ((BaseElement)element).Location.Y + ((BaseElement)element).Size.Height;
                    }
                }
            }
            var newRule = new RuleCell(231, highestY - 5);
            designer_trafo.Document.AddElement(newRule);
            DesignerUtil.ArrangeTheOrder(designer_trafo);
            return newRule;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            debugPanel.Visible = !debugPanel.Visible;
        }

        private void turnRightButton_Click(object sender, EventArgs e)
        {
            if (designer_trafo.Document.SelectedElements.Count == 1
                && designer_trafo.Document.SelectedElements[0] is BirdCell bird)
            {
                bird.TurnRight();
            }
            else
            {
                MessageBox.Show("Please select a bird first to turn right.", "No bird selected!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void turnLeftButton_Click(object sender, EventArgs e)
        {
            if (designer_trafo.Document.SelectedElements.Count == 1
                && designer_trafo.Document.SelectedElements[0] is BirdCell bird)
            {
                bird.TurnLeft();
            }
            else
            {
                MessageBox.Show("Please select a bird first to turn left.", "No bird selected!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void increaseRuleCountButton_Click_1(object sender, EventArgs e)
        {
            if (designer_trafo.Document.SelectedElements.GetArray().Where(el => el is RuleCell).Count() >= 1)
            {
                ((RuleCell)designer_trafo.Document.SelectedElements.GetArray().Where(el => el is RuleCell).First()).IncreaseRuleCount();
            }
            else
            {
                MessageBox.Show("Please select a rule first to increase its rule count.", "No rule selected!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void decreaseRuleCountButton_Click_1(object sender, EventArgs e)
        {
            if (designer_trafo.Document.SelectedElements.GetArray().Where(el => el is RuleCell).Count() >= 1)
            {
                ((RuleCell)designer_trafo.Document.SelectedElements.GetArray().Where(el => el is RuleCell).First()).DecreaseRuleCount();
            }
            else
            {
                MessageBox.Show("Please select a rule first to decrease its rule count.", "No rule selected!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void copyLhsToRhsButton_Click_1(object sender, EventArgs e)
        {
            if (designer_trafo.Document.SelectedElements.GetArray().Where(el => el is RuleCell).Count() >= 1)
            {
                CopyLHStoRHS((RuleCell)designer_trafo.Document.SelectedElements.GetArray().Where(el => el is RuleCell).First());
            }
            else
            {
                MessageBox.Show("Please select a rule first to copy its 'current pattern' to its 'pattern after'.", "No rule selected!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void CopyLHStoRHS(RuleCell rule)
        {
            var lhsElements = TrafoUtil.FindPreConditionElements(designer_trafo.Document.Elements.GetArray().ToList(),
                                rule);

            foreach (var element in lhsElements)
            {
                if (element is EmptyCell empty)
                {
                    designer_trafo.Document.AddElement(new EmptyCell(empty.Location.X + 200, empty.Location.Y));
                }
                else if (element is BirdCell bird)
                {
                    designer_trafo.Document.AddElement(new BirdCell(bird.Location.X + 200, bird.Location.Y, bird.Direction));
                }
            }
        }

        private void startOverButton_MouseEnter(object sender, EventArgs e)
        {
            startOverButton.BackgroundImage = Resources.start_over_button_over;
        }

        private void startOverButton_MouseLeave(object sender, EventArgs e)
        {
            startOverButton.BackgroundImage = Resources.start_over_button;
        }

        private void startOverButton_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("This will reset the puzzle to its start state and delete all the blocks you've added or changed.", "Are you sure you want to start over?", MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                startOver();
            }
        }

        private void startOver()
        {
            Reset();
            foreach (var element in designer_trafo.Document.Elements.GetArray().Where(el => !(el is StartCell || el is SnapCell || el.Location.X < 200)))
            {
                designer_trafo.Document.DeleteElement(element);
            }
            designer_trafo.Document.ClearSelection();
        }

        private void runButton_MouseEnter(object sender, EventArgs e)
        {
            runButton.BackgroundImage = Resources.run_button_over;
        }

        private void runButton_MouseLeave(object sender, EventArgs e)
        {
            runButton.BackgroundImage = Resources.run_button;
        }

        private void resetButton_MouseEnter(object sender, EventArgs e)
        {
            resetButton.BackgroundImage = Resources.reset_button_over;
        }

        private void resetButton_MouseLeave(object sender, EventArgs e)
        {
            resetButton.BackgroundImage = Resources.reset_button;
        }

        private void runButton_Click(object sender, EventArgs e)
        {
            theStart.Highlight();
            trafoRunner.RunWorkerAsync();
        }

        private void resetButton_Click_1(object sender, EventArgs e)
        {
            Reset();
        }

        void Reset()
        {
            theStart.Unhighlight();
            theBird?.Reset();
            foreach (var rule in designer_trafo.Document.Elements.GetArray().Where(el => el is RuleCell))
            {
                ((RuleCell)rule).Unhighlight();
            }
        }

        private void maze1button_MouseEnter(object sender, EventArgs e)
        {
            maze1button.BackgroundImage = Resources.maze1_over;
        }

        private void maze1button_MouseLeave(object sender, EventArgs e)
        {
            maze1button.BackgroundImage = Resources.maze1;
        }

        private void maze2button_MouseEnter(object sender, EventArgs e)
        {
            maze2button.BackgroundImage = Resources.maze2_over;
        }

        private void maze2button_MouseLeave(object sender, EventArgs e)
        {
            maze2button.BackgroundImage = Resources.maze2;
        }

        private void maze3button_MouseEnter(object sender, EventArgs e)
        {
            maze3button.BackgroundImage = Resources.maze3_over;
        }

        private void maze3button_MouseLeave(object sender, EventArgs e)
        {
            maze3button.BackgroundImage = Resources.maze3;
        }

        private void maze1button_Click(object sender, EventArgs e)
        {
            startOver();
            LoadLevel1();
        }

        private void duplicateButton_Click(object sender, EventArgs e)
        {
            if (designer_trafo.Document.SelectedElements.GetArray().Where(el => el is RuleCell).Count() >= 1)
            {
                // TODO duplicate
            }
            else
            {
                MessageBox.Show("Please select a rule first to duplicate.", "No rule selected!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void maze2button_Click(object sender, EventArgs e)
        {
            startOver();
            LoadLevel2();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            RuleCell firstRule = AddRuleToNextEmptySpot();
            firstRule.IncreaseRuleCount();
            designer_trafo.Document.AddElement(new EmptyCell(firstRule.Location.X + 50, firstRule.Location.Y + 50));
            designer_trafo.Document.AddElement(new EmptyCell(firstRule.Location.X + 100, firstRule.Location.Y + 50));
            designer_trafo.Document.AddElement(new BirdCell(firstRule.Location.X + 50, firstRule.Location.Y + 50,Direction.Right));
            designer_trafo.Document.AddElement(new EmptyCell(firstRule.Location.X + 250, firstRule.Location.Y + 50));
            designer_trafo.Document.AddElement(new EmptyCell(firstRule.Location.X + 300, firstRule.Location.Y + 50));
            designer_trafo.Document.AddElement(new BirdCell(firstRule.Location.X + 300, firstRule.Location.Y + 50, Direction.Right));

            RuleCell secondRule = AddRuleToNextEmptySpot();
            designer_trafo.Document.AddElement(new EmptyCell(secondRule.Location.X + 50, secondRule.Location.Y + 50));
            designer_trafo.Document.AddElement(new BirdCell(secondRule.Location.X + 50, secondRule.Location.Y + 50, Direction.Right));
            designer_trafo.Document.AddElement(new EmptyCell(secondRule.Location.X + 250, secondRule.Location.Y + 50));
            designer_trafo.Document.AddElement(new BirdCell(secondRule.Location.X + 250, secondRule.Location.Y + 50, Direction.Down));

            RuleCell thirdRule = AddRuleToNextEmptySpot();
            designer_trafo.Document.AddElement(new EmptyCell(thirdRule.Location.X + 50, thirdRule.Location.Y + 50));
            designer_trafo.Document.AddElement(new EmptyCell(thirdRule.Location.X + 50, thirdRule.Location.Y + 100));
            designer_trafo.Document.AddElement(new BirdCell(thirdRule.Location.X + 50, thirdRule.Location.Y + 50, Direction.Down));
            designer_trafo.Document.AddElement(new EmptyCell(thirdRule.Location.X + 250, thirdRule.Location.Y + 50));
            designer_trafo.Document.AddElement(new EmptyCell(thirdRule.Location.X + 250, thirdRule.Location.Y + 100));
            designer_trafo.Document.AddElement(new BirdCell(thirdRule.Location.X + 250, thirdRule.Location.Y + 100, Direction.Down));
        }

        private void maze3button_Click(object sender, EventArgs e)
        {
            startOver();
            LoadLevel3();
        }
    }
}
