using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TutoToons
{
    public class RopesManager : MonoBehaviour
    {
        public static RopesManager Instance { get; private set; }

        [SerializeField] private GameObject _rope;
        

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
