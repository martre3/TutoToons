using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.Notifications.iOS;
using UnityEngine;

namespace TutoToons
{
    public class Point : MonoBehaviour
    {
        public PointState State { get; private set; } = PointState.Default;
        [SerializeField] private Sprite _defaultButton;
        [SerializeField] private Sprite _activatedButton;

        private SpriteRenderer _renderer;
        private Rope _rope;

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
        }

        private void Awake()
        {
            _renderer = GetComponentInChildren<SpriteRenderer>();
            _renderer.sprite = _defaultButton;
            _rope = GetComponentInChildren<Rope>();
        }
    }
}
