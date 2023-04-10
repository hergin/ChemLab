using BirdCommand.Custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCommand.Model
{
    enum ChangeStepType {
        Add, Delete
    }

    internal  class ChangeStep
    {
        public ChangeStepType Type { get; set; }
        public Compound Compound { get; set; }

        public override string ToString()
        {
            return Type +": "+Compound.ToString();
        }
    }
}
