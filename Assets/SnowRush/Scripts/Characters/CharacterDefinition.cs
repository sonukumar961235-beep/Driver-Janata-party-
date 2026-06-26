using UnityEngine;

namespace SnowRush.Characters
{
    [CreateAssetMenu(menuName = "Snow Rush/Character")]
    public sealed class CharacterDefinition : ScriptableObject
    {
        public string Id;
        public string DisplayName;
        public GameObject Prefab;
        public RuntimeAnimatorController Animator;
        public int BonusPercent;
    }
}
