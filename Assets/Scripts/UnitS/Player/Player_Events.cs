using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Events : MonoBehaviour
{
    #region Properties
    private HealtSystem healtSystem;
    [SerializeField] Material hitedMatherial;
    [SerializeField] Material normalMatherial;
    [SerializeField] private Transform pfRoket;
    [SerializeField] private Sprite[] roketSprite;

    #endregion

    #region Events

    private void Awake()
    {
        GetComponent<Player_Base>().onChangeWeapon += Player_ChangeWeapon_onChangeWeapon;
        GetComponent<Player_Base>().onDamage += Player_Collition_onDamage;
        GetComponent<Player_Base>().onLunch += Player_RoketLounch_onLunch;
    }

    #endregion

    #region Functions

    private void Player_ChangeWeapon_onChangeWeapon(object sender, Player_Base.onChangeWeaponEventArgs e)
    {
        e.gameSystem.PlayerCanAim(false) ;
        if (e.isActive)
        {
            Transform turretTransform = e.Turret;
            turretTransform.GetComponent<Animator>().SetTrigger("Change");
            AudioSource mainAudio = turretTransform.GetComponent<AudioSource>();
            AudioClip changeWeaponSound = e.changeWeaponSound;
            mainAudio.PlayOneShot(changeWeaponSound);
            Player_Base.weaponType = e.selectedWeapon;
        }
        else
        {
            Transform turretTransform = e.Turret;
            turretTransform.GetComponent<Animator>().SetTrigger("Show");
            AudioSource mainAudio = turretTransform.GetComponent<AudioSource>();
            AudioClip changeWeaponSound = e.changeWeaponSound;
            mainAudio.PlayOneShot(changeWeaponSound);
            Player_Base.weaponType = e.selectedWeapon;
        }

    }

    private void Player_Collition_onDamage(object sender, Player_Base.onCollitionEventsArgs e)
    {

        Enemy_Main enemyTarget = e.objectHitted.GetComponent<Enemy_Main>();
        if (enemyTarget != null)
        {
            TaikingDamage();
            Base_Utils.ShakeCamera(0.5f, 0.4f);
            e.argHealthSystem.Damage(enemyTarget.GetScoreValue());
            enemyTarget.GetDestroyed();
            Destroy(enemyTarget.gameObject);
        }
    }

    private void Player_RoketLounch_onLunch(object sender, Player_Base.onLunchEventArgs e)
    {
        Transform turretTransform = e.Turret;
        turretTransform.GetComponent<Animator>().SetTrigger("Reload");

        turretTransform.Find("Aim").GetComponent<Animator>().SetTrigger("Shoot");

        pfRoket.GetComponent<SpriteRenderer>().sprite = roketSprite[Base_Utils.getWeaponFromType(Player_Base.weaponType)];

        Transform roketToLunch = Instantiate(pfRoket, e.LuncherPosition, Quaternion.Euler(new Vector3(0, 0, e.roketAngle)), transform.parent);
        SoundManager.PlaySound(SoundManager.GameSounds.RokedLunched);

        Vector3 lunchDirect = (e.TargetPosition - e.LuncherPosition).normalized;
        roketToLunch.GetComponent<Roket>().SetUpRoket(lunchDirect, Player_Base.weaponType);
    }

    private void TaikingDamage()
    {
        transform.GetComponent<SpriteRenderer>().material = hitedMatherial;
        transform.GetComponent<Animator>().SetTrigger("Damage");
    }

    private void normalSatate()
    {
        transform.GetComponent<SpriteRenderer>().material = normalMatherial;
    }

    #endregion


    #region Private Class



    #endregion
}
