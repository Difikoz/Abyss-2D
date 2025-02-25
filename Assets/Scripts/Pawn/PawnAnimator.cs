using UnityEngine;

namespace WinterUniverse
{
    [RequireComponent(typeof(Animator))]
    public class PawnAnimator : MonoBehaviour
    {
        private PawnController _pawn;
        private Animator _animator;

        public void Initialize()
        {
            _pawn = GetComponent<PawnController>();
            _animator = GetComponent<Animator>();
        }

        public void OnFixedUpdate()
        {
            _animator.SetFloat("MoveSpeed", _pawn.PawnStatus.MoveSpeed);
            _animator.SetFloat("ForwardVelocity", _pawn.PawnStatus.ForwardVelocity);
            _animator.SetFloat("RightVelocity", _pawn.PawnStatus.RightVelocity);
            _animator.SetBool("IsMoving", _pawn.PawnStatus.IsMoving);
            _animator.SetBool("IsDashing", _pawn.PawnStatus.IsDashing);
        }

        public void PlayActionAnimation(string name, float fadeTime = 0.1f, bool isPerfomingAction = true, bool canMove = false, bool canRotate = false, bool canDash = false)
        {
            _pawn.PawnStatus.IsPerfomingAction = isPerfomingAction;
            _pawn.PawnStatus.CanMove = canMove;
            _pawn.PawnStatus.CanRotate = canRotate;
            _pawn.PawnStatus.CanDash = canDash;
            _animator.CrossFade(name, fadeTime);
        }
    }
}