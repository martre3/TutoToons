using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TutoToons
{
    public class PoolManager : MonoBehaviour
    {
        public static PoolManager Instance { get; private set; }

        [SerializeField] private Transform _gameParent;
        [SerializeField] private PoolableObject[] _poolableObjects;
        private Dictionary<PoolGroup, Pool> _pools = new Dictionary<PoolGroup, Pool>();

        private void Awake()
        {
            if (Instance == null)
            {
                for (int i = 0; i < _poolableObjects.Length; i++)
                {
                    _pools.Add(
                        _poolableObjects[i].Group,
                        new Pool(_poolableObjects[i].Group, _poolableObjects[i].Object, _gameParent)
                    );
                }

                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public GameObject GetNextObject(PoolGroup group)
        {
            if (_pools.ContainsKey(group) == false)
            {
                throw new ArgumentException($"{group.ToString()} is not initialized");
            }

            if (_pools[group].HasNext() == false)
            {
                _pools[group].Add(Instantiate(_pools[group].Obj));
            }

            return _pools[group].GetNext();
        }

        public void Pool(PoolGroup group, int amount)
        {
            if (_pools.ContainsKey(group) == false)
            {
                throw new ArgumentException($"{group.ToString()} is not initialized");
            }

            int amountToAdd = Math.Max(amount - _pools[group].Size, 0);
            
            for (int i = 0; i < amountToAdd; i++)
            {
                _pools[group].Add(Instantiate(_pools[group].Obj));
            }
        }

        public void ResetSpawned()
        {
            foreach (var pool in _pools)
            {
                pool.Value.Reset();
            }
        }
    }
}