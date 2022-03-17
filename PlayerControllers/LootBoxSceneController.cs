using System;
using System.Collections.Generic;
using System.Linq;
using SingularityLab.Assets._RobotStars.Scripts.GameCore.Lootboxes;
using SingularityLab.Assets._RobotStars.Scripts.GameScene;
using SingularityLab.Assets._RobotStars.Scripts.GameUI;
using SingularityLab.Assets._RobotStars.Scripts.GameUI.MainMenu;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SingularityLab.Assets._RobotStars.Scripts.GameCore.PlayerControllers
{
    public class LootBoxSceneController : SerializedMonoBehaviour
    {
        [SerializeField, Required] private Transform _lootBoxSpawnPoint;
        [SerializeField] private Dictionary<LootBoxType, GameObject> _lootBoxController = new Dictionary<LootBoxType, GameObject>();

        [SerializeField] private LootBoxCanvasController _lootBoxCanvasController;

        private SceneLoader _sceneLoader;
        private LootBoxController _lootBox;

        private void Awake()
        {
            _sceneLoader = new SceneLoader(new LoadingScreenProgress());
        }

        private void Start()
        {
            if (_lootBoxCanvasController.gameObject.activeInHierarchy == true)
            {
                _lootBoxCanvasController.gameObject.SetActive(false);
            }
            
            LoadingScreenController.Hide();
            ShowLootBox();
            
            _lootBox.SubscribeOnComplete(OnAnimationComplete);
        }

        [Button]
        public void ShowLootBox()
        {
            if (_lootBox != null)
            {
                _lootBox.UnsubscribeOnComplete(OnAnimationComplete);

                var parent = _lootBox.transform.parent;
                Destroy(parent.gameObject);
            }
            
            var randomType = GetRandomLootBoxType();
            
            _lootBox = Instantiate(_lootBoxController[LootBoxType.Epic], _lootBoxSpawnPoint).GetComponentInChildren<LootBoxController>();
        }

        [Button]
        private LootBoxType GetRandomLootBoxType()
        {
            var lootBoxType = Enum.GetValues(typeof(LootBoxType)).Cast<LootBoxType>().ToList();
            var randomLootBox = Random.Range(0, lootBoxType.Count());
            
            return lootBoxType[randomLootBox];
        }

        private void OnAnimationComplete()
        {
            _lootBoxCanvasController.gameObject.SetActive(true);
        }
    }
}