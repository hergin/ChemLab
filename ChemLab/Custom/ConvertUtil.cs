using Dalssoft.DiagramNet;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace ChemLab.Custom
{
    public class ConvertUtil
    {
        public static Graph PatternToGraphChemistry(List<BaseElement> elements)
        {
            Graph result = new Graph();

            foreach (var element in elements)
            {
                if (element is IonCell ionCell)
                {
                    var compound = ionCell.GetCompound();
                    result.Add(new Node { Id = compound.Symbol, Type = compound.Symbol });
                }
            }

            return result;
        }

        public static String IdFromLocation(Point Location, int adjustX = 0, int adjustY = 0)
        {
            return (Location.X - adjustX) + "_" + (Location.Y - adjustY);
        }
    }
}
