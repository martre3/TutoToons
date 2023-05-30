using System;
using System.Collections;
using System.Collections.Generic;
using TutoToons.Raw;
using System.Linq;
using UnityEngine;

namespace TutoToons
{
    public class LevelParser: Parser
    {
        private static readonly List<ValidationRule<Level, GameSettings>> _rules = new List<ValidationRule<Level, GameSettings>> {
            new ValidationRule<Level, GameSettings>(
                (level, settings) => level.Points.Count > 1,
                "[Level Parser] Not enough points"
            ),
            new ValidationRule<Level, GameSettings>(
                (level, settings) => level.Points.Min(point => Mathf.Min(point.x, point.y)) >= 0,
                "[Level Parser] Coordinate cannot be less than 0"
            ),
            new ValidationRule<Level, GameSettings>(
                (level, settings) => level.Points.Max(point => Mathf.Max(point.x, point.y)) <= settings.LevelSize,
                "[Level Parser] Coordinate value cannot be greater than board size"
            ),
        };

        public static List<Level> ParseRaw(LevelsContainerRaw container, GameSettings settings)
        {
            var levels = new List<Level>();

            foreach (var rawLevel in container.levels)
            {
                if (rawLevel.level_data.Count % 2 != 0)
                {
                    Debug.LogError("[Level Parser] Invalid number of coordinates in level data");

                    continue;
                }

                var level = new Level();
                level.Points = rawLevel.level_data
                    .Select((point, i) => new {Value = point, Group = i / 2})
                    .GroupBy(point => point.Group)
                    .Select(pair => new Vector2(pair.First().Value, settings.LevelSize - pair.Last().Value))
                    .ToList();

                if (Validate(_rules, level, settings) == false)
                {
                    continue;
                }

                levels.Add(level);
            }

            return levels;
        }
    }
}