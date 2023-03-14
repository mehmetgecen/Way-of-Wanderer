using UnityEngine;

namespace RPG.Stats
{
    [CreateAssetMenu(fileName = "Progression", menuName = "Stats/New Progression", order = 0)]
    public class Progression : ScriptableObject
    {
        [SerializeField] private CharacterClassProgression[] characterClassProgress;
        
        [System.Serializable]
        class CharacterClassProgression
        {
            [SerializeField] private CharacterClass characterClass;
            [SerializeField] int[] healthValues;
            
        }
        
    }    
}
