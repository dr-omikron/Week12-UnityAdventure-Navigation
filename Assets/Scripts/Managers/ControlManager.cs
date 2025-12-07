using Characters;
using Controllers;
using Physic;
using PlayerInputs;
using Spawners;
using Timers;
using UnityEngine;
using UnityEngine.AI;
using Utils;

namespace Managers
{
    public class ControlManager : MonoBehaviour
    {
        [SerializeField] private Character _character;
        [SerializeField] private LayerMask _movableMask;
        [SerializeField] private float _idleTime;
        [SerializeField] private float _idlePositionGenerationRadius;

        [SerializeField] private MovementPointIndicatorSpawner _movementPointIndicatorSpawner;
        [SerializeField] private MovementPointIndicator _movementPointIndicatorPrefab;

        private Controller _characterController;
        private Controller _playerCompositeController;
        private Controller _randomPointCompositeController;

        private PlayerInput _playerInput;
        private Raycaster _raycaster;
        private RaycastPlayerController _raycastPlayerController;
        private RandomTargetPositionGenerator _randomTargetPositionGenerator;
        private NavMeshPath _path;
        private Timer _idleTimer;

        private void Awake()
        {
            _playerInput = new PlayerInput();
            _raycaster = new Raycaster();
            _path = new NavMeshPath();

            _randomTargetPositionGenerator = new RandomTargetPositionGenerator(_character.transform, _idlePositionGenerationRadius);

            _idleTimer = new Timer(_idleTime);
 
            NavMeshQueryFilter queryFilter = new NavMeshQueryFilter
            {
                agentTypeID = 0,
                areaMask = NavMesh.AllAreas
            };

            _movementPointIndicatorSpawner.Initialize(_character, _movementPointIndicatorPrefab);

            _raycastPlayerController = new RaycastPlayerController(
                _character, 
                _raycaster, 
                _playerInput, 
                _path,
                queryFilter, 
                _movableMask);

            _playerCompositeController = new CompositeController(_raycastPlayerController,
                new AlongMovableVelocityRotatableController(_character, _character));

            _randomPointCompositeController = new CompositeController(
                new RandomPointDirectionalMovableController(
                    _character, 
                    _randomTargetPositionGenerator, 
                    _path, 
                    queryFilter), 

                new AlongMovableVelocityRotatableController(_character, _character));

            _characterController = _playerCompositeController;

            _characterController.Enable();
        }

        private void Update()
        {
            if (IsCharacterMovingToRaycastTarget() == false && _characterController == _playerCompositeController)
            {
                if(_idleTimer.IsActive() == false)
                    _idleTimer.Start();
            }
            else
            {
                if(_idleTimer.IsActive())
                    _idleTimer.Stop();
            }

            if(_playerInput.IsRayCastingStart && _characterController == _randomPointCompositeController)
            {
                SwitchController(_playerCompositeController);
                _idleTimer.Stop();
            }

            if (_idleTimer.TimeIsUp)
            {
                SwitchController(_randomPointCompositeController);
                _idleTimer.Stop();
            }

            if(_character.IsDead() && _characterController.IsEnabled)
            {
                _characterController.Disable();
                _idleTimer.Stop();
            }

            if(_character.IsDamaged() && _characterController.IsEnabled)
                _characterController.Disable();
            else if(_character.IsDead() == false && _characterController.IsEnabled == false)
                _characterController.Enable();

            _characterController.Update(Time.deltaTime);
            _idleTimer.Update(Time.deltaTime);
        }

        private bool IsCharacterMovingToRaycastTarget() => _raycastPlayerController.IsMovementToTarget;

        private void SwitchController(Controller controller)
        {
            _characterController.Disable();
            _characterController = controller;
            _characterController.Enable();
        }
    }
}
