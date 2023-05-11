using ChemLab.Custom;
using Dalssoft.DiagramNet;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace ChemLab.Model
{
    public class TrafoDesigner : Designer
    {
        RectangleNode blockPanel;
        StartCell theStart;
        SnapCell theSnapCell;
        TrashCell theTrashCell;

        public TrafoDesigner()
        {
            this.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Location = new System.Drawing.Point(434, 142);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "trafoDesigner";
            this.Size = new System.Drawing.Size(929, 868);
            this.TabIndex = 11;
            this.Document.GridSize = new System.Drawing.Size(10000, 10000);

            blockPanel = new RectangleNode(0, 0, 200, 220);
            blockPanel.FillColor1 = Color.FromArgb(228, 228, 228);
            blockPanel.FillColor2 = Color.FromArgb(228, 228, 228);
            this.Document.AddElement(blockPanel);

            theStart = new StartCell();
            this.Document.AddElement(theStart);

            theSnapCell = new SnapCell(0, 0);
            this.Document.AddElement(theSnapCell);
            theSnapCell.Visible = false;

            theTrashCell = new TrashCell();
            this.Document.AddElement(theTrashCell);

            this.Resize += TrafoDesigner_Resize;
            this.ElementClick += TrafoDesigner_ElementClick;
            this.ElementMouseUp += TrafoDesigner_ElementMouseUp;
            this.MouseMove += TrafoDesigner_MouseMove;
            this.ElementMoving += TrafoDesigner_ElementMoving;
            this.ElementMouseDown += TrafoDesigner_ElementMouseDown;
        }

        private void TrafoDesigner_ElementMouseDown(object sender, ElementMouseEventArgs e)
        {
            DesignerUtil.ArrangeTheOrder(this);
            if (e.Element is ReactionCell)
            {
                this.Document.ClearSelection();
                List<BaseElement> list = DesignerUtil.FindElementsWithin(this, e.Element);
                this.Document.SelectElements(list.ToArray());
            }
        }

        private void TrafoDesigner_ElementMoving(object sender, ElementEventArgs e)
        {
            if (e.Element is ReactionCell)
            {
                var possibleElements = DesignerUtil.GetTrafoElementsOutsideBlockWithoutSnapOrBlock(this).Where(el => (el is StartCell || el is ReactionCell)
                    && !el.Equals(e.Element));

                var smallest = int.MaxValue;
                BaseElement closestElement = null;
                foreach (var element in possibleElements)
                {
                    if (Math.Abs(element.Location.Y + element.Size.Height - e.Element.Location.Y) < smallest)
                    {
                        smallest = Math.Abs(element.Location.Y + element.Size.Height - e.Element.Location.Y);
                        closestElement = element;
                    }
                }
                theSnapCell.Location = new Point(closestElement.Location.X + 11, closestElement.Location.Y + closestElement.Size.Height - 5);
                theSnapCell.Visible = true;
                this.Document.BringToFrontElement(theSnapCell);
            }
            else if (e.Element is RectangleNode block)
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
            this.Document.BringToFrontElement(theTrashCell);
        }

        private void TrafoDesigner_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.X > theTrashCell.Location.X && e.X < theTrashCell.Location.X + theTrashCell.Size.Width
                && e.Y > theTrashCell.Location.Y && e.Y < theTrashCell.Location.Y + theTrashCell.Size.Height)
            {
                theTrashCell.OpenCan();
            }
            else
            {
                theTrashCell.CloseCan();
            }
        }

        private void TrafoDesigner_ElementMouseUp(object sender, ElementMouseEventArgs e)
        {
            theSnapCell.Visible = false;

            if (e.X > theTrashCell.Location.X && e.X < theTrashCell.Location.X + theTrashCell.Size.Width
                && e.Y > theTrashCell.Location.Y && e.Y < theTrashCell.Location.Y + theTrashCell.Size.Height)
            {
                this.Document.DeleteSelectedElements();
                return;
            }

            if (e.Element is ReactionCell reaction)
            {
                DesignerUtil.MoveReactionAndItsContents(this, reaction, theSnapCell.Location.X - 11, theSnapCell.Location.Y+5);

                // TODO Move the rest of the rules accordingly (if we put rule within two rules)
                foreach (var otherRule in DesignerUtil.GetTrafoElementsOutsideBlockWithoutStartOrSnapOrBlock(this).Where(el => el is ReactionCell && el.Location.Y > reaction.Location.Y))
                {
                    // this should be investigated more. Because some elements of the newly moved rule might be on top of the existing rule which will mess things up.
                }
            }
            else if (e.Element is IonCell ionCell)
            {
                DesignerUtil.SnapIonCellToNeighbor(this, ionCell);
            }
        }

        private void TrafoDesigner_ElementClick(object sender, ElementEventArgs e)
        {
            if (e.Element is RectangleNode block)
            {
                this.Document.ClearSelection();
            }
            else if (e.Element is StartCell start)
            {
                this.Document.ClearSelection();
            }
            else if (e.Element is TrashCell trash)
            {
                this.Document.ClearSelection();
            }
        }

        private void TrafoDesigner_Resize(object sender, EventArgs e)
        {
            blockPanel.Size = new Size(200, this.Height);
        }

        public void SendBlockToBack()
        {
            this.Document.SendToBackElement(blockPanel);
        }

        public void HighlightStart()
        {
            theStart.Highlight();
        }

        public void UnHighlightStart()
        {
            theStart.Unhighlight();
        }
    }
}
