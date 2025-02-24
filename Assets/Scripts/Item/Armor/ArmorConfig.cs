using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Armor Config", menuName = "Winter Universe/Item/Armor/New Config")]
    public class ArmorConfig : ItemConfig
    {
        [SerializeField] private ArmorType _armorType;
        [SerializeField] private Sprite _sprite;

        public ArmorType ArmorType => _armorType;
        public Sprite Sprite => _sprite;
    }
}