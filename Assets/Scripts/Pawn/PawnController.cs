using UnityEngine;

namespace WinterUniverse
{
    [RequireComponent(typeof(PawnAnimator))]
    [RequireComponent(typeof(PawnCombat))]
    [RequireComponent(typeof(PawnEquipment))]
    [RequireComponent(typeof(PawnInput))]
    [RequireComponent(typeof(PawnInventory))]
    [RequireComponent(typeof(PawnLocomotion))]
    [RequireComponent(typeof(PawnStatus))]
    public class PawnController : MonoBehaviour
    {
        private PawnAnimator _pawnAnimator;
        private PawnCombat _pawnCombat;
        private PawnEquipment _pawnEquipment;
        private PawnInput _pawnInput;
        private PawnInventory _pawnInventory;
        private PawnLocomotion _pawnLocomotion;
        private PawnStatus _pawnStatus;

        public PawnAnimator PawnAnimator => _pawnAnimator;
        public PawnCombat PawnCombat => _pawnCombat;
        public PawnEquipment PawnEquipment => _pawnEquipment;
        public PawnInput PawnInput => _pawnInput;
        public PawnInventory PawnInventory => _pawnInventory;
        public PawnLocomotion PawnLocomotion => _pawnLocomotion;
        public PawnStatus PawnStatus => _pawnStatus;

        public void InitializePawn()
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
            _pawnInventory = GetComponent<PawnInventory>();
            _pawnLocomotion = GetComponent<PawnLocomotion>();
            _pawnStatus = GetComponent<PawnStatus>();
        }

        private void InitializeComponents()
        {
            _pawnAnimator.Initialize();
            _pawnCombat.Initialize();
            _pawnEquipment.Initialize();
            _pawnInput.Initialize();
            _pawnInventory.Initialize();
            _pawnLocomotion.Initialize();
            _pawnStatus.Initialize();
        }

        public void OnFixedUpdate()
        {
            _pawnInput.OnFixedUpdate();
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
            if (_pawnStatus.IsDead)
            {
                return;
            }
            _pawnStatus.HealthCurrent = 0f;
            _pawnStatus.InvokeOnHealthChanged();
            _pawnStatus.IsDead = true;
            _pawnAnimator.PlayActionAnimation("Death");
        }

        public void Revive()
        {
            _pawnStatus.IsDead = false;
            _pawnStatus.RestoreHealth(_pawnStatus.HealthMaxStat.CurrentValue);
            _pawnStatus.RestoreEnergy(_pawnStatus.EnergyMaxStat.CurrentValue);
            _pawnAnimator.PlayActionAnimation("Revive");
        }
    }
}