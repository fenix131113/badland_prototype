using System;
using System.Collections.Generic;
using UnityEngine;

namespace LevelGenerationSystem.Data
{
    [Serializable]
    public class SpawnVariantChanceGroup
    {
        [field: SerializeField] public GameObject Variant { get; private set; }
        [field: SerializeField] public List<GameObject> VariantsGroup { get; private set; }
        [field: SerializeField] public int Weight { get; private set; } = 50;
    }
}