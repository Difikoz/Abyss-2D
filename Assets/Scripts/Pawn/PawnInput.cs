using UnityEngine;

namespace WinterUniverse
{
    public class PawnInput : MonoBehaviour
    {
        public Vector2 MoveDirection;
        public Vector2 LookDirection;
        public Vector2 LookPoint;
        public bool AttackInput;

        public void Initialize()
        {
            MoveDirection = Vector2.zero;
            LookDirection = Vector2.zero;
            LookPoint = Vector2.zero;
            AttackInput = false;
        }
    }
}