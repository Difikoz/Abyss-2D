using UnityEngine;

namespace WinterUniverse
{
    public class WeaponSlot : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;

        [SerializeField] private WeaponConfig _weapon;
        [SerializeField] private PolygonCollider2D _collider;

        public WeaponConfig Weapon => _weapon;

        public void Initialize()
        {
            _collider = _spriteRenderer.GetComponent<PolygonCollider2D>();
            DisableDamageCollider();
        }

        public void Setup(WeaponConfig weapon)
        {
            if (_collider != null)
            {
                Destroy(_collider);
            }
            _weapon = weapon;
            if (weapon != null)
            {
                _spriteRenderer.sprite = weapon.Sprite;
                _collider = _spriteRenderer.gameObject.AddComponent<PolygonCollider2D>();
                _collider.isTrigger = true;
            }
            else
            {
                _spriteRenderer.sprite = null;
            }
        }

        public void EnableDamageCollider()
        {
            if (_collider == null)
            {
                return;
            }
            _collider.enabled = true;
        }

        public void DisableDamageCollider()
        {
            if (_collider == null)
            {
                return;
            }
            _collider.enabled = false;
        }
    }
}