using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneBackGroundHandler : MonoBehaviour
{
    #region Properties
    [SerializeField] private Transform pfFireWork;
    private GameSystem gameSystem;
    private ScoreSystem scoreSystem;
    private HealtSystem healthSystem;
    private LevelSystem levelSystem;
    #endregion

    #region Events


    #endregion

    #region Functions

    public void SetUpBackGround(GameSystem gameGameSystem, ScoreSystem gameScoreSystem, HealtSystem gameHealthSystem, LevelSystem gameLevelSystem)
    {
        this.gameSystem = gameGameSystem;
        this.scoreSystem = gameScoreSystem;
        this.healthSystem = gameHealthSystem;
        this.levelSystem = gameLevelSystem;
        transform.Find("SceneUISpace").Find("GetReady").GetComponent<GettingReady>().SetUpPart(gameGameSystem, scoreSystem);
        transform.Find("SceneUISpace").Find("PauseWindow").GetComponent<PauseWindow>().SetUpWindow(gameGameSystem);
        gameSystem.onPlayerDestroyed += GameSystem_onPlayerDestroyed;
        scoreSystem.onWaveChange += ScoreSystem_onWaveChange;
        scoreSystem.onLevelClined += ScoreSystem_onLevelClined;
    }

    

    private void ScoreSystem_onWaveChange(object sender, System.EventArgs e)
    {
        transform.Find("SceneUISpace").Find("GetReady").Find("Message").Find("Timer").GetComponent<Text>().text = "3";
        gameSystem.ShowGetReady();
    }

    private void GameSystem_onPlayerDestroyed(object sender, System.EventArgs e)
    {
        transform.GetComponent<Animator>().SetTrigger("GameOver");
        transform.Find("SceneUISpace").Find("GameOverScreen").GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        transform.Find("SceneUISpace").Find("GameOverScreen").gameObject.SetActive(true);
    }

    private void ScoreSystem_onLevelClined(object sender, System.EventArgs e)
    {
        transform.GetComponent<Animator>().SetTrigger("StageClear");
        transform.Find("SceneUISpace").Find("StageClean").GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        transform.Find("SceneUISpace").Find("StageClean").gameObject.SetActive(true);
        StartCoroutine(LunshFireworks());
    }

    public void ProcessGameOver()
    {
        int score = transform.Find("SceneUISpace").Find("IndicatorsArea").GetComponent<ScoreWindow>().getScoreSystem().GetScore();
        Transform scoreWscoreDilog = transform.Find("SceneUISpace").Find("ScoreTable");
        scoreWscoreDilog.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        scoreWscoreDilog.GetComponent<HighScore>().ProcessScores(score);
    }

    private IEnumerator LunshFireworks()
    {
        int iCounter = 0;
        while (iCounter < 20)
        {
            Vector3 fireworkPosition = Base_Utils.getRandomWorldPosition();
            Transform FireWork = Instantiate(pfFireWork, fireworkPosition, Quaternion.identity);
            TimerFunction.Create(() => Destroy(FireWork.gameObject), 3f);
            iCounter++;
            yield return new WaitForSeconds(3f);
        }

    }

    public void ProcessStageClear()
    {

        string jsonPlayerStats = Base_Utils.ReadData();
        PlayerStats newStats = JsonUtility.FromJson<StatsofPlayer>(jsonPlayerStats).stats;

        int levelScore = Base_Utils.CalcScore(healthSystem.GetHealthPercent());
        int[] tempLevelStars = newStats.levelStars;
        if(tempLevelStars[GameConfigurator.SelectedLevel] < levelScore)
        {
            tempLevelStars[GameConfigurator.SelectedLevel] = levelScore;
        }
        if(newStats.currentLevel == GameConfigurator.SelectedLevel)
        {
            newStats.currentLevel = scoreSystem.GetLevel() + 1;
        }
        newStats.levelStars = tempLevelStars;
        newStats.PlayerExperience = levelSystem.GetExperience();
        newStats.score = scoreSystem.GetScore();

        if(levelSystem.GetSkillSPoints() > 0)
        {
            newStats.PlayerLevel = levelSystem.GetLevel();
            newStats.maxHealth = healthSystem.GetCurrentMaxHealth();
            newStats.AvalibleSkillPoints = levelSystem.GetSkillSPoints();
        }

        StatsofPlayer defoultStats = new StatsofPlayer { stats = newStats };
        jsonPlayerStats = JsonUtility.ToJson(defoultStats);
        Base_Utils.WriteData(jsonPlayerStats);
        StartCoroutine(CrossFade.InstanSiateCrossFade.CrossFade_Show(Loader_Class.Scene.LevelMenu));
    }

    #endregion


    #region Private Class

    private class StatsofPlayer
    {
        public PlayerStats stats;
    }

    [System.Serializable]
    private class PlayerStats
    {
        public int AvalibleSkillPoints;
        public int PlayerLevel;
        public int PlayerExperience;
        public int currentLevel;
        public int maxHealth;
        public int score;
        public int DUMBweaponDamage;
        public int FIZZweaponDamage;
        public int BUZZweaponDamage;
        public int FIZZBUZZweaponDamage;
        public float RoketSpeed;
        public float slowTimeOnSelect;
        public float spownSpeedModificator;
        public int[] levelStars;
    }

    #endregion
}
