using System.Collections.Generic;
using Core;
using LevelGenerationSystem.Data;
using UnityEngine;
using Zenject;

namespace LevelGenerationSystem
{
    public class LevelGeneration : IInitializable
    {
        private GenerationSettingsSO _generationSettings;
        private GameStats _gameStats;

        private readonly List<GameObject> _levelObjects = new();
        private float _nextSpawnXPosition;
        private LevelSegmentSO _lastSegment;

        [Inject]
        public LevelGeneration(GenerationSettingsSO generationSettings, GameStats gameStats)
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
                _lastSegment = _lastSegment
                    ? _lastSegment.GetRandomNextLevelSegment()
                    : _generationSettings.StartSegment.GetRandomNextLevelSegment();
                CreateLevelSegment(_lastSegment.Prefab, Vector3.right * _nextSpawnXPosition);
            }
            
            CreateLevelSegment(_generationSettings.EndSegment.Prefab, Vector3.right * _nextSpawnXPosition);
        }

        private void CreateLevelSegment(GameObject prefab, Vector3 position)
        {
            _levelObjects.Add(Object.Instantiate(prefab, position, Quaternion.identity));
            _nextSpawnXPosition += _generationSettings.SpawnOffset;
        }

        public void Rebuild()
        {
            _nextSpawnXPosition = 0;
            foreach (var segment in _levelObjects)
                Object.Destroy(segment.gameObject);

            GenerateLevel();
        }
    }
}