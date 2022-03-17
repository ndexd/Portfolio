using System.Linq;
using SingularityLab.Assets._RobotStars.Scripts.GameCamera;
using SingularityLab.Assets._RobotStars.Scripts.GamePlayer.PlayerCamera;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SingularityLab.Assets._RobotStars.Scripts.GameCore.PlayerControllers.CameraControllers
{
    public class InGameCameraController : CameraController
    {
        [Required] [SerializeField] private PlayerTargetGroupController _targetGroupController;

        [SerializeField] private CustomCameraFollowPointData[] _followPoints;

        private CustomCameraFollowPointData _currentFollowPoint;

        private Vector3 _velocity;
        private Vector3 _lookVelocity;

        public PlayerTargetGroupController TargetGroupController => _targetGroupController;

        private void Start()
        {
            SwitchTo(CameraViewType.Full);
        }

        private void FixedUpdate()
        {
            if (_currentFollowPoint == null)
            {
                return;
            }

            var targetPosition = _currentFollowPoint.Transform.position;

            // transform.position = Vector3.SmoothDamp(transform.position,
            //     targetPosition + _currentFollowPoint.Offset,
            //     ref _velocity, _currentFollowPoint.MovementSpeed, _currentFollowPoint.MovementSpeed * 10f,
            //     Time.smoothDeltaTime);
            // targetPosition.z = 0f;
            //
            // transform.position = targetPosition;

            Vector3 smoothDump = Vector3.SmoothDamp(transform.position, targetPosition + _currentFollowPoint.Offset,
                ref _velocity, _currentFollowPoint.MovementSpeed, _currentFollowPoint.MovementSpeed * 1000000f,
                Time.fixedDeltaTime);

            transform.position = smoothDump;
            Vector3 forward = (targetPosition + _currentFollowPoint.LookAtOffset - smoothDump).normalized;

            transform.forward = Vector3.SmoothDamp(transform.forward, forward, ref _lookVelocity,
                _currentFollowPoint.LookAtSpeed, _currentFollowPoint.LookAtSpeed * 10000, Time.fixedDeltaTime);
            Debug.DrawRay(transform.position, transform.forward);
            
            // Quaternion targetRotation =
            //     Quaternion.LookRotation(targetPosition + _currentFollowPoint.LookAtOffset - transform.position);
            //
            // transform.rotation = Quaternion.Slerp(transform.rotation,
            //     targetRotation,
            //     Time.deltaTime * _currentFollowPoint.LookAtSpeed);
        }

        public void AddTargetTransform(CameraControllerSetupData targetTransform)
        {
            _targetGroupController.AddMember(targetTransform);
        }

        public void RemoveTargetTransform(Transform targetTransform)
        {
            _targetGroupController.RemoveMember(targetTransform);
        }

        public void SwitchTo(CameraViewType cameraViewType)
        {
            var followPoint = _followPoints.FirstOrDefault(a => a.CameraViewType == cameraViewType);
            if (followPoint == null)
            {
                return;
            }

            _currentFollowPoint = followPoint;
        }
    }
}