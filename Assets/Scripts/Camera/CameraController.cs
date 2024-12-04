using UnityEngine;

namespace WinterUniverse
{
    public class CameraController : MonoBehaviour
    {
        private Transform _target;

        [SerializeField] private float _followSpeed = 10f;
        [SerializeField] private Vector3 _offset;

        public void OnLateUpdate()
        {
            if (_target == null)
            {
                return;
            }
            transform.position = Vector3.Lerp(transform.position, _target.position + _offset, _followSpeed * Time.deltaTime);
        }

        public void SetTarget(Transform target)
        {
            _target = target;
        }
    }
}