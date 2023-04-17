using BirdCommand.Custom;
using BirdCommand.Model;
using BirdCommand.Properties;
using Dalssoft.DiagramNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace BirdCommand
{
    // TODO when you click on the newly slide area to create something or to select, it automatically goes to top.
    // TODO when maze is done, decide what's gonna happen next (going to other maze? etc.)
    // TODO get rid of all magical numbers somehow
    // TODO don't allow patterns with disjoint empty cells unless there are diagonal links (such as just a diagonal, see below) 
    //                                                             E
    //                                                              E
    // TODO allow only patterns with same number of empty cells FOR NOW
    // TODO solve the first one by using the whole pattern and it looks like failing sometime.
    public partial class BirdCommandMain : Form
    {
        string currentLab = "1";
        private const int TimeoutBetweenRuleExecution = 250;
        public static int CELL_SIZE = 50;
        Point ion1Location = new Point(70, 10),
            ion2Location = new Point(70, 75),
            pigButtonLocation = new Point(70, 120),
            ruleButtonLocation = new Point(25,220);
        BirdCell theBird;
        PigCell thePig;
        StartCell theStart;
        SnapCell theSnapCell;
        TrashCell theTrashCell;
        RectangleNode blockPanel;

        public BirdCommandMain()
        {
            InitializeComponent();

            theStart = new StartCell();
            designer_trafo.Document.AddElement(theStart);

            designer_trafo.ElementClick += Designer_trafo_ElementClick;
            designer_trafo.MouseDown += Designer_trafo_MouseDown;
            designer_trafo.ElementMouseUp += Designer_trafo_ElementMouseUp;
            designer_trafo.ElementMoving += Designer_trafo_ElementMoving;
            designer_trafo.ElementMouseDown += Designer_trafo_ElementMouseDown;
            designer_trafo.Resize += Designer_trafo_Resize;
            designer_trafo.MouseMove += Designer_trafo_MouseMove;
            designer_trafo.MouseDoubleClick += Designer_trafo_MouseDoubleClick;

            toolTip1.SetToolTip(increaseRuleCountButton, "Increase the rule count of the selected rule");
            toolTip1.SetToolTip(decreaseRuleCountButton, "Decrease the rule count of the selected rule");
            toolTip1.SetToolTip(copyLhsToRhsButton, "Copy 'Current Pattern' to 'Pattern After'");
            toolTip1.SetToolTip(resetButton, "Reset the tranformation");
            toolTip1.SetToolTip(startOverButton, "This will reset the lab to its start state and delete all the blocks you've added or changed.");
            toolTip1.SetToolTip(duplicateButton, "Duplicate the selected rule");
            toolTip1.SetToolTip(maze1button, "Open maze 1");
            toolTip1.SetToolTip(maze2button, "Open maze 2");
            toolTip1.SetToolTip(maze3button, "Open maze 3");
            toolTip1.SetToolTip(maze9button, "Open maze 9");

            designer_trafo.Document.GridSize = new System.Drawing.Size(10000, 10000);
            blockPanel = new RectangleNode(0, 0, 200, 220);
            blockPanel.FillColor1 = Color.FromArgb(228, 228, 228);
            blockPanel.FillColor2 = Color.FromArgb(228, 228, 228);
            designer_trafo.Document.AddElement(blockPanel);

            var addRuleButtonOnCanvas = new RuleCell(ruleButtonLocation.X, ruleButtonLocation.Y, 140, 70);
            designer_trafo.Document.AddElement(addRuleButtonOnCanvas);

            var ion1 =new Sodium();
            var ionList1 = new List<Ion>() { ion1};
            var ioncell = new IonCell(ion1Location.X, ion1Location.Y,ionList1);

            designer_trafo.Document.AddElement(ioncell);

            var ion2 = new Chlorine();
            var ionList2 = new List<Ion>() {  ion2};
            var ioncell2 = new IonCell(ion2Location.X, ion2Location.Y,ionList2);

            designer_trafo.Document.AddElement(ioncell2);


            theTrashCell = new TrashCell();
            designer_trafo.Document.AddElement(theTrashCell);


            theSnapCell = new SnapCell(0, 0);
            designer_trafo.Document.AddElement(theSnapCell);
            theSnapCell.Visible = false;

            trafoRunner.DoWork += TrafoRunner_DoWork;
            trafoRunner.ProgressChanged += TrafoRunner_ProgressChanged;

            designer_board.Document.GridSize = new Size(500,500);

            // TODO uncommenting this removes the rule button and trash button
            //LoadLevel(currentLab);
        }

        private void Designer_trafo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if(designer_trafo.Document.FindElement(new Point(e.X, e.Y)) is BirdCell bird)
            {
                bird.TurnLeft();
            }
        }

        private void Designer_trafo_ElementMouseDown(object sender, ElementMouseEventArgs e)
        {
            DesignerUtil.ArrangeTheOrder(designer_trafo);
            if (e.Element is RuleCell rule)
            {
                designer_trafo.Document.ClearSelection();
                List<BaseElement> list = DesignerUtil.FindElementsWithin(designer_trafo, e.Element);
                designer_trafo.Document.SelectElements(list.ToArray());
            }
        }

        private void Designer_trafo_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.X > theTrashCell.Location.X && e.X < theTrashCell.Location.X + theTrashCell.Size.Width
                && e.Y > theTrashCell.Location.Y && e.Y < theTrashCell.Location.Y + theTrashCell.Size.Height)
            {
                theTrashCell.OpenCan();
            }
            else
            {
                theTrashCell.CloseCan();
            }
        }

        private void Designer_trafo_Resize(object sender, EventArgs e)
        {
            blockPanel.Size = new Size(200, designer_trafo.Height);
        }

        private void Designer_trafo_MouseDown(object sender, MouseEventArgs e)
        {
            // TODO if element is not dragged to the canvas, delete the newly created one or the old one! It sure has duplication on the button area.
            if (designer_trafo.Document.FindElement(e.Location) != null
                && designer_trafo.Document.FindElement(e.Location) is IonCell ion1
                && ion1.Location == ion1Location)
            {
                var sampleIon = ion1.GetIons()[0];
                var ionValues = new Ion {Id = Guid.NewGuid(), Name = sampleIon.Name, Charge = sampleIon.Charge, Symbol =sampleIon.Symbol, Color1 = sampleIon.Color1, Color2 = sampleIon.Color2, Radius = sampleIon.Radius };
                var newIonCell = new IonCell(ion1Location.X, ion1Location.Y,new List<Ion> {ionValues});
                designer_trafo.Document.AddElement(newIonCell);
                designer_trafo.Document.SendToBackElement(newIonCell);
                designer_trafo.Document.SendToBackElement(blockPanel);
            }
            else if (designer_trafo.Document.FindElement(e.Location) != null
              && designer_trafo.Document.FindElement(e.Location) is IonCell ion2
              && ion2.Location == ion2Location)
            {
                var sampleIon = ion2.GetIons()[0];
                var ionValues = new Ion {Id = Guid.NewGuid(), Name = sampleIon.Name, Charge = sampleIon.Charge, Symbol =sampleIon.Symbol, Color1 = sampleIon.Color1, Color2 = sampleIon.Color2, Radius = sampleIon.Radius };
                var newIonCell = new IonCell(ion2Location.X, ion2Location.Y,new List<Ion> {ionValues});
                designer_trafo.Document.AddElement(newIonCell);
                designer_trafo.Document.SendToBackElement(newIonCell);
                designer_trafo.Document.SendToBackElement(blockPanel);
            }
            else if (designer_trafo.Document.FindElement(e.Location) != null
              && designer_trafo.Document.FindElement(e.Location) is PigCell pig
              && pig.Location == pigButtonLocation)
            {
                var newPigCell = new PigCell(pigButtonLocation.X, pigButtonLocation.Y);
                designer_trafo.Document.AddElement(newPigCell);
                designer_trafo.Document.SendToBackElement(newPigCell);
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
                case (int)TrafoProgress.UpdateModel:
                    var changes = e.UserState as List<ChangeStep>;
                    DesignerUtil.ApplyChanges(designer_board, changes);
                    break;
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
            var allRules = DesignerUtil.GetTrafoElementsOutsideBlockWithoutStartOrSnapOrBlock(designer_trafo).Where(el => el is RuleCell).Cast<RuleCell>().ToList();
            allRules.Sort((a, b) => { return a.Location.Y - b.Location.Y; });
            foreach (var rule in allRules)
            {
                try
                {
                    trafoRunner.ReportProgress((int)TrafoProgress.Highlight, rule);

                    var trafoElements = DesignerUtil.GetTrafoElementsOutsideBlockWithoutStartOrSnapOrBlock(designer_trafo);

                    for (int i = 0; i < ((RuleCell)rule).RuleCount; i++)
                    {
                        var preConditionElements = TrafoUtil.FindPreConditionElementsChemistry(trafoElements, rule);
                        var postConditionElements = TrafoUtil.FindPostConditionElementsChemistry(trafoElements, rule);
                        //var cloneOfPreConditionElements = PatternUtil.Clone(preConditionElements);
                        //var cloneOfPostConditionElements = PatternUtil.Clone(postConditionElements);

                        
                        if (PyUtil.IsPatternInTheModelChemistry(designer_board.Document.Elements.GetArray().ToList(),
                            preConditionElements))
                        {
                            var changes = PyUtil.FindChangesInTheRuleChemistry(preConditionElements, postConditionElements);
                            // TODO if there are no changes (patterns are same), there is an exception from c# parsing py response. Handle it gracefully.
                            trafoRunner.ReportProgress((int)TrafoProgress.UpdateModel, changes);

                            Thread.Sleep(TimeoutBetweenRuleExecution);
                            // TODO decide what to do below
                            if (false && theBird.Location.Equals(thePig.Location))
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
            // TODO decide what to do when done
            //trafoRunner.ReportProgress((int)TrafoProgress.Failure);
            trafoRunner.CancelAsync();
            return;
        }

        private void Designer_trafo_ElementMouseUp(object sender, ElementMouseEventArgs e)
        {
            theSnapCell.Visible = false;

            if (e.X > theTrashCell.Location.X && e.X < theTrashCell.Location.X + theTrashCell.Size.Width
                && e.Y > theTrashCell.Location.Y && e.Y < theTrashCell.Location.Y + theTrashCell.Size.Height)
            {
                designer_trafo.Document.DeleteSelectedElements();
                return;
            }

            if (e.Element is RuleCell rule)
            {
                DesignerUtil.MoveRuleAndItsContents(designer_trafo, rule, theSnapCell.Location.X - 11, theSnapCell.Location.Y);

                // TODO Move the rest of the rules accordingly (if we put rule within two rules)
                foreach (var otherRule in DesignerUtil.GetTrafoElementsOutsideBlockWithoutStartOrSnapOrBlock(designer_trafo).Where(el=>el is RuleCell && el.Location.Y > rule.Location.Y))
                {
                    // this should be investigated more. Because some elements of the newly moved rule might be on top of the existing rule which will mess things up.
                }

            }
            else if (e.Element is BirdCell bird)
            {
                // the snapping is based on mouse position, not the bird origin
                var cellUnderneath = DesignerUtil.FindCellUnderneath(designer_trafo, new Point(e.X, e.Y));
                if (cellUnderneath != null)
                    bird.Location = cellUnderneath.Location;
            }
            else if (e.Element is PigCell pig)
            {
                // the snapping is based on mouse position, not the bird origin
                var cellUnderneath = DesignerUtil.FindCellUnderneath(designer_trafo, new Point(e.X, e.Y));
                if (cellUnderneath != null)
                    pig.Location = cellUnderneath.Location;
            }
            else if(e.Element is EmptyCell emptyCell)
            {
                DesignerUtil.SnapNewEmptyCellToExistingNeighbors(designer_trafo, emptyCell, new Point(e.X, e.Y));
            }
            else if(e.Element is IonCell ionCell)
            {
                DesignerUtil.SnapIonCellToNeighbor(designer_trafo,ionCell);
            }
        }

        private void Designer_trafo_ElementMoving(object sender, ElementEventArgs e)
        {
            if(e.Element is RuleCell)
            {
                var possibleElements = DesignerUtil.GetTrafoElementsOutsideBlockWithoutSnapOrBlock(designer_trafo).Where(el => (el is RuleCell || el is StartCell)
                    && !el.Equals(e.Element));

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
            } else if(e.Element is RectangleNode block)
            {
                block.Location = new Point(0, 0);
            }
            else if (e.Element is StartCell start)
            {
                start.Location = new Point(230, 30);
            }
            else if (e.Element is TrashCell trash)
            {
                trash.Location = new Point(60, 330);
            }
            designer_trafo.Document.BringToFrontElement(theTrashCell);
        }

        private void Designer_trafo_ElementClick(object sender, ElementEventArgs e)
        {
            if(e.Element is RectangleNode block)
            {
                designer_trafo.Document.ClearSelection();
            }
            else if (e.Element is StartCell start)
            {
                designer_trafo.Document.ClearSelection();
            }
            else if (e.Element is TrashCell trash)
            {
                designer_trafo.Document.ClearSelection();
            }
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

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            debugPanel.Visible = !debugPanel.Visible;
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
                DesignerUtil.CopyLHStoRHS(designer_trafo, (RuleCell)designer_trafo.Document.SelectedElements.GetArray().Where(el => el is RuleCell).First());
            }
            else
            {
                MessageBox.Show("Please select a rule first to copy its 'current pattern' to its 'pattern after'.", "No rule selected!", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            DialogResult dialogResult = MessageBox.Show("This will reset the lab to its start state and delete all the blocks you've added or changed.", "Are you sure you want to start over?", MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                StartOver();
            }
        }

        private void StartOver()
        {
            Reset();
            foreach (var element in DesignerUtil.GetTrafoElementsOutsideBlockWithoutStartOrSnapOrBlock(designer_trafo))
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
            if (!trafoRunner.IsBusy)
            {
                Reset();
                theStart.Highlight();
                trafoRunner.RunWorkerAsync();
            }
        }

        private void resetButton_Click_1(object sender, EventArgs e)
        {
            Reset();
        }

        void Reset()
        {
            theStart.Unhighlight();
            theBird?.Reset();
            foreach (var rule in DesignerUtil.GetTrafoElementsOutsideBlockWithoutStartOrSnapOrBlock(designer_trafo).Where(el => el is RuleCell))
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

        private void maze9button_MouseEnter(object sender, EventArgs e)
        {
            maze9button.BackgroundImage = Resources.maze9_over;
        }

        private void maze9button_MouseLeave(object sender, EventArgs e)
        {
            maze9button.BackgroundImage = Resources.maze9;
        }

        private void button11_Click(object sender, EventArgs ev)
        {
            designer_board.Document.SelectAllElements();
            designer_board.Document.DeleteSelectedElements();

            LevelDesigner.GenericLevelDesign(designer_board, Resources.small_maze);

            theBird = (BirdCell)designer_board.Document.Elements.GetArray().Where(e => e is BirdCell).First();
            thePig = (PigCell)designer_board.Document.Elements.GetArray().Where(e => e is PigCell).First();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            designer_board.Enabled = true;
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void mazeButtons_Click(object sender, EventArgs e)
        {
            LoadLevel((sender as PictureBox).Tag.ToString());
        }

        private void LoadLevel(String level)
        {
            label_selectlab.Visible = false;
            currentLab = level;
            StartOver();

            designer_board.Document.Elements.Clear();

            var resourceName = "Lab" + level;
            LevelDesigner.GenericLabLevelDesign(designer_board, Resources.ResourceManager.GetString(resourceName));

            var ion2 = new Chlorine();
            var ionList2 = new List<Ion>() {  ion2};
            var ioncell2 = new IonCell(ion2Location.X, ion2Location.Y,ionList2);

            designer_trafo.Document.AddElement(ioncell2);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            DesignerUtil.SolveMaze3(designer_trafo);
        }

       
    }
}
