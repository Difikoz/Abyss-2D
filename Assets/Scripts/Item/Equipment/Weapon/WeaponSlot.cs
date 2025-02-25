using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class WeaponSlot : MonoBehaviour
    {
        [SerializeField] private Transform _shootPoint;
        [SerializeField] private List<SpriteRenderer> _spriteRenderers = new();

        private WeaponConfig _weapon;

        public WeaponConfig Weapon => _weapon;
        public Transform ShootPoint => _shootPoint;

        public void Setup(WeaponConfig weapon)
        {
            _weapon = weapon;
            if (weapon != null)
            {
                foreach (SpriteRenderer sr in _spriteRenderers)
                {
                    sr.sprite = weapon.Sprite;
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