namespace DefaultNamespace
{
    public class Step
    {
        private int _id;

        public int ID
        {
            get => _id;
            set => _id = value;
        }

        public bool WronglyDone
        {
            get => _wronglyDone;
            set => _wronglyDone = value;
        }

        private string _name;
        private bool _stepDone;
        private bool _wronglyDone;
        private string _hintText;
        private string _tutorialText;

        public string TutorialText
        {
            get => _tutorialText;
            set => _tutorialText = value;
        }

        public Step(int id, string name, bool stepDone, bool wronglyDone, string hintText, string tutorialText)
        {
            _id = id;
            _name = name;
            _stepDone = stepDone;
            _wronglyDone = wronglyDone;
            _hintText = hintText;
            _tutorialText = tutorialText;
        }

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public bool StepDone
        {
            get => _stepDone;
            set => _stepDone = value;
        }

        public string Hint
        {
            get => _hintText;
            set => _hintText = value;
        }
    }
}