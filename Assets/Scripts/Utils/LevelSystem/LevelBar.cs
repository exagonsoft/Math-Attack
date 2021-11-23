using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelBar : MonoBehaviour
{
    #region Properties
    private Transform Bar;
    private LevelSystem LevelSystem;

    #endregion

    #region Events

    void Awake()
    {
        
    }

    #endregion

    #region Functions

    public void SetUpWindow(LevelSystem levelSystem)
    {
        LevelSystem = levelSystem;
        Bar = transform.Find("Bar");
        SetExperience();
        SetLevel();
        SetSkillPoints();
        LevelSystem.onExperienceChange += LevelSystem_onExperienceChange;
        levelSystem.onLevelChange += LevelSystem_onLevelChange;
    }

    private void LevelSystem_onLevelChange(object sender, System.EventArgs e)
    {
        SetLevel();
        SetSkillPoints();
    }

    private void LevelSystem_onExperienceChange(object sender, LevelSystem.OnExperienceChangeEventArgs e)
    {
        ShowExperiencePopUp(e.experienceAmount);
        SetExperience();
    }

    private void SetExperience()
    {
        
        float experiencePercent = LevelSystem.GetExpirencePercent();
        Bar.localScale = new Vector3(experiencePercent, 1);
    }

    private void ShowExperiencePopUp(int experienceAmount)
    {
        Transform parent = transform.parent.parent;
        Experience_PopUp.Create(experienceAmount, parent);
    }

    private void SetLevel()
    {
        transform.Find("LevelNumber").GetComponent<Text>().text = LevelSystem.GetLevel().ToString();
    }

    private void SetSkillPoints()
    {
        transform.Find("AvialableSkillPoints").Find("Skills").GetComponent<Text>().text = LevelSystem.GetSkillSPoints().ToString();
    }

    #endregion
}
