using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class messiCursor : MonoBehaviour
{
    [SerializeField] Texture2D cursorGeneral;
    [SerializeField] Texture2D cursorInteraction;
    [SerializeField] Texture2D blackcursor;

    public Camera MainCam;
    public Camera BathCam;
    public Camera RoomCam;
    private CameraCon cameraCon;
    private int h;
    private int w;
    [SerializeField] private GameObject system;
    Vector2 hotSpotG;
    Vector2 hotSpotI;    
    Vector2 hotSpotB;
    public static bool canClick;

    private void Awake()
    {
        //cursorGeneral.Resize(5, 5);
        hotSpotG.x = cursorGeneral.width / 2;
        hotSpotG.y = cursorGeneral.height / 2;

        hotSpotI.x = cursorInteraction.width / 2;
        hotSpotI.y = cursorInteraction.height / 2;

        hotSpotB.x = blackcursor.width / 2;
        hotSpotB.y = blackcursor.height / 2;
    }
    void Start()
    {
        cameraCon = system.GetComponent<CameraCon>();
        Cursor.SetCursor(cursorGeneral, hotSpotG, CursorMode.ForceSoftware);
        //Cursor.lockState = CursorLockMode.Confined;
        //Cursor.SetCursor(cursorInteraction, hotSpotI, CursorMode.ForceSoftware);
        h = Screen.height;
        w = Screen.width;
    }
    // Update is called once per frame
    void Update()
    {
        if (cameraCon.nowCam == CameraCon.CamType.messi)
        {
            clickSet();
            cursorSet();
        }
    }

    void clickSet()
    {
        Vector3 mp = Input.mousePosition;
        if (MainCam.enabled == true)
        {
            if (mp.y / h > 820f / 1080f)
            {
                if (mp.x / w > 1300f / 1920f && mp.x / w < 1355f / 1920f && mp.y / h < 1000f / 1080f && mp.y / h > 875f / 1080f) canClick = true;
                else canClick = false;
            }
            else if (mp.y / h < 370f / 1080f)
            {
                if (mp.y / h > 310f / 1080f && ((mp.x / w < 650f / 1920f && mp.x / w > 510f / 1920f) || (mp.x / w > 1180f / 1920f && mp.x / w < 1500f / 1920f))) canClick = true;
                else canClick = false;
                
            }
            else if (mp.x / w < 525f / 1920f)
            {
                canClick = false;
            }
            else if (mp.x / w > 1500f / 1920f)
            {
                if (mp.x / w > 1590f / 1920f && mp.y / h > 695f / 1080f && mp.x / w < 1770f / 1920f && mp.y / h < 810f / 1080f) canClick = true;
                else canClick = false;
            }
            else
            {
                canClick = true;
            }
        }
        
        else if (BathCam.enabled == true)
        {
            if (mp.x / w > 995f / 1920f && mp.y / h > 570f / 1080f)
            {
                canClick = false;
            }
            else if (mp.x / w < 770f / 1920f)
            {
                canClick = false;
            }
            else if (mp.y / h < 260f / 1080f)
            {
                if (mp.x / w > 910f / 1920f && mp.y / h < 175f / 1080f && mp.x / w < 1030f / 1920f && mp.y / h > 90f / 1080f)
                {
                    canClick = true;
                }
                else canClick = false;
            }
            else if (mp.y / h > 810f / 1080f)
            {
                canClick = false;
            }
            else if (mp.x / w > 1185f / 1920f)
            {
                canClick = false;
            }
            else
            {
                canClick = true;
            }
        }

        else if (RoomCam.enabled == true)
        {
            if (mp.x / w < 495f / 1920f)
            {
                if (mp.x / w < 395f / 1920f && mp.y / h < 410f / 1080f && mp.x / w > 215f / 1920f && mp.y / h > 300f / 1080f)
                {
                    canClick = true;
                }
                else canClick = false;
            }
            else if (mp.x / w > 1405f / 1920f)
            {
                canClick = false;
            }
            else if (mp.y / h < 275f / 1080f)
            {
                canClick = false;
            }
            else if (mp.y / h > 805f / 1080f)
            {
                canClick = false;
            }
            else if (mp.x / w < 555f / 1920f && mp.y / h < 310f / 1080f)
            {
                canClick = false;
            }
            else
            {
                canClick = true;
            }
        }
    }

    void cursorSet()
    {
        if (canClick == false)
        {
            Cursor.SetCursor(blackcursor, hotSpotB, CursorMode.ForceSoftware);
        }
        else
        {
            Cursor.SetCursor(cursorGeneral, hotSpotG, CursorMode.ForceSoftware);
        }
    }
}
