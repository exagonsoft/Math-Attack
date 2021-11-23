using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem
{
    #region Properties
    public event EventHandler onGettingReady;
    public event EventHandler onReady;
    public event EventHandler onStartLevelSound;
    public event EventHandler onStartLevelWave;
    public event EventHandler onPlayerDestroyed;
    public event EventHandler onPause;
    public event EventHandler onResume;
    public event EventHandler onExit;
    public event EventHandler<onCanAimChangeEventsArgs> onCanAimChange;
    public class onCanAimChangeEventsArgs : EventArgs
    {
        public bool canAim;
    }
    public event EventHandler<onCanShootChangeEventsArgs> onCanShootChange;
    public class onCanShootChangeEventsArgs : EventArgs
    {
        public bool canShoot;
    }
    public event EventHandler<onRoketLunshedEventsArgs> onRoketLunshed;
    public class onRoketLunshedEventsArgs : EventArgs
    {
        public Base_Utils.enemyType selectedWeapon;
        public Vector3 enemyPos;
    }


    public GameSystem()
    {

    }

    #endregion

    #region Events


    #endregion

    #region Functions

    public void ShowGetReady()
    {
        if (onGettingReady != null) onGettingReady?.Invoke(this, EventArgs.Empty);
    }

    public void HideGetReady()
    {
        if (onReady != null) onReady?.Invoke(this, EventArgs.Empty);
    }

    public void StartLevelSound()
    {
        if (onStartLevelSound != null) onStartLevelSound?.Invoke(this, EventArgs.Empty);
    }

    public void StartLevelWave()
    {
        if (onStartLevelWave != null) onStartLevelWave?.Invoke(this, EventArgs.Empty);
    }

    public void PlayerDestroyed()
    {
        if (onPlayerDestroyed != null) onPlayerDestroyed?.Invoke(this, EventArgs.Empty);
    }

    public void PlayerCanAim(bool canAim)
    {
        if (onCanAimChange != null) onCanAimChange?.Invoke(this, new onCanAimChangeEventsArgs {
            canAim = canAim
        });
    }

    public void PlayerCanShoot(bool canShoot)
    {
        if (onCanShootChange != null) onCanShootChange?.Invoke(this, new onCanShootChangeEventsArgs
        {
            canShoot = canShoot
        });
    }

    public void LunchRoket(Base_Utils.enemyType selectedWeapon, Vector3 enemyPos)
    {
        if (onRoketLunshed != null) onRoketLunshed?.Invoke(this, new onRoketLunshedEventsArgs
        {
            selectedWeapon = selectedWeapon,
            enemyPos = enemyPos
        });
    }

    public void PuseGame()
    {
        if (onPause != null) onPause?.Invoke(this, EventArgs.Empty);
    }

    public void ResumeGame() 
    {
        if (onResume != null) onResume?.Invoke(this, EventArgs.Empty);
    }

    public void ExitGame()
    {
        if (onExit != null) onExit?.Invoke(this, EventArgs.Empty);
    }

    #endregion
}
