using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TutoToons
{
    public class LevelBuilder : MonoBehaviour
    {
        public static LevelBuilder Instance { get; private set; }

        [SerializeField] private Texture _background;
        private PoolManager _poolManager;
        
        public void Build(Level level)
        {
            foreach (var coordinates in level.Points)
            {
                var point = _poolManager.GetNextObject(PoolGroup.Point);
                point.transform.position = new Vector2(coordinates.x, coordinates.y);
            }
            
            _poolManager.Pool(PoolGroup.Rope, level.Points.Count);
        }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                _poolManager = PoolManager.Instance;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
