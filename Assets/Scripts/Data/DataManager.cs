using System.Collections;
using System.Collections.Generic;
using TutoToons.Raw;
using UnityEngine;
using System.Linq;

namespace TutoToons
{
    public class DataManager : MonoBehaviour
    {
        public static DataManager Instance { get; private set; }
        public List<Level> Levels { get; private set; }

        private const string _levelsDataPath = "Data/level_data";

        private IDataLoader _dataLoader;
        private GameSettingsManager _settingsManager;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                _dataLoader = GetComponent<IDataLoader>();
                _settingsManager = GameSettingsManager.Instance;

                Load();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Load()
        {
            LoadLevels();
        }

        private void LoadLevels()
        {
            Levels = LevelParser.ParseRaw(
                _dataLoader.Load<LevelsContainerRaw>(_levelsDataPath),
                _settingsManager.Settings
            );
        }
    }
}