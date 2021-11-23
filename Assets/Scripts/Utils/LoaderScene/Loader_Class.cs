using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader_Class
{
    private class LoadingMonoBehaviourDumyClass : MonoBehaviour { }

    public enum Scene
    {
        Game,
        Loading,
        LevelMenu,
        MainMenu,
        ConfigurationScene
    }

    private static Action onLoaderCallback;
    private static AsyncOperation loadingAsyncOperation;

    public static void Load(Scene destinyScene)
    {
        // Set the loader callback action to load the target scene
        onLoaderCallback = () => {
            GameObject loadingGameObject = new GameObject("Loading Game DumyObject");
            loadingGameObject.AddComponent<LoadingMonoBehaviourDumyClass>().StartCoroutine(LoadSceneAsync(destinyScene));
        };


        // Load the loading scene
        SceneManager.LoadScene(Scene.Loading.ToString());
    }

    private static IEnumerator LoadSceneAsync(Scene toScene)
    {
        yield return null;

        loadingAsyncOperation = SceneManager.LoadSceneAsync(toScene.ToString());

        while (!loadingAsyncOperation.isDone)
        {
            yield return null;
        }
    }

    public static float GetProgress()
    {
        if (loadingAsyncOperation != null)
        {
            return loadingAsyncOperation.progress;
        }
        else
        {
            return 1f;
        }
    }

    public static void LoaderCallback()
    {
        // Triggered after the first Update which lets the screen refresh
        // Execute the loader callback action which will load the target scene
        if (onLoaderCallback != null)
        {
            onLoaderCallback();
            onLoaderCallback = null;
        }
    }
}
