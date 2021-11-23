using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Experience_PopUp : MonoBehaviour
{
    #region Properties

    private Text textPopUp;

    public static Experience_PopUp Create(int experienceAmount, Transform parent)
    {
        Transform experiencePopUp = Instantiate(GameAssets.AssetsInstance.pfExperiencePopUp, Vector3.zero, Quaternion.identity, parent);
        Experience_PopUp experienceScript = experiencePopUp.GetComponent<Experience_PopUp>();
        experienceScript.SetUp(experienceAmount);
        TimerFunction.Create(() => Destroy(experiencePopUp.gameObject), 3f);

        return experienceScript;
    }

    #endregion

    #region Events

    private void Awake()
    {
        textPopUp = transform.GetComponent<Text>(); 
    }

    private void Update()
    {
        MuvePopUpUp();
    }

    #endregion

    #region Functions

    public void SetUp(int experienceAmount)
    {
        textPopUp.text = experienceAmount.ToString();
    }

    private void MuvePopUpUp()
    {
        float MoveYVelocity = 20f;
        transform.position += new Vector3(0, MoveYVelocity) * Time.deltaTime;
    }

    #endregion


    #region Private Class



    #endregion
}
