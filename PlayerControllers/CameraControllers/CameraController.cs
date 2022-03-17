using Sirenix.OdinInspector;
using UnityEngine;

namespace SingularityLab.Assets._RobotStars.Scripts.GameCore.PlayerControllers.CameraControllers
{
    [RequireComponent(typeof(Camera))]
    public class CameraController : SerializedMonoBehaviour
    {
        protected Camera Camera;

        protected virtual void Awake()
        {
            Camera = GetComponent<Camera>();
        }
    }
}