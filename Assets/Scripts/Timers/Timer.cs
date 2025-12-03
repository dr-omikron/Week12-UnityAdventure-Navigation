namespace Timers
{
    public class Timer
    {
        private float _currentTime;
        private readonly float _eventTime;
        private bool _isActive;

        public Timer(float eventTime)
        {
            _eventTime = eventTime;
        }

        public bool TimeIsUp { get; private set; }

        public void Start() => _isActive = true;

        public bool IsActive() => _isActive;

        public void Stop()
        {
            _isActive = false;
            TimeIsUp = false;
            ResetTime();
        }

        public void Update(float deltaTime)
        {
            if (_isActive == false)
                return;

            _currentTime += deltaTime;

            if (_currentTime >= _eventTime)
            {
                TimeIsUp = true;
                ResetTime();
                return;
            }

            TimeIsUp = false;
        }

        private void ResetTime()
        {
            _currentTime = 0;
        }
    }
}
