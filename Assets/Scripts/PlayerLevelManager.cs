using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerLevelManager
{

    [SerializeField]
    private List<Level> levels;

    private int currentExp;

    public Level CurrentLevel { get; private set; }

    public event Action<Level> OnLevelUp;

    private void Awake()
    {

    }

    public void AddExperience(int exp)
    {
        currentExp += exp;
        CalculateCurrentLevel();
    }

    public void ResetLevel()
    {
        levels[levels.Count - 1].ExperienceNeeded = int.MaxValue;
        for (int i = 0; i < levels.Count; i++)
        {
            levels[i].level = i + 1;
        }
        currentExp = 0;
        CalculateCurrentLevel();
    }

    private void CalculateCurrentLevel()
    {
        var exp = currentExp;
        foreach (var level in levels)
        {
            if (exp >= level.ExperienceNeeded)
            {
                exp -= level.ExperienceNeeded;
            }
            else
            {
                if (CurrentLevel == level)
                    break;

                CurrentLevel = level;
                OnLevelUp?.Invoke(CurrentLevel);
                break;
            }
        }

    }

    [System.Serializable]
    public class Level
    {
        [field: SerializeField]
        public int ExperienceNeeded { get; set; }

        [field: SerializeField]
        public int Damage { get; set; }

        [HideInInspector]
        public int level;

    }

}
