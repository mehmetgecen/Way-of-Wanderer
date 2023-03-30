using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats
{
    [CreateAssetMenu(fileName = "Progression", menuName = "Stats/New Progression", order = 0)]
    public class Progression : ScriptableObject
    {
        [SerializeField] private CharacterClassProgression[] characterClassProgress;

        private Dictionary<CharacterClass, Dictionary<Stat, float[]>> lookupTable = null;
        public float GetStat(Stat stat,CharacterClass characterClass, int level)
        {
            BuildLookUp();
            
            float[] levels = lookupTable[characterClass][stat];

            if (levels.Length < level)
            {
                return 0;
            }

            return levels[level - 1]; 
            
        }

        // New Lookup technic with dictionaries.
        // Searches only one when called.
        // More Performant.
        private void BuildLookUp()
        {
            if (lookupTable != null) return;

            lookupTable = new Dictionary<CharacterClass, Dictionary<Stat, float[]>>();

            foreach (CharacterClassProgression progressionClass in characterClassProgress)
            {
                var statLookUpTable = new Dictionary<Stat, float[]>();

                foreach (ProgressionStat progressionStat in progressionClass.stats)
                {
                    statLookUpTable[progressionStat.stat] = progressionStat.levels;
                }

                lookupTable[progressionClass.characterClass] = statLookUpTable;
            }
            
        }

        public int GetLevels(Stat stat, CharacterClass characterClass)
        {
            BuildLookUp();

            float[] levels = lookupTable[characterClass][stat];
            return levels.Length;
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
