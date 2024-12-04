using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace WinterUniverse
{
    public class PlayerController : PawnController
    {
        public Action<Interactable, Vector2> OnInteractableChanged;

        private Vector2 _moveInput;
        private Vector2 _cursorPosition;
        private Interactable _interactable;
        private RaycastHit2D _cameraHit;

        public void OnMove(InputValue value)
        {
            _moveInput = value.Get<Vector2>();
        }

        public void OnCursorMove(InputValue value)
        {
            _cursorPosition = value.Get<Vector2>();
        }

        public void OnInteract()
        {
            if (_interactable == null || Vector2.Distance(transform.position, _interactable.PointToInteract.position) > _interactable.DistanceToInteract)
            {
                return;
            }
            if (_interactable.CanInteract(this))
            {
                _interactable.Interact(this);
            }
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            _cameraHit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(_cursorPosition), Vector2.zero, 50f, _detectableMask);
            if (_cameraHit.collider != null)
            {
                if (_cameraHit.collider.TryGetComponent(out Interactable interactable) && interactable != _interactable)
                {
                    _interactable = interactable;
                    OnInteractableChanged?.Invoke(_interactable, _cursorPosition);
                }
            }
            else if (_interactable != null)
            {
                _interactable = null;
                OnInteractableChanged?.Invoke(_interactable, _cursorPosition);
            }
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            _rb.linearVelocity = _moveInput.normalized * _moveSpeed;
        }
    }
}