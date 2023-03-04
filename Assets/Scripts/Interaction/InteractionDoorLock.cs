using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractionDoorLock : InteractionTrigger
{
    [SerializeField] private Camera focusCam;
    [SerializeField] private GameObject sys;
    [SerializeField] private AudioClip[] sounds = new AudioClip[3];
    [SerializeField] private float buttonVolume = 0.25f;
    [SerializeField] private float wrongVolume = 0.5f;
    [SerializeField] private float answerVolume = 0.5f;

    AudioSource audioSource;

    int layerMaskForInteractionSec = 1 << 9;
    private float rayLength = 70f;
    [SerializeField] private String numString = null;
    [SerializeField] private int numCnt = 0;

    [SerializeField] private GameObject keyPadUI;
    [SerializeField] private GameObject escUI;
    [SerializeField] private TextMeshProUGUI escText;

    private ChangePasswordManager sys_passwordManager;

    private CameraCon cameraCon;

    private enum Steps
    {
        notWork,
        showDoorLock,
        checkPassword
    }
    private enum RoomNum
    {
        ROOM_NULL,
        ROOM_723,
        ROOM_724
    }

    [SerializeField] private Steps nowStep = Steps.notWork;
    [SerializeField] private RoomNum nowRoomNum = RoomNum.ROOM_NULL;
    private bool soundPlayed = false;

    [SerializeField] Texture2D whiteCursor;

    // Start is called before the first frame update
    void Start()
    {
        cameraCon = sys.GetComponent<CameraCon>();
        focusCam.enabled = false;
        focusCam.GetComponent<AudioListener>().enabled = false;
        audioSource = GetComponent<AudioSource>();
        if (sys == null)
        {
            sys = GameObject.Find("System");
        }
        sys_passwordManager = sys.GetComponent<ChangePasswordManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isWorking)
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                isWorking = false;
                cameraCon.CloseInteractionCam(focusCam);
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                audioSource.Stop();
                keyPadUI.SetActive(false);
                escUI.SetActive(false);
                nowStep = Steps.notWork;
            }
            else
            {
                switch (nowStep)
                {
                    case Steps.notWork:
                        cameraCon.ShowInteractionCam(focusCam);
                        nowStep = Steps.showDoorLock;
                        numString = null;
                        numCnt = 0;
                        audioSource.clip = null;
                        break;
                    case Steps.showDoorLock:
                        if(numCnt>=4)
                        {
                            nowStep = Steps.checkPassword;
                        }
            
                        break;
                    case Steps.checkPassword:
                        String tmp = null;
                        if(nowRoomNum == RoomNum.ROOM_723) tmp = sys_passwordManager.password723.ToString();
                        else if(nowRoomNum == RoomNum.ROOM_724) tmp = sys_passwordManager.password724.ToString();
                        while (tmp.Length < 4) tmp = "0" + tmp;
                        if (numString == tmp)
                        {
                            isWorking = false;
                            cameraCon.CloseInteractionCam(focusCam);
                            Cursor.visible = false;
                            Cursor.lockState = CursorLockMode.Locked;
                            audioSource.Stop();
                            keyPadUI.SetActive(false);
                            escUI.SetActive(false);
                            nowStep = Steps.notWork;

                            if (nowRoomNum == RoomNum.ROOM_724)
                            {
                                sys_passwordManager.is724Open = true;
                                this.gameObject.layer = 3;
                            }
                            audioSource.clip = sounds[2];
                            audioSource.volume = answerVolume;
                            audioSource.Play();
                        }
                        else
                        {
                            numString = null;
                            numCnt = 0;
                            audioSource.clip = sounds[1];
                            audioSource.volume = wrongVolume;
                            audioSource.Play();
                            nowStep = Steps.showDoorLock;
                        }
                        break;
                    default:
                        break;
                }
            }
        }
    }

    public override void InteractionFunction()
    {
        base.InteractionFunction();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.SetCursor(whiteCursor, new Vector2(whiteCursor.width / 2, whiteCursor.height / 2), CursorMode.ForceSoftware);
        keyPadUI.SetActive(true);
        escUI.SetActive(true);
        escText.text = "ESC로 나가기";
    }

    public void keypadButton(String n)
    {
        numString += n;
        audioSource.clip = sounds[0];
        audioSource.volume = buttonVolume;
        audioSource.Play();
        numCnt++;
    }
}
