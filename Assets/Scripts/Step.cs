namespace DefaultNamespace
{
    public class Step
    {
        private int _id;
        private string _name;
        private int _nextStep;
        private int _previousStep;
        private bool _previousStepDone;
        private bool _failed;
        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public int NextStep
        {
            get => _nextStep;
            set => _nextStep = value;
        }

        public int PreviousStep
        {
            get => _previousStep;
            set => _previousStep = value;
        }

        public bool PreviousStepDone
        {
            get => _previousStepDone;
            set => _previousStepDone = value;
        }

        public bool Failed
        {
            get => _failed;
            set => _failed = value;
        }

        public Step(int id, string name, int nextStep, int previousStep, bool previousStepDone, bool failed)
        {
            this._id = id;
            this._name = name;
            this._nextStep = nextStep;
            this._previousStep = previousStep;
            this._previousStepDone = previousStepDone;
            this._failed = failed;
        }
    }
}