using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossFade : MonoBehaviour
{
    #region Properties

    public static CrossFade InstanSiateCrossFade;

    #endregion

    #region Events

    private void Awake()
    {
        InstanSiateCrossFade = this;
    }

    #endregion

    #region Functions

       
    public IEnumerator CrossFade_Show(Loader_Class.Scene sCene)
    {
        transform.GetComponent<Animator>().SetTrigger("FadeOut");
        yield return new WaitForSeconds(1f);
        Loader_Class.Load(sCene);
    }

    #endregion


    #region Private Class



    #endregion
}
