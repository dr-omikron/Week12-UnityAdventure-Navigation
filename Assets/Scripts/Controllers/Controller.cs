
namespace Controllers
{
    public abstract class Controller
    {
        public bool IsEnabled { get; private set; }

        public virtual void Enable() => IsEnabled = true;
        public virtual void Disable() => IsEnabled = false;

        public void Update(float deltaTime)
        {
            if (IsEnabled == false)
                return;

            UpdateLogic(deltaTime);
        }

        protected abstract void UpdateLogic(float deltaTime);
    }
}
