using Lean.Pool;
using UnityEngine;

namespace WinterUniverse
{
    public class PlayerManager : MonoBehaviour
    {
        private PawnController _pawn;
        private PlayerInputActions _inputActions;

        [SerializeField] private GameObject _pawnPrefab;
        [SerializeField] private Transform _spawnPoint;

        private void Start()
        {
            _pawn = LeanPool.Spawn(_pawnPrefab, _spawnPoint.position, Quaternion.identity).GetComponent<PawnController>();
            _inputActions = new();
            _inputActions.Enable();
        }

        private void Update()
        {
            _pawn.PawnInput.MoveDirection = _inputActions.Pawn.Move.ReadValue<Vector2>();
            _pawn.PawnInput.LookPoint = Camera.main.ScreenToWorldPoint(_inputActions.Pawn.Cursor.ReadValue<Vector2>());
            _pawn.PawnInput.AimInput = _inputActions.Pawn.Aim.IsPressed();
        }
    }
}