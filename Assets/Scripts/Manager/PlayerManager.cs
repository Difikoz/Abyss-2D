using UnityEngine;
using UnityEngine.InputSystem;

namespace WinterUniverse
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private PawnController _pawn;

        public void OnMove(InputValue value)
        {
            _pawn.PawnInput.MoveDirection = value.Get<Vector2>();
        }
    }
}