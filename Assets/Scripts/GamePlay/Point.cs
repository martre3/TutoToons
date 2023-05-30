using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace TutoToons
{
    public class Point : MonoBehaviour
    {
        public PointState State { get; private set; } = PointState.Default;
        public int Number { get; private set; }
        
        [SerializeField] private Sprite _defaultButton;
        [SerializeField] private GameObject _particles;

        private static readonly int Fade = Animator.StringToHash("Fade");
        private static readonly int Click = Animator.StringToHash("Click");
        private TextMeshPro _numberText;
        private SpriteRenderer _renderer;
        private Animator _spriteAnimator;
        private Animator _animator;

        public bool IsDisabled()
        {
            return State != PointState.Default;
        }

        public void Disable()
        {
            State = PointState.Clicked;
            _spriteAnimator.SetTrigger(Click);
            _animator.SetTrigger(Fade);
        }

        public void Connected()
        {
            State = PointState.Connected;
            _particles.SetActive(true);
        }

        public void SetNumber(int number)
        {
            Number = number;
            _numberText.text = number.ToString();
        }

        private void Awake()
        {
            _numberText = GetComponentInChildren<TextMeshPro>();
            _animator = _numberText.gameObject.GetComponent<Animator>();
            _renderer = GetComponentInChildren<SpriteRenderer>();
            _renderer.sprite = _defaultButton;
            _spriteAnimator = _renderer.gameObject.GetComponent<Animator>();
        }
    }
}
