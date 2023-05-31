using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TutoToons
{
    public class LevelSelectionPanel : MonoBehaviour
    {
        [SerializeField] private GameObject _levelSelectionButton;
        [SerializeField] private int _spacing = 20;
        [Header("Default Button Colors")] [SerializeField] private ColorBlock _defaultButtonColors;
        [Header("Completed Button Colors")] [SerializeField] private ColorBlock _completedButtonColors;
        
        private LevelManager _levelManager;
        private List<Button> _buttons = new List<Button>();
        
        private void Awake()
        {
            _levelManager = LevelManager.Instance;
            DrawLevels(_levelManager.Levels);
        }
        
        private void DrawLevels(List<Level> levels)
        {
            for (int i = 0; i < levels.Count; i++)
            {
                var level = levels[i];
                var levelButton = Instantiate(_levelSelectionButton, transform);
                var buttonRect = levelButton.GetComponent<RectTransform>();
                var buttonTextMesh = levelButton.GetComponentInChildren<TextMeshProUGUI>();
                var button = levelButton.GetComponent<Button>();
                
                buttonRect.anchoredPosition = new Vector2(0, (buttonRect.sizeDelta.y / 2 + (buttonRect.sizeDelta.y + _spacing) * i) * -1);
                buttonTextMesh.text = $"Level {i + 1}";
                button.onClick.AddListener(() => _levelManager.LoadLevel(level));
                
                _buttons.Add(button);
            }
            
            UpdateButtons();
        }

        private void OnEnable()
        {
            Debug.Log("Enable");
            UpdateButtons();
        }

        private void UpdateButtons()
        {
            if (_buttons.Count == 0)
            {
                return;
            }
            
            for (int i = 0; i < _levelManager.Levels.Count; i++)
            {
                Debug.Log(_levelManager.Levels[i].Completed);
                
                _buttons[i].colors = _levelManager.Levels[i].Completed ? _completedButtonColors : _defaultButtonColors;
            }
        }
    }
}
