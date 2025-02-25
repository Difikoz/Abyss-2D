using UnityEngine;

namespace WinterUniverse
{
    public class ToggleActionFlags : StateMachineBehaviour
    {
        private PawnController _pawn;
        [SerializeField] private bool _addIsPerfomingAction = false;
        [SerializeField] private bool _removeIsPerfomingAction = true;
        [SerializeField] private bool _addCanMove = true;
        [SerializeField] private bool _removeCanMove = false;
        [SerializeField] private bool _addCanRotate = true;
        [SerializeField] private bool _removeCanRotate = false;
        [SerializeField] private bool _addCanDash = true;
        [SerializeField] private bool _removeCanDash = false;
        [SerializeField] private bool _addCanAttack = true;
        [SerializeField] private bool _removeCanAttack = false;

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _pawn = animator.GetComponent<PawnController>();
            if (_addIsPerfomingAction)
            {
                _pawn.PawnStatus.IsPerfomingAction = true;
            }
            else if (_removeIsPerfomingAction)
            {
                _pawn.PawnStatus.IsPerfomingAction = false;
            }
            if (_addCanMove)
            {
                _pawn.PawnStatus.CanMove = true;
            }
            else if (_removeCanMove)
            {
                _pawn.PawnStatus.CanMove = false;
            }
            if (_addCanRotate)
            {
                _pawn.PawnStatus.CanRotate = true;
            }
            else if (_removeCanRotate)
            {
                _pawn.PawnStatus.CanRotate = false;
            }
            if (_addCanDash)
            {
                _pawn.PawnStatus.CanDash = true;
            }
            else if (_removeCanDash)
            {
                _pawn.PawnStatus.CanDash = false;
            }
            if (_addCanAttack)
            {
                _pawn.PawnStatus.CanAttack = true;
            }
            else if (_removeCanAttack)
            {
                _pawn.PawnStatus.CanAttack = false;
            }
        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    
        //}

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    
        //}

        // OnStateMove is called right after Animator.OnAnimatorMove()
        //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    // Implement code that processes and affects root motion
        //}

        // OnStateIK is called right after Animator.OnAnimatorIK()
        //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    // Implement code that sets up animation IK (inverse kinematics)
        //}
    }
}