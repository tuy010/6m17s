using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialQuest : MonoBehaviour
{
    public enum Steps
    {
        init,
        clickTap,
        clickTapSec,
        holdCude,
        rotateCude,
        clear
    }public Steps nowStep;

    [SerializeField] private GameObject sysObj;
    [SerializeField] private GameObject wakCamera;
    [SerializeField] private GameObject questUI;
    [SerializeField] private TextMeshProUGUI UIText;

    private float timer;
    AudioSource AS;

    [SerializeField] private AudioClip sounds;
    [SerializeField] private float questVolume = 0.25f;

    // Start is called before the first frame update
    void Start()
    {
        nowStep = Steps.init;
        questUI.SetActive(false);
        timer = 0;
        AS = wakCamera.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.tutorial == true)
        {
            switch (nowStep)
            {
                case Steps.init:
                    StartTutorial();
                    break;
                case Steps.clickTap:
                    if (Input.GetKeyDown(KeyCode.Tab))
                    {
                        nowStep = Steps.clickTapSec;
                        UIText.text = "Tab 키를 눌러\n시점 변경하기 (2/2)";
                    }
                    break;
                case Steps.clickTapSec:
                    if (Input.GetKeyDown(KeyCode.Tab))
                    {
                        nowStep = Steps.holdCude;
                        UIText.text = "로비로 이동해\n매력큐브 잡기 (좌클릭 꾹)";

                        this.GetComponent<AudioSource>().clip = sounds;
                        this.GetComponent<AudioSource>().volume = questVolume;
                        this.GetComponent<AudioSource>().Play();
                    }
                    break;
                case Steps.holdCude:
                    if (wakCamera.transform.childCount > 0)
                    {
                        nowStep = Steps.rotateCude;
                        UIText.text = "매력큐브 살펴보기 (우클릭 꾹)";
                        timer = 0;

                        this.GetComponent<AudioSource>().clip = sounds;
                        this.GetComponent<AudioSource>().volume = questVolume;
                        this.GetComponent<AudioSource>().Play();
                    }
                    break;
                case Steps.rotateCude:
                    if (wakCamera.transform.childCount < 1)
                    {
                        nowStep = Steps.holdCude;
                        UIText.text = "매력큐브 잡기 (좌클릭 꾹)";
                        timer = 0;
                    }
                    else if (timer > 1)
                    {
                        nowStep = Steps.clear;
                        UIText.text = "";
                        timer = 0;

                        this.GetComponent<AudioSource>().clip = sounds;
                        this.GetComponent<AudioSource>().volume = questVolume;
                        this.GetComponent<AudioSource>().Play();
                    }
                    else if (wakCamera.transform.childCount > 0 && Input.GetMouseButton(1))
                    {
                        timer += Time.deltaTime;
                    }
                    break;
                case Steps.clear:
                    if (timer < 3)
                    {
                        timer += Time.deltaTime;
                    }
                    else
                    {
                        questUI.SetActive(false);
                        GameManager.tutorial = false;
                        AS.Play();
                    }
                    break;
                default:
                    break;

            }
        }
        else
        {
            //this.GetComponent<TutorialQuest>().enabled = false;
        }
    }

    void StartTutorial()
    {
        nowStep = Steps.clickTap;
        questUI.SetActive(true);
        UIText.text = "Tab 키를 눌러\n시점 변경하기 (1/2)";    
    }
}
