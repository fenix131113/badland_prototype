using UnityEngine;

namespace LevelGenerationSystem.Data
{
    [CreateAssetMenu(fileName = "New GenerationSettings", menuName = "SOs/New Generating Settings")]
    public class GenerationSettingsSO : ScriptableObject
    {
        [field: SerializeField] public float SpawnOffset { get; private set; }
        [field: SerializeField] public int StartSegmentsCount { get; private set; }
        [field: SerializeField] public LevelSegmentSO StartSegment { get; private set; }
        [field: SerializeField] public LevelSegmentSO EndSegment { get; private set; }
    }
}