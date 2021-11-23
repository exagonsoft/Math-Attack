using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Base : MonoBehaviour
{
    #region Properties
    [SerializeField] private Transform pfExplotion;
    [SerializeField] private Transform pfLevelEfect;
    [SerializeField] private AudioClip changeWeapon;
    [SerializeField] private Sprite[] roketSprite;

    private HealtSystem healthSystem;
    private LevelSystem levelSystem;
    private ScoreSystem scoreSystem;
    private GameSystem gameSystem;

    public static Base_Utils.enemyType weaponType { get; set; }
    private bool canAim;
    private float weaponAngle;
    private Transform aimTransform;
    private Vector3 Target;

    public event EventHandler<onLunchEventArgs> onLunch;
    public class onLunchEventArgs : EventArgs
    {
        public Transform Turret;
        public Vector3 LuncherPosition;
        public Vector3 TargetPosition;
        public Transform Launcher;
        public float roketAngle;
        public Transform Roket;
    }
    public event EventHandler<onChangeWeaponEventArgs> onChangeWeapon;
    public class onChangeWeaponEventArgs : EventArgs
    {
        public Transform Turret;
        public AudioClip changeWeaponSound;
        public Base_Utils.enemyType selectedWeapon;
        public bool isActive;
        public GameSystem gameSystem;
    }
    public event EventHandler<onCollitionEventsArgs> onDamage;
    public class onCollitionEventsArgs : EventArgs
    {
        public Collider2D objectHitted;
        public HealtSystem argHealthSystem;
        public ScoreSystem argScoreSystem;
    }

    #endregion

    #region Events

    private void Awake()
    {
        weaponType = 0;
        canAim = false;
        aimTransform = transform.Find("Aim");
    }

    private void Update()
    {
        HandleAiming();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (onDamage != null)
        {
            onDamage?.Invoke(this, new onCollitionEventsArgs
            {
                objectHitted = collision,
                argHealthSystem = healthSystem,
                argScoreSystem = scoreSystem
            });
        }
    }

    private void OnDestroy()
    {
        
    }

    #endregion

    #region Functions

    public void Fire()
    {
        onLunch?.Invoke(this, new onLunchEventArgs
        {
            Turret = this.transform,
            LuncherPosition = transform.Find("Aim").Find("roketHolder").Find("pfRoket").position,
            TargetPosition = Target,
            Launcher = aimTransform,
            roketAngle = Base_Utils.getRelativeAngle(Target, transform.position),
            Roket = aimTransform.Find("pfRoket")
        });
        canAim = true;
    }

    public void changeWeaponRoket()
    {
        transform.Find("Aim").Find("roketHolder").Find("pfRoket").GetComponent<SpriteRenderer>().sprite = roketSprite[Base_Utils.getWeaponFromType(weaponType)];
        transform.parent.Find("pfBackGround(Clone)").Find("SceneUISpace").Find("UIWindow").Find("WeponIndicator").Find("WeaponImage").GetComponent<Image>().sprite = roketSprite[Base_Utils.getWeaponFromType(weaponType)];
        switch (weaponType)
        {
            case Base_Utils.enemyType.DUMB:
                {
                    transform.parent.Find("pfBackGround(Clone)").Find("SceneUISpace").Find("UIWindow").Find("WeponIndicator").Find("WeaponDescription").GetComponent<Text>().text = "Neither 3 or 5";
                    break;
                }
            case Base_Utils.enemyType.FIZZ:
                {
                    transform.parent.Find("pfBackGround(Clone)").Find("SceneUISpace").Find("UIWindow").Find("WeponIndicator").Find("WeaponDescription").GetComponent<Text>().text = "Only 3";
                    break;
                }
            case Base_Utils.enemyType.BUZZ:
                {
                    transform.parent.Find("pfBackGround(Clone)").Find("SceneUISpace").Find("UIWindow").Find("WeponIndicator").Find("WeaponDescription").GetComponent<Text>().text = "Only 5";
                    break;
                }
            case Base_Utils.enemyType.FIZZBUZZ:
                {
                    transform.parent.Find("pfBackGround(Clone)").Find("SceneUISpace").Find("UIWindow").Find("WeponIndicator").Find("WeaponDescription").GetComponent<Text>().text = "Either 3 or 5";
                    break;
                }
        }
    }

    public void playchangeWeaponSound()
    {
        GetComponent<AudioSource>().PlayOneShot(changeWeapon);
    }

    public void Changed()
    {
    }

    public void SetUpPlayer(HealtSystem gameHealthSystem, LevelSystem gameLevelSystem, ScoreSystem gameScoreSystem, GameSystem gameGameSystem)
    {
        this.healthSystem = gameHealthSystem;
        this.levelSystem = gameLevelSystem;
        this.scoreSystem = gameScoreSystem;
        this.gameSystem = gameGameSystem;

        healthSystem.onHealthChanged += HealthSystem_onHealthChanged;
        levelSystem.onLevelChange += LevelSystem_onLevelChange;
        gameSystem.onCanAimChange += GameSystem_onCanAimChange;
        gameSystem.onRoketLunshed += GameSystem_onRoketLunshed;
    }

    private void GameSystem_onRoketLunshed(object sender, GameSystem.onRoketLunshedEventsArgs e)
    {
        Target = e.enemyPos;
        if (weaponType == e.selectedWeapon)
        {
            Fire();
        }
        else
        {
            weaponType = e.selectedWeapon;
            HandleWeapons(true);
        }
    }

    private void GameSystem_onCanAimChange(object sender, GameSystem.onCanAimChangeEventsArgs e)
    {
        this.canAim = e.canAim;
    }

    private void LevelSystem_onLevelChange(object sender, EventArgs e)
    {
        Transform levelUpEfect = Instantiate(pfLevelEfect, transform);
        TimerFunction.Create(() => Destroy(levelUpEfect.gameObject), 3f);
    }

    private void HealthSystem_onHealthChanged(object sender, EventArgs e)
    {
        if (healthSystem.GethHealt() <= 0)
        {
            
            gameSystem.PlayerDestroyed();
            GetDestroyed();
            Destroy(gameObject);
        }
    }

    private void HandleAiming()
    {
        if (canAim)
        {
            Vector3 mausePosition = Base_Utils.GetMouseWorldPosition();
            Vector3 aimDirection = (mausePosition - transform.position).normalized;
            weaponAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
            aimTransform.eulerAngles = new Vector3(0, 0, weaponAngle);
        }

    }

    public void PlayerReady()
    {
        HandleWeapons(false);
        gameSystem.ShowGetReady();
    }

    private void HandleWeapons(bool weaponState)
    {
        onChangeWeapon?.Invoke(this, new onChangeWeaponEventArgs
        {
            Turret = this.transform,
            changeWeaponSound = this.changeWeapon,
            selectedWeapon = weaponType,
            isActive = weaponState,
            gameSystem = gameSystem
        });
    }

    private void GetDestroyed()
    {
        Transform explotion = Instantiate(pfExplotion, Vector3.zero, Quaternion.identity);
        TimerFunction.Create(() => Destroy(explotion.gameObject), 3f);
    }

    #endregion
}
