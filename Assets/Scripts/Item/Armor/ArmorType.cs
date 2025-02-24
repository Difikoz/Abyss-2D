using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Armor Type", menuName = "Winter Universe/Item/Armor/New Type")]
    public class ArmorType : ScriptableObject
    {
        [SerializeField] private string _displayName = "Name";
        [SerializeField] private Color _color = Color.white;
        [SerializeField] private Sprite _icon;
        [SerializeField] private bool _allowUnequip;

        public string DisplayName => _displayName;
        public Color Color => _color;
        public Sprite Icon => _icon;
        public bool AllowUnequip => _allowUnequip;
    }
}