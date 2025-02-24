using UnityEngine;

namespace WinterUniverse
{
    [RequireComponent(typeof(PawnAnimator))]
    [RequireComponent(typeof(PawnCombat))]
    [RequireComponent(typeof(PawnEquipment))]
    [RequireComponent(typeof(PawnInput))]
    [RequireComponent(typeof(PawnLocomotion))]
    [RequireComponent(typeof(PawnStatus))]
    public class PawnController : MonoBehaviour
    {
        private PawnAnimator _pawnAnimator;
        private PawnCombat _pawnCombat;
        private PawnEquipment _pawnEquipment;
        private PawnInput _pawnInput;
        private PawnLocomotion _pawnLocomotion;
        private PawnStatus _pawnStatus;

        public PawnAnimator PawnAnimator => _pawnAnimator;
        public PawnCombat PawnCombat => _pawnCombat;
        public PawnEquipment PawnEquipment => _pawnEquipment;
        public PawnInput PawnInput => _pawnInput;
        public PawnLocomotion PawnLocomotion => _pawnLocomotion;
        public PawnStatus PawnStatus => _pawnStatus;

        private void Awake()
        {
            GetComponents();
            InitializeComponents();
        }

        private void GetComponents()
        {
            _pawnAnimator = GetComponent<PawnAnimator>();
            _pawnCombat = GetComponent<PawnCombat>();
            _pawnEquipment = GetComponent<PawnEquipment>();
            _pawnInput = GetComponent<PawnInput>();
            _pawnLocomotion = GetComponent<PawnLocomotion>();
            _pawnStatus = GetComponent<PawnStatus>();
        }

        private void InitializeComponents()
        {
            _pawnAnimator.Initialize();
            _pawnCombat.Initialize();
            _pawnEquipment.Initialize();
            _pawnInput.Initialize();
            _pawnLocomotion.Initialize();
            _pawnStatus.Initialize();
        }

        private void FixedUpdate()
        {
            _pawnStatus.OnFixedUpdate();
            _pawnLocomotion.OnFixedUpdate();
            _pawnAnimator.OnFixedUpdate();
        }

        public void Die()
        {
            //if (_statusModule == null)
            //{
            //    return;
            //}
            _pawnStatus.HealthCurrent = 0f;
            _pawnStatus.InvokeOnHealthChanged();
            _pawnStatus.IsDead = true;
            _pawnAnimator.PlayActionAnimation("Death");
        }

        public void Revive()
        {
            _pawnStatus.IsDead = false;
            _pawnStatus.RestoreHealth(_pawnStatus.HealthMax);
            _pawnStatus.RestoreEnergy(_pawnStatus.EnergyMax);
            _pawnAnimator.PlayActionAnimation("Revive");
        }
    }
}