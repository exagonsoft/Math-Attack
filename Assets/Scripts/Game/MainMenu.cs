using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    #region Properties
    private List<Transform> EntriesTransformList;
    private Transform entrieContainer;
    private Transform entrieTemplate;
    
    #endregion

    #region Events


    private void Awake()
    {
        transform.Find("MainMenuButtons").GetComponent<Animator>().SetTrigger("FirstEneter");
        //SetingUp the Game Components
        SetUPGameData();


        //Handling the Main Menu Scene
        entrieContainer = transform.Find("Scores").Find("Table").Find("Entries");
        entrieTemplate = transform.Find("Scores").Find("Table").Find("Entries").Find("EntrieTemplate");
        entrieTemplate.gameObject.SetActive(false);

        //Declaring Objects Events

        //Main Menu Buttons
        transform.Find("MainMenuButtons").Find("btnNewGame").GetComponent<UI_Button>().ClickFunc = () => {
            ResetPlayerStats();
            GameConfigurator.SelectedLevel = 1;

            StartCoroutine(CrossFade.InstanSiateCrossFade.CrossFade_Show(Loader_Class.Scene.Game));
        };

        if (CheckGameStarted())
        {
            transform.Find("MainMenuButtons").Find("btnContinue").GetComponent<UI_Button>().ClickFunc = () => {
                Loader_Class.Load(Loader_Class.Scene.LevelMenu);
            };
        }
        else
        {
            transform.Find("MainMenuButtons").Find("btnContinue").GetComponent<UI_Button>().hoverBehaviour_Move = false;
        }

        transform.Find("MainMenuButtons").Find("btnTutorial").GetComponent<UI_Button>().ClickFunc = () => {
            ShowTutorial();
        };

        transform.Find("MainMenuButtons").Find("HighScores").GetComponent<UI_Button>().ClickFunc = () => {
            ShowScores();
        };

        transform.Find("MainMenuButtons").Find("btnExit").GetComponent<UI_Button>().ClickFunc = () => Application.Quit();

       //Tutorial SubMenu Buttons

        transform.Find("Tutorial").Find("btnBack").GetComponent<UI_Button>().ClickFunc = () => {
            ShowMainMenu();
        };

        //HighScores Submenu Buttons
        transform.Find("Scores").Find("btnBack").GetComponent<UI_Button>().ClickFunc = () => {
            ShowMainMenu();
        };

        transform.Find("Scores").Find("btnReset").GetComponent<UI_Button>().ClickFunc = () => {
            PlayerPrefs.SetString("Scores", "");
            PlayerPrefs.Save();
            LoadScores();
        };
    }

    #endregion

    #region Functions

    //IEnumerator ShowFade(Loader_Class.Scene sCene)
    //{
    //    transform.parent.Find("pfCrossFade").GetComponent<Animator>().SetTrigger("FadeOut");
    //    yield return new WaitForSeconds(1f);
    //    Loader_Class.Load(sCene);
    //}

    private void ShowMainMenu()
    {
        transform.Find("MainMenuButtons").gameObject.SetActive(true);
        transform.Find("Scores").gameObject.SetActive(false);
        transform.Find("Tutorial").gameObject.SetActive(false);
    }

    private void ShowScores() {
        transform.Find("MainMenuButtons").gameObject.SetActive(false);
        transform.Find("Scores").GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        LoadScores();
        transform.Find("Scores").gameObject.SetActive(true);
    }

    private void ShowTutorial()
    {
        transform.Find("MainMenuButtons").gameObject.SetActive(false);
        transform.Find("Tutorial").GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        transform.Find("Scores").gameObject.SetActive(false);
        transform.Find("Tutorial").gameObject.SetActive(true);
        
    }

    private void LoadScores() {
        string JsonData = PlayerPrefs.GetString("Scores");
        if(JsonData != null && JsonData != "")
        {
            Top10Scores top10Scores = JsonUtility.FromJson<Top10Scores>(JsonData);
            CreateTransforms(top10Scores.Top10List);
        }
    }

    private void CreateTransforms(List<ScoreEntrie> scoresList)
    {
        EntriesTransformList = new List<Transform>();
        int iEntriesCounter = 0;
        foreach (ScoreEntrie entire in scoresList)
        {
            if (iEntriesCounter < 10)
                CreateScoreEntrie(entire, entrieContainer, EntriesTransformList);

            iEntriesCounter++;
        }
    }

    private void CreateScoreEntrie(ScoreEntrie newEntrie, Transform entrieContainer, List<Transform> entrieList)
    {
        float templateHeigth = 30f;
        Transform entrieTransform = Instantiate(entrieTemplate, entrieContainer);
        RectTransform entrieRectTransform = entrieTransform.GetComponent<RectTransform>();
        entrieRectTransform.anchoredPosition = new Vector2(0, -templateHeigth * entrieList.Count);
        entrieTransform.gameObject.SetActive(true);

        int Rank = entrieList.Count + 1;
        string RankString;
        switch (Rank)
        {
            default:
                RankString = Rank + "TH"; break;

            case 1: RankString = "1ST"; break;
            case 2: RankString = "2ND"; break;
            case 3: RankString = "3RD"; break;

        }
        entrieTransform.Find("ScorePos").GetComponent<Text>().text = RankString;
        int sCore = newEntrie.score;
        entrieTransform.Find("Score").GetComponent<Text>().text = sCore.ToString();
        string name = newEntrie.name;
        entrieTransform.Find("Name").GetComponent<Text>().text = name;

        entrieList.Add(entrieTransform);
    }

    private void SetUPGameData()
    {
        if (!PlayerPrefs.HasKey("Score"))
        {
            PlayerPrefs.SetString("Score", "");
            PlayerPrefs.Save();
        }
        if (!PlayerPrefs.HasKey("Levels"))
        {
            PlayerPrefs.SetString("Levels", "");
            PlayerPrefs.Save();
        }

    }

    private void ResetPlayerStats()
    {
        int[] ilevelStars = new int[11];
        for (int iLevel = 0; iLevel < 11; iLevel++)
        {
            ilevelStars[iLevel] = 0;
        }
        PlayerStats startStats = new PlayerStats
        {
            AvalibleSkillPoints = 0,
            PlayerLevel = 1,
            PlayerExperience = 0,
            currentLevel = 1,
            maxHealth = 50,
            score = 0,
            DUMBweaponDamage = 0,
            FIZZweaponDamage = 0,
            BUZZweaponDamage = 0,
            FIZZBUZZweaponDamage = 0,
            RoketSpeed = 100f,
            slowTimeOnSelect = .6f,
            spownSpeedModificator = 0f,
            levelStars = ilevelStars
        
        };
        StatsofPlayer defoultStats = new StatsofPlayer { stats = startStats };
        string jsonPlayerStats = JsonUtility.ToJson(defoultStats);
        Base_Utils.WriteData(jsonPlayerStats);
    }

    private bool CheckGameStarted()
    {
        bool startedGame = false;

        string jsonPlayerString = Base_Utils.ReadData();
        StatsofPlayer tempStats = JsonUtility.FromJson<StatsofPlayer>(jsonPlayerString);
        if (tempStats.stats.currentLevel > 1)
        {
            startedGame = true;
        }
        return startedGame;
    }

    #endregion

    #region Private Class

    private class Top10Scores
    {
        public List<ScoreEntrie> Top10List;
    }

    [System.Serializable]
    private class ScoreEntrie
    {
        public int score;
        public string name;
    }

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
