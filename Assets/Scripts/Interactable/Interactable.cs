using UnityEngine;

namespace WinterUniverse
{
    public abstract class Interactable : MonoBehaviour
    {
        [SerializeField] private string _interactionText = "Interact";
        [SerializeField] private Transform _pointToInteract;
        [SerializeField] private float _distanceToInteract = 1f;

        public Transform PointToInteract => _pointToInteract;
        public float DistanceToInteract => _distanceToInteract;

        public virtual string GetText()
        {
            return _interactionText;
        }

        public virtual bool CanInteract(PlayerController player)
        {
            return true;
        }

        public abstract void Interact(PlayerController player);
    }
}