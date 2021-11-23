using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuLevelEngine
{
    #region Properties
    public event EventHandler<OnShowPlayerEventArgs> onShowPlayer;
    public class OnShowPlayerEventArgs
    {
        public Vector3 targetPosition;
        public bool isEnteringMenu;
    }

    public MenuLevelEngine()
    {
        
    }

    #endregion

    #region Events


    #endregion

    #region Functions

    public void MuvePlayer(Vector3 targetPosition, bool menuEntering)
    {
        if (onShowPlayer != null) onShowPlayer?.Invoke(this, new OnShowPlayerEventArgs { targetPosition = targetPosition, isEnteringMenu = menuEntering });
    }

    #endregion

    #region Private Class



    #endregion
}
