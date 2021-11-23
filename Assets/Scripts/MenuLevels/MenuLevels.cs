using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuLevels : MonoBehaviour
{
    #region Properties
    private MenuLevelEngine menuLevelEngine;
    private PlayerStats playerStats;
    private List<Level> levelsList;

    private Vector3 Target;
    private Transform Player;
    #endregion

    #region Events

    private void Start()
    {
        SoundManager.PlaySound(SoundManager.GameSounds.PlayerEnter);
    }

    private void Awake()
    {
        SetUpSystems();
        LoadData();
        InitialiceMenuObjects();
        FillReminderSkills();
        FillSkillsMenuData();
        SetUPPlayer();
        ShowPlayer();
    }

    private void Update()
    {

    }


    #endregion

    #region Functions

    private void SetUpSystems()
    {
        this.menuLevelEngine = new MenuLevelEngine();
    }

    private void InitialiceMenuObjects()
    {
        SettingUpLevels();

        transform.Find("Confirm Dialog").gameObject.SetActive(false);
        transform.Find("SkillsMenu").gameObject.SetActive(false);
        transform.Find("MainView").gameObject.SetActive(true);

        transform.Find("MainView").Find("btnBack").GetComponent<UI_Button>().ClickFunc = () => {
            ShowDialog();
        };

        transform.Find("MainView").Find("btnSkillsMenu").GetComponent<UI_Button>().ClickFunc = () => {
            ShowSkillMenu();
        };

        transform.Find("Confirm Dialog").Find("btnConfirmationOk").GetComponent<UI_Button>().ClickFunc = () => {
            Loader_Class.Load(Loader_Class.Scene.MainMenu);
        };

        transform.Find("Confirm Dialog").Find("bntConfirmCancel").GetComponent<UI_Button>().ClickFunc = () => {
            HideDialog();
        };

        transform.Find("SkillsMenu").Find("btnBack").GetComponent<UI_Button>().ClickFunc = () => {
            HideSkillMenu();
        };

        transform.Find("MainView").Find("Levels").Find("Level1").GetComponent<Cust_Button>().ClickFunc = () => {
            GameConfigurator.SelectedLevel = 1;
            Vector3 targetPosition = transform.Find("MainView").Find("Levels").Find("Level1").Find("PlayerDok").position;
            menuLevelEngine.MuvePlayer(targetPosition, false);
        };

        if (!isLocked(2))
        {
            transform.Find("MainView").Find("Levels").Find("Level2").GetComponent<Cust_Button>().ClickFunc = () => {
                GameConfigurator.SelectedLevel = 2;
                Vector3 targetPosition = transform.Find("MainView").Find("Levels").Find("Level2").Find("PlayerDok").position;
                menuLevelEngine.MuvePlayer(targetPosition, false);
            };
        }
        if (!isLocked(3))
        {
            transform.Find("MainView").Find("Levels").Find("Level3").GetComponent<Cust_Button>().ClickFunc = () => {
                GameConfigurator.SelectedLevel = 3;
                Vector3 targetPosition = transform.Find("MainView").Find("Levels").Find("Level3").Find("PlayerDok").position;
                menuLevelEngine.MuvePlayer(targetPosition, false);
            };
        }
        if (!isLocked(4))
        {
            transform.Find("MainView").Find("Levels").Find("Level4").GetComponent<Cust_Button>().ClickFunc = () => {
                GameConfigurator.SelectedLevel = 4;
                Loader_Class.Load(Loader_Class.Scene.Game);
            };
        }
        if (!isLocked(5))
        {
            transform.Find("MainView").Find("Levels").Find("Level5").GetComponent<Cust_Button>().ClickFunc = () => {
                GameConfigurator.SelectedLevel = 5;
                Loader_Class.Load(Loader_Class.Scene.Game);
            };
        }
        if (!isLocked(6))
        {
            transform.Find("MainView").Find("Levels").Find("Level6").GetComponent<Cust_Button>().ClickFunc = () => {
                GameConfigurator.SelectedLevel = 6;
                Loader_Class.Load(Loader_Class.Scene.Game);
            };
        }
        if (!isLocked(7))
        {
            transform.Find("MainView").Find("Levels").Find("Level7").GetComponent<Cust_Button>().ClickFunc = () => {
                GameConfigurator.SelectedLevel = 7;
                Loader_Class.Load(Loader_Class.Scene.Game);
            };
        }
        if (!isLocked(8))
        {
            transform.Find("MainView").Find("Levels").Find("Level8").GetComponent<Cust_Button>().ClickFunc = () => {
                GameConfigurator.SelectedLevel = 8;
                Loader_Class.Load(Loader_Class.Scene.Game);
            };
        }
        if (!isLocked(9))
        {
            transform.Find("MainView").Find("Levels").Find("Level9").GetComponent<Cust_Button>().ClickFunc = () => {
                GameConfigurator.SelectedLevel = 9;
                Loader_Class.Load(Loader_Class.Scene.Game);
            };
        }
        if (!isLocked(10))
        {
            transform.Find("MainView").Find("Levels").Find("Level10").GetComponent<Cust_Button>().ClickFunc = () => {
                GameConfigurator.SelectedLevel = 10;
                Loader_Class.Load(Loader_Class.Scene.Game);
            };
        }

        transform.Find("SkillsMenu").Find("Weapon Updates").Find("btnDumbUpgrade").GetComponent<UI_Button>().ClickFunc = () => {
            if (playerStats.AvalibleSkillPoints > 0)
            {
                string jsonPlayerStats = Base_Utils.ReadData();
                PlayerStats newStats = JsonUtility.FromJson<StatsofPlayer>(jsonPlayerStats).stats;

                
                newStats.DUMBweaponDamage += 1;
                newStats.AvalibleSkillPoints -= 1;

                StatsofPlayer defoultStats = new StatsofPlayer { stats = newStats };
                jsonPlayerStats = JsonUtility.ToJson(defoultStats);
                Base_Utils.WriteData(jsonPlayerStats);
                playerStats = newStats;
                transform.Find("SkillsMenu").Find("Weapon Updates").Find("btnDumbUpgrade").Find("Level").Find("value").GetComponent<Text>().text = newStats.DUMBweaponDamage.ToString();
                transform.Find("SkillsMenu").Find("RemainingPoints").Find("Skills").GetComponent<Text>().text = newStats.AvalibleSkillPoints.ToString();

            }
        };

        transform.Find("SkillsMenu").Find("Weapon Updates").Find("btnFizzUpgrade").GetComponent<UI_Button>().ClickFunc = () => {
            if (playerStats.AvalibleSkillPoints > 0)
            {
                string jsonPlayerStats = Base_Utils.ReadData();
                PlayerStats newStats = JsonUtility.FromJson<StatsofPlayer>(jsonPlayerStats).stats;


                newStats.FIZZweaponDamage += 1;
                newStats.AvalibleSkillPoints -= 1;

                StatsofPlayer defoultStats = new StatsofPlayer { stats = newStats };
                jsonPlayerStats = JsonUtility.ToJson(defoultStats);
                Base_Utils.WriteData(jsonPlayerStats);
                playerStats = newStats;
                transform.Find("SkillsMenu").Find("Weapon Updates").Find("btnFizzUpgrade").Find("Level").Find("value").GetComponent<Text>().text = newStats.FIZZweaponDamage.ToString();
                transform.Find("SkillsMenu").Find("RemainingPoints").Find("Skills").GetComponent<Text>().text = newStats.AvalibleSkillPoints.ToString();

            }
        };

        transform.Find("SkillsMenu").Find("Weapon Updates").Find("btnBuzzUpgrade").GetComponent<UI_Button>().ClickFunc = () => {
            if (playerStats.AvalibleSkillPoints > 0)
            {
                string jsonPlayerStats = Base_Utils.ReadData();
                PlayerStats newStats = JsonUtility.FromJson<StatsofPlayer>(jsonPlayerStats).stats;


                newStats.BUZZweaponDamage += 1;
                newStats.AvalibleSkillPoints -= 1;

                StatsofPlayer defoultStats = new StatsofPlayer { stats = newStats };
                jsonPlayerStats = JsonUtility.ToJson(defoultStats);
                Base_Utils.WriteData(jsonPlayerStats);
                playerStats = newStats;
                transform.Find("SkillsMenu").Find("Weapon Updates").Find("btnBuzzUpgrade").Find("Level").Find("value").GetComponent<Text>().text = newStats.BUZZweaponDamage.ToString();
                transform.Find("SkillsMenu").Find("RemainingPoints").Find("Skills").GetComponent<Text>().text = newStats.AvalibleSkillPoints.ToString();

            }
        };

        transform.Find("SkillsMenu").Find("Weapon Updates").Find("btnFizzBuzzUpgrade").GetComponent<UI_Button>().ClickFunc = () => {
            if (playerStats.AvalibleSkillPoints > 0)
            {
                string jsonPlayerStats = Base_Utils.ReadData();
                PlayerStats newStats = JsonUtility.FromJson<StatsofPlayer>(jsonPlayerStats).stats;


                newStats.FIZZBUZZweaponDamage += 1;
                newStats.AvalibleSkillPoints -= 1;

                StatsofPlayer defoultStats = new StatsofPlayer { stats = newStats };
                jsonPlayerStats = JsonUtility.ToJson(defoultStats);
                Base_Utils.WriteData(jsonPlayerStats);
                playerStats = newStats;
                transform.Find("SkillsMenu").Find("Weapon Updates").Find("btnFizzBuzzUpgrade").Find("Level").Find("value").GetComponent<Text>().text = newStats.FIZZBUZZweaponDamage.ToString();
                transform.Find("SkillsMenu").Find("RemainingPoints").Find("Skills").GetComponent<Text>().text = newStats.AvalibleSkillPoints.ToString();

            }
        };

        transform.Find("SkillsMenu").Find("General Updates").Find("btnRoketsSpeedUpgrade").GetComponent<UI_Button>().ClickFunc = () => {
            if (playerStats.AvalibleSkillPoints > 0)
            {
                string jsonPlayerStats = Base_Utils.ReadData();
                PlayerStats newStats = JsonUtility.FromJson<StatsofPlayer>(jsonPlayerStats).stats;


                newStats.RoketSpeed += 1;
                newStats.AvalibleSkillPoints -= 1;

                StatsofPlayer defoultStats = new StatsofPlayer { stats = newStats };
                jsonPlayerStats = JsonUtility.ToJson(defoultStats);
                Base_Utils.WriteData(jsonPlayerStats);
                playerStats = newStats;
                transform.Find("SkillsMenu").Find("General Updates").Find("btnRoketsSpeedUpgrade").Find("Level").Find("value").GetComponent<Text>().text = newStats.RoketSpeed.ToString();
                transform.Find("SkillsMenu").Find("RemainingPoints").Find("Skills").GetComponent<Text>().text = newStats.AvalibleSkillPoints.ToString();

            }
        };

        transform.Find("SkillsMenu").Find("General Updates").Find("btnSlowTime").GetComponent<UI_Button>().ClickFunc = () => {
            if (playerStats.AvalibleSkillPoints > 0)
            {
                string jsonPlayerStats = Base_Utils.ReadData();
                PlayerStats newStats = JsonUtility.FromJson<StatsofPlayer>(jsonPlayerStats).stats;


                newStats.slowTimeOnSelect -= .1f;
                newStats.AvalibleSkillPoints -= 1;

                StatsofPlayer defoultStats = new StatsofPlayer { stats = newStats };
                jsonPlayerStats = JsonUtility.ToJson(defoultStats);
                Base_Utils.WriteData(jsonPlayerStats);
                playerStats = newStats;
                transform.Find("SkillsMenu").Find("General Updates").Find("btnSlowTime").Find("Level").Find("value").GetComponent<Text>().text = newStats.slowTimeOnSelect.ToString();
                transform.Find("SkillsMenu").Find("RemainingPoints").Find("Skills").GetComponent<Text>().text = newStats.AvalibleSkillPoints.ToString();

            }
        };

        transform.Find("SkillsMenu").Find("General Updates").Find("btnSpawnSpeed").GetComponent<UI_Button>().ClickFunc = () => {
            if (playerStats.AvalibleSkillPoints > 0)
            {
                string jsonPlayerStats = Base_Utils.ReadData();
                PlayerStats newStats = JsonUtility.FromJson<StatsofPlayer>(jsonPlayerStats).stats;


                newStats.spownSpeedModificator += 1f;
                newStats.AvalibleSkillPoints -= 1;

                StatsofPlayer defoultStats = new StatsofPlayer { stats = newStats };
                jsonPlayerStats = JsonUtility.ToJson(defoultStats);
                Base_Utils.WriteData(jsonPlayerStats);
                playerStats = newStats;
                transform.Find("SkillsMenu").Find("General Updates").Find("btnSpawnSpeed").Find("Level").Find("value").GetComponent<Text>().text = newStats.spownSpeedModificator.ToString();
                transform.Find("SkillsMenu").Find("RemainingPoints").Find("Skills").GetComponent<Text>().text = newStats.AvalibleSkillPoints.ToString();

            }
        };

    }

    private void ShowDialog()
    {
        transform.Find("Confirm Dialog").GetComponent<RectTransform>().anchoredPosition = new Vector3(0f, 0f, -30f);
        transform.Find("MainView").gameObject.SetActive(false);
        transform.Find("Confirm Dialog").gameObject.SetActive(true);
    }

    private void HideDialog()
    {
        transform.Find("Confirm Dialog").gameObject.SetActive(false);
        transform.Find("MainView").gameObject.SetActive(true);
    }

    private void ShowSkillMenu()
    {
        transform.Find("SkillsMenu").GetComponent<RectTransform>().anchoredPosition = new Vector3(0f, 0f, -30f);
        transform.Find("MainView").gameObject.SetActive(false);
        transform.Find("SkillsMenu").gameObject.SetActive(true);
    }

    private void HideSkillMenu()
    {
        transform.Find("SkillsMenu").gameObject.SetActive(false);
        transform.Find("MainView").gameObject.SetActive(true);
    }

    private void LoadData()
    {
        string jsonData = Base_Utils.ReadData();
        StatsofPlayer tempStats = JsonUtility.FromJson<StatsofPlayer>(jsonData);
        playerStats = tempStats.stats;

        string jsonLevels = PlayerPrefs.GetString("Levels");
        LevelStats tempLevels = JsonUtility.FromJson<LevelStats>(jsonLevels);
        levelsList = tempLevels.LevelStatsList;
    }

    private void SettingUpLevels()
    {
        int iLevelNumber = 1;
        foreach (Level level in levelsList)
        {
            string levelObject = "Level" + iLevelNumber.ToString();

            //Repainting Planets, Chequing if is Locked, and painting Stars
            if (iLevelNumber == 1)
            {
                transform.Find("MainView").Find("Levels").Find(levelObject).GetComponent<Menu_Level>().SetUpLevel(playerStats.levelStars[iLevelNumber], false, level.SpaceBack_Mat);
            }
            else
            {
                transform.Find("MainView").Find("Levels").Find(levelObject).GetComponent<Menu_Level>().SetUpLevel(playerStats.levelStars[iLevelNumber], isLocked(iLevelNumber), level.SpaceBack_Mat);
            }


            iLevelNumber++;
        }
    }

    private void FillReminderSkills()
    {
        Transform SkillsSign = transform.Find("MainView").Find("btnSkillsMenu").Find("SkillSPoints");
        if (playerStats.AvalibleSkillPoints > 0)
        {
            SkillsSign.Find("Skills").GetComponent<Text>().text = playerStats.AvalibleSkillPoints.ToString();
        }
        else
        {
            SkillsSign.gameObject.SetActive(false);
        }
    }

    private void FillSkillsMenuData()
    {
        transform.Find("SkillsMenu").Find("RemainingPoints").Find("Skills").GetComponent<Text>().text = playerStats.AvalibleSkillPoints.ToString();

        transform.Find("SkillsMenu").Find("Basic Stats").Find("Level").Find("Text").GetComponent<Text>().text = playerStats.PlayerLevel.ToString();
        transform.Find("SkillsMenu").Find("Basic Stats").Find("Experience").Find("Text").GetComponent<Text>().text = playerStats.PlayerExperience.ToString();
        transform.Find("SkillsMenu").Find("Basic Stats").Find("ToNextLevel").Find("Text").GetComponent<Text>().text = playerStats.AvalibleSkillPoints.ToString();
        transform.Find("SkillsMenu").Find("Basic Stats").Find("Healt").Find("Text").GetComponent<Text>().text = playerStats.maxHealth.ToString();

        transform.Find("SkillsMenu").Find("Weapon Stats").Find("Dumb Damage").Find("Text").GetComponent<Text>().text = (playerStats.DUMBweaponDamage * 2).ToString();
        transform.Find("SkillsMenu").Find("Weapon Stats").Find("Fizz Damage").Find("Text").GetComponent<Text>().text = (playerStats.FIZZweaponDamage * 2).ToString();
        transform.Find("SkillsMenu").Find("Weapon Stats").Find("Buzz Damage").Find("Text").GetComponent<Text>().text = (playerStats.BUZZweaponDamage * 2).ToString();
        transform.Find("SkillsMenu").Find("Weapon Stats").Find("Fizz - Buzz Damage").Find("Text").GetComponent<Text>().text = (playerStats.FIZZBUZZweaponDamage *2).ToString();

        transform.Find("SkillsMenu").Find("Weapon Updates").Find("btnDumbUpgrade").Find("Level").Find("value").GetComponent<Text>().text = playerStats.DUMBweaponDamage.ToString();
        transform.Find("SkillsMenu").Find("Weapon Updates").Find("btnFizzUpgrade").Find("Level").Find("value").GetComponent<Text>().text = playerStats.FIZZweaponDamage.ToString();
        transform.Find("SkillsMenu").Find("Weapon Updates").Find("btnBuzzUpgrade").Find("Level").Find("value").GetComponent<Text>().text = playerStats.BUZZweaponDamage.ToString();
        transform.Find("SkillsMenu").Find("Weapon Updates").Find("btnFizzBuzzUpgrade").Find("Level").Find("value").GetComponent<Text>().text = playerStats.FIZZBUZZweaponDamage.ToString();

        transform.Find("SkillsMenu").Find("General Updates").Find("btnRoketsSpeedUpgrade").Find("Level").Find("value").GetComponent<Text>().text = playerStats.RoketSpeed.ToString();
        transform.Find("SkillsMenu").Find("General Updates").Find("btnSlowTime").Find("Level").Find("value").GetComponent<Text>().text = playerStats.slowTimeOnSelect.ToString();
        transform.Find("SkillsMenu").Find("General Updates").Find("btnSpawnSpeed").Find("Level").Find("value").GetComponent<Text>().text = playerStats.spownSpeedModificator.ToString();
    }

    private void SetUPPlayer()
    {
        transform.Find("MainView").Find("Player").GetComponent<Player_LevelMenu>().SetUpPlayer(menuLevelEngine);
        Player = transform.Find("MainView").Find("Player");
    }

    private void ShowPlayer()
    {
        string targetLevel = "Level" + playerStats.currentLevel.ToString();
        Vector3 targetPosition = transform.Find("MainView").Find("Levels").Find(targetLevel).Find("PlayerDok").position;
        menuLevelEngine.MuvePlayer(targetPosition, true);
    }


    #endregion

    #region Utils

    private bool isLocked(int iLevelNumber)
    {
        return playerStats.levelStars[iLevelNumber - 1] == 0;
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

    private class LevelStats
    {
        public List<Level> LevelStatsList;
    }

    [System.Serializable]
    private class Level
    {
        public int levelNumber;
        public int levelWaves;
        public int levelWaveEnemiesBase;

        public int DUMBEnemyHealth;
        public float DUMBEnemySpeed;
        public int DUMBEnemyDamage;
        public int DUMBEnemyScoreValue;

        public int FIZZEnemyHealth;
        public float FIZZEnemySpeed;
        public int FIZZEnemyDamage;
        public int FIZZEnemyScoreValue;

        public int BUZZEnemyHealth;
        public float BUZZEnemySpeed;
        public int BUZZEnemyDamage;
        public int BUZZEnemyScoreValue;

        public int FIZZBUZZEnemyHealth;
        public float FIZZBUZZEnemySpeed;
        public int FIZZBUZZEnemyDamage;
        public int FIZZBUZZEnemyScoreValue;

        public Material SpaceBack_Mat;
        public AudioClip LevelMusic;
        public float spownSpeed;
    }

    #endregion
}
