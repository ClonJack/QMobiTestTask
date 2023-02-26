using UnityEngine;

namespace ShipGame
{
    public class GameEntry : MonoBehaviour
    {
        [SerializeField] private BaseShip _playerPrefab;
        [SerializeField] private PoolService _poolServicePrefab;
        [SerializeField] private PlanetService _planetServicePrefab;
        [SerializeField] private EnemyService _enemyServicePrefab;
        [SerializeField] private UIService _uiServicePrefab;

        private SpaceshipPlayer _player;
        private PoolService _poolService;
        private PlanetService _planetService;
        private EnemyService _enemyService;
        private UIService _uiService;
        private LevelService _levelService;

        private void Awake()
        {
            BindPools();
            BindPlayer();
            BindPlanets();
            BindEnemy();
            BindUI();
            BindLevel();
        }

        private void BindLevel()
        {
            _levelService = new LevelService();
            _levelService.Init(_player);
        }
        private void BindUI()
        {
            _uiService = Instantiate(_uiServicePrefab);
            _uiService.Init(_player);
        }
        private void BindPlanets()
        {
            _planetService = Instantiate(_planetServicePrefab);
            _planetService.SetPoolService(_poolService);
        }
        private void BindPools()
        {
            _poolService = Instantiate(_poolServicePrefab);
            _poolService.Init();
        }
        private void BindPlayer()
        {
            _player = Instantiate(_playerPrefab) as SpaceshipPlayer;
            _player.SetPoolService(_poolService);
            _player.Init();
        }
        private void BindEnemy()
        {
            _enemyService = Instantiate(_enemyServicePrefab);
            _enemyService.SetPoolService(_poolService);
            _enemyService.Init(_poolService, _player.transform);
        }
    }
}