using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.WebCam;

public class CameraCon : MonoBehaviour
{
    public enum CamType
    {
        messi,
        wak,
        interactionCam,
        interaction_SeeObject,
        end
    }
    private bool camset = true;
    public CamType nowCam = CamType.messi;
    public Camera MainCam;
    public Camera BathCam;
    public Camera RoomCam;
    public Camera WakCam;
    public Camera EndCam;

    [SerializeField]
    private GameObject interaction_info;

    [SerializeField]
    private GameObject messi;

    [SerializeField]
    private GameObject wakgood; //WakHead

    [SerializeField] GameObject cursorUI;

    // Start is called before the first frame update
    void Start()
    {
        BathCam.enabled = false;
        RoomCam.enabled = false;
        MainCam.enabled = false;
        BathCam.GetComponent<AudioListener>().enabled = false;
        RoomCam.GetComponent<AudioListener>().enabled = false;
        MainCam.GetComponent<AudioListener>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.production == false)
        {
            if (camset == true) // 한번만 실행되는 if문
            {
                nowCam = CamType.wak;
                camset = false;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                cursorUI.SetActive(true);
                BathCam.enabled = false;
                RoomCam.enabled = false;
                MainCam.enabled = false;
                BathCam.GetComponent<AudioListener>().enabled = false;
                RoomCam.GetComponent<AudioListener>().enabled = false;
                MainCam.GetComponent<AudioListener>().enabled = false;
                WakCam.enabled = true;
                WakCam.GetComponent<AudioListener>().enabled = true;
                interaction_info.gameObject.SetActive(true);
                wakgood.GetComponent<WakMove>().enabled = true;
            }
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                CheckmessiCam();
            }
        }
    }

    public void CheckmessiCam()
    {
        if (nowCam == CamType.messi) //메시시점에서 왁굳시점으로 가는 조건문
        {
            BathCam.enabled = false;
            RoomCam.enabled = false;
            MainCam.enabled = false;
            BathCam.GetComponent<AudioListener>().enabled = false;
            RoomCam.GetComponent<AudioListener>().enabled = false;
            MainCam.GetComponent<AudioListener>().enabled = false;
            WakCam.enabled = true;
            WakCam.GetComponent<AudioListener>().enabled = true;
            nowCam = CamType.wak;
            interaction_info.gameObject.SetActive(true);
            wakgood.GetComponent<WakMove>().enabled = true; //메시캠 활성시 왁굳 이동 중지
        }
        else if (nowCam == CamType.wak) //왁굳시점에서 메시시점으로 가는 조건문
        {
            WakCam.enabled = false;
            WakController.NoneCursor();
            cursorUI.SetActive(false);
            messiMove tmp = messi.GetComponent<messiMove>();
            if ((tmp.messiinBath == false) && (tmp.messiinRoom == false)) ShowMainCam();
            else if (tmp.messiinBath == true) ShowBathCam();
            else if (tmp.messiinRoom == true) ShowRoomCam();
            nowCam = CamType.messi;
            interaction_info.gameObject.SetActive(false);
            wakgood.GetComponent<WakMove>().enabled = false; //메시캠 비활성시 왁굳 이동 재개
            WakCam.GetComponent<AudioListener>().enabled = false;
        }
    }

    public void ShowMainCam()
    {
        BathCam.enabled = false;
        RoomCam.enabled = false;
        MainCam.enabled = true;
        BathCam.GetComponent<AudioListener>().enabled = false;
        RoomCam.GetComponent<AudioListener>().enabled = false;
        MainCam.GetComponent<AudioListener>().enabled = true;
    }

    public void ShowBathCam()
    {
        RoomCam.enabled = false;
        MainCam.enabled = false;
        BathCam.enabled = true;
        BathCam.GetComponent<AudioListener>().enabled = true;
        RoomCam.GetComponent<AudioListener>().enabled = false;
        MainCam.GetComponent<AudioListener>().enabled = false;
    }
    public void ShowRoomCam()
    {
        BathCam.enabled = false;
        MainCam.enabled = false;
        RoomCam.enabled = true;
        BathCam.GetComponent<AudioListener>().enabled = false;
        RoomCam.GetComponent<AudioListener>().enabled = true;
        MainCam.GetComponent<AudioListener>().enabled = false;
    }

    public void ShowInteractionCam(Camera focusCam)
    {
        if (nowCam == CamType.wak)
        {
            nowCam = CamType.interactionCam;
            focusCam.enabled = true;
            focusCam.GetComponent<AudioListener>().enabled = true;
            WakCam.enabled = false;
            WakCam.GetComponent<AudioListener>().enabled = false;
            BathCam.enabled = false;
            MainCam.enabled = false;
            RoomCam.enabled = false;
        }
    }
    public void CloseInteractionCam(Camera focusCam)
    {
        if (nowCam == CamType.interactionCam)
        {
            nowCam = CamType.wak;
            focusCam.enabled = false;
            focusCam.GetComponent<AudioListener>().enabled = false;
            WakCam.enabled = true;
            WakCam.GetComponent<AudioListener>().enabled = true;
            BathCam.enabled = false;
            MainCam.enabled = false;
            RoomCam.enabled = false;
        }
    }
    public void ShowInteractionShowObject()
    {
        BathCam.enabled = false;
        RoomCam.enabled = false;
        MainCam.enabled = false;
        BathCam.GetComponent<AudioListener>().enabled = false;
        RoomCam.GetComponent<AudioListener>().enabled = false;
        MainCam.GetComponent<AudioListener>().enabled = false;
        WakCam.enabled = true;
        WakCam.GetComponent<AudioListener>().enabled = true;
        nowCam = CamType.interaction_SeeObject;
        interaction_info.gameObject.SetActive(true);
        wakgood.GetComponent<WakMove>().enabled = false;
    }
    public void CloseInteractionShowObject()
    {
        BathCam.enabled = false;
        RoomCam.enabled = false;
        MainCam.enabled = false;
        BathCam.GetComponent<AudioListener>().enabled = false;
        RoomCam.GetComponent<AudioListener>().enabled = false;
        MainCam.GetComponent<AudioListener>().enabled = false;
        WakCam.enabled = true;
        WakCam.GetComponent<AudioListener>().enabled = true;
        nowCam = CamType.wak;
        interaction_info.gameObject.SetActive(true);
        wakgood.GetComponent<WakMove>().enabled = true;
    }

    public void EndCameraSet()
    {
        MainCam.enabled = false;
        BathCam.enabled = false;
        RoomCam.enabled = false;
        WakCam.enabled = false;
        EndCam.enabled = true;
        nowCam = CamType.end;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
