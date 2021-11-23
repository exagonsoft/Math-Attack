using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base_Utils : MonoBehaviour
{
    #region Properties

    public enum enemyType
    {
        DUMB,
        FIZZ,
        BUZZ,
        FIZZBUZZ
    }

    public class enemyStats
    {
        public enemyType type;
        public float muveSpedd;
        public int hitDamage;
        public int maxHealth;
        public int scoreValue;
    }

    public class weaponStats
    {
        public enemyType targetType;
        public float muveSpedd;
        public int hitDamage;
    }

    public static Vector3[] positions = {
            new Vector3(0, 0, 0),
            new Vector3(235, 125, 0),
            new Vector3(235, -125, 0),
            new Vector3(-235, -125, 0),
            new Vector3(-235, 125, 0),
            new Vector3(0, 125, 0),
            new Vector3(0, -125, 0),
            new Vector3(235, 0, 0),
            new Vector3(-235, 0, 0),
            new Vector3(-117.5f, 125, 0),
            new Vector3(117.5f, 125, 0),
            new Vector3(-117.5f, -125, 0),
            new Vector3(-117.5f, 125, 0),
            new Vector3(-235, 62.5f, 0),
            new Vector3(235, 62.5f, 0),
            new Vector3(235, -62.5f, 0),
            new Vector3(-235, -62.5f, 0)
        };

    private static int iLastPos = 0;
    private static int iLastLastPos = 0;

    #endregion

    #region Events

    void Start()
    {

    }

    void Update()
    {

    }

    #endregion

    #region Methods

    // Get Mouse Position in World with Z = 0f
    public static Vector3 GetMouseWorldPosition()
    {
        Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        vec.z = 0f;
        return vec;
    }

    public static Vector3 GetMouseWorldPositionWithZ()
    {
        return GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
    }

    public static Vector3 GetMouseWorldPositionWithZ(Camera worldCamera)
    {
        return GetMouseWorldPositionWithZ(Input.mousePosition, worldCamera);
    }

    public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera)
    {
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }

    // Screen Shake
    public static void ShakeCamera(float intensity, float timer)
    {
        Vector3 originalPosition = Camera.main.transform.position;
        Vector3 lastCameraMovement = Vector3.zero;
        UpdaterFunctions.Create(delegate () {
            timer -= Time.unscaledDeltaTime;
            Vector3 randomMovement = new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized * intensity;
            Camera.main.transform.position = Camera.main.transform.position - lastCameraMovement + randomMovement;
            lastCameraMovement = randomMovement;
            return timer <= 0f;
        }, "CAMERA_SHAKE");
        Camera.main.transform.position = originalPosition;
    }

    public static enemyType getEnemyType(int enemyNumber)
    {
        enemyType typeResoult = enemyType.DUMB;

        if (enemyNumber % 3 == 0)
        {
            typeResoult = enemyType.FIZZ;
            if (enemyNumber % 5 == 0)
            {
                typeResoult = enemyType.FIZZBUZZ;
            }
        }
        else
        {
            if (enemyNumber % 5 == 0)
            {
                typeResoult = enemyType.BUZZ;
            }
        }

        return typeResoult;
    }

    public static Vector3 getRandomPosition()
    {
        int iPos = UnityEngine.Random.Range(1, 16);
        if (iPos == iLastPos || iPos == iLastLastPos)
        {
            getRandomPosition();
        }
        iLastLastPos = iLastPos;
        iLastPos = iPos;
        return positions[iPos];
    }

    public static enemyType GetRoketType(int iWeapon)
    {
        enemyType roketType = enemyType.DUMB;

        switch (iWeapon)
        {
            case 1:
                {
                    roketType = enemyType.FIZZ;
                    break;
                }
            case 2:
                {
                    roketType = enemyType.BUZZ;
                    break;
                }
            case 3:
                {
                    roketType = enemyType.FIZZBUZZ;
                    break;
                }
        }

        return roketType;
    }

    public static float getRelativeAngle(Vector3 pos1, Vector3 pos2)
    {
        float resoult = 0;

        Vector3 aimDirection = (pos1 - pos2).normalized;
        resoult = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

        return resoult;
    }

    public static int getWeaponFromType(Base_Utils.enemyType weaponType)
    {
        int weaponResoult = 0;

        switch (weaponType)
        {
            case enemyType.FIZZ:
                {
                    weaponResoult = 1;
                    break;
                }
            case enemyType.BUZZ:
                {
                    weaponResoult = 2;
                    break;
                }
            case enemyType.FIZZBUZZ:
                {
                    weaponResoult = 3;
                    break;
                }
        }

        return weaponResoult;
    }

    public static Base_Utils.enemyType getWeaponFromInt(int weaponType)
    {
        Base_Utils.enemyType weaponResoult = enemyType.DUMB;

        switch (weaponType)
        {
            case 1:
                {
                    weaponResoult = enemyType.FIZZ;
                    break;
                }
            case 2:
                {
                    weaponResoult = enemyType.BUZZ;
                    break;
                }
            case 3:
                {
                    weaponResoult = enemyType.FIZZBUZZ;
                    break;
                }
        }

        return weaponResoult;
    }

    public static void WriteData(string jsonData)
    {
        string sFilePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\ExagonSoft\\MathAtack\\";
        string sFileName = "PlayerData.ext";
        try
        {
            //Create File if don't exist
            if (!File.Exists(sFilePath + sFileName))
            {
                if (!Directory.Exists(sFilePath))
                {
                    Directory.CreateDirectory(sFilePath);
                }
                System.IO.StreamWriter dataWritter = new StreamWriter(sFilePath + sFileName);
                dataWritter.Write(jsonData);
                dataWritter.Close();
                dataWritter.Dispose();
            }
            else
            {
                System.IO.StreamWriter dataWritter = new StreamWriter(sFilePath + sFileName);
                dataWritter.Write(jsonData);
                dataWritter.Close();
                dataWritter.Dispose();
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public static string ReadData()
    {
        string sData = "";
        string sFilePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\ExagonSoft\\MathAtack\\PlayerData.ext";

        try
        {

            if (File.Exists(sFilePath))
            {
                StreamReader fileReader = new StreamReader(sFilePath);
                sData = fileReader.ReadToEnd();
                fileReader.Close();
                fileReader.Dispose();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return sData;
    }

    public static Vector3 getRandomWorldPosition()
    {
        return new Vector3(UnityEngine.Random.Range(-120f, 120f), UnityEngine.Random.Range(-120f, 120f), 80f);
    }

    public static int CalcScore(float toCalc)
    {
        int iresoult = 0;

        if (toCalc > .95f)
        {
            iresoult = 3;
        }
        else {
            if (toCalc > .50f)
            {
                iresoult = 2;
            }
            else {
                iresoult = 1;
            }
        }

        return iresoult;
    }

    public static class WeaponBaseDamage
    {
        public static int DUMB_Weapon = 10;
        public static int FIZZ_Weapon = 10;
        public static int BUZZ_Weapon = 10;
        public static int FIZBUZZ_Weapon = 15;
    }
        

    #endregion
}
