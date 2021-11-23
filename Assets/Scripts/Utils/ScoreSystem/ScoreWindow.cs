using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreWindow : MonoBehaviour
{
    #region Properties
    private Transform ScoreWindowTrnasform;
    private ScoreSystem ScoreSystem;

    #endregion

    #region Events

    #endregion

    #region Functions
    public void SetUpWindow(ScoreSystem scoreSystem)
    {
        this.ScoreSystem = scoreSystem;
        transform.Find("Level").GetComponent<Text>().text = ScoreSystem.GetLevel().ToString();
        transform.Find("Waves").GetComponent<Text>().text = ScoreSystem.GetWaves().ToString();
        transform.Find("Enemies").GetComponent<Text>().text = ScoreSystem.GetLEvelEnemies().ToString();
        transform.Find("ReminderWaves").GetComponent<Text>().text = ScoreSystem.GetWave().ToString();
        transform.Find("RemainderEnemis").GetComponent<Text>().text = ScoreSystem.GEtRemainderEnemies().ToString();
        transform.Find("Score").GetComponent<Text>().text = ScoreSystem.GetScore().ToString();
        ScoreSystem.onEnemyDestroyed += ScoreSystem_onEnemyDestroyed;
        ScoreSystem.onWaveChange += ScoreSystem_onWaveChange;
        scoreSystem.onLevelClined += ScoreSystem_onLevelClined;
    }

    

    private void ScoreSystem_onWaveChange(object sender, System.EventArgs e)
    {
        transform.Find("ReminderWaves").GetComponent<Text>().text = ScoreSystem.GetWave().ToString();
        transform.Find("RemainderEnemis").GetComponent<Text>().text = ScoreSystem.GEtRemainderEnemies().ToString();
        transform.Find("Enemies").GetComponent<Text>().text = ScoreSystem.GetLEvelEnemies().ToString();
        transform.Find("Score").GetComponent<Text>().text = ScoreSystem.GetScore().ToString();
    }

    private void ScoreSystem_onEnemyDestroyed(object sender, System.EventArgs e)
    {
        transform.Find("RemainderEnemis").GetComponent<Text>().text = ScoreSystem.GEtRemainderEnemies().ToString();
        transform.Find("Score").GetComponent<Text>().text = ScoreSystem.GetScore().ToString();
    }

    private void ScoreSystem_onLevelClined(object sender, System.EventArgs e)
    {
        transform.Find("RemainderEnemis").GetComponent<Text>().text = ScoreSystem.GEtRemainderEnemies().ToString();
    }

    public ScoreSystem getScoreSystem()
    {
        return this.ScoreSystem;
    }
    #endregion
}
