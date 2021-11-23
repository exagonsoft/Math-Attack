using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    #region Properties

    public static GameAssets AssetsInstance;
    public SoundClip[] gameSoundsClips;
    public Transform pfExperiencePopUp;

    #endregion

    #region Events

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        AssetsInstance = this;
    }

    #endregion

    #region Functions


    #endregion


    #region Private Class
    [Serializable]
    public class SoundClip
    {
        public SoundManager.GameSounds sound;
        public AudioClip soundClip;
    }


    #endregion
}
