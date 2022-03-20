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
    public class TrashCell : RectangleElement
    {
        public TrashCell() : base(60, 330, 70, 70)
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
