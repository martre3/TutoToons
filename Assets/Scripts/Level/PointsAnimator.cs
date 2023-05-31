using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace TutoToons
{
    public class PointsAnimator : MonoBehaviour
    {
        public static PointsAnimator Instance { get; private set; }

        [SerializeField] private int _groupWidth = 100;
        [SerializeField] private float _groupSpawnInterval = 0.1f;


        public void AnimateSpawn(List<Point> points)
        {
            StartCoroutine(SpawnGroupedPoints(GroupPointsVertically(points)));
        }

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
        
        private IEnumerator SpawnGroupedPoints(List<List<Point>> groups)
        {
            var timeout = new WaitForSeconds(_groupSpawnInterval);
            
            foreach (var group in groups)
            {
                foreach (var point in group)
                {
                    point.gameObject.SetActive(true);
                }

                yield return timeout;
            }
        }

        private List<List<Point>> GroupPointsVertically(List<Point> points)
        {
            var a = points
                .Select(point => new {Point = point, Group = (int) point.transform.position.x / _groupWidth});

            foreach (var VARIABLE in a)
            {
                Debug.Log(VARIABLE.Group);
            }
            
            return points
                .Select(point => new { Point = point, Group = (int) point.transform.position.x / _groupWidth })
                .OrderBy(pair => pair.Group)
                .GroupBy(pair  => pair.Group)
                .Select(group => group.Select(pair => pair.Point).ToList())
                .ToList();
        }
    }
}