using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseWindow : MonoBehaviour
{
    #region Properties
    [SerializeField] private AudioClip UISound;
    private GameSystem gameSystem;
    private bool exit;
    #endregion

    #region Events

    void Awake()
    {
        transform.Find("Butons").Find("btnHelp").GetComponent<UI_Button>().ClickFunc = () =>
        {
            ShowHealp();
        };

        transform.Find("Butons").Find("btnResume").GetComponent<UI_Button>().ClickFunc = () =>
        {
            gameSystem.ResumeGame();
            transform.gameObject.SetActive(false);
        };

        transform.Find("Butons").Find("bntMainMenu").GetComponent<UI_Button>().ClickFunc = () =>
        {
            exit = false;
            ShowDilog();

        };

        transform.Find("Butons").Find("bntToWindow").GetComponent<UI_Button>().ClickFunc = () =>
        {
            exit = true;
            ShowDilog();
        };

        transform.Find("Confirm Dialog").Find("bntConfirmCancel").GetComponent<UI_Button>().ClickFunc = () =>
        {
            exit = false;
            HideDilog();
        };

        transform.Find("Confirm Dialog").Find("btnConfirmationOk").GetComponent<UI_Button>().ClickFunc = () =>
        {

            if (exit)
            {
                Application.Quit();
            }
            else
            {
                StartCoroutine(CrossFade.InstanSiateCrossFade.CrossFade_Show(Loader_Class.Scene.MainMenu));
                gameSystem.ExitGame();
            }
            
        };

        transform.Find("HelpWindow").Find("bntBack").GetComponent<UI_Button>().ClickFunc = () =>
        {

            HideHealp();

        };

        transform.Find("HelpWindow").Find("bntResume").GetComponent<UI_Button>().ClickFunc = () =>
        {
            HideHealp();
            gameSystem.ResumeGame();
        };
    }

    #endregion

    #region Functions
    public void SetUpWindow(GameSystem gameGameSystem)
    {
        gameSystem = gameGameSystem;
        exit = false;
        transform.Find("Confirm Dialog").gameObject.SetActive(false);
        transform.Find("HelpWindow").gameObject.SetActive(false);
        transform.Find("Butons").gameObject.SetActive(true);

        gameSystem.onPause += GameSystem_onPause;
        gameSystem.onResume += GameGameSystem_onResume;
    }

    private void GameGameSystem_onResume(object sender, System.EventArgs e)
    {
        exit = false;
        transform.Find("Confirm Dialog").gameObject.SetActive(false);
        transform.Find("HelpWindow").gameObject.SetActive(false);
        transform.Find("Butons").gameObject.SetActive(true);
        transform.gameObject.SetActive(false);
    }

    private void GameSystem_onPause(object sender, System.EventArgs e)
    {
        transform.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        transform.gameObject.SetActive(true);
    }

    private void ShowDilog()
    {
        transform.Find("Confirm Dialog").GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        transform.Find("Butons").gameObject.SetActive(false);
        transform.Find("HelpWindow").gameObject.SetActive(false);
        transform.Find("Confirm Dialog").gameObject.SetActive(true);
    }

    private void HideDilog()
    {
        transform.Find("Confirm Dialog").gameObject.SetActive(false);
        transform.Find("Butons").gameObject.SetActive(true);
    }

    private void ShowHealp()
    {
        transform.Find("HelpWindow").GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        transform.Find("Butons").gameObject.SetActive(false);
        transform.Find("Confirm Dialog").gameObject.SetActive(false);
        transform.Find("HelpWindow").gameObject.SetActive(true);
    }

    private void HideHealp()
    {
        transform.Find("HelpWindow").gameObject.SetActive(false);
        transform.Find("Butons").gameObject.SetActive(true);
    }

    #endregion
}
