using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TutoToons
{
    public class GameEndingHandler : MonoBehaviour
    {
        public static GameEndingHandler Instance { get; private set; }

        [SerializeField] private float _animationInterval = 0.05f;
        [SerializeField] private float _finishDelay = 1f;

        private LevelManager _levelManager;
        private StateManager _stateManager;
        private PoolManager _poolManager;
        
        private void Awake()
        {
            if (Instance == null)
            {
                _levelManager = LevelManager.Instance;
                _stateManager = StateManager.Instance;
                _poolManager = PoolManager.Instance;
                
                StateManager.OnStateChange += HandleState;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void HandleState(GameState state)
        {
            if (state != GameState.LevelFinished)
            {
                return;
            }

            StartCoroutine(AnimateEnding(_levelManager.CurrentLevel.Points));
        }

        private IEnumerator AnimateEnding(List<Point> points)
        {
            var timeout = new WaitForSeconds(_animationInterval);
            
            foreach (var point in points)
            {
                point.Connected();

                yield return timeout;
            }
            
            yield return new WaitForSeconds(_finishDelay);
            
            EndLevel();
        }

        private void EndLevel()
        {
            _levelManager.CurrentLevel.Level.Completed = true;
            _poolManager.ResetSpawned();
            _stateManager.SetState(GameState.Menu);
        }
    }
}
