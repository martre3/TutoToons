using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TutoToons
{
    public class StateManager : MonoBehaviour
    {
        public GameState State { get; private set; }
        public static event Action<GameState> OnStateChange;
        public static StateManager Instance { get; private set; }

        public void SetState(GameState state)
        {
            State = state;
            OnStateChange?.Invoke(state);
        }
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
