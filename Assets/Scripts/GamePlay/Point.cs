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
        [SerializeField] private Sprite _activatedButton;

        private static readonly int Fade = Animator.StringToHash("Fade");
        private TextMeshPro _numberText;
        private SpriteRenderer _renderer;
        private Animator _animator;

        public bool IsDisabled()
        {
            return State != PointState.Default;
        }

        public void Disable()
        {
            State = PointState.Clicked;
        }

        public void Connected()
        {
            State = PointState.Connected;
            _renderer.sprite = _activatedButton;
            _animator.SetTrigger(Fade);
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
        }
    }
}
