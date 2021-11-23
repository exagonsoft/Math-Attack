using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GettingReady : MonoBehaviour
{
    #region Properties
    private int TimerTick;
    private GameSystem gameSystem;
    private ScoreSystem scoreSystem;

    #endregion

    #region Events

    private void Awake()
    {
        TimerTick = 3;
    }


    #endregion

    #region Functions
    public void TimmerTick()
    {
        string timmerMessage = "GO!";
        if (TimerTick > 0)
        {
            TimerTick--;
            timmerMessage = TimerTick.ToString();
        }

        transform.Find("Message").Find("Timer").GetComponent<Text>().text = timmerMessage;
    }

    public void StartWave()
    {
        TimerTick = 3;
        gameSystem.StartLevelWave();
    }

    public void StartSound()
    {
        gameSystem.StartLevelSound();
    }

    private void GetReady()
    {
        transform.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        transform.Find("Message").Find("lblLevel").GetComponent<Text>().text = scoreSystem.GetLevel().ToString();
        transform.Find("Message").Find("lblWavaes").GetComponent<Text>().text = scoreSystem.GetWave().ToString();
        gameObject.SetActive(true);
        transform.GetComponent<Animator>().SetTrigger("GetReady");
    }

    public void SetUpPart(GameSystem gameGameSystem, ScoreSystem gameScoreSystem)
    {
        this.gameSystem = gameGameSystem;
        this.scoreSystem = gameScoreSystem;

        gameSystem.onGettingReady += GameSystem_onGettingReady;
        gameSystem.onReady += GameSystem_onReady;
    }

    private void GameSystem_onReady(object sender, System.EventArgs e)
    {
        gameObject.SetActive(false);
    }

    private void GameSystem_onGettingReady(object sender, System.EventArgs e)
    {
        GetReady();
    }

    #endregion
}
