using UnityEngine;

namespace WinterUniverse
{
    public class CameraManager : MonoBehaviour
    {
        private Transform _target;
        private float _followSpeed;

        public void OnLateUpdate()
        {
            if (_target != null)
            {
                transform.position = Vector3.MoveTowards(transform.position, _target.position, _followSpeed * Time.deltaTime);
            }
        }

        public void SetTarget(Transform target, float speed = 10f)
        {
            _followSpeed = speed;
            if (target != null)
            {
                _target = target;
            }
            else
            {
                ResetTarget();
            }
        }

        public void ResetTarget()
        {
            _target = null;
        }
    }
}