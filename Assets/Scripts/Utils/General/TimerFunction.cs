using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerFunction : MonoBehaviour
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


    private static List<TimerFunction> timerList; // Holds a reference to all active timers
    private static GameObject initGameObject; // Global game object used for initializing class, is destroyed on scene change

    private static void InitIfNeeded()
    {
        if (initGameObject == null)
        {
            initGameObject = new GameObject("TimerFunction_Global");
            timerList = new List<TimerFunction>();
        }
    }




    public static TimerFunction Create(Action action, float timer)
    {
        return Create(action, timer, "", false, false);
    }
    public static TimerFunction Create(Action action, float timer, string functionName)
    {
        return Create(action, timer, functionName, false, false);
    }
    public static TimerFunction Create(Action action, float timer, string functionName, bool useUnscaledDeltaTime)
    {
        return Create(action, timer, functionName, useUnscaledDeltaTime, false);
    }
    public static TimerFunction Create(Action action, float timer, string functionName, bool useUnscaledDeltaTime, bool stopAllWithSameName)
    {
        InitIfNeeded();

        if (stopAllWithSameName)
        {
            StopAllTimersWithName(functionName);
        }

        GameObject obj = new GameObject("TimerFunction Object " + functionName, typeof(MonoBehaviourHook));
        TimerFunction funcTimer = new TimerFunction(obj, action, timer, functionName, useUnscaledDeltaTime);
        obj.GetComponent<MonoBehaviourHook>().OnUpdate = funcTimer.Update;

        timerList.Add(funcTimer);

        return funcTimer;
    }
    public static void RemoveTimer(TimerFunction funcTimer)
    {
        InitIfNeeded();
        timerList.Remove(funcTimer);
    }
    public static void StopAllTimersWithName(string functionName)
    {
        InitIfNeeded();
        for (int i = 0; i < timerList.Count; i++)
        {
            if (timerList[i].functionName == functionName)
            {
                timerList[i].DestroySelf();
                i--;
            }
        }
    }
    public static void StopFirstTimerWithName(string functionName)
    {
        InitIfNeeded();
        for (int i = 0; i < timerList.Count; i++)
        {
            if (timerList[i].functionName == functionName)
            {
                timerList[i].DestroySelf();
                return;
            }
        }
    }





    private GameObject gameObject;
    private float timer;
    private string functionName;
    private bool active;
    private bool useUnscaledDeltaTime;
    private Action action;



    public TimerFunction(GameObject gameObject, Action action, float timer, string functionName, bool useUnscaledDeltaTime)
    {
        this.gameObject = gameObject;
        this.action = action;
        this.timer = timer;
        this.functionName = functionName;
        this.useUnscaledDeltaTime = useUnscaledDeltaTime;
    }

    private void Update()
    {
        if (useUnscaledDeltaTime)
        {
            timer -= Time.unscaledDeltaTime;
        }
        else
        {
            timer -= Time.deltaTime;
        }
        if (timer <= 0)
        {
            // Timer complete, trigger Action
            action();
            DestroySelf();
        }
    }
    private void DestroySelf()
    {
        RemoveTimer(this);
        if (gameObject != null)
        {
            UnityEngine.Object.Destroy(gameObject);
        }
    }




    /*
     * Class to trigger Actions manually without creating a GameObject
     * */
    public class TimerFunctionObject
    {

        private float timer;
        private Action callback;

        public TimerFunctionObject(Action callback, float timer)
        {
            this.callback = callback;
            this.timer = timer;
        }

        public void Update()
        {
            Update(Time.deltaTime);
        }
        public void Update(float deltaTime)
        {
            timer -= deltaTime;
            if (timer <= 0)
            {
                callback();
            }
        }
    }

    // Create a Object that must be manually updated through Update();
    public static TimerFunctionObject CreateObject(Action callback, float timer)
    {
        return new TimerFunctionObject(callback, timer);
    }
}
