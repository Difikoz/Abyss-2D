using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Armor Type", menuName = "Winter Universe/Item/Equipment/Armor/New Type")]
    public class ArmorType : BaseInfoConfig
    {
        [SerializeField] private bool _allowUnequip;

        public bool AllowUnequip => _allowUnequip;
    }
}