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

        public RuleCell(int x, int y) : base(x, y, 400, 200)
        {
            Background = Resources.rule;
            FillColor1 = Color.Transparent;
            FillColor2 = Color.Transparent;
            Label = new LabelElement(x, y,400,200);
            Label.Alignment = StringAlignment.Far;
            Label.LineAlignment = StringAlignment.Near;
            label.Font = new Font("Trebuchet MS", 25);
            label.ForeColor1 = Color.Blue;
            Label.Text = "1";
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
