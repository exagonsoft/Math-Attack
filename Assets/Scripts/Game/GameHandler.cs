using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    #region Properties
    [SerializeField] private Transform pfGameBackScene;
    [SerializeField] private Transform pfPlayer;
    [SerializeField] private AudioClip PlayerEnteringSound;
    [SerializeField] private Transform pfEnemy;



    private ScoreSystem gameScoreSystem;
    private LevelSystem gameLevelSystem;
    private HealtSystem gamePlayerHealtSystem;
    private GameSystem gameGameSystem;

    private Transform GameHolder;
    private Transform backgroundObject;
    private Transform gameScoreWindow;
    private Transform gameUiWindow;
    private Transform playerObject;
    private gameStates gameState;

    private PlayerStats PlayercurrentStats;
    private List<Level> LevelList;
    private Level currentLevel;

    private int currentWave;
    private int currentEnemiesCount;
    private float SpownSpeed;
    public static bool canShoot { get; set; }
    public static float levelSlowNess { get; set; }

    public static GameHandler InstanciatedGameHandler;

    #endregion

    #region Events

    private void Awake()
    {
        InstanciatedGameHandler = this;
        //SettingUp Components
        try
        {
            GameHolder = transform.Find("GameHolder");
            GetJsonData();
            SetUpScene();

            //SetingUp Events
            gameGameSystem.onStartLevelSound += GameGameSystem_onStartLevelSound;
            gameGameSystem.onStartLevelWave += GameGameSystem_onStartLevelWave;
            gameGameSystem.onPlayerDestroyed += GameGameSystem_onPlayerDestroyed;
            gameGameSystem.onCanShootChange += GameGameSystem_onCanShootChange;
            gameGameSystem.onResume += GameGameSystem_onResume;
            gameGameSystem.onExit += GameGameSystem_onExit;
            gameLevelSystem.onLevelChange += GameLevelSystem_onLevelChange;
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    

    private void Update()
    {
        try
        {
            HandleUI();
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    #endregion

    #region Functions

    private void HandleUI()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor_Manager.Instance.SetActiveCursor(Cursor_Manager.CursorType.Arrow);
            if (gameState != gameStates.Paused)
            {
                Pause();
            }
            else
            {
                Resume();
            }
        }
    }

    public void Pause()
    {
        gameState = gameStates.Paused;
        gameGameSystem.PuseGame();
        Time.timeScale = 0;
    }

    public void Resume()
    {
        gameGameSystem.ResumeGame();
    }

    private void GetJsonData()
    {
        string jsonPlayerString = Base_Utils.ReadData();
        if (jsonPlayerString != null && jsonPlayerString != "")
        {
            StatsofPlayer tempStats = JsonUtility.FromJson<StatsofPlayer>(jsonPlayerString);
            PlayercurrentStats = tempStats.stats;
        }

        string jsonLevelsString = PlayerPrefs.GetString("Levels");
        if (jsonLevelsString != null && jsonLevelsString != "")
        {
            LevelStats tempLevels = JsonUtility.FromJson<LevelStats>(jsonLevelsString);
            LevelList = tempLevels.LevelStatsList;
        }

    }

    private void SetUpScene()
    {
        //Starting Scene

        //Getting Game Data
        GetLevelData();

        //Initialising Game Systems
        SetUpGameSystems();

        //Initialize Scene Objects and Filling Data 
        SetUpBackGround();
        SetUpUiWindows();
        SetUpPlayer();

    }

    private void SetUpBackGround()
    {
        backgroundObject = Instantiate(pfGameBackScene, GameHolder);
        backgroundObject.GetComponent<SceneBackGroundHandler>().SetUpBackGround(gameGameSystem, gameScoreSystem, gamePlayerHealtSystem, gameLevelSystem);
        backgroundObject.Find("DeapSpace").GetComponent<MeshRenderer>().material = currentLevel.SpaceBack_Mat;
        backgroundObject.Find("SceneUISpace").Find("GameOverScreen").gameObject.SetActive(false);
        backgroundObject.Find("SceneUISpace").Find("pfWeaponSelector").gameObject.SetActive(false);
        backgroundObject.Find("SceneUISpace").Find("ScoreTable").gameObject.SetActive(false);
        backgroundObject.Find("SceneUISpace").Find("GetReady").gameObject.SetActive(false);
        backgroundObject.Find("SceneUISpace").Find("PauseWindow").gameObject.SetActive(false);
    }

    private void GetLevelData()
    {
        currentWave++;
        levelSlowNess = PlayercurrentStats.slowTimeOnSelect;
        currentLevel = GetCurrentLevel();
        SpownSpeed = currentLevel.spownSpeed + PlayercurrentStats.spownSpeedModificator;
    }

    private void SetUpGameSystems()
    {
        this.gameLevelSystem = new LevelSystem(PlayercurrentStats.PlayerLevel, PlayercurrentStats.PlayerExperience, PlayercurrentStats.AvalibleSkillPoints);
        this.gameScoreSystem = new ScoreSystem(GameConfigurator.SelectedLevel, currentLevel.levelWaves, currentLevel.levelWaveEnemiesBase, PlayercurrentStats.score);
        this.gamePlayerHealtSystem = new HealtSystem(PlayercurrentStats.maxHealth);
        this.gameGameSystem = new GameSystem();
    }

    private void SetUpUiWindows()
    {
        gameUiWindow = backgroundObject.Find("SceneUISpace").Find("UIWindow");

        HealtBar healthBar = gameUiWindow.Find("HealtBar").GetComponent<HealtBar>();
        healthBar.SetUpBar(this.gamePlayerHealtSystem);

        LevelBar levelBar = gameUiWindow.Find("LevelBar").GetComponent<LevelBar>();
        levelBar.SetUpWindow(this.gameLevelSystem);

        gameScoreWindow = backgroundObject.Find("SceneUISpace").Find("IndicatorsArea");
        ScoreWindow scoreWindow = gameScoreWindow.GetComponent<ScoreWindow>();
        scoreWindow.SetUpWindow(gameScoreSystem);
    }

    private void SetUpPlayer()
    {
        playerObject = Instantiate(pfPlayer, GameHolder);
        playerObject.GetComponent<Player_Base>().SetUpPlayer(gamePlayerHealtSystem, gameLevelSystem, gameScoreSystem, gameGameSystem);
        SpawnPlayer();
        
    }

    private void SpawnPlayer()
    {
        //Taking Player to Scene center Screen 
        Animator playerAnimator = playerObject.GetComponent<Animator>();
        SoundManager.PlaySound(SoundManager.GameSounds.PlayerEnter);
        playerAnimator.SetTrigger("Enter");
    }

    IEnumerator SpawnEnemy()
    {
        int iCounter = 0;
        int iEnemies = gameScoreSystem.GetLEvelEnemies();
        while (iCounter < iEnemies)
        {
            Vector3 spownPosition = Base_Utils.getRandomPosition();
            Vector3 pointTo = (new Vector3(0, 0, 0) - spownPosition).normalized;
            float enemyAngle = Mathf.Atan2(pointTo.y, pointTo.x) * Mathf.Rad2Deg;
            Transform enemy = Instantiate(pfEnemy, spownPosition, Quaternion.Euler(0, 0, enemyAngle), GameHolder);
            enemy.GetComponent<Enemy_Main>().SetUpEnemy(pointTo, gameScoreSystem, gameLevelSystem, gameGameSystem);
            iCounter++;
            yield return new WaitForSeconds(SpownSpeed);
        }

    }

    private void GameGameSystem_onStartLevelWave(object sender, System.EventArgs e)
    {
        try
        {
            gameGameSystem.HideGetReady();
            gameGameSystem.PlayerCanAim(true);
            canShoot = true;
            //Lunching Wave
            StartCoroutine(SpawnEnemy());
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private void GameGameSystem_onStartLevelSound(object sender, System.EventArgs e)
    {
        AudioSource audio = transform.GetComponent<AudioSource>();
        audio.volume = 1;
        audio.clip = currentLevel.LevelMusic;
        audio.loop = true;
        audio.Play();
        transform.GetComponent<Animator>().SetTrigger("StartSound");
    }

    private void GameGameSystem_onPlayerDestroyed(object sender, EventArgs e)
    {
        canShoot = false;
    }

    private void GameGameSystem_onCanShootChange(object sender, GameSystem.onCanShootChangeEventsArgs e)
    {
        canShoot = e.canShoot;
    }

    private void GameGameSystem_onResume(object sender, EventArgs e)
    {
        gameState = gameStates.InGame;
        Time.timeScale = 1;
    }

    private void GameLevelSystem_onLevelChange(object sender, EventArgs e)
    {
        int newMaxHealth = gamePlayerHealtSystem.GetCurrentMaxHealth() + (gamePlayerHealtSystem.GetCurrentMaxHealth() / 2);
        gamePlayerHealtSystem.RenewHealthSystem(newMaxHealth);
        PlayercurrentStats.AvalibleSkillPoints++;
        PlayercurrentStats.maxHealth = newMaxHealth;
        StatsofPlayer defoultStats = new StatsofPlayer { stats = PlayercurrentStats };
        string jsonPlayerStats = JsonUtility.ToJson(defoultStats);
        PlayerPrefs.SetString("PlayerStats", jsonPlayerStats);
        PlayerPrefs.Save();
    }

    private void GameGameSystem_onExit(object sender, EventArgs e)
    {
        Time.timeScale = 1f;
    }

    #endregion

    #region Utils

    private Level GetCurrentLevel()
    {
        Level resoultLevel = new Level();

        foreach (Level tempLevel in LevelList)
        {
            if (tempLevel.levelNumber == GameConfigurator.SelectedLevel)
            {
                resoultLevel = tempLevel;
                break;
            }
        }

        return resoultLevel;
    }

    public Base_Utils.enemyStats getEnemyData(Base_Utils.enemyType type)
    {
        Base_Utils.enemyStats statsResoult = new Base_Utils.enemyStats();

        switch (type)
        {
            case Base_Utils.enemyType.DUMB:
                {
                    statsResoult = new Base_Utils.enemyStats {
                        hitDamage = currentLevel.DUMBEnemyDamage,
                        maxHealth = currentLevel.DUMBEnemyHealth,
                        muveSpedd = currentLevel.DUMBEnemySpeed,
                        scoreValue = currentLevel.DUMBEnemyScoreValue,
                        type = Base_Utils.enemyType.DUMB
                    };
                    break;
                }
            case Base_Utils.enemyType.FIZZ:
                {
                    statsResoult = new Base_Utils.enemyStats
                    {
                        hitDamage = currentLevel.FIZZEnemyDamage,
                        maxHealth = currentLevel.FIZZEnemyHealth,
                        muveSpedd = currentLevel.FIZZEnemySpeed,
                        scoreValue = currentLevel.FIZZEnemyScoreValue,
                        type = Base_Utils.enemyType.FIZZ
                    };
                    break;
                }
            case Base_Utils.enemyType.BUZZ:
                {
                    statsResoult = new Base_Utils.enemyStats
                    {
                        hitDamage = currentLevel.BUZZEnemyDamage,
                        maxHealth = currentLevel.BUZZEnemyHealth,
                        muveSpedd = currentLevel.BUZZEnemySpeed,
                        scoreValue = currentLevel.BUZZEnemyScoreValue,
                        type = Base_Utils.enemyType.BUZZ
                    };
                    break;
                }
            case Base_Utils.enemyType.FIZZBUZZ:
                {
                    statsResoult = new Base_Utils.enemyStats
                    {
                        hitDamage = currentLevel.FIZZBUZZEnemyDamage,
                        maxHealth = currentLevel.FIZZBUZZEnemyHealth,
                        muveSpedd = currentLevel.FIZZBUZZEnemySpeed,
                        scoreValue = currentLevel.FIZZBUZZEnemyScoreValue,
                        type = Base_Utils.enemyType.FIZZBUZZ
                    };
                    break;
                }
        }

        return statsResoult;
    }

    public Base_Utils.weaponStats getWeaponData(Base_Utils.enemyType type)
    {
        Base_Utils.weaponStats statsResoult = new Base_Utils.weaponStats();

        switch (type)
        {
            case Base_Utils.enemyType.DUMB:
                {
                    statsResoult = new Base_Utils.weaponStats { 
                         hitDamage = Base_Utils.WeaponBaseDamage.DUMB_Weapon + (PlayercurrentStats.DUMBweaponDamage * 2),
                         muveSpedd = 100f,
                         targetType = Base_Utils.enemyType.DUMB
                    };
                    break;
                }
            case Base_Utils.enemyType.FIZZ:
                {
                    statsResoult = new Base_Utils.weaponStats
                    {
                        hitDamage = Base_Utils.WeaponBaseDamage.FIZZ_Weapon + (PlayercurrentStats.FIZZweaponDamage * 2),
                        muveSpedd = 110f,
                        targetType = Base_Utils.enemyType.FIZZ
                    };
                    break;
                }
            case Base_Utils.enemyType.BUZZ:
                {
                    statsResoult = new Base_Utils.weaponStats
                    {
                        hitDamage = Base_Utils.WeaponBaseDamage.BUZZ_Weapon + (PlayercurrentStats.BUZZweaponDamage * 2),
                        muveSpedd = 120f,
                        targetType = Base_Utils.enemyType.BUZZ
                    };
                    break;
                }
            case Base_Utils.enemyType.FIZZBUZZ:
                {
                    statsResoult = new Base_Utils.weaponStats
                    {
                        hitDamage = Base_Utils.WeaponBaseDamage.FIZBUZZ_Weapon + (PlayercurrentStats.FIZZBUZZweaponDamage * 2),
                        muveSpedd = 140f,
                        targetType = Base_Utils.enemyType.FIZZBUZZ
                    };
                    break;
                }
        }

        return statsResoult;
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

    private enum gameStates
    {
        Entering,
        InGame,
        Paused
    }
    #endregion
}
