using UnityEngine;

namespace WinterUniverse
{
    public class ItemConfig : ScriptableObject
    {
        [SerializeField] private string _displayName = "Name";
        [SerializeField, TextArea] private string _description = "Description";
        [SerializeField] private Color _color;
        [SerializeField] private Sprite _icon;

        public string DisplayName => _displayName;
        public string Description => _description;
        public Color Color => _color;
        public Sprite Icon => _icon;
    }
}