using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace TutoToons
{
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager Instance { get; private set; }

        private DataManager _data;
        
        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                _data = DataManager.Instance;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Update()
        {
            
        }

        private void LoadLevel()
        {
            
        }
    }
}