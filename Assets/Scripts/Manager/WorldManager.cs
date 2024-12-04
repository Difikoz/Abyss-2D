using UnityEngine;
using UnityEngine.SceneManagement;

namespace WinterUniverse
{
    public class WorldManager : Singleton<WorldManager>
    {
        private PlayerController _player;
        private CameraController _camera;
        private InteractableBarUI _interactableBar;

        public PlayerController Player => _player;
        public CameraController Camera => _camera;

        protected override void Awake()
        {
            base.Awake();
            _player = FindFirstObjectByType<PlayerController>();
            _camera = FindFirstObjectByType<CameraController>();
            _interactableBar = FindFirstObjectByType<InteractableBarUI>();
            _player.OnAwake();
            _camera.SetTarget(_player.transform);
            _interactableBar.OnAwake();
        }

        private void Update()
        {
            _player.OnUpdate();
        }

        private void FixedUpdate()
        {
            _player.OnFixedUpdate();
        }

        private void LateUpdate()
        {
            _camera.OnLateUpdate();
        }

        public void LoadScene(int index)
        {
            _interactableBar.OnSceneChanged();
            SceneManager.LoadScene(index, LoadSceneMode.Single);
        }
    }
}