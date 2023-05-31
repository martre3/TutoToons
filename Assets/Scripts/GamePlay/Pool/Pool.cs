using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TutoToons
{
    public class Pool
    {
        private struct Poolable
        {
            public GameObject obj { get; }
            public IPoolable component { get; }

            public Poolable(GameObject obj, IPoolable component)
            {
                this.obj = obj;
                this.component = component;
            }
        }
        
        public GameObject Obj { get; }
        public int Size => _pool.Count;

        private List<GameObject> _pool = new List<GameObject>();
        private GameObject _container;
        private int _current;

        public Pool(PoolGroup group, GameObject obj, Transform parent)
        {
            _container = new GameObject($"{group.ToString()} (Pool)");
            _container.transform.SetParent(parent);

            Obj = obj;
        }

        public bool HasNext()
        {
            return _pool.Count != _current;
        }

        public GameObject GetNext()
        {
            var obj = _pool[_current];
            obj.SetActive(true);

            _current++;

            return obj;
        }

        public void Add(GameObject obj)
        {
            obj.transform.SetParent(_container.transform);
            obj.SetActive(false);

            // if (obj.TryGetComponent(out IPoolable component) == false)
            // {
            //     throw new ArgumentException($"Poolable object {obj.name} must have a component implementing IPoolable interface");
            // }
            
            _pool.Add(obj);
        }

        public void Reset()
        {
            for (int i = _current - 1; i >= 0; i--)
            {
                // _pool[i].component.Reset();
                _pool[i].SetActive(false);
            }

            _current = 0;
        }
    }
}