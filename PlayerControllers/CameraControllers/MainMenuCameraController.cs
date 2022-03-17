using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

namespace SingularityLab.Assets._RobotStars.Scripts.GameCore.PlayerControllers.CameraControllers
{
    public class MainMenuCameraController : CameraController
    {
        [SerializeField] private Dictionary<MainMenuCameraViewType, CinemachineVirtualCamera> _viewTypes =
            new Dictionary<MainMenuCameraViewType, CinemachineVirtualCamera>();

        public CinemachineVirtualCamera GetVirtualCamera(MainMenuCameraViewType viewType)
        {
            return _viewTypes[viewType];
        }

        public void SwitchTo(MainMenuCameraViewType mainMenuCameraViewType)
        {
            if (!_viewTypes.ContainsKey(mainMenuCameraViewType))
            {
                return;
            }

            foreach (CinemachineVirtualCamera cinemachineVirtualCamera in _viewTypes.Values)
            {
                cinemachineVirtualCamera.Priority = 0;
            }

            _viewTypes[mainMenuCameraViewType].Priority = 10;
        }
    }
}