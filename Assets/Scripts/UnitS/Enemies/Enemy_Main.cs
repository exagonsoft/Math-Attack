using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy_Main : MonoBehaviour
{
    #region Properties

    [SerializeField] private Transform pfExplotion;
    [SerializeField] private Sprite[] shipBody;
    private Transform healthBarTransform;

    private int shipBodyNumber;
    private int enemyNumber;
    private Enemy enemyStats;
    private HealtSystem healthSystem;
    private ScoreSystem scoreSystem;
    private LevelSystem levelSystem;
    private GameSystem gameSystem;
    private bool evading;
    
    #endregion

    #region Events

    #endregion

    #region Functions

    public void SetUpEnemy(Vector3 muveDirection, ScoreSystem gameScoreSystem, LevelSystem gameLevelSystem, GameSystem gameGameSystem)
    {
        this.scoreSystem = gameScoreSystem;
        this.levelSystem = gameLevelSystem;
        this.gameSystem = gameGameSystem;
        enemyNumber = UnityEngine.Random.Range(1, 1000);
        transform.Find("EnemyNumber").Find("Number").GetComponent<TMP_Text>().text = enemyNumber.ToString();
        FillStats(enemyNumber);
        HandleHealtSystem();
        shipBodyNumber = UnityEngine.Random.Range(0, 5);
        transform.GetComponent<SpriteRenderer>().sprite = shipBody[shipBodyNumber];
        transform.GetComponent<Cust_Button>().ClickFunc = () =>
        {
            if (GameHandler.canShoot)
            {
                beenShooted();
            }

        };
        transform.gameObject.AddComponent<PolygonCollider2D>();
        Rigidbody2D enemyBody = GetComponent<Rigidbody2D>();
        enemyBody.AddForce(muveDirection * enemyStats.Speed, ForceMode2D.Impulse);
    }

    private void HandleHealtSystem()
    {
        healthSystem = new HealtSystem(enemyStats.maxHealth);
        healthBarTransform = transform.Find("EnemyNumber").Find("pfHealtBar");
        HealtBar Bar = healthBarTransform.GetComponent<HealtBar>();
        Bar.SetUpBar(healthSystem);
    }

    public void takeDamage(int damage)
    {
        healthSystem.Damage(damage);
        if (healthSystem.GethHealt() <= 0)
        {
            GetDestroyed();
            Destroy(gameObject);
        }
    }

    public void Evade()
    {
        if (!evading)
        {
            transform.Rotate(new Vector3(-65, transform.rotation.y, transform.rotation.z), Space.Self);
            transform.GetComponent<Animator>().SetTrigger("Evade");
            evading = true;
        }
        
    }

    public void exitEvade()
    {
        transform.Rotate(new Vector3(65, transform.rotation.y, transform.rotation.z), Space.Self);
        evading = false;
    }

    public Base_Utils.enemyType getType()
    {
        return enemyStats.type;
    }

    private void beenShooted()
    {
        gameSystem.PlayerCanAim(false);
        gameSystem.PlayerCanShoot(false);
        transform.parent.Find("pfBackGround(Clone)").Find("SceneUISpace").Find("pfWeaponSelector").GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        transform.parent.Find("pfBackGround(Clone)").Find("SceneUISpace").Find("pfWeaponSelector").GetComponent<weaponWindowController>().showOpenAnimationWindow((int selectedWeapon) =>
        {
            if(transform != null)
            {
                Transform gameHandler = transform.parent;
                gameSystem.LunchRoket(Base_Utils.getWeaponFromInt(selectedWeapon), transform.position);
            }
        }, gameSystem);
    }

    private void FillStats(int enemyNumber)
    {
        Base_Utils.enemyStats tempStats = transform.parent.parent.GetComponent<GameHandler>().getEnemyData(Base_Utils.getEnemyType(enemyNumber));

        enemyStats = new Enemy
        {
            hitDamage = tempStats.hitDamage,
            maxHealth = tempStats.maxHealth,
            Speed = tempStats.muveSpedd,
            scoreValue = tempStats.scoreValue,
            type = tempStats.type
        };
}

    public int GetScoreValue()
    {
        return enemyStats.scoreValue;
    }

    public void GetDestroyed()
    {
        Transform explotion = Instantiate(pfExplotion, transform.position, Quaternion.identity);
        TimerFunction.Create(() => Destroy(explotion.gameObject), 3f);

        levelSystem.AddExperience(enemyStats.scoreValue);
        scoreSystem.EnemieDestroyed(enemyStats.scoreValue);
    }

    public float GetHealth()
    {
        return (float)healthSystem.GethHealt();
    }

    #endregion

    #region Private Class

    private class Enemy
    {
        public Base_Utils.enemyType type;
        public int maxHealth;
        public int hitDamage;
        public float Speed;
        public int scoreValue;
    }

    #endregion
}