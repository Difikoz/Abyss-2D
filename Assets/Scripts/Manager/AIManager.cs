using Lean.Pool;
using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class AIManager : MonoBehaviour
    {
        [SerializeField] private List<Transform> _spawnPoints = new();

        private List<PawnController> _pawns = new();

        public void Initialize()
        {
            for (int i = 0; i < _spawnPoints.Count; i++)
            {
                _pawns.Add(LeanPool.Spawn(GameManager.StaticInstance.PawnPrefab, _spawnPoints[i].position, Quaternion.identity).GetComponent<PawnController>());
                _pawns[i].InitializePawn();
                _pawns[i].Revive();
            }
        }

        public void OnFixedUpdate()
        {
            foreach (PawnController pawn in _pawns)
            {
                pawn.OnFixedUpdate();
            }
        }
    }
}