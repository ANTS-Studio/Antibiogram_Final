namespace DefaultNamespace
{
    public class Step
    {
        private int _id;
        private string _name;
        private int _nextStep;
        private int _previousStep;
        private bool _previousStepDone;

        public Step(int id, string name, int nextStep, int previousStep, bool previousStepDone)
        {
            this._id = id;
            this._name = name;
            this._nextStep = nextStep;
            this._previousStep = previousStep;
            this._previousStepDone = previousStepDone;
        }
        
        public Step(bool previousStepDone)
        {
            this._previousStepDone = previousStepDone;
        }
    }
}