using System;
using System.Collections.Generic;
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
        public bool IsAttacking;
        public bool IsAiming;
        public bool IsMoving;
        public bool IsRotating;
        public bool IsDashing;
        public bool IsKnockedBack;
        public bool IsDead;

        [Header("HEALTH")]
        public float HealthCurrent;
        public float HealthPercent => HealthCurrent / HealthMaxStat.CurrentValue;

        [SerializeField] private float _healthRegenerationDelayTime = 10f;
        [SerializeField] private float _healthRegenerationTickTime = 0.5f;

        private float _healthRegenerationDelayCurrentTime;
        private float _healthRegenerationTickCurrentTime;

        [Header("ENERGY")]
        public float EnergyCurrent;
        public float EnergyPercent => EnergyCurrent / EnergyMaxStat.CurrentValue;

        [SerializeField] private float _energyRegenerationDelayTime = 5f;
        [SerializeField] private float _energyRegenerationTickTime = 0.25f;

        private float _energyRegenerationDelayCurrentTime;
        private float _energyRegenerationTickCurrentTime;

        [Header("LOCOMOTION")]
        public float ForwardVelocity;
        public float RightVelocity;
        public float TurnVelocity;

        [Header("STATS")]
        private List<Stat> _stats = new();

        public List<Stat> Stats => _stats;

        private Stat _healthMaxStat;
        private Stat _healthRegenerationStat;
        private Stat _energyMaxStat;
        private Stat _energyRegenerationStat;
        private Stat _accelerationStat;
        private Stat _decelerationStat;
        private Stat _moveSpeedStat;
        private Stat _rotateSpeedStat;
        private Stat _dashForceStat;
        private Stat _massStat;

        public Stat HealthMaxStat => _healthMaxStat;
        public Stat HealthRegenerationStat => _healthRegenerationStat;
        public Stat EnergyMaxStat => _energyMaxStat;
        public Stat EnergyRegenerationStat => _energyRegenerationStat;
        public Stat AccelerationStat => _accelerationStat;
        public Stat DecelerationStat => _decelerationStat;
        public Stat MoveSpeedStat => _moveSpeedStat;
        public Stat RotateSpeedStat => _rotateSpeedStat;
        public Stat DashForceStat => _dashForceStat;
        public Stat MassStat => _massStat;

        public void Initialize()
        {
            _pawn = GetComponent<PawnController>();
            CreateStats();
            AssignStats();
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

        #region STATS
        public void AddStatModifier(StatModifierCreator creator)
        {
            foreach (Stat s in _stats)
            {
                if (s.Config == creator.Stat)
                {
                    s.AddModifier(creator.Modifier);
                    break;
                }
            }
            RecalculateStats();
        }

        public void RemoveStatModifier(StatModifierCreator creator)
        {
            foreach (Stat s in _stats)
            {
                if (s.Config == creator.Stat)
                {
                    s.RemoveModifier(creator.Modifier);
                    break;
                }
            }
            RecalculateStats();
        }

        private void CreateStats()
        {
            _stats = new();
            foreach (StatConfig stat in GameManager.StaticInstance.Stats)
            {
                _stats.Add(new(stat));
            }
        }

        private void AssignStats()
        {
            foreach (Stat s in _stats)
            {
                switch (s.Config.DisplayName)
                {
                    case "Health":
                        _healthMaxStat = s;
                        break;
                    case "Health Regeneration":
                        _healthRegenerationStat = s;
                        break;
                    case "Energy":
                        _energyMaxStat = s;
                        break;
                    case "Energy Regeneration":
                        _energyRegenerationStat = s;
                        break;
                    case "Acceleration":
                        _accelerationStat = s;
                        break;
                    case "Deceleration":
                        _decelerationStat = s;
                        break;
                    case "Move Speed":
                        _moveSpeedStat = s;
                        break;
                    case "Rotate Speed":
                        _rotateSpeedStat = s;
                        break;
                    case "Dash Force":
                        _dashForceStat = s;
                        break;
                    case "Mass":
                        _massStat = s;
                        break;
                }
            }
        }

        public void RecalculateStats()
        {

        }
        #endregion

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
            HealthCurrent = Mathf.Clamp(HealthCurrent - value, 0f, HealthMaxStat.CurrentValue);
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
            HealthCurrent = Mathf.Clamp(HealthCurrent + value, 0f, HealthMaxStat.CurrentValue);
            InvokeOnHealthChanged();
        }

        public void InvokeOnHealthChanged()
        {
            OnHealthChanged?.Invoke(HealthCurrent, HealthMaxStat.CurrentValue);
        }

        private void TickHealthRegeneration()
        {
            if (_healthRegenerationDelayCurrentTime >= _healthRegenerationDelayTime)
            {
                if (_healthRegenerationTickCurrentTime >= _healthRegenerationTickTime)
                {
                    if (HealthPercent < 1f)
                    {
                        RestoreHealth(HealthRegenerationStat.CurrentValue);
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
            EnergyCurrent = Mathf.Clamp(EnergyCurrent - value, 0f, EnergyMaxStat.CurrentValue);
            InvokeOnEnergyChanged();
        }

        public void RestoreEnergy(float value)
        {
            if (_pawn.PawnStatus.IsDead)
            {
                return;
            }
            EnergyCurrent = Mathf.Clamp(EnergyCurrent + value, 0f, EnergyMaxStat.CurrentValue);
            InvokeOnEnergyChanged();
        }

        public void InvokeOnEnergyChanged()
        {
            OnEnergyChanged?.Invoke(EnergyCurrent, EnergyMaxStat.CurrentValue);
        }

        private void TickEnergyRegeneration()
        {
            if (_energyRegenerationDelayCurrentTime >= _energyRegenerationDelayTime)
            {
                if (_energyRegenerationTickCurrentTime >= _energyRegenerationTickTime)
                {
                    if (EnergyPercent < 1f)
                    {
                        RestoreEnergy(EnergyRegenerationStat.CurrentValue);
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