using SingularityLab.Assets._RobotStars.Scripts.GameCore.GameModes;
using SingularityLab.Assets._RobotStars.Scripts.GameNetwork.ObjectSpawner;
using SingularityLab.Assets._RobotStars.Scripts.GameNetwork.ObjectSpawner.PlayersSpawnStrategy;
using SingularityLab.Assets._RobotStars.Scripts.GameNetwork.Player;
using SingularityLab.Assets._RobotStars.Scripts.GameNetwork.Repository.Abstraction;
using SingularityLab.Assets._RobotStars.Scripts.GamePlayer.PlayerUI;
using UnityEngine;

namespace SingularityLab.Assets._RobotStars.Scripts.GameCore.PlayerControllers
{
    public class TeamFightPlayerController : BasePlayerController, IPlayersSpawner
    {
        private TurnBasedTeamFightGameMode _turnBasedTeamFightGameMode;
        private RobotInfoMono _localPlayerTurnController;

        public GameObject LocalCharacter { get; private set; }

        private void Awake()
        {
            _turnBasedTeamFightGameMode = BaseGameMode.CastedInstance<TurnBasedTeamFightGameMode>();
        }

        public GameObject GetLocalCharacter()
        {
            return LocalCharacter;
        }

        public void Spawn(IPlayersInfoController playersInfoController)
        {
            IPlayersSpawner playersSpawner;

            if (BaseGameMode.CastedInstance<BaseMultiplayerGameMode>().IsOffline())
            {
                playersSpawner = new DefaultSpawnPlayersStrategy();
            }
            else
            {
                playersSpawner = new PhotonSpawnPlayersStrategy();
            }

            playersSpawner.Spawn(playersInfoController);

            LocalCharacter = playersSpawner.GetLocalCharacter();
        }
    }
}