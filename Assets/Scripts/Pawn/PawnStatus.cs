using System;
using UnityEngine;

namespace WinterUniverse
{
    public class PawnStatus : MonoBehaviour
    {
        private PawnController _pawn;

        [Header("EVENTS")]
        public Action<float, float> OnHealthChanged;
        public Action<float, float> OnEnergyChanged;
        public Action<PawnController> OnTakedDamageFromSource;
        public Action OnDied;

        [Header("STATES")]
        public bool IsPerfomingAction;
        public bool CanMove;
        public bool CanRotate;
        public bool CanDash;
        public bool IsAiming;
        public bool IsMoving;
        public bool IsRotating;
        public bool IsDashing;
        public bool IsKnockedBack;
        public bool IsDead;

        [Header("HEALTH")]
        public float HealthCurrent;
        public float HealthMax;
        public float HealthRegeneration;
        public float HealthPercent => HealthCurrent / HealthMax;

        [SerializeField] private float _healthRegenerationDelayTime = 10f;
        [SerializeField] private float _healthRegenerationTickTime = 0.5f;

        private float _healthRegenerationDelayCurrentTime;
        private float _healthRegenerationTickCurrentTime;

        [Header("ENERGY")]
        public float EnergyCurrent;
        public float EnergyMax;
        public float EnergyRegeneration;
        public float EnergyPercent => EnergyCurrent / EnergyMax;

        [SerializeField] private float _energyRegenerationDelayTime = 5f;
        [SerializeField] private float _energyRegenerationTickTime = 0.25f;

        private float _energyRegenerationDelayCurrentTime;
        private float _energyRegenerationTickCurrentTime;

        [Header("LOCOMOTION")]
        public float Acceleration;
        public float Deceleration;
        public float MoveSpeed;
        public float RotateSpeed;
        public float DashForce;
        public float Mass;
        public float ForwardVelocity;
        public float RightVelocity;
        public float TurnVelocity;

        public void Initialize()
        {
            _pawn = GetComponent<PawnController>();
            IsPerfomingAction = false;
            CanMove = true;
            CanRotate = true;
            CanDash = true;
            IsDead = false;
        }

        public void OnFixedUpdate()
        {
            TickHealthRegeneration();
            TickEnergyRegeneration();
        }

        #region HEALTH
        public void DamageHealth(float value, PawnController source = null)
        {
            if (IsDead)
            {
                return;
            }
            if (source != null)
            {
                OnTakedDamageFromSource?.Invoke(source);
            }
            _healthRegenerationDelayCurrentTime = 0f;
            HealthCurrent = Mathf.Clamp(HealthCurrent - value, 0f, HealthMax);
            if (HealthCurrent <= 0f)
            {
                _pawn.Die();
            }
            else
            {
                InvokeOnHealthChanged();
            }
        }

        public void RestoreHealth(float value)
        {
            if (_pawn.PawnStatus.IsDead)
            {
                return;
            }
            HealthCurrent = Mathf.Clamp(HealthCurrent + value, 0f, HealthMax);
            InvokeOnHealthChanged();
        }

        public void InvokeOnHealthChanged()
        {
            OnHealthChanged?.Invoke(HealthCurrent, HealthMax);
        }

        private void TickHealthRegeneration()
        {
            if (_healthRegenerationDelayCurrentTime >= _healthRegenerationDelayTime)
            {
                if (_healthRegenerationTickCurrentTime >= _healthRegenerationTickTime)
                {
                    if (HealthPercent < 1f)
                    {
                        RestoreHealth(HealthRegeneration);
                    }
                    _healthRegenerationTickCurrentTime = 0f;
                }
                else
                {
                    _healthRegenerationTickCurrentTime += Time.fixedDeltaTime;
                }
            }
            else
            {
                _healthRegenerationDelayCurrentTime += Time.fixedDeltaTime;
            }
        }
        #endregion

        #region ENERGY

        public void DamageEnergy(float value)
        {
            if (IsDead)
            {
                return;
            }
            _energyRegenerationDelayCurrentTime = 0f;
            EnergyCurrent = Mathf.Clamp(EnergyCurrent - value, 0f, EnergyMax);
            InvokeOnEnergyChanged();
        }

        public void RestoreEnergy(float value)
        {
            if (_pawn.PawnStatus.IsDead)
            {
                return;
            }
            EnergyCurrent = Mathf.Clamp(EnergyCurrent + value, 0f, EnergyMax);
            InvokeOnEnergyChanged();
        }

        public void InvokeOnEnergyChanged()
        {
            OnEnergyChanged?.Invoke(EnergyCurrent, EnergyMax);
        }

        private void TickEnergyRegeneration()
        {
            if (_energyRegenerationDelayCurrentTime >= _energyRegenerationDelayTime)
            {
                if (_energyRegenerationTickCurrentTime >= _energyRegenerationTickTime)
                {
                    if (EnergyPercent < 1f)
                    {
                        RestoreEnergy(EnergyRegeneration);
                    }
                    _energyRegenerationTickCurrentTime = 0f;
                }
                else
                {
                    _energyRegenerationTickCurrentTime += Time.fixedDeltaTime;
                }
            }
            else
            {
                _energyRegenerationDelayCurrentTime += Time.fixedDeltaTime;
            }
        }
        #endregion
    }
}