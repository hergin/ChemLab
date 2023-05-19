﻿using ChemLab.Custom;
using ChemLab.Model;
using ChemLab.Model.IonCells;
using ChemLab.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;


namespace ChemLab
{
    // TODO when you click on the newly slide area to create something or to select, it automatically goes to top.
    // TODO when maze is done, decide what's gonna happen next (going to other maze? etc.)
    // TODO get rid of all magical numbers somehow
    // TODO don't allow patterns with disjoint empty cells unless there are diagonal links (such as just a diagonal, see below) 
    //                                                             E
    //                                                              E
    // TODO allow only patterns with same number of empty cells FOR NOW
    // TODO solve the first one by using the whole pattern and it looks like failing sometime.
    public partial class ChemLabMain : Form
    {
        string currentLab = "1";
        private const int TimeoutBetweenRuleExecution = 250;
        public static int CELL_SIZE = 50;


        TrafoDesigner trafoDesigner;

        public ChemLabMain()
        {
            trafoDesigner = new TrafoDesigner();
            this.Controls.Add(trafoDesigner);

            InitializeComponent();
            trafoDesigner.SendToBack();

            trafoDesigner.MouseDown += trafoDesigner_MouseDown;

            FillBlocksPanel();

            trafoRunner.DoWork += TrafoRunner_DoWork;
            trafoRunner.ProgressChanged += TrafoRunner_ProgressChanged;

            designer_board.Document.GridSize = new Size(500, 500);

            // TODO uncommenting this removes the rule button and trash button
            //LoadLevel(currentLab);
        }

        void FillBlocksPanel()
        {
            trafoDesigner.Document.AddElement(new ReactionCell(28, 540, 140, 35));

            trafoDesigner.Document.AddElement(new SodiumIonCell(70, 30));
            trafoDesigner.Document.AddElement(new ChlorineIonCell(67, 110));
            trafoDesigner.Document.AddElement(new SilverIonCell(70, 200));
            trafoDesigner.Document.AddElement(new NitrateIonCell(63, 280));
            trafoDesigner.Document.AddElement(new LeadIonCell(70, 370));
            trafoDesigner.Document.AddElement(new PotassiumIonCell(73, 450));
        }



