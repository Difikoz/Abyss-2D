using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class ArmorSlot : MonoBehaviour
    {
        [SerializeField] private ArmorType _type;
        [SerializeField] private List<SpriteRenderer> _spriteRenderers = new();

        private ArmorConfig _armor;

        public ArmorConfig Armor => _armor;
        public ArmorType Type => _type;

        public void Setup(ArmorConfig armor)
        {
            _armor = armor;
            if (armor != null)
            {
                foreach (SpriteRenderer sr in _spriteRenderers)
                {
                    sr.sprite = armor.Sprite;
                }
            }
            else
            {
                foreach (SpriteRenderer sr in _spriteRenderers)
                {
                    sr.sprite = null;
                }
            }
        }
    }
}