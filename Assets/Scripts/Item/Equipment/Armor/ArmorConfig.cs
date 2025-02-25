using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Armor Config", menuName = "Winter Universe/Item/Equipment/Armor/New Config")]
    public class ArmorConfig : ItemConfig
    {
        [SerializeField] private ArmorType _armorType;
        [SerializeField] private Sprite _sprite;
        [SerializeField] private List<StatModifierCreator> _modifiers = new();

        public ArmorType ArmorType => _armorType;
        public Sprite Sprite => _sprite;
        public List<StatModifierCreator> Modifiers => _modifiers;
    }
}