        private void trafoDesigner_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Location.X < 200)
            {
                // TODO if element is not dragged to the canvas, delete the newly created one or the old one! It sure has duplication on the button area.
                if (trafoDesigner.Document.FindElement(e.Location) != null
                    && trafoDesigner.Document.FindElement(e.Location) is SodiumIonCell sodiumIon)
                {
                    var newIonCell = new SodiumIonCell(sodiumIon.Location.X, sodiumIon.Location.Y);
                    trafoDesigner.Document.AddElement(newIonCell);
                    trafoDesigner.Document.SendToBackElement(newIonCell);
                    trafoDesigner.SendBlockToBack();
                }
                else if (trafoDesigner.Document.FindElement(e.Location) != null
                    && trafoDesigner.Document.FindElement(e.Location) is ChlorineIonCell chlorineIon)
                {
                    var newIonCell = new ChlorineIonCell(chlorineIon.Location.X, chlorineIon.Location.Y);
                    trafoDesigner.Document.AddElement(newIonCell);
                    trafoDesigner.Document.SendToBackElement(newIonCell);
                    trafoDesigner.SendBlockToBack();
                }
                else if (trafoDesigner.Document.FindElement(e.Location) != null
                    && trafoDesigner.Document.FindElement(e.Location) is SilverIonCell silverIon)
                {
                    var newIonCell = new SilverIonCell(silverIon.Location.X, silverIon.Location.Y);
                    trafoDesigner.Document.AddElement(newIonCell);
                    trafoDesigner.Document.SendToBackElement(newIonCell);
                    trafoDesigner.SendBlockToBack();
                }
                else if (trafoDesigner.Document.FindElement(e.Location) != null
                    && trafoDesigner.Document.FindElement(e.Location) is NitrateIonCell nitrateIon)
                {
                    var newIonCell = new NitrateIonCell(nitrateIon.Location.X, nitrateIon.Location.Y);
                    trafoDesigner.Document.AddElement(newIonCell);
                    trafoDesigner.Document.SendToBackElement(newIonCell);
                    trafoDesigner.SendBlockToBack();
                }
                else if (trafoDesigner.Document.FindElement(e.Location) != null
                    && trafoDesigner.Document.FindElement(e.Location) is LeadIonCell leadIon)
                {
                    var newIonCell = new LeadIonCell(leadIon.Location.X, leadIon.Location.Y);
                    trafoDesigner.Document.AddElement(newIonCell);
                    trafoDesigner.Document.SendToBackElement(newIonCell);
                    trafoDesigner.SendBlockToBack();
                }
                else if (trafoDesigner.Document.FindElement(e.Location) != null
                    && trafoDesigner.Document.FindElement(e.Location) is PotassiumIonCell potassiumIon)
                {
                    var newIonCell = new PotassiumIonCell(potassiumIon.Location.X, potassiumIon.Location.Y);
                    trafoDesigner.Document.AddElement(newIonCell);
                    trafoDesigner.Document.SendToBackElement(newIonCell);
                    trafoDesigner.SendBlockToBack();
                }
                else if (trafoDesigner.Document.FindElement(e.Location) != null
                    && trafoDesigner.Document.FindElement(e.Location) is ReactionCell reaction)
                {
                    trafoDesigner.Document.BringToFrontElement(reaction);
                    var newReaction = new ReactionCell(reaction.Location.X, reaction.Location.Y, reaction.Size.Width, reaction.Size.Height);
                    reaction.ResizeToOriginal();
                    trafoDesigner.Document.AddElement(newReaction);
                    trafoDesigner.Document.SendToBackElement(newReaction);
                    trafoDesigner.SendBlockToBack();
                }
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
                    ((ReactionCell)e.UserState).Highlight();
                    break;
                case (int)TrafoProgress.Unhighlight:
                    ((ReactionCell)e.UserState).Unhighlight();
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
            var allReactions = DesignerUtil.GetTrafoElementsOutsideBlockWithoutStartOrSnapOrBlock(trafoDesigner).Where(el => el is ReactionCell).Cast<ReactionCell>().ToList();
            allReactions.Sort((a, b) => { return a.Location.Y - b.Location.Y; });
            foreach (var reaction in allReactions)
            {
                try
                {
                    trafoRunner.ReportProgress((int)TrafoProgress.Highlight, reaction);

                    var trafoElements = DesignerUtil.GetTrafoElementsOutsideBlockWithoutStartOrSnapOrBlock(trafoDesigner);

                    //for (int i = 0; i < ((ReactionCell)reaction).RuleCount; i++)
                    //{
                        var preConditionElements = TrafoUtil.FindPreConditionElementsChemistry(trafoElements, reaction);
                        var postConditionElements = TrafoUtil.FindPostConditionElementsChemistry(trafoElements, reaction);
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
                            if (false)
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
                    //}

                    trafoRunner.ReportProgress((int)TrafoProgress.Unhighlight, reaction);
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

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            debugPanel.Visible = !debugPanel.Visible;
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
            DialogResult dialogResult = MessageBox.Show("This will reset the lab to its start state and delete all the blocks you've added or changed.", "Are you sure you want to start over?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                StartOver();
            }
        }

        private void StartOver()
        {
            LoadLevel(currentLab);
            Reset();
            foreach (var element in DesignerUtil.GetTrafoElementsOutsideBlockWithoutStartOrSnapOrBlock(trafoDesigner))
            {
                trafoDesigner.Document.DeleteElement(element);
            }
            trafoDesigner.Document.ClearSelection();
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
                trafoDesigner.HighlightStart();
                trafoRunner.RunWorkerAsync();
            }
        }

        private void resetButton_Click_1(object sender, EventArgs e)
        {
            Reset();
        }

        void Reset()
        {
            trafoDesigner.UnHighlightStart();
            foreach (var reaction in DesignerUtil.GetTrafoElementsOutsideBlockWithoutStartOrSnapOrBlock(trafoDesigner).Where(el => el is ReactionCell))
            {
                ((ReactionCell)reaction).Unhighlight();
            }
        }


        private void button5_Click_1(object sender, EventArgs e)
        {
            designer_board.Enabled = true;
        }

        private void mazeButtons_Click(object sender, EventArgs e)
        {
            currentLab = (sender as LinkLabel).Tag.ToString();
            LoadLevel(currentLab);
        }

        private void LoadLevel(String level)
        {
            label_selectlab.Visible = false;

            designer_board.Document.Elements.Clear();

            var resourceName = "Lab" + level;
            LevelDesigner.GenericLabLevelDesign(designer_board, Resources.ResourceManager.GetString(resourceName));


            Reset();
            foreach (var element in DesignerUtil.GetTrafoElementsOutsideBlockWithoutStartOrSnapOrBlock(trafoDesigner))
            {
                trafoDesigner.Document.DeleteElement(element);
            }
            trafoDesigner.Document.ClearSelection();
        }

    }
}
