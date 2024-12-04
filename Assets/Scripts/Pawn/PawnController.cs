using UnityEngine;

namespace WinterUniverse
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Animator))]
    public abstract class PawnController : MonoBehaviour
    {
        protected Rigidbody2D _rb;
        protected Animator _anim;

        [SerializeField] protected float _moveSpeed = 4f;
        [SerializeField] protected LayerMask _detectableMask;

        public virtual void OnAwake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _anim = GetComponent<Animator>();
        }

        public virtual void OnUpdate()
        {

        }

        public virtual void OnFixedUpdate()
        {

        }
    }
}