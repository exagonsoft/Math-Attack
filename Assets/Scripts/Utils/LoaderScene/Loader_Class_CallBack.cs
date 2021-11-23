using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader_Class_CallBack : MonoBehaviour
{
    private bool isFirstUpdate = true;

    private void Update()
    {
        if (isFirstUpdate)
        {
            Cursor_Manager.Instance.SetActiveCursor(Cursor_Manager.CursorType.Arrow);
            isFirstUpdate = false;
            Loader_Class.LoaderCallback();
        }
    }
}
