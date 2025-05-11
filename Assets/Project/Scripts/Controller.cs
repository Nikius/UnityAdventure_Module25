namespace Project.Scripts
{
    public abstract class Controller
    {
        private bool _isEnabled;
        
        public void Enable() => _isEnabled = true;
        
        public void Disable() => _isEnabled = false;
        
        public void Update()
        {
            if (_isEnabled == false)
                return;
            
            UpdateLogic();
        }

        protected abstract void UpdateLogic();
    }
}