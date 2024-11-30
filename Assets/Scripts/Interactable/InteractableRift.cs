using UnityEngine;

namespace WinterUniverse
{
    public class InteractableRift : Interactable
    {
        public override void Interact(PlayerController player)
        {
            // load dungeon scene
            Debug.Log("Interacted with Rift");
        }
    }
}