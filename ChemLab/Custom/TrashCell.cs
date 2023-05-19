using ChemLab.Properties;
using Dalssoft.DiagramNet;
using System;
using System.Drawing;

namespace ChemLab.Custom
{

    [Serializable]
    public class TrashCell : RectangleElement
    {
        public TrashCell() : base(60, 630, 70, 70)
        {
            Background = Resources.canclosed;
            FillColor1 = Color.Transparent;
            FillColor2 = Color.Transparent;
        }

        public void OpenCan()
        {
            Background = Resources.canopen;
        }

        public void CloseCan()
        {
            Background = Resources.canclosed;
        }
    }
}
