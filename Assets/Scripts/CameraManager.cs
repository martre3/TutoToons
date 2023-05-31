using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace TutoToons
{
    public class CameraManager : MonoBehaviour
    {
        public static CameraManager Instance { get; private set; }
        
        private GameSettingsManager _settingsManager;
        private Camera _camera;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                _settingsManager = GameSettingsManager.Instance;
                _camera = Camera.main;
                InitCamera();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void InitCamera()
        {
            int levelSize = _settingsManager.Settings.LevelSize + _settingsManager.Settings.LevelPadding;
            int centerX = _settingsManager.Settings.LevelSize / 2;

            _camera.transform.position = new Vector3(centerX, levelSize / 2, _camera.transform.position.z);
            _camera.orthographicSize = levelSize / 2;
        }
    }
}