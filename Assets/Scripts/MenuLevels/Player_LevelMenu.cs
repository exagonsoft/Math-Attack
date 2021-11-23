using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_LevelMenu : MonoBehaviour
{
    #region Properties

    private MenuLevelEngine menulevelEngeine;
    private Vector3 target;
    private bool isEnteringMenu;

    #endregion

    #region Events

    private void Awake()
    {
        isEnteringMenu = true;
        target = transform.position;
    }

    private void Update()
    {
        Muve();
    }

    #endregion

    #region Functions

    public void SetUpPlayer(MenuLevelEngine menuLevelEngine)
    {
        this.menulevelEngeine = menuLevelEngine;
        menulevelEngeine.onShowPlayer += MenulevelEngeine_onShowPlayer;
    }

    private void MenulevelEngeine_onShowPlayer(object sender, MenuLevelEngine.OnShowPlayerEventArgs e)
    {
        target = e.targetPosition;
        isEnteringMenu = e.isEnteringMenu;
    }

    private void Muve()
    {
        float sPeed = 5f;
        if (Vector3.Distance(transform.position, target) > 0.1f)
        {
            Vector3 moveDir = (target - transform.position).normalized;
            transform.position = transform.position + moveDir * sPeed * Time.deltaTime;
        }
        else
        {
            if (!isEnteringMenu)
            {
                Loader_Class.Load(Loader_Class.Scene.Game);
            }
        }
    }

    #endregion


    #region Private Class



    #endregion
}
