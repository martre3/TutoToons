using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TutoToons
{
    public class ChangeSprite : MonoBehaviour
    {
        [SerializeField] private Sprite _changeTo;
        
        private SpriteRenderer _spriteRenderer;
        
        public void Change()
        {
            _spriteRenderer.sprite = _changeTo;
        }

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }
}
