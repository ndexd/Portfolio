using Cinemachine;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SingularityLab.Assets._RobotStars.Scripts.GameCore.PlayerControllers.CameraControllers
{
    public class TargetGroupFollower : MonoBehaviour
    {
        [Required] [SerializeField] private CinemachineTargetGroup _targetGroup;
        
        private void Update()
        {
            var targetPosition = _targetGroup.BoundingBox.center;
            targetPosition.z = -_targetGroup.BoundingBox.size.magnitude;
            targetPosition.z = Mathf.Clamp(targetPosition.z, -25f, -5f);
            transform.position = targetPosition;
        }
    }
}