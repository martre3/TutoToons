using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace TutoToons
{
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager Instance { get; private set; }
        public List<Level> Levels => _dataManager.Levels;
        public LevelState CurrentLevel { get; private set; }
        
        private DataManager _dataManager;
        private StateManager _stateManager;
        private LevelBuilder _levelBuilder;
        private PointsAnimator _pointsAnimator;
        
        public void LoadLevel(Level level)
        {
            CurrentLevel = _levelBuilder.Build(level);
            _pointsAnimator.AnimateSpawn(CurrentLevel.Points);
            
            _stateManager.SetState(GameState.Playing);
        }
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                _dataManager = DataManager.Instance;
                _stateManager = StateManager.Instance;
                _pointsAnimator = PointsAnimator.Instance;
                _levelBuilder = gameObject.GetComponent<LevelBuilder>();
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}