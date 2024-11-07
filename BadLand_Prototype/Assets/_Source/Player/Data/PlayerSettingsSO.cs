using UnityEngine;

namespace Player.Data
{
    [CreateAssetMenu(fileName = "New PlayerSettingsSO", menuName = "SOs/New Player Settings")]
    public class PlayerSettingsSO : ScriptableObject
    {
        [field: SerializeField] public float PlayerSpeed { get; private set; }
        [field: SerializeField] public float PlayerJumpForce { get; private set; }
        [field: SerializeField] public float PlayerMaxJumpForce { get; private set; }
        [field: SerializeField] public float PlayerMaxSpeed { get; private set; }
        [field: SerializeField] public float PlayerXStoppingResistance { get; private set; }
    }
}