using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem
{
    #region Properties
    private int Level;
    private int Experience;
    private int SkillsPoints;
    private int TotalExperience;
    public event EventHandler<OnExperienceChangeEventArgs> onExperienceChange;
    public class OnExperienceChangeEventArgs
    {
        public int experienceAmount;
    }
    public event EventHandler onLevelChange;
    private static readonly int[] experiencePerLevel = new[] { 0, 100, 200, 400, 800, 1600, 3200, 6400, 12800, 25600, 55000 };

    public LevelSystem(int level, int experience, int skillpoints)
    {
        Level = level;
        Experience = experience;
        TotalExperience = Experience;
        SkillsPoints = skillpoints;
    }

    #endregion

    #region Events


    #endregion

    #region Functions

    public void AddExperience(int experienceAmount)
    {
        if (!IsMaxLevel())
        {
            this.Experience += experienceAmount;
            this.TotalExperience += experienceAmount;
            if (onExperienceChange != null) onExperienceChange?.Invoke(this, new OnExperienceChangeEventArgs { experienceAmount = experienceAmount });
            while (!IsMaxLevel() && Experience >= GetExperienceToNextLevel(Level))
            {
                
                Experience -= GetExperienceToNextLevel(Level);
                Level++;
                SkillsPoints++;
                if (onLevelChange != null) onLevelChange?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public int GetLevel()
    {
        return Level;
    }
    
    public float GetExpirencePercent()
    {
        return (float)Experience / GetExperienceToNextLevel(Level);
    }

    public int GetExperience()
    {
        return TotalExperience;
    }

    public int GetExperienceToNextLevel(int level)
    {
        if (level < experiencePerLevel.Length)
        {
            return experiencePerLevel[level];
        }
        else
        {
            // Level Invalid
            Debug.LogError("Level invalid: " + level);
            return 100;
        }
    }

    public int GetSkillSPoints()
    {
        return SkillsPoints;
    }

    public bool IsMaxLevel()
    {
        return IsMaxLevel(Level);
    }

    public bool IsMaxLevel(int level)
    {
        return level == experiencePerLevel.Length - 1;
    }

    #endregion
}
