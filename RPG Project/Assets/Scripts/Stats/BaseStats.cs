using System.Collections;
using System.Collections.Generic;
using RPG.Stats;
using UnityEngine;

namespace RPG.Stats
{
    public class BaseStats : MonoBehaviour
    {
        [Range(1,99)]
        [SerializeField] int startLevel = 1;

        [SerializeField] private CharacterClass characterClass;
        [SerializeField] private Progression progression = null;

        public float GetStat(Stat stat)
        {
            return progression.GetStat(stat, characterClass, startLevel);
        }

        public float GetLevel()
        {
            float currentXP = GetComponent<Experience>().GetPoints();

            int penultimateLevel = progression.GetLevels(Stat.ExperienceToLevelUp, characterClass);

            for (int level = 1; level < penultimateLevel; level++)
            {
                float XPToLevelUP = progression.GetStat(Stat.ExperienceToLevelUp, characterClass, level);
                
                if (XPToLevelUP > currentXP)
                {
                    return level;
                }
                
            }

            return penultimateLevel + 1;

        }
    }
}

