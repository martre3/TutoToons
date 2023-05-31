using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TutoToons
{
    public class EndGameHandler : MonoBehaviour
    {
        public static EndGameHandler Instance { get; private set; }

        private PoolManager _poolManager;
        private StateManager _stateManager;
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                _poolManager = PoolManager.Instance;
                _stateManager = StateManager.Instance;
                
                StateManager.OnStateChange += HandleStateChange;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void HandleStateChange(GameState state)
        {
            if (state != GameState.LevelFinished)
            {
                return;
            }

            // _poolManager.ResetSpawned();
            // _stateManager.SetState(GameState.Menu);
        }
    }
}