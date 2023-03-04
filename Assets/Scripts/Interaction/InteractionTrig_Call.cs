using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractionTrig_Call : InteractionTrigger
{
    [SerializeField] private Camera focusCam;
    [SerializeField] private GameObject sys;
    [SerializeField] private GameObject[] buttons = new GameObject[10];
    [SerializeField] private AudioClip[] sounds = new AudioClip[10];
    [SerializeField] private float buttonVolume = 0.25f;
    [SerializeField] private float voiceVolume = 0.75f;
    AudioSource audioSource;
    SubtitleTrigger subtitleTrigger;

    int layerMaskForInteractionSec = 1 << 9;
    private float rayLength = 70f;
    [SerializeField] private String numString = null;
    [SerializeField] private int numCnt = 0;

    private ChangePasswordManager sys_passwordManager;

    [SerializeField] Texture2D blackcursor;
    [SerializeField] GameObject escapeUI;
    [SerializeField] TextMeshProUGUI escapeText;

    private CameraCon cameraCon;
    private enum Steps
    {
        notWork,
        showExplaination,
        getCallNum,
        checkResultFirst,
        getCharmNum,
        checkResultSec,
        resetCharmNum,
        endWork
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


    // Start is called before the first frame update
    void Start()
    {
        cameraCon = sys.GetComponent<CameraCon>();
        focusCam.enabled = false;
        focusCam.GetComponent<AudioListener>().enabled = false;
        subtitleTrigger = GetComponent<SubtitleTrigger>();
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
        if(isWorking)
        {
            if(Input.GetKeyUp(KeyCode.Escape))
            {
                isWorking = false;
                cameraCon.CloseInteractionCam(focusCam);
                subtitleTrigger.OffSubtitle();
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                audioSource.Stop();
                escapeUI.SetActive(false);
                if (nowStep != Steps.resetCharmNum)
                {
                    nowStep = Steps.notWork;
                    nowRoomNum = RoomNum.ROOM_NULL;
                    soundPlayed = false;
                }
            }
            else
            {
                switch (nowStep)
                {
                    case Steps.notWork:
                        escapeUI.SetActive(true);
                        escapeText.text = "ESC로 나가기";
                        cameraCon.ShowInteractionCam(focusCam);
                        nowStep = Steps.showExplaination;
                        numString = null;
                        numCnt = 0;
                        audioSource.clip = null;
                        break;
                    case Steps.showExplaination:
                        nowStep = Steps.getCallNum;
                        break;
                    case Steps.getCallNum:
                        RaycastHit hit;

                        Ray touchray = focusCam.ScreenPointToRay(Input.mousePosition);
                        Physics.Raycast(touchray, out hit, rayLength, layerMaskForInteractionSec);

                        if (Input.GetMouseButtonDown(0) && hit.collider != null)
                        {
                            numString += hit.collider.gameObject.name[0];
                            numCnt++;
                            string tmp_buttonNum = null;
                            tmp_buttonNum += hit.collider.gameObject.name[0];
                            if (hit.collider.gameObject.name[0] == '#') audioSource.clip = sounds[10];
                            else if (hit.collider.gameObject.name[0] == '*') audioSource.clip = sounds[11];
                            else audioSource.clip = sounds[Int32.Parse(tmp_buttonNum)];
                            audioSource.volume = buttonVolume;
                            audioSource.Play();
                        }
                        if(numString != null)
                        {
                            subtitleTrigger.ShowOneSentence(numString, "(번호)");
                        }
                        if (numCnt >= 11)
                        {
                            nowStep = Steps.checkResultFirst;
                        }
                        break;
                    case Steps.checkResultFirst:
                        if((numString == "72300240724") || (numString == "72400240724"))
                        {
                            if (numString == "72300240724") nowRoomNum = RoomNum.ROOM_723;
                            else if (numString == "72400240724") nowRoomNum = RoomNum.ROOM_724;

                            subtitleTrigger.TriggerSubtitle(0, 1);
                            audioSource.clip = sounds[12];
                            audioSource.volume = voiceVolume;
                            audioSource.Play();
                            nowStep = Steps.getCharmNum;
                            numString = null;
                            numCnt = 0;                       
                        }
                        else if((numString == "70014200116") || (numString == "70014200610")|| (numString == "70006100309") || (numString == "70006101008"))
                        {
                            subtitleTrigger.ShowOneSentence("상담 가능 시간이 아닙니다.");
                            audioSource.clip = sounds[19];
                            audioSource.volume = voiceVolume;
                            audioSource.Play();
                            nowStep = Steps.getCallNum;
                            numString = null;
                            numCnt = 0;
                        }
                        else if (Int32.Parse(numString.Substring(0,3))>700 && Int32.Parse(numString.Substring(0, 3)) < 741 && numString.Substring(3, numString.Length-3) =="00240724" )
                        {
                            subtitleTrigger.ShowOneSentence("알 수 없는 오류가 발생했습니다.");
                            audioSource.clip = sounds[20];
                            audioSource.volume = voiceVolume;
                            audioSource.Play();
                            nowStep = Steps.getCallNum;
                            numString = null;
                            numCnt = 0;
                        }
                        else
                        {
                            subtitleTrigger.ShowOneSentence("존재하지 않는 번호입니다.");
                            if (!soundPlayed)
                            {
                                audioSource.clip = sounds[13];
                                audioSource.volume = voiceVolume;
                                audioSource.Play();
                            }
                            numString = null;
                            numCnt = 0;
                            nowStep = Steps.getCallNum;
                        }
                        break;
                    case Steps.getCharmNum:
                        touchray = focusCam.ScreenPointToRay(Input.mousePosition);
                        Physics.Raycast(touchray, out hit, rayLength, layerMaskForInteractionSec);

                        if (Input.GetMouseButtonDown(0) && hit.collider != null)
                        {
                            if (subtitleTrigger.isShow)
                            {
                                subtitleTrigger.OffSubtitle();
                            }
                            numString += hit.collider.gameObject.name[0];
                            numCnt++;
                            string tmp_buttonNum = null;
                            tmp_buttonNum += hit.collider.gameObject.name[0];
                            if (hit.collider.gameObject.name[0] == '#') audioSource.clip = sounds[10];
                            else if (hit.collider.gameObject.name[0] == '*') audioSource.clip = sounds[11];
                            else audioSource.clip = sounds[Int32.Parse(tmp_buttonNum)];
                            audioSource.volume = buttonVolume;
                            audioSource.Play();
                        }
                        if (numString != null)
                        {
                            subtitleTrigger.ShowOneSentence(numString, "(매력코드)");
                        }
                        if (numCnt >= 7)
                        {
                            nowStep = Steps.checkResultSec;
                        }
                        break;
                    case Steps.checkResultSec:
                        if (numString == "1678915")
                        {
                            nowStep = Steps.resetCharmNum;
                            numString = null;
                            if (nowRoomNum == RoomNum.ROOM_723) audioSource.clip = sounds[14];
                            else if (nowRoomNum == RoomNum.ROOM_724) audioSource.clip = sounds[15];
                            audioSource.volume = voiceVolume;
                            audioSource.Play();
                        }
                        else
                        {
                            subtitleTrigger.ShowOneSentence("매력코드가 일치하지 않습니다.");
                            numString = null;
                            numCnt = 0;
                            audioSource.clip = sounds[16];
                            audioSource.volume = voiceVolume;
                            audioSource.Play();
                            nowStep = Steps.getCharmNum;
                        }
                        break;
                    case Steps.resetCharmNum:
                        if(cameraCon.nowCam != CameraCon.CamType.interactionCam) cameraCon.ShowInteractionCam(focusCam);
                        string tmp_roomNum = null;
                        if (nowRoomNum == RoomNum.ROOM_723)
                        {
                            sys_passwordManager.nowState = ChangePasswordManager.State.ChangePassword_723;
                            tmp_roomNum = "723";
                        }
                        else if (nowRoomNum == RoomNum.ROOM_724)
                        {
                            sys_passwordManager.nowState = ChangePasswordManager.State.ChangePassword_724;
                            tmp_roomNum = "724";
                        }
                        subtitleTrigger.ShowOneSentence(tmp_roomNum + "호 비밀번호를 재설정합니다. 스위치를 작동시켜 비밀번호를 변경한 후 #을 눌러주세요.");
                        if(escapeUI.activeSelf == false) escapeUI.SetActive(true);
                        escapeText.text = "ESC로 나가기 (대기 중)";

                        touchray = focusCam.ScreenPointToRay(Input.mousePosition);
                        Physics.Raycast(touchray, out hit, rayLength, layerMaskForInteractionSec);

                        if (Input.GetMouseButtonDown(0) && hit.collider != null)
                        {
                            numString += hit.collider.gameObject.name[0];
                            numCnt++;
                            string tmp_buttonNum = null;
                            tmp_buttonNum += hit.collider.gameObject.name[0];
                            audioSource.volume = buttonVolume;
                            if (hit.collider.gameObject.name[0] == '#')
                            {
                                nowStep = Steps.endWork;
                                if(nowRoomNum == RoomNum.ROOM_723) audioSource.clip = sounds[17];
                                else if(nowRoomNum == RoomNum.ROOM_724) audioSource.clip = sounds[18];
                                audioSource.volume = voiceVolume;
                                soundPlayed = true;
                                escapeText.text = "ESC로 나가기";
                            }
                            else if (hit.collider.gameObject.name[0] == '*') audioSource.clip = sounds[11];
                            else audioSource.clip = sounds[Int32.Parse(tmp_buttonNum)];
                            audioSource.Play();
                        }
                        break;
                    case Steps.endWork:
                        tmp_roomNum = null;
                        if (nowRoomNum == RoomNum.ROOM_723) tmp_roomNum = "723";
                        else if (nowRoomNum == RoomNum.ROOM_724) tmp_roomNum = "724";
                        subtitleTrigger.ShowOneSentence(tmp_roomNum + "호 비밀번호의 재설정이 완료되었습니다.");
                        sys_passwordManager.nowState = ChangePasswordManager.State.NotWorking;
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
        Cursor.SetCursor(blackcursor, new Vector2(blackcursor.width / 2, blackcursor.height / 2), CursorMode.ForceSoftware);
    }
}
