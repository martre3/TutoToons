using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TutoToons
{
    [CreateAssetMenu(menuName = "Scriptable/PoolableObject")]
    [Serializable]
    public class PoolableObject : ScriptableObject
    {
        public PoolGroup Group;
        public GameObject Object;
    }
}
