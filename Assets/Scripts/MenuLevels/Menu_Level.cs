using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_Level : MonoBehaviour
{
    #region Properties
    private int levelStars;
    private bool locked;
    private Material planetMaterial;

    public static Menu_Level InstanciateLevel;
    #endregion

    #region Events

    private void Awake()
    {
        InstanciateLevel = this;
    }

    #endregion

    #region Functions

    public void SetUpLevel(int stars, bool isLocked, Material planetMaterial)
    {
        levelStars = stars;
        locked = isLocked;
        this.planetMaterial = planetMaterial;

        RepaintPlanet(this.planetMaterial);
        RepaintStars(levelStars);
        transform.Find("LockState").gameObject.SetActive(locked);
    }

    private void RepaintPlanet(Material planetMaterial)
    {
        transform.Find("Planet").GetComponent<MeshRenderer>().material = planetMaterial;
    }

    private void RepaintStars(int stars)
    {
        switch (stars)
        {
            case 0:
                {
                    transform.Find("LevelScore").gameObject.SetActive(false);
                    break;
                }
            case 1:
                {
                    transform.Find("LevelScore").Find("Star2").gameObject.SetActive(false);
                    transform.Find("LevelScore").Find("Star3").gameObject.SetActive(false);
                    break;
                }
            case 2:
                {
                    transform.Find("LevelScore").Find("Star3").gameObject.SetActive(false);
                    break;
                }
            case 3:
                {
                    break;
                }
        }
    }

    private void RepaintLock(bool locked)
    {
        
    }

    public int GetStars()
    {
        return levelStars;
    }

    public bool IsLocked()
    {
        return locked;
    }

    #endregion

    #region Private Class



    #endregion
}
