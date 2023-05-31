using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TutoToons
{
    public class Rope : MonoBehaviour
    {
        public bool IsConnected { get; private set; }
        private SpriteRenderer _spriteRenderer;
        private ISpriteExtender _spriteExtender;
        
        public void Connect(Point point1, Point point2)
        {
            _spriteRenderer.enabled = true;

            transform.position = point1.transform.position;
            _spriteExtender.ExtendTo(point2.transform.position, (finished) => IsConnected = finished);
        }

        private void OnEnable()
        {
            _spriteRenderer.enabled = false;
            IsConnected = false;
        }

        private void Awake()
        {
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            _spriteExtender = GetComponent<ISpriteExtender>();

            _spriteRenderer.enabled = false;
        }
    }
}
