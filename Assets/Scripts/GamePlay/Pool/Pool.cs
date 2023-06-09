using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TutoToons
{
    public class Pool
    {
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

        public GameObject GetNext(bool active)
        {
            var obj = _pool[_current];
            obj.SetActive(active);

            _current++;

            return obj;
        }

        public void Add(GameObject obj)
        {
            obj.transform.SetParent(_container.transform);
            obj.SetActive(false);

            _pool.Add(obj);
        }

        public void Reset()
        {
            for (int i = _current - 1; i >= 0; i--)
            {
                _pool[i].SetActive(false);
            }

            _current = 0;
        }
    }
}