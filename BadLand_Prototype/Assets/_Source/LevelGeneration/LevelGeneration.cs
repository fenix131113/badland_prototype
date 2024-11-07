using Core;
using LevelGeneration.Data;
using UExtra.Collections;
using UnityEngine;
using Zenject;

namespace LevelGeneration
{
    public class LevelGeneration : IInitializable
    {
        private GenerationSettingsSO _generationSettings;
        private GameStats _gameStats;

        private float _nextSpawnXPosition;
        private LevelSegmentSO _lastSegment;

        [Inject]
        private void Construct(GenerationSettingsSO generationSettings, GameStats gameStats)
        {
            _generationSettings = generationSettings;
            _gameStats = gameStats;
        }

        public void Initialize()
        {
            GenerateLevel();
        }

        private void GenerateLevel()
        {
            CreateLevelSegment(_generationSettings.StartSegment.Prefab, Vector3.zero);

            for (var i = 0; i < _generationSettings.StartSegmentsCount - 2 + _gameStats.CurrentLevel * 2; i++)
            {
                _lastSegment = _lastSegment ? _lastSegment.GetRandomNextLevelSegment() : _generationSettings.StartSegment.GetRandomNextLevelSegment();
                CreateLevelSegment(_lastSegment.Prefab, Vector3.right * _nextSpawnXPosition);
            }
        }

        private void CreateLevelSegment(GameObject prefab, Vector3 position)
        {
            Object.Instantiate(prefab, position, Quaternion.identity);
            _nextSpawnXPosition += _generationSettings.SpawnOffset;
        }
    }
}