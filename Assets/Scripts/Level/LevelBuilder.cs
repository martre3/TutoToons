using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

namespace TutoToons
{
    public class LevelBuilder : MonoBehaviour
    {
        public static LevelBuilder Instance { get; private set; }
        
        private PoolManager _poolManager;
        
        public LevelState Build(Level level)
        {
            List<Point> points = new List<Point>();
            
            for (int i = 0; i < level.Points.Count; i++)
            {
                var coordinates = level.Points[i];
                var point = _poolManager.GetNextObject(PoolGroup.Point, false);
                
                point.transform.position = new Vector2(coordinates.x, coordinates.y);
                var pointComponent = point.GetComponent<Point>();
                
                pointComponent.SetNumber(i + 1);
                points.Add(pointComponent);
            }
            
            _poolManager.Pool(PoolGroup.Rope, level.Points.Count);

            return new LevelState(level, points);
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
