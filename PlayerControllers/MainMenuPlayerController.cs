using System.Collections.Generic;
using SingularityLab.Assets._RobotStars.Scripts.GameCombat;
using SingularityLab.Assets._RobotStars.Scripts.GameCore.GameModes;
using SingularityLab.Assets._RobotStars.Scripts.GameCore.Lootboxes;
using SingularityLab.Assets._RobotStars.Scripts.GameCore.PlayerControllers.CameraControllers;
using SingularityLab.Assets._RobotStars.Scripts.GameUI.MainMenu;
using SingularityLab.Assets._RobotStars.Scripts.MainMenu;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SingularityLab.Assets._RobotStars.Scripts.GameCore.PlayerControllers
{
    public class MainMenuPlayerController : BasePlayerController
    {
        [Required] [SerializeField] private MainMenuPlayerRobotsController _robotsController;
        [Required] [SerializeField] private Transform _lootBoxSpawnPoint;

        [SerializeField] private Dictionary<LootBoxType, GameObject> _lootBoxControllers =
            new Dictionary<LootBoxType, GameObject>();

        public MainMenuCameraController MainMenuCameraController => (MainMenuCameraController) CameraController;
        public RobotSO CurrentRobot => _robotsController.CurrentRobot;
        private LootBoxController _lootBoxController;

        public void LoadRobots()
        {
            _robotsController.StartLoadingFromAddressable();
        }

        [Button]
        public void ShowLootBox()
        {
            _robotsController.ShowHideCurrent(false, () => { GenerateLootbox(); });
        }

        private void GenerateLootbox()
        {
            if (_lootBoxController != null)
            {
                Destroy(_lootBoxController.gameObject);
            }

            var canvasController = BaseGameMode.Instance.GetCastedCanvasController<MainMenuUiCanvasesController>();

            MainMenuCameraController.SwitchTo(MainMenuCameraViewType.LootBoxView);

            canvasController.ShowInstant(MainMenuCanvasType.LootBox);
            _lootBoxController = Instantiate(_lootBoxControllers[LootBoxType.Common], _lootBoxSpawnPoint).GetComponent<LootBoxController>();

            if (_lootBoxController)
            {
                _lootBoxController.Open();
            }
        }

        public void CloseLootBoxView()
        {
            var canvasController = BaseGameMode.Instance.GetCastedCanvasController<MainMenuUiCanvasesController>();
            canvasController.ShowInstant(MainMenuCanvasType.Main);

            MainMenuCameraController.SwitchTo(MainMenuCameraViewType.FullRobotView);
        }
    }
}