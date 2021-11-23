using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager
{
    public enum GameSounds
    {
        PlayerEnter,
        playerChangeWeapon,
        smallExplotion,
        BigExplotion,
        RokedLunched,
        RokedReload,
        UIClick,
        UIOver,
        UIShowWindow,
        UIHideWindow,
    }

    public static void PlaySound(GameSounds sound)
    {
        GameObject SoundManagerDummyObject = new GameObject("DummySound");
        SoundManagerDummyObject.transform.position = Vector3.zero;
        AudioSource soundPlayer = SoundManagerDummyObject.AddComponent<AudioSource>();
        soundPlayer.priority = 100;
        soundPlayer.volume = 1;
        soundPlayer.PlayOneShot(GetAudioClip(sound));
        TimerFunction.Create(() => TimerFunction.DestroyImmediate(SoundManagerDummyObject), 8f);
    }

    private static AudioClip GetAudioClip(GameSounds sound)
    {
        foreach (GameAssets.SoundClip clip in GameAssets.AssetsInstance.gameSoundsClips)
        {
            if(clip.sound == sound)
            {
                return clip.soundClip;
            }
        }

        return null;
    }

}
