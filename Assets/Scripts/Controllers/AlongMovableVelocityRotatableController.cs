using Movement;

namespace Controllers
{
    public class AlongMovableVelocityRotatableController : Controller
    {
        private readonly IDirectionalMovable _movable;
        private readonly IDirectionalRotatable _rotatable;

        public AlongMovableVelocityRotatableController(IDirectionalMovable movable, IDirectionalRotatable rotatable)
        {
            _movable = movable;
            _rotatable = rotatable;
        }

        protected override void UpdateLogic(float deltaTime)
        {
            _rotatable.SetRotationDirection(_movable.CurrentVelocity);
        }
    }
}
