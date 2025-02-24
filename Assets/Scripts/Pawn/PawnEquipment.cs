using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class PawnEquipment : MonoBehaviour
    {
        private PawnController _pawn;
        private List<ArmorSlot> _armorSlots = new();

        public void Initialize()
        {
            _pawn = GetComponent<PawnController>();
            ArmorSlot[] armorSlots = GetComponentsInChildren<ArmorSlot>();
            foreach (ArmorSlot slot in armorSlots)
            {
                _armorSlots.Add(slot);
            }
        }

        public void EquipArmor(ArmorConfig armor, bool removeNewFromInventory = true, bool addOldToInventory = true)
        {
            if (_pawn.PawnStatus.IsDead || armor == null)
            {
                return;
            }
            if (removeNewFromInventory)
            {
                // remove from inventory
            }
            foreach (ArmorSlot slot in _armorSlots)
            {
                if (slot.Type == armor.ArmorType)
                {
                    if (slot.Armor != null)
                    {
                        if (addOldToInventory)
                        {
                            // add to inventory
                        }
                        // remove stat modifiers
                    }
                    slot.Setup(armor);
                    // add stat modifiers
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
                            // add to inventory
                        }
                        // remove stat modifiers
                    }
                    slot.Setup(null);
                    break;
                }
            }
        }
    }
}