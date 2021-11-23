using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roket : MonoBehaviour
{
    #region Properties
    [SerializeField] private Transform pfExplotion;
    private Vector3 rocketDirection;
    private bool salfeDestroy;
    private RoketStats currentRoketStats;
    private DateTime LastHit;

    #endregion

    #region Events

    void Start()
    {
        salfeDestroy = true;
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        Hit(collision);
    }


    #endregion

    #region Functions

    public void SetUpRoket(Vector3 Direction, Base_Utils.enemyType weaponType)
    {
        this.rocketDirection = Direction;
        FillStats(weaponType);
        Rigidbody2D roketBody = GetComponent<Rigidbody2D>();
        roketBody.AddForce(rocketDirection * currentRoketStats.muveSpeed, ForceMode2D.Impulse);
        Destroy(gameObject, 2.2f);
    }

    private void Hit(Collider2D collision)
    {
        Enemy_Main enemyTarget = collision.GetComponent<Enemy_Main>();
        if (enemyTarget != null)
        {
            //Target Hitted
            TargetHited(enemyTarget);
        }
    }

    private void TargetHited(Enemy_Main enemyTarget)
    {
        Base_Utils.enemyType typeEnemy = enemyTarget.getType();
        if (typeEnemy == currentRoketStats.targetType)
        {
            if (DateTime.Now > LastHit.AddMilliseconds((double)50))
            {
                LastHit = DateTime.Now;
                enemyTarget.takeDamage(currentRoketStats.hitDamage);
                if (enemyTarget.GetHealth() <= 0)
                {
                    salfeDestroy = false;
                }
                else
                {
                    salfeDestroy = true;
                }
                GetDestroyed();
                Destroy(gameObject);
            }
        }
        else
        {
            enemyTarget.Evade();
        }
    }

    private void FillStats(Base_Utils.enemyType weponType)
    {
        Base_Utils.weaponStats tempStats = GameHandler.InstanciatedGameHandler.getWeaponData(weponType);
        currentRoketStats = new RoketStats
        {
            targetType = tempStats.targetType,
            hitDamage = tempStats.hitDamage,
            muveSpeed = tempStats.muveSpedd
        };
}

    public void GetDestroyed()
    {
        if (salfeDestroy)
        {
            Transform explotion = Instantiate(pfExplotion, transform.position, Quaternion.identity);
            explotion.localScale = new Vector3(1, 1, 1);
            TimerFunction.Create(() => Destroy(explotion.gameObject), 2f);
            SoundManager.PlaySound(SoundManager.GameSounds.smallExplotion);
        }
        else
        {
            
        }
        salfeDestroy = true;
    }

    #endregion

    #region Private Class

    private class RoketStats
    {
        public Base_Utils.enemyType targetType;
        public int hitDamage;
        public float muveSpeed;
    }

    #endregion
}
