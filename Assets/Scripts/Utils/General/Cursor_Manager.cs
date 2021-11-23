using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor_Manager : MonoBehaviour
{
    #region Properties
    public static Cursor_Manager Instance { get; private set; }
    [SerializeField] private List<CursorAnimationHandler> cursorAnimations;

    private CursorAnimationHandler cursorAnimation;
    private int texturesCount;
    private int currentTexture;
    private float textureTimer;
   
    public enum CursorType
    {
        Arrow,
        Pointer
    }

    #endregion

    #region Events

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SetActiveCursorType(GetCursorAnimationHandler(CursorType.Arrow));
    }

    private void Update()
    {
        textureTimer -= Time.deltaTime;
        if (textureTimer <= 0f)
        {
            textureTimer += cursorAnimation.textureChangeRate;
            currentTexture = (currentTexture + 1) % texturesCount;
            Cursor.SetCursor(cursorAnimation.cursorTextures[currentTexture], cursorAnimation.textureOffSett, CursorMode.Auto);
        }
    }

    #endregion

    #region Functions

    private void SetActiveCursorType(CursorAnimationHandler animationHandler)
    {
        this.cursorAnimation = animationHandler;
        currentTexture = 0;
        textureTimer = cursorAnimation.textureChangeRate;
        this.texturesCount = cursorAnimation.cursorTextures.Length;
    }

    public void SetActiveCursor(CursorType cursorType)
    {
        SetActiveCursorType(GetCursorAnimationHandler(cursorType));
    }

    private CursorAnimationHandler GetCursorAnimationHandler(CursorType cursorType)
    {
        foreach(CursorAnimationHandler tempAnim in cursorAnimations)
        {
            if(tempAnim.cursorType == cursorType)
            {
                return tempAnim;
            }
        }
        return null;
    }

    #endregion


    #region Private Class
    [System.Serializable]
    public class CursorAnimationHandler
    {
        public CursorType cursorType;
        public Texture2D[] cursorTextures;
        public float textureChangeRate;
        public Vector2 textureOffSett;
    }

    #endregion
}
