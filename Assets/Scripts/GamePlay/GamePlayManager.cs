using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TutoToons
{
    public class GamePlayManager : MonoBehaviour
    {
        public GameObject Rope;

        private const string _buttonTag = "Button";

        private Camera _camera;
        private IScreenInput _input;
        private LevelManager _levelManager;
        private StateManager _stateManager;
        private PoolManager _poolManager;
        private Queue<Point> _buttonsToActivate;

        private void Awake()
        {
            _camera = Camera.main;
            _levelManager = LevelManager.Instance;
            _stateManager = StateManager.Instance;
            _poolManager = PoolManager.Instance;
            _input = GetComponent<IScreenInput>();

            StateManager.OnStateChange += HandleStateChange;
        }

        private void Update()
        {
            if (_stateManager.State != GameState.Playing)
            {
                return;
            }

            HandlePointSelection();
        }

        private void HandlePointSelection()
        {
            if (_input.IsTriggered())
            {
                RaycastHit2D hit = Physics2D.Raycast(_camera.ScreenToWorldPoint(_input.GetPositionOnScreen()),
                    Vector2.zero);

                if (hit.collider != null && hit.collider.CompareTag(_buttonTag))
                {
                    var button = hit.transform.GetComponent<Point>();

                    if (button != null && button.IsDisabled() == false)
                    {
                        _buttonsToActivate.Enqueue(button);
                        button.Disable();
                    }
                }
            }
        }

        private void HandleStateChange(GameState state)
        {
            switch (state)
            {
                case GameState.Playing:
                    _buttonsToActivate = new Queue<Point>();
                    StartCoroutine(ProcessQueue());
                    break;
                default:
                    StopAllCoroutines();
                    break;
            }
        }

        private IEnumerator ProcessQueue()
        {
            var timeout = new WaitForSeconds(0.2f);
            Point previousPoint = null;

            while (true)
            {
                if (_buttonsToActivate.Count > 0)
                {
                    var point = _buttonsToActivate.Dequeue();

                    if (previousPoint)
                    {
                        var rope = _poolManager
                            .GetNextObject(PoolGroup.Rope)
                            .GetComponent<Rope>();

                        rope.Connect(previousPoint, point);
                        
                        yield return new WaitUntil(() => rope.IsConnected);
                        
                    }
                    point.Connected();
                    previousPoint = point;
                }

                yield return timeout;
            }
        }
    }
}