using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdaterFunctions : MonoBehaviour
{
    /*
          * Class to hook Actions into MonoBehaviour
          * */
    private class MonoBehaviourHook : MonoBehaviour
    {

        public Action OnUpdate;

        private void Update()
        {
            if (OnUpdate != null) OnUpdate();
        }

    }

    private static List<UpdaterFunctions> updaterList; // Holds a reference to all active updaters
    private static GameObject initGameObject; // Global game object used for initializing class, is destroyed on scene change

    private static void InitIfNeeded()
    {
        if (initGameObject == null)
        {
            initGameObject = new GameObject("FunctionUpdater_Global");
            updaterList = new List<UpdaterFunctions>();
        }
    }




    public static UpdaterFunctions Create(Action updateFunc)
    {
        return Create(() => { updateFunc(); return false; }, "", true, false);
    }
    public static UpdaterFunctions Create(Func<bool> updateFunc)
    {
        return Create(updateFunc, "", true, false);
    }
    public static UpdaterFunctions Create(Func<bool> updateFunc, string functionName)
    {
        return Create(updateFunc, functionName, true, false);
    }
    public static UpdaterFunctions Create(Func<bool> updateFunc, string functionName, bool active)
    {
        return Create(updateFunc, functionName, active, false);
    }
    public static UpdaterFunctions Create(Func<bool> updateFunc, string functionName, bool active, bool stopAllWithSameName)
    {
        InitIfNeeded();

        if (stopAllWithSameName)
        {
            StopAllUpdatersWithName(functionName);
        }

        GameObject gameObject = new GameObject("FunctionUpdater Object " + functionName, typeof(MonoBehaviourHook));
        UpdaterFunctions functionUpdater = new UpdaterFunctions(gameObject, updateFunc, functionName, active);
        gameObject.GetComponent<MonoBehaviourHook>().OnUpdate = functionUpdater.Update;

        updaterList.Add(functionUpdater);
        return functionUpdater;
    }
    private static void RemoveUpdater(UpdaterFunctions funcUpdater)
    {
        InitIfNeeded();
        updaterList.Remove(funcUpdater);
    }
    public static void DestroyUpdater(UpdaterFunctions funcUpdater)
    {
        InitIfNeeded();
        if (funcUpdater != null)
        {
            funcUpdater.DestroySelf();
        }
    }
    public static void StopUpdaterWithName(string functionName)
    {
        InitIfNeeded();
        for (int i = 0; i < updaterList.Count; i++)
        {
            if (updaterList[i].functionName == functionName)
            {
                updaterList[i].DestroySelf();
                return;
            }
        }
    }
    public static void StopAllUpdatersWithName(string functionName)
    {
        InitIfNeeded();
        for (int i = 0; i < updaterList.Count; i++)
        {
            if (updaterList[i].functionName == functionName)
            {
                updaterList[i].DestroySelf();
                i--;
            }
        }
    }





    private GameObject gameObject;
    private string functionName;
    private bool active;
    private Func<bool> updateFunc; // Destroy Updater if return true;

    public UpdaterFunctions(GameObject gameObject, Func<bool> updateFunc, string functionName, bool active)
    {
        this.gameObject = gameObject;
        this.updateFunc = updateFunc;
        this.functionName = functionName;
        this.active = active;
    }
    public void Pause()
    {
        active = false;
    }
    public void Resume()
    {
        active = true;
    }

    private void Update()
    {
        if (!active) return;
        if (updateFunc())
        {
            DestroySelf();
        }
    }
    public void DestroySelf()
    {
        RemoveUpdater(this);
        if (gameObject != null)
        {
            UnityEngine.Object.Destroy(gameObject);
        }
    }
}
