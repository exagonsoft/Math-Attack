using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponWindowController : MonoBehaviour
{
    #region Properties

    private UI_Button dumpButon;
    private UI_Button fizzButon;
    private UI_Button buzzButon;
    private UI_Button fizzbuzzButon;

    //Next Version
    //private float remainingTime;
    private Action<int> onSelect;
    private GameSystem gameSystem;

    #endregion

    #region Events

    void Start()
    {

    }

    void Awake()
    {
        dumpButon = transform.Find("Selector").Find("dumpWeapon").GetComponent<UI_Button>();
        fizzButon = transform.Find("Selector").Find("fizzWeapon").GetComponent<UI_Button>();
        buzzButon = transform.Find("Selector").Find("buzzWeapon").GetComponent<UI_Button>();
        fizzbuzzButon = transform.Find("Selector").Find("fizzbuzzWeapon").GetComponent<UI_Button>();
        gameObject.SetActive(false);
    }


    #endregion

    #region Functions

    public void showOpenAnimationWindow(Action<int> onSelectAction, GameSystem gameGameSystem)
    {
        //setUpWindow();
        GameHandler.canShoot = false;
        this.gameSystem = gameGameSystem;
        onSelect = onSelectAction;
        gameObject.SetActive(true);
        transform.GetComponent<Animator>().SetTrigger("Open");
    }

    public void showCloseAnimation()
    {
        transform.GetComponent<Animator>().SetTrigger("Close");
    }

    public void hideWindow()
    {
        gameObject.SetActive(false);
        gameSystem.PlayerCanShoot(true);
        gameSystem.PlayerCanAim(true);
        Time.timeScale = 1;
    }

    public void SetUpWindow()
    {
        Time.timeScale = GameHandler.levelSlowNess;

        dumpButon.ClickFunc = () =>
        {
            hideWindow();
            onSelect(0);
        };
        fizzButon.ClickFunc = () =>
        {
            hideWindow();
            onSelect(1);
        };
        buzzButon.ClickFunc = () =>
        {
            hideWindow();
            onSelect(2);
        };
        fizzbuzzButon.ClickFunc = () =>
        {
            hideWindow();
            onSelect(3);
        };

        gameSystem.onPlayerDestroyed += GameSystem_onPlayerDestroyed;
    }

    private void GameSystem_onPlayerDestroyed(object sender, EventArgs e)
    {
        if (gameObject.activeInHierarchy)
        {
            gameObject.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    #endregion
}
