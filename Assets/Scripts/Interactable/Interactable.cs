using UnityEngine;

namespace WinterUniverse
{
    public abstract class Interactable : MonoBehaviour
    {
        [SerializeField] private string _interactionText = "Interact";
        [SerializeField] private Transform _pointToInteract;

        public Transform PointToInteract => _pointToInteract;

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