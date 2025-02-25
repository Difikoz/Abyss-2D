using Lean.Pool;
using UnityEngine;

namespace WinterUniverse
{
    public class PlayerManager : MonoBehaviour
    {
        private PawnController _pawn;
        private PlayerInputActions _inputActions;

        [SerializeField] private Transform _spawnPoint;

        private void Awake()
        {
            _pawn = LeanPool.Spawn(GameManager.StaticInstance.PawnPrefab, _spawnPoint.position, Quaternion.identity).GetComponent<PawnController>();
        }

        private void Start()
        {
            _inputActions = new();
            _inputActions.Enable();
            _pawn.InitializePawn();
        }

        private void Update()
        {
            _pawn.PawnInput.MoveDirection = _inputActions.Pawn.Move.ReadValue<Vector2>();
            _pawn.PawnInput.LookPoint = Camera.main.ScreenToWorldPoint(_inputActions.Pawn.Cursor.ReadValue<Vector2>());
            _pawn.PawnInput.AttackInput = _inputActions.Pawn.Attack.IsPressed();
            _pawn.PawnInput.AimInput = _inputActions.Pawn.Aim.IsPressed();
            _pawn.PawnInput.DashInput = _inputActions.Pawn.Dash.IsPressed();
        }

        private void FixedUpdate()
        {
            _pawn.OnFixedUpdate();
        }
    }
}