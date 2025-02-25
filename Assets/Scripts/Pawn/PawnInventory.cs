using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class PawnInventory : MonoBehaviour
    {
        private PawnController _pawn;
        private List<ItemConfig> _stacks = new();

        public void Initialize()
        {
            _pawn = GetComponent<PawnController>();
        }

        public void AddItem(ItemConfig item, int amount = 1)
        {

        }

        public void RemoveItem(ItemConfig item, int amount = 1)
        {

        }

        public void DropItem(ItemConfig item, int amount = 1)
        {

        }

        public int AmountOfItem(ItemConfig item)
        {
            int amount = 0;
            foreach (ItemConfig stack in _stacks)
            {
                if (stack == item)
                {
                    //amount += stack.Amount;
                }
            }
            return amount;
        }
    }
}