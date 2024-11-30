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

        protected virtual void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _anim = GetComponent<Animator>();
        }

        protected virtual void Start()
        {

        }

        protected virtual void OnEnable()
        {

        }

        protected virtual void OnDisable()
        {

        }

        protected virtual void Update()
        {

        }

        protected virtual void FixedUpdate()
        {

        }
    }
}