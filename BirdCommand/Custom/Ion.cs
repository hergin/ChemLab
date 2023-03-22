using System.Drawing;
using System;

namespace BirdCommand.Custom
{
    public class Ion
    {
        public Guid Id { get;set;}
        public string Name { get; set; }
        public string Symbol { get; set; }
        public string Description { get; set; }
        public int Charge { get; set; }
        public int Radius { get; set; }
        public Color Color1 { get; set; }
        public Color Color2 { get; set; }
    }
}