using UnityEngine;

namespace WinterUniverse
{
    public abstract class BaseInfoConfig : ScriptableObject
    {
        [SerializeField] private string _displayName = "Name";
        [SerializeField, TextArea] private string _description = "Description";
        [SerializeField] private Color _color = Color.white;
        [SerializeField] private Sprite _icon;

        public string DisplayName => _displayName;
        public string Description => _description;
        public Color Color => _color;
        public Sprite Icon => _icon;
    }
}