using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TutoToons
{
    [CreateAssetMenu(menuName = "Scriptable/PoolableObject")]
    public class PoolableObject : ScriptableObject
    {
        public PoolGroup Group;
        public GameObject Object;
    }
}
