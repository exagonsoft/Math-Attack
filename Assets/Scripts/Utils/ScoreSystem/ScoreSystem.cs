using System;

public class ScoreSystem
{
    #region Properties
    public event EventHandler onEnemyDestroyed;
    public event EventHandler onWaveChange;
    public event EventHandler onLevelClined;

    private int Level { get; set; }
    private int LevelWaves { get; set; }
    private int LevelWave { get; set; }
    private int ReminderLevelWaves { get; set; }
    private int WaveEnemies { get; set; }
    private int ReminderWaveEnemies { get; set; }
    private int Score { get; set; }
    private int BaseLevelEnemies;

    public ScoreSystem(int level, int levelWaves, int baseLevelEnemies, int score)
    {
        Level = level;
        LevelWaves = levelWaves;
        LevelWave = 1;
        BaseLevelEnemies = baseLevelEnemies;
        ReminderLevelWaves = levelWaves - LevelWave;
        WaveEnemies = GetWaveEnemies();
        ReminderWaveEnemies = WaveEnemies;
        Score = score;
    }

    #endregion

    #region Events


    #endregion

    #region Functions
    public int GetLevel()
    {
        return Level;
    }

    public void SetLevel(int level)
    {
        Level = level;
    }
   
    public int GetWaves()
    {
        return LevelWaves;
    }

    public void SetWaves(int waves)
    {
        LevelWaves = waves;
    }

    public int GetWave()
    {
        return LevelWave;
    }

    public void SetWave(int wave)
    {
        LevelWave = wave;
        ReminderLevelWaves = (LevelWaves - LevelWave);
    }

    public int GetReminderWaves()
    {
        return ReminderLevelWaves;
    }

    public int GetLEvelEnemies()
    {
        return WaveEnemies;
    }

    public void SetWaveEnemies(int Enemies)
    {
        WaveEnemies = Enemies;
        ReminderWaveEnemies = WaveEnemies;
    }

    public int GEtRemainderEnemies()
    {
        return ReminderWaveEnemies;
    }

    public int GetScore()
    {
        return Score;
    }

    public void EnemieDestroyed(int enemieValue)
    {
        Score += enemieValue;
        ReminderWaveEnemies--;
        if(ReminderWaveEnemies == 0)
        {
            
            if(LevelWave < LevelWaves)
            {
                LevelWave++;
                WaveEnemies = GetWaveEnemies();
                ReminderWaveEnemies = WaveEnemies;
                if (onWaveChange != null) onWaveChange?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                if (onLevelClined != null) onLevelClined?.Invoke(this, EventArgs.Empty);
            }
            
        }
        else
        {
            if(onEnemyDestroyed != null)onEnemyDestroyed?.Invoke(this, EventArgs.Empty);
        }
    }

    public void PlayerHited(int enemieValue)
    {
        Score -= enemieValue;
        onEnemyDestroyed?.Invoke(this, EventArgs.Empty);
    }

    private int GetWaveEnemies()
    {
        return (BaseLevelEnemies * (Level + LevelWave));
    }

    #endregion
}
