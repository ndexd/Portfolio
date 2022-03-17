using SingularityLab.Assets._RobotStars.Scripts.GameCore.PlayerControllers.CameraControllers;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SingularityLab.Assets._RobotStars.Scripts.GameCore.PlayerControllers
{
    public abstract class BasePlayerController : SerializedMonoBehaviour
    {
        [Required] [SerializeField] protected CameraController _cameraController;

        public CameraController CameraController => _cameraController;
    }
}