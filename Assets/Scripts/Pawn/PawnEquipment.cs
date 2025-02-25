using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class PawnEquipment : MonoBehaviour
    {
        private PawnController _pawn;
        private int _attackComboIndex;

        [SerializeField] private List<WeaponSlot> _weaponSlots = new();
        [SerializeField] private List<ArmorSlot> _armorSlots = new();

        public List<WeaponSlot> WeaponSlots => _weaponSlots;
        public List<ArmorSlot> ArmorSlots => _armorSlots;

        public void Initialize()
        {
            _pawn = GetComponent<PawnController>();
            foreach (WeaponSlot slot in _weaponSlots)
            {
                slot.Initialize();
            }
        }

        public void EquipWeapon(WeaponConfig weapon, int slot, bool removeNewFromInventory = true, bool addOldToInventory = true)
        {
            EquipWeapon(weapon, _weaponSlots[slot], removeNewFromInventory, addOldToInventory);
        }

        public void EquipWeapon(WeaponConfig weapon, WeaponSlot slot, bool removeNewFromInventory = true, bool addOldToInventory = true)
        {
            if (_pawn.PawnStatus.IsDead || weapon == null)
            {
                return;
            }
            if (removeNewFromInventory)
            {
                _pawn.PawnInventory.RemoveItem(weapon);
            }
            if (slot.Weapon != null)
            {
                if (addOldToInventory)
                {
                    _pawn.PawnInventory.AddItem(slot.Weapon);
                }
                foreach (StatModifierCreator creator in slot.Weapon.Modifiers)
                {
                    _pawn.PawnStatus.RemoveStatModifier(creator);
                }
            }
            slot.Setup(weapon);
            foreach (StatModifierCreator creator in weapon.Modifiers)
            {
                _pawn.PawnStatus.AddStatModifier(creator);
            }
        }

        public void UnequipWeapon(int slot, bool addToInventory = true)
        {
            UnequipWeapon(_weaponSlots[slot], addToInventory);
        }

        public void UnequipWeapon(WeaponSlot slot, bool addToInventory = true)
        {
            if (slot.Weapon != null)
            {
                if (addToInventory)
                {
                    _pawn.PawnInventory.AddItem(slot.Weapon);
                }
                foreach (StatModifierCreator creator in slot.Weapon.Modifiers)
                {
                    _pawn.PawnStatus.RemoveStatModifier(creator);
                }
            }
            slot.Setup(null);
        }

        public void EquipArmor(ArmorConfig armor, bool removeNewFromInventory = true, bool addOldToInventory = true)
        {
            if (_pawn.PawnStatus.IsDead || armor == null)
            {
                return;
            }
            if (removeNewFromInventory)
            {
                _pawn.PawnInventory.RemoveItem(armor);
            }
            foreach (ArmorSlot slot in _armorSlots)
            {
                if (slot.Type == armor.ArmorType)
                {
                    if (slot.Armor != null)
                    {
                        if (addOldToInventory)
                        {
                            _pawn.PawnInventory.AddItem(slot.Armor);
                        }
                        foreach (StatModifierCreator creator in slot.Armor.Modifiers)
                        {
                            _pawn.PawnStatus.RemoveStatModifier(creator);
                        }
                    }
                    slot.Setup(armor);
                    foreach (StatModifierCreator creator in armor.Modifiers)
                    {
                        _pawn.PawnStatus.AddStatModifier(creator);
                    }
                    break;
                }
            }
        }

        public void UnequipArmor(int slot, bool addToInventory = true)
        {
            UnequipArmor(_armorSlots[slot], addToInventory);
        }

        public void UnequipArmor(ArmorType type, bool addToInventory = true)
        {
            foreach (ArmorSlot slot in _armorSlots)
            {
                if (slot.Type == type)
                {
                    UnequipArmor(slot, addToInventory);
                    break;
                }
            }
        }

        public void UnequipArmor(ArmorSlot slot, bool addToInventory = true)
        {
            if (!slot.Type.AllowUnequip)
            {
                return;
            }
            if (slot.Armor != null)
            {
                if (addToInventory)
                {
                    _pawn.PawnInventory.AddItem(slot.Armor);
                }
                foreach (StatModifierCreator creator in slot.Armor.Modifiers)
                {
                    _pawn.PawnStatus.RemoveStatModifier(creator);
                }
            }
            slot.Setup(null);
        }

        public void PerformAttack()
        {
            if (!_pawn.PawnStatus.CanAttack)
            {
                return;
            }
            switch (_attackComboIndex)
            {
                case 0:
                    _pawn.PawnAnimator.PlayActionAnimation($"Attack {(_weaponSlots[0].Weapon != null ? _weaponSlots[0].Weapon.WeaponType.WeaponAttackType : WeaponAttackType.Stab)} Right");
                    break;
                case 1:
                    _pawn.PawnAnimator.PlayActionAnimation($"Attack {(_weaponSlots[1].Weapon != null ? _weaponSlots[1].Weapon.WeaponType.WeaponAttackType : WeaponAttackType.Stab)} Left");
                    break;
            }
        }

        public void OpenDamageCollider()
        {
            switch (_attackComboIndex)
            {
                case 0:
                    _weaponSlots[0].EnableDamageCollider();
                    break;
                case 1:
                    _weaponSlots[1].EnableDamageCollider();
                    break;
            }
        }

        public void CloseDamageCollider()
        {
            switch (_attackComboIndex)
            {
                case 0:
                    _attackComboIndex++;
                    _weaponSlots[0].DisableDamageCollider();
                    break;
                case 1:
                    _attackComboIndex--;
                    _weaponSlots[1].DisableDamageCollider();
                    break;
            }
        }
    }
}