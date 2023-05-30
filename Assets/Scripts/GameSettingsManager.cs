using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TutoToons
{
    public class GameSettingsManager : MonoBehaviour
    {
        public static GameSettingsManager Instance { get; private set; }
        public GameSettings Settings => _settings;

        [SerializeField] private GameSettings _settings;
        
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
