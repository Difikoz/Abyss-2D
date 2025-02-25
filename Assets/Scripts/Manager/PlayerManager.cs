using Lean.Pool;
using UnityEngine;

namespace WinterUniverse
{
    public class PlayerManager : MonoBehaviour
    {
        private PawnController _pawn;
        private PlayerInputActions _inputActions;

        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private WeaponConfig _startWeaponRight;
        [SerializeField] private WeaponConfig _startWeaponLeft;

        public PawnController Pawn => _pawn;

        public void Initialize()
        {
            _inputActions = new();
            _inputActions.Enable();
            _pawn = LeanPool.Spawn(GameManager.StaticInstance.PawnPrefab, _spawnPoint.position, Quaternion.identity).GetComponent<PawnController>();
            _pawn.InitializePawn();
            _pawn.Revive();
            _pawn.PawnEquipment.EquipWeapon(_startWeaponRight, 0);
            _pawn.PawnEquipment.EquipWeapon(_startWeaponLeft, 1);
        }

        public void OnUpdate()
        {
            _pawn.PawnInput.MoveDirection = _inputActions.Pawn.Move.ReadValue<Vector2>();
            _pawn.PawnInput.LookPoint = Camera.main.ScreenToWorldPoint(_inputActions.Pawn.Cursor.ReadValue<Vector2>());
            _pawn.PawnInput.AttackInput = _inputActions.Pawn.Attack.IsPressed();
            _pawn.PawnInput.AimInput = _inputActions.Pawn.Aim.IsPressed();
            _pawn.PawnInput.DashInput = _inputActions.Pawn.Dash.IsPressed();
        }

        public void OnFixedUpdate()
        {
            _pawn.OnFixedUpdate();
        }
    }
}