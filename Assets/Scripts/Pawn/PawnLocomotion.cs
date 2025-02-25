using UnityEngine;

namespace WinterUniverse
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PawnLocomotion : MonoBehaviour
    {
        private PawnController _pawn;
        private Rigidbody2D _rb;
        private Vector2 _moveVelocity;
        private Vector2 _dashVelocity;
        private Vector2 _knockbackVelocity;
        private float _lookAngle;

        [SerializeField] private float _turnAngle = 45f;

        public void Initialize()
        {
            _pawn = GetComponent<PawnController>();
            _rb = GetComponent<Rigidbody2D>();
        }

        public void OnFixedUpdate()
        {
            HandleMovement();
            HandleRotation();
            _rb.linearVelocity = _moveVelocity + _dashVelocity + _knockbackVelocity;
            _rb.rotation = Mathf.MoveTowardsAngle(_rb.rotation, _lookAngle, _pawn.PawnStatus.RotateSpeedStat.CurrentValue * Time.fixedDeltaTime);
            HandleStates();
        }

        private void HandleMovement()
        {
            if (_pawn.PawnStatus.CanMove && _pawn.PawnInput.MoveDirection != Vector2.zero)
            {
                _moveVelocity = Vector2.MoveTowards(_moveVelocity, _pawn.PawnInput.MoveDirection.normalized * _pawn.PawnStatus.MoveSpeedStat.CurrentValue, _pawn.PawnStatus.AccelerationStat.CurrentValue * Time.fixedDeltaTime);
            }
            else
            {
                _moveVelocity = Vector2.MoveTowards(_moveVelocity, Vector2.zero, _pawn.PawnStatus.DecelerationStat.CurrentValue * Time.fixedDeltaTime);
            }
            if (_dashVelocity != Vector2.zero)
            {
                _dashVelocity = Vector2.MoveTowards(_dashVelocity, Vector2.zero, _pawn.PawnStatus.DashForceStat.CurrentValue / 0.5f * Time.fixedDeltaTime);
            }
            if (_knockbackVelocity != Vector2.zero)
            {
                _knockbackVelocity = Vector2.MoveTowards(_knockbackVelocity, Vector2.zero, _pawn.PawnStatus.MassStat.CurrentValue / 100f * Time.fixedDeltaTime);
            }
        }

        private void HandleRotation()
        {
            if (!_pawn.PawnStatus.CanRotate)
            {
                return;
            }
            if (_pawn.PawnInput.LookDirection != Vector2.zero)
            {
                _lookAngle = Mathf.Atan2(_pawn.PawnInput.LookDirection.y, _pawn.PawnInput.LookDirection.x) * Mathf.Rad2Deg;
            }
        }

        private void HandleStates()
        {
            _pawn.PawnStatus.ForwardVelocity = Vector2.Dot(_moveVelocity, transform.right) / _pawn.PawnStatus.MoveSpeedStat.CurrentValue;
            _pawn.PawnStatus.RightVelocity = Vector2.Dot(_moveVelocity, -transform.up) / _pawn.PawnStatus.MoveSpeedStat.CurrentValue;
            _pawn.PawnStatus.TurnVelocity = Vector2.SignedAngle(_pawn.PawnInput.LookDirection, transform.right) * _pawn.PawnStatus.RotateSpeedStat.CurrentValue / _turnAngle / 360f;
            _pawn.PawnStatus.IsMoving = _moveVelocity != Vector2.zero;
            _pawn.PawnStatus.IsRotating = _rb.rotation != _lookAngle;
            _pawn.PawnStatus.IsDashing = _dashVelocity != Vector2.zero;
            _pawn.PawnStatus.IsKnockedBack = _knockbackVelocity != Vector2.zero;
        }

        public void Dash()
        {
            if (_pawn.PawnInput.MoveDirection != Vector2.zero)
            {
                Dash(_pawn.PawnInput.MoveDirection);
            }
            else if (_pawn.PawnInput.LookDirection != Vector2.zero)
            {
                Dash(_pawn.PawnInput.LookDirection);
            }
            else
            {
                Dash(_pawn.transform.right);// dash forward
            }
        }

        public void Dash(Vector2 direction)
        {
            if (!_pawn.PawnStatus.CanDash)
            {
                return;
            }
            _dashVelocity = direction.normalized * _pawn.PawnStatus.DashForceStat.CurrentValue;
        }

        public void Knockback(Vector2 direction, float force)
        {
            _knockbackVelocity += direction.normalized * force;
        }
    }
}