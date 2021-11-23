using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour
{
    #region Properties

    private Transform entrieContainer;
    private Transform entrieTemplate;
    private Transform entrieDialog;
    private Transform scoreTable;
    private List<ScoreEntrie> EntriesList;
    private List<Transform> EntriesTransformList;
    private int newScore;

    #endregion

    #region Events

    void Awake()
    {
        gameObject.SetActive(false);
        scoreTable = transform.Find("Table");
        entrieDialog = transform.Find("ScoreDialog");
        entrieContainer = transform.Find("Table").Find("Entries");
        entrieTemplate = transform.Find("Table").Find("Entries").Find("EntrieTemplate");
        entrieTemplate.gameObject.SetActive(false);
        scoreTable.gameObject.SetActive(false);

        entrieDialog.Find("bntOk").GetComponent<UI_Button>().ClickFunc = () => {

            string sName = entrieDialog.Find("yourName").Find("Text").GetComponent<Text>().text;
            if (sName != null && sName != "")
            {
                
                ShowTable(newScore, sName);
            }
            else
            {
                entrieDialog.Find("yourName").Find("Placeholder").GetComponent<Text>().color = Color.red;
            }
            
        };

        scoreTable.Find("bntMainMenu").GetComponent<UI_Button>().ClickFunc = () => {

            StartCoroutine(CrossFade.InstanSiateCrossFade.CrossFade_Show(Loader_Class.Scene.MainMenu));
        };
    }

    void Update()
    {

    }

    #endregion

    #region Functions

    public void ProcessScores(int scoreValue)
    {
        newScore = scoreValue;
        entrieDialog.Find("yourScore").GetComponent<Text>().text = newScore.ToString();
        //entrieDialog.Find("yourScore").GetComponent<Text>().text = nameValue;
        gameObject.SetActive(true);
    }

    public void ShowTable(int score, string name)
    {
        EntriesList = new List<ScoreEntrie>();
        entrieDialog.gameObject.SetActive(false);
        string JsonData = PlayerPrefs.GetString("Scores");
        if (JsonData != null && JsonData != "")
        {
            Top10Scores top10Scores = JsonUtility.FromJson<Top10Scores>(JsonData);
            EntriesList = top10Scores.Top10List;
        }

        ScoreEntrie newScore = new ScoreEntrie { name = name, score = score };
        EntriesList.Add(newScore);
        EntriesList = SortList(EntriesList);
        SaveScore(EntriesList);
        CreateTransforms(EntriesList);

        if (EntriesList.Count < 9)
        {
            
            
        }
        else
        {

        }

        scoreTable.gameObject.SetActive(true);
        
        
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

    private List<ScoreEntrie> SortList(List<ScoreEntrie> list)
    {
        for (int iCounter = 0; iCounter < list.Count; iCounter++)
        {
            for (int iComparer = iCounter + 1; iComparer < list.Count; iComparer++)
            {
                if (list[iComparer].score > list[iCounter].score)
                {
                    ScoreEntrie tempEntrie = list[iCounter];
                    list[iCounter] = list[iComparer];
                    list[iComparer] = tempEntrie;
                }
            }
        }

        return list;
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

    private void SaveScore(List<ScoreEntrie> newScores)
    {
        Top10Scores top10Scores = new Top10Scores { Top10List = newScores };
        string jsonScore = JsonUtility.ToJson(top10Scores);
        PlayerPrefs.SetString("Scores", jsonScore);
        PlayerPrefs.Save();
    }

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

    #endregion
}
