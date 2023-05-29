using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TutoToons
{
    public class AnimatedSpriteExtender : MonoBehaviour, ISpriteExtender
    {
        [SerializeField] private float _animationSpeed = 15f;
        [SerializeField] private Transform _anchor;
        [SerializeField] private SpriteRenderer _sprite;
        private Transform _spriteTransform;

        public void ExtendTo(Vector2 to, Action<bool> callback)
        {
            Vector2 direction = to - (Vector2) transform.position;
            transform.rotation = Quaternion.FromToRotation(Vector3.right, direction);

            StartCoroutine(Animate(to, callback));
        }

        private IEnumerator Animate(Vector2 to, Action<bool> callback)
        {
            float destinationSize = Vector2.Distance(_anchor.position, to) / _anchor.localScale.y;
            float currentSize = 0;
            var timeout = new WaitForFixedUpdate();

            while (currentSize <= destinationSize)
            {
                currentSize += _animationSpeed * Time.deltaTime;
                UpdateSize(Math.Min(currentSize, destinationSize));
                
                yield return timeout;
            }

            callback(true);
        }

        private void UpdateSize(float currentSize)
        {
            _sprite.size = new Vector2(_sprite.size.x, currentSize);
            _sprite.transform.localPosition = new Vector2(0, currentSize / 2);
        }
    }
}
