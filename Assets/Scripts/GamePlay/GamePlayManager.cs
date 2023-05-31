using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TutoToons
{
    public class GamePlayManager : MonoBehaviour
    {
        [SerializeField] private float _finishDelay = 1.5f;
        
        private const string _pointTag = "Button";

        private Camera _camera;
        private IScreenInput _input;
        private LevelManager _levelManager;
        private StateManager _stateManager;
        private PoolManager _poolManager;

        private Queue<Point> _buttonsToConnect;
        private Point _previousActivatedPoint;

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

                if (hit.collider != null && hit.collider.CompareTag(_pointTag))
                {
                    OnPointClicked(hit.transform.GetComponent<Point>());
                }
            }
        }

        private void OnPointClicked(Point point)
        {
            if (CanPointBeActivated(point))
            {
                _buttonsToConnect.Enqueue(point);
                _previousActivatedPoint = point;
                point.Disable();

                if (IsLevelFinished())
                {
                    _buttonsToConnect.Enqueue(_levelManager.CurrentLevel.Points.First());
                }
            }
        }
        
        private bool CanPointBeActivated(Point point)
        {
            if (_previousActivatedPoint == null && point.Number == 1)
            {
                return true;
            }

            return point != null
                   && point.IsDisabled() == false
                   && _previousActivatedPoint != null
                   && point.Number == _previousActivatedPoint.Number + 1;
        }

        private bool IsLevelFinished()
        {
            return _previousActivatedPoint && _previousActivatedPoint.Number == _levelManager.CurrentLevel.Points.Count;
        }
        
        private void HandleStateChange(GameState state)
        {
            switch (state)
            {
                case GameState.Playing:
                    _buttonsToConnect = new Queue<Point>();
                    StartCoroutine(ProcessQueue());
                    break;
                default:
                    _previousActivatedPoint = null;
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
                Debug.Log("sss");
                
                if (_buttonsToConnect.Count > 0)
                {
                    var point = _buttonsToConnect.Dequeue();

                    if (previousPoint)
                    {
                        yield return ConnectRope(previousPoint, point);
                        point.Connected();
                    }
                    
                    previousPoint = point;
                }

                if (IsLevelFinished() && _buttonsToConnect.Count == 0)
                {
                    yield return WaitAndEndLevel();
                    break;
                }
                
                yield return timeout;
            }
        }

        private IEnumerator WaitAndEndLevel()
        {
            yield return new WaitForSeconds(_finishDelay);
            
            _poolManager.ResetSpawned();
            _stateManager.SetState(GameState.Menu);
            // _stateManager.SetState(GameState.LevelFinished);
        }
        
        private IEnumerator ConnectRope(Point point1, Point point2)
        {
            var rope = _poolManager
                .GetNextObject(PoolGroup.Rope)
                .GetComponent<Rope>();

            rope.Connect(point1, point2);

            return new WaitUntil(() => rope.IsConnected);
        }
    }
}