using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TutoToons
{
    public class UiManager : MonoBehaviour
    {
        public static UiManager Instance { get; private set; }

        [SerializeField] private GameObject _mainMenu;
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                StateManager.OnStateChange += UpdateUI;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void UpdateUI(GameState state)
        {
            _mainMenu.SetActive(false);

            switch (state)
            {
                case GameState.Menu:
                    _mainMenu.SetActive(true);
                    break;
            }
        }
        
    }
}
