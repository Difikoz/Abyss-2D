using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Weapon Config", menuName = "Winter Universe/Item/Equipment/Weapon/New Config")]
    public class WeaponConfig : ItemConfig
    {
        [SerializeField] private WeaponType _weaponType;
        [SerializeField] private Sprite _sprite;
        [SerializeField] private List<StatModifierCreator> _modifiers = new();

        public WeaponType WeaponType => _weaponType;
        public Sprite Sprite => _sprite;
        public List<StatModifierCreator> Modifiers => _modifiers;
    }
}