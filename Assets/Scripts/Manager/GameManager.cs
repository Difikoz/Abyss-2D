using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField] private GameObject _pawnPrefab;
        [SerializeField] private List<StatConfig> _stats = new();

        private CameraManager _cameraManager;
        private PlayerManager _playerManager;
        private AIManager _aiManager;

        public GameObject PawnPrefab => _pawnPrefab;
        public List<StatConfig> Stats => _stats;
        public CameraManager CameraManager => _cameraManager;
        public PlayerManager PlayerManager => _playerManager;
        public AIManager AIManager => _aiManager;

        private void Start()
        {
            _cameraManager = GetComponentInChildren<CameraManager>();
            _playerManager = GetComponentInChildren<PlayerManager>();
            _aiManager = GetComponentInChildren<AIManager>();
            _playerManager.Initialize();
            _aiManager.Initialize();
            _cameraManager.SetTarget(_playerManager.Pawn.transform);
        }

        private void Update()
        {
            _playerManager.OnUpdate();
        }

        private void LateUpdate()
        {
            _cameraManager.OnLateUpdate();
        }

        private void FixedUpdate()
        {
            _playerManager.OnFixedUpdate();
            _aiManager.OnFixedUpdate();
        }
    }
}