using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField] private GameObject _pawnPrefab;
        [SerializeField] private List<StatConfig> _stats = new();

        public GameObject PawnPrefab => _pawnPrefab;
        public List<StatConfig> Stats => _stats;
    }
}