using UnityEngine;

namespace RPG.Stats
{
    [CreateAssetMenu(fileName = "Progression", menuName = "Stats/New Progression", order = 0)]
    public class Progression : ScriptableObject
    {
        [SerializeField] private CharacterClassProgression[] characterClassProgress;

        public float GetStat(Stat stat,CharacterClass characterClass, int level)
        {
            foreach (CharacterClassProgression progressionClass in characterClassProgress)
            {
                if (progressionClass.characterClass != characterClass) continue;

                foreach (ProgressionStat progressionStat in progressionClass.stats )
                {
                    if (progressionStat.stat != stat) continue;

                    if (progressionStat.levels.Length < level) continue;
                    
                    return progressionStat.levels[level - 1];
                    
                }
            }

            return 0;
        }
        
        [System.Serializable]
        class CharacterClassProgression
        {
            public CharacterClass characterClass;
            public ProgressionStat[] stats;
            //public int[] healthValues;

        }
        
        [System.Serializable]
        class ProgressionStat
        {
            public Stat stat;
            public float[] levels;
        }
        
    }    
}
