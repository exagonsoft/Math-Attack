using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConfigurator : MonoBehaviour
{
    #region Properties
    [SerializeField] private Material[] spaceMaterials;
    [SerializeField] private AudioClip[] musics;
    public static int SelectedLevel;

    #endregion

    #region Events

    private void Awake()
    {
        RecreateLevels();
        Loader_Class.Load(Loader_Class.Scene.MainMenu);
    }

    #endregion

    #region Functions

    private void RecreateLevels()
    {
        LevelStats levelsStats = new LevelStats { LevelStatsList = CreateLevels() };
        string jsonLevelsData = JsonUtility.ToJson(levelsStats);
        PlayerPrefs.SetString("Levels", jsonLevelsData);
        PlayerPrefs.Save();
    }

    private List<Level> CreateLevels()
    {
        List<Level> resoultList = new List<Level>();

        //Level1
        Level currentLevel1 = new Level
        {
            levelNumber = 1,
            levelWaves = 2,
            levelWaveEnemiesBase = 3,

            DUMBEnemyHealth = 7,
            DUMBEnemySpeed = 8,
            DUMBEnemyDamage = 5,
            DUMBEnemyScoreValue = 10,

            FIZZEnemyHealth = 9,
            FIZZEnemySpeed = 10,
            FIZZEnemyDamage = 7,
            FIZZEnemyScoreValue = 14,

            BUZZEnemyHealth = 12,
            BUZZEnemySpeed = 12,
            BUZZEnemyDamage = 10,
            BUZZEnemyScoreValue = 20,

            FIZZBUZZEnemyHealth = 15,
            FIZZBUZZEnemySpeed = 15,
            FIZZBUZZEnemyDamage = 12,
            FIZZBUZZEnemyScoreValue = 30,

            SpaceBack_Mat = spaceMaterials[0],
            LevelMusic = musics[0],
            spownSpeed = 10f
        };
        resoultList.Add(currentLevel1);

        //Level2
        Level currentLevel2 = new Level
        {
            levelNumber = 2,
            levelWaves = 3,
            levelWaveEnemiesBase = 4,

            DUMBEnemyHealth = 9,
            DUMBEnemySpeed = 10,
            DUMBEnemyDamage = 7,
            DUMBEnemyScoreValue = 14,

            FIZZEnemyHealth = 11,
            FIZZEnemySpeed = 12,
            FIZZEnemyDamage = 9,
            FIZZEnemyScoreValue = 18,

            BUZZEnemyHealth = 14,
            BUZZEnemySpeed = 14,
            BUZZEnemyDamage = 12,
            BUZZEnemyScoreValue = 25,

            FIZZBUZZEnemyHealth = 18,
            FIZZBUZZEnemySpeed = 18,
            FIZZBUZZEnemyDamage = 15,
            FIZZBUZZEnemyScoreValue = 35,

            SpaceBack_Mat = spaceMaterials[1],
            LevelMusic = musics[1],
            spownSpeed = 9f
        };
        resoultList.Add(currentLevel2);

        //Level3
        Level currentLevel3 = new Level
        {
            levelNumber = 3,
            levelWaves = 3,
            levelWaveEnemiesBase = 4,

            DUMBEnemyHealth = 12,
            DUMBEnemySpeed = 12,
            DUMBEnemyDamage = 10,
            DUMBEnemyScoreValue = 20,

            FIZZEnemyHealth = 14,
            FIZZEnemySpeed = 14,
            FIZZEnemyDamage = 12,
            FIZZEnemyScoreValue = 24,

            BUZZEnemyHealth = 16,
            BUZZEnemySpeed = 16,
            BUZZEnemyDamage = 12,
            BUZZEnemyScoreValue = 30,

            FIZZBUZZEnemyHealth = 20,
            FIZZBUZZEnemySpeed = 20,
            FIZZBUZZEnemyDamage = 20,
            FIZZBUZZEnemyScoreValue = 40,

            SpaceBack_Mat = spaceMaterials[2],
            LevelMusic = musics[2],
            spownSpeed = 8f
        };
        resoultList.Add(currentLevel3);

        //Level4
        Level currentLevel4 = new Level
        {
            levelNumber = 4,
            levelWaves = 3,
            levelWaveEnemiesBase = 4,

            DUMBEnemyHealth = 14,
            DUMBEnemySpeed = 13,
            DUMBEnemyDamage = 12,
            DUMBEnemyScoreValue = 22,

            FIZZEnemyHealth = 16,
            FIZZEnemySpeed = 16,
            FIZZEnemyDamage = 14,
            FIZZEnemyScoreValue = 26,

            BUZZEnemyHealth = 18,
            BUZZEnemySpeed = 18,
            BUZZEnemyDamage = 16,
            BUZZEnemyScoreValue = 32,

            FIZZBUZZEnemyHealth = 22,
            FIZZBUZZEnemySpeed = 22,
            FIZZBUZZEnemyDamage = 22,
            FIZZBUZZEnemyScoreValue = 42,

            SpaceBack_Mat = spaceMaterials[3],
            LevelMusic = musics[3],
            spownSpeed = 7f
        };
        resoultList.Add(currentLevel4);

        //Level5
        Level currentLevel5 = new Level
        {
            levelNumber = 5,
            levelWaves = 5,
            levelWaveEnemiesBase = 5,

            DUMBEnemyHealth = 16,
            DUMBEnemySpeed = 16,
            DUMBEnemyDamage = 14,
            DUMBEnemyScoreValue = 24,

            FIZZEnemyHealth = 18,
            FIZZEnemySpeed = 18,
            FIZZEnemyDamage = 16,
            FIZZEnemyScoreValue = 28,

            BUZZEnemyHealth = 20,
            BUZZEnemySpeed = 20,
            BUZZEnemyDamage = 17,
            BUZZEnemyScoreValue = 34,

            FIZZBUZZEnemyHealth = 24,
            FIZZBUZZEnemySpeed = 24,
            FIZZBUZZEnemyDamage = 24,
            FIZZBUZZEnemyScoreValue = 44,

            SpaceBack_Mat = spaceMaterials[4],
            LevelMusic = musics[4],
            spownSpeed = 6f
        };
        resoultList.Add(currentLevel5);

        //Level6
        Level currentLevel6 = new Level
        {
            levelNumber = 6,
            levelWaves = 6,
            levelWaveEnemiesBase = 5,

            DUMBEnemyHealth = 18,
            DUMBEnemySpeed = 18,
            DUMBEnemyDamage = 16,
            DUMBEnemyScoreValue = 26,

            FIZZEnemyHealth = 20,
            FIZZEnemySpeed = 20,
            FIZZEnemyDamage = 18,
            FIZZEnemyScoreValue = 30,

            BUZZEnemyHealth = 22,
            BUZZEnemySpeed = 22,
            BUZZEnemyDamage = 19,
            BUZZEnemyScoreValue = 36,

            FIZZBUZZEnemyHealth = 26,
            FIZZBUZZEnemySpeed = 26,
            FIZZBUZZEnemyDamage = 26,
            FIZZBUZZEnemyScoreValue = 46,

            SpaceBack_Mat = spaceMaterials[5],
            LevelMusic = musics[5],
            spownSpeed = 5f
        };
        resoultList.Add(currentLevel6);

        //Level7
        Level currentLevel7 = new Level
        {
            levelNumber = 7,
            levelWaves = 7,
            levelWaveEnemiesBase = 5,

            DUMBEnemyHealth = 20,
            DUMBEnemySpeed = 20,
            DUMBEnemyDamage = 18,
            DUMBEnemyScoreValue = 28,

            FIZZEnemyHealth = 22,
            FIZZEnemySpeed = 22,
            FIZZEnemyDamage = 20,
            FIZZEnemyScoreValue = 32,

            BUZZEnemyHealth = 24,
            BUZZEnemySpeed = 24,
            BUZZEnemyDamage = 21,
            BUZZEnemyScoreValue = 38,

            FIZZBUZZEnemyHealth = 28,
            FIZZBUZZEnemySpeed = 28,
            FIZZBUZZEnemyDamage = 28,
            FIZZBUZZEnemyScoreValue = 48,

            SpaceBack_Mat = spaceMaterials[6],
            LevelMusic = musics[6],
            spownSpeed = 4f
        };
        resoultList.Add(currentLevel7);

        //Level8
        Level currentLevel8 = new Level
        {
            levelNumber = 8,
            levelWaves = 8,
            levelWaveEnemiesBase = 6,

            DUMBEnemyHealth = 22,
            DUMBEnemySpeed = 22,
            DUMBEnemyDamage = 20,
            DUMBEnemyScoreValue = 30,

            FIZZEnemyHealth = 24,
            FIZZEnemySpeed = 24,
            FIZZEnemyDamage = 22,
            FIZZEnemyScoreValue = 34,

            BUZZEnemyHealth = 26,
            BUZZEnemySpeed = 26,
            BUZZEnemyDamage = 23,
            BUZZEnemyScoreValue = 40,

            FIZZBUZZEnemyHealth = 30,
            FIZZBUZZEnemySpeed = 30,
            FIZZBUZZEnemyDamage = 30,
            FIZZBUZZEnemyScoreValue = 50,

            SpaceBack_Mat = spaceMaterials[7],
            LevelMusic = musics[7],
            spownSpeed = 4f
        };
        resoultList.Add(currentLevel8);

        //Level9
        Level currentLevel9 = new Level
        {
            levelNumber = 9,
            levelWaves = 9,
            levelWaveEnemiesBase = 6,

            DUMBEnemyHealth = 24,
            DUMBEnemySpeed = 24,
            DUMBEnemyDamage = 22,
            DUMBEnemyScoreValue = 32,

            FIZZEnemyHealth = 26,
            FIZZEnemySpeed = 26,
            FIZZEnemyDamage = 24,
            FIZZEnemyScoreValue = 36,

            BUZZEnemyHealth = 28,
            BUZZEnemySpeed = 28,
            BUZZEnemyDamage = 25,
            BUZZEnemyScoreValue = 42,

            FIZZBUZZEnemyHealth = 32,
            FIZZBUZZEnemySpeed = 32,
            FIZZBUZZEnemyDamage = 32,
            FIZZBUZZEnemyScoreValue = 52,

            SpaceBack_Mat = spaceMaterials[8],
            LevelMusic = musics[8],
            spownSpeed = 4f
        };
        resoultList.Add(currentLevel9);

        //Level10
        Level currentLevel10 = new Level
        {
            levelNumber = 10,
            levelWaves = 10,
            levelWaveEnemiesBase = 10,

            DUMBEnemyHealth = 26,
            DUMBEnemySpeed = 26,
            DUMBEnemyDamage = 24,
            DUMBEnemyScoreValue = 34,

            FIZZEnemyHealth = 28,
            FIZZEnemySpeed = 28,
            FIZZEnemyDamage = 26,
            FIZZEnemyScoreValue = 38,

            BUZZEnemyHealth = 30,
            BUZZEnemySpeed = 30,
            BUZZEnemyDamage = 27,
            BUZZEnemyScoreValue = 44,

            FIZZBUZZEnemyHealth = 34,
            FIZZBUZZEnemySpeed = 34,
            FIZZBUZZEnemyDamage = 34,
            FIZZBUZZEnemyScoreValue = 54,

            SpaceBack_Mat = spaceMaterials[9],
            LevelMusic = musics[9],
            spownSpeed = 4f
        };
        resoultList.Add(currentLevel10);

        return resoultList;
    }



    #endregion


    #region Private Class

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
