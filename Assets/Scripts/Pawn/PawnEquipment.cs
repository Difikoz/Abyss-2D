using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class PawnEquipment : MonoBehaviour
    {
        private PawnController _pawn;
        private List<WeaponSlot> _weaponSlots = new();
        private List<ArmorSlot> _armorSlots = new();

        public List<WeaponSlot> WeaponSlots => _weaponSlots;
        public List<ArmorSlot> ArmorSlots => _armorSlots;

        public void Initialize()
        {
            _pawn = GetComponent<PawnController>();
            WeaponSlot[] weaponSlots = GetComponentsInChildren<WeaponSlot>();
            foreach (WeaponSlot slot in weaponSlots)
            {
                _weaponSlots.Add(slot);
            }
            ArmorSlot[] armorSlots = GetComponentsInChildren<ArmorSlot>();
            foreach (ArmorSlot slot in armorSlots)
            {
                _armorSlots.Add(slot);
            }
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

        public void UnequipArmor(ArmorType type, bool addToInventory = true)
        {
            if (!type.AllowUnequip)
            {
                return;
            }
            foreach (ArmorSlot slot in _armorSlots)
            {
                if (slot.Type == type)
                {
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
                    break;
                }
            }
        }
    }
}