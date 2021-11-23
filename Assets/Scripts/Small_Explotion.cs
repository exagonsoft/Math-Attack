using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Small_Explotion : MonoBehaviour
{
    #region Properties

    private Vector3 explotionLocation;

    #endregion

    #region Events

    void Start()
    {

    }

    void Update()
    {

    }

    #endregion

    #region Functions

    public void ExplotionSetUp(Vector3 Location)
    {
        this.explotionLocation = Location;
        Destroy(gameObject, 1.35f);
    }

    #endregion
}
