using UnityEngine;

namespace RPG.Stats
{
    [CreateAssetMenu(fileName = "Progression", menuName = "Stats/New Progression", order = 0)]
    public class Progression : ScriptableObject
    {
        [SerializeField] private CharacterClassProgression[] characterClassProgress;

        public float GetHealth(CharacterClass characterClass, int level)
        {
            foreach (CharacterClassProgression progressionClass in characterClassProgress)
            {
                if (progressionClass.characterClass==characterClass)
                {
                    return progressionClass.healthValues[level-1];
                }
            }

            return 0;
        }
        
        [System.Serializable]
        class CharacterClassProgression
        {
            public CharacterClass characterClass;
            public int[] healthValues;
            
        }
        
    }    
}
