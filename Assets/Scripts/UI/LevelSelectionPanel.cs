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
        
        private LevelManager _levelManager;
        
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

                buttonRect.anchoredPosition = new Vector2(0, (buttonRect.sizeDelta.y / 2 + (buttonRect.sizeDelta.y + _spacing) * i) * -1);
                buttonTextMesh.text = $"Level {i + 1}";
                
                levelButton.GetComponent<Button>().onClick.AddListener(() => _levelManager.LoadLevel(level));
            }
        }
    }
}
