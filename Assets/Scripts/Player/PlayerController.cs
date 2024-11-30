using UnityEngine;
using UnityEngine.InputSystem;

namespace WinterUniverse
{
    public class PlayerController : PawnController
    {
        [SerializeField] private float _distanceToInteract = 2f;

        private Vector2 _moveInput;
        private Vector2 _cursorPosition;
        private Interactable _interactable;
        private RaycastHit2D _cameraHit;

        public void OnrMove(InputValue value)
        {
            _moveInput = value.Get<Vector2>();
        }

        public void OnCursorMove(InputValue value)
        {
            _cursorPosition = value.Get<Vector2>();
        }

        public void OnInteract()
        {
            if (_interactable == null || Vector2.Distance(transform.position, _interactable.PointToInteract.position) < _distanceToInteract)
            {
                return;
            }
            if (_interactable.CanInteract(this))
            {
                _interactable.Interact(this);
            }
        }

        protected override void Update()
        {
            base.Update();
            _cameraHit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(_cursorPosition), Vector2.zero, 50f, _detectableMask);
            if (_cameraHit.collider != null)
            {
                if (_cameraHit.collider.TryGetComponent(out Interactable interactable) && interactable != _interactable)
                {
                    _interactable = interactable;
                    // get and show text
                }
            }
            else if (_interactable != null)
            {
                _interactable = null;
                // reset text
            }
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            _rb.linearVelocity = _moveInput.normalized * _moveSpeed;
        }
    }
}