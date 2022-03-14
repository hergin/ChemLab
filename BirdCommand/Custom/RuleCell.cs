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
    [Serializable]
    public class RuleCell : RectangleElement
    {
        int ruleCount = 1;
        public int RuleCount { get { return ruleCount; } }

        public RuleCell(int x, int y) : this(x, y, 400, 200) { }

        public RuleCell(int x, int y, int w, int h) : base(x, y, w, h)
        {
            Background = Resources.rule;
            FillColor1 = Color.Transparent;
            FillColor2 = Color.Transparent;
            Label = new LabelElement(x, y, w, h);
            Label.Alignment = StringAlignment.Far;
            Label.LineAlignment = StringAlignment.Near;
            label.Font = new Font("Trebuchet MS", h/8);
            label.ForeColor1 = Color.Blue;
            Label.Text = "1";
        }

        public void ResizeToOriginal()
        {
            this.Size = new Size(400, 200);
            Label.Size = new Size(400, 200);
            label.Font = new Font("Trebuchet MS", 25);
            OnAppearanceChanged(new EventArgs());
        }

        public void Highlight()
        {
            Background = Resources.rule_highlighted;
        }

        public void Unhighlight()
        {
            Background = Resources.rule;
        }

        public void IncreaseRuleCount()
        {
            ruleCount++;
            Label.Text = ruleCount.ToString();
            OnAppearanceChanged(new EventArgs());
        }

        public void DecreaseRuleCount()
        {
            if (ruleCount > 1)
            {
                ruleCount--;
                Label.Text = ruleCount.ToString();
                OnAppearanceChanged(new EventArgs());
            }
        }
    }
}
