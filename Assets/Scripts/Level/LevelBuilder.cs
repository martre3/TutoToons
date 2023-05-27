using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TutoToons
{
    public class LevelBuilder : MonoBehaviour
    {
        [SerializeField] private Transform _gameParent;
        [SerializeField] private Texture _background;
        [SerializeField] private GameObject _button;
        
        public void Build(Level level)
        {
            var buttonsWrapper = new GameObject("Buttons");
            buttonsWrapper.transform.SetParent(_gameParent);
            
            foreach (var point in level.Points)
            {
                Instantiate(
                    _button, 
                    new Vector2(point.x, point.y), 
                    Quaternion.identity,
                    buttonsWrapper.transform
                );
            }
        }
    }
}
