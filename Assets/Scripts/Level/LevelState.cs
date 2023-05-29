using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TutoToons
{
    public class LevelState
    {
        public Level Level { get; }
        public List<Point> Points { get; }

        public LevelState(Level level, List<Point> points)
        {
            Level = level;
            Points = points;
        }
    }
}
