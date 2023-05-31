using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TutoToons
{
    public class BackgroundManager : MonoBehaviour
    {
        public static BackgroundManager Instance { get; private set; }

        [SerializeField] private Transform _background;
        
        private float _previousAspect;

        private Camera _camera;
        private Sprite _backgroundSprite;
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                _camera = Camera.main;
                _backgroundSprite = _background.GetComponent<SpriteRenderer>().sprite;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
        private void Update()
        {
            if (_camera.aspect != _previousAspect)
            {
                FitBackgroundIntoScreen();
            }

            _previousAspect = _camera.aspect;
        }

        private void FitBackgroundIntoScreen()
        {
            var backgroundAspect = _backgroundSprite.rect.width / _backgroundSprite.rect.height;
            var cameraYSize = _camera.orthographicSize * 2;

            if (_camera.aspect < backgroundAspect)
            {
                ScaleAndCenterBackground(cameraYSize / _backgroundSprite.bounds.size.y);
            }
            else
            {
                ScaleAndCenterBackground(cameraYSize * _camera.aspect / _backgroundSprite.bounds.size.x);
            }
        }

        private void ScaleAndCenterBackground(float newScale)
        {
            _background.transform.localScale = new Vector3(newScale, newScale, 1);
            _background.transform.position =
                new Vector3(_camera.transform.position.x, _camera.transform.position.y, 0);
        }
    }
}
