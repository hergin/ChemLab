namespace ChemLab.Model
{
    enum ChangeStepType
    {
        Add, Delete
    }

    internal class ChangeStep
    {
        public ChangeStepType Type { get; set; }
        public Compound Compound { get; set; }

        public override string ToString()
        {
            return Type + ": " + Compound.ToString();
        }
    }
}
