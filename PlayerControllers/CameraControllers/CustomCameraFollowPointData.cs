using System;
using SingularityLab.Assets._RobotStars.Scripts.GameCamera;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SingularityLab.Assets._RobotStars.Scripts.GameCore.PlayerControllers.CameraControllers
{
    [Serializable]
    public class CustomCameraFollowPointData
    {
        public Vector3 Offset;
        public Vector3 LookAtOffset;
        public float MovementSpeed = 0.6f;
        public float LookAtSpeed = 2f;
        [Required] public Transform Transform;
        public CameraViewType CameraViewType;
    }
}