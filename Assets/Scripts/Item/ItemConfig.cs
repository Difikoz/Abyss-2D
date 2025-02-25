using UnityEngine;

namespace WinterUniverse
{
    public class ItemConfig : BaseInfoConfig
    {
        [SerializeField] private float _weight = 1f;
        [SerializeField] private int _maxInStack = 1;
        [SerializeField] private int _price = 100;

        public float Weight => _weight;
        public int MaxInStack => _maxInStack;
        public int Price => _price;
    }
}