using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Weapon Type", menuName = "Winter Universe/Item/Equipment/Weapon/New Type")]
    public class WeaponType : BaseInfoConfig
    {
        [SerializeField] private WeaponAttackType _weaponAttackType;

        public WeaponAttackType WeaponAttackType => _weaponAttackType;
    }
}