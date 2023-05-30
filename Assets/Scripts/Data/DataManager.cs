using System.Collections;
using System.Collections.Generic;
using TutoToons.Raw;
using UnityEngine;
using System.Linq;

namespace TutoToons
{
    public class DataManager : MonoBehaviour
    {
        public static DataManager Instance { get; private set; }
        public List<Level> Levels { get; private set; }
        
        private IDataLoader _dataLoader;
        
        private const string _levelsDataPath = "Data/level_data";
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                _dataLoader = GetComponent<IDataLoader>();
                
                Load();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Load()
        {
            LoadLevels();
        }

        private void LoadLevels()
        {
            Levels = new List<Level>();
            
            var container = _dataLoader.Load<LevelsContainerRaw>(_levelsDataPath);
        
            foreach (var rawLevel in container.levels)
            {
                if (rawLevel.level_data.Count % 2 != 0)
                {
                    Debug.LogError("Invalid number of coordinates in level data");
                    
                    continue;
                }
                
                var level = new Level();
                level.Points = rawLevel.level_data
                        .Select((point, i) => new { Value = point, Group = i / 2})
                        .GroupBy(point => point.Group)
                        .Select(pair => new Vector2(pair.First().Value, 1000 - pair.Last().Value))
                        .ToList();
                
                Levels.Add(level);
            }
        }
    }
}