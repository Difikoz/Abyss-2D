using UnityEngine;

namespace WinterUniverse
{
    public class PawnInput : MonoBehaviour
    {
        private PawnController _pawn;

        public Vector2 MoveDirection;
        public Vector2 LookDirection;
        public Vector2 LookPoint;
        public bool AttackInput;
        public bool AimInput;
        public bool DashInput;

        public void Initialize()
        {
            _pawn = GetComponent<PawnController>();
            MoveDirection = Vector2.zero;
            LookDirection = Vector2.zero;
            LookPoint = Vector2.zero;
            AttackInput = false;
            AimInput = false;
            DashInput = false;
        }

        public void OnFixedUpdate()
        {
            if (AttackInput)
            {
                _pawn.PawnEquipment.PerformAttack();
            }
            _pawn.PawnStatus.IsAiming = AimInput;
            if (AimInput)
            {
                LookDirection = LookPoint - (Vector2)transform.position;
            }
            else if (MoveDirection != Vector2.zero)
            {
                LookDirection = MoveDirection;
            }
            if (DashInput)
            {
                _pawn.PawnLocomotion.Dash();
            }
        }
    }
}