using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class EndingProduction : MonoBehaviour
{
    [SerializeField] GameObject sys;

    [SerializeField] GameObject messi;
    [SerializeField] GameObject wakgood;
    [SerializeField] GameObject realwakgood;
    private Animator animator;
    AudioSource AS;

    [SerializeField] Camera wakCam;
    [SerializeField] AudioClip dub2;
    [SerializeField] SubtitleTrigger dev;
    [SerializeField] SubtitleTrigger doc;

    float time;
    float time2;
    float time3;
    float time4;
    float time5;
    float time6;
    float time7;
    bool go = false;
    bool go2 = false;
    bool go3 = false;

    [SerializeField] GameObject endingTextUI;
    [SerializeField] Text end1;
    [SerializeField] Text end2;
    float a = 0f;
    float a2 = 0f;
    float a3 = 0f;
    float a4 = 1f;
    float a5 = 1f;

    [SerializeField] CheckTime checkTime;

    [SerializeField] GameObject tui;
    [SerializeField] Text ttry;

    [SerializeField] CameraCon camHandler;
    [SerializeField] endingButton endingButtonfunc;

    [SerializeField] Image fade;

    [SerializeField] GameObject cursorUI;
    [SerializeField] RawImage c;

    [SerializeField] Light main;
    [SerializeField] Light forMessi;
    [SerializeField] Light bath;

    // Start is called before the first frame update
    void Start()
    {
        if (messi != null && realwakgood != null && wakCam != null)
        {
            animator = messi.GetComponent<Animator>();
            AS = wakCam.GetComponent<AudioSource>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (go)
        {
            time += Time.deltaTime;
            if (time > 1.75f)
            {
                realwakgood.GetComponent<RotateToMoues>().enabled = false;
                realwakgood.GetComponent<WakMove>().enabled = false;
                realwakgood.GetComponent<WakController>().enabled = false;
                sys.GetComponent<CameraCon>().enabled = false;
                Cursor.visible = false;
                if (dub2 != null)
                {
                    AS.clip = dub2;
                    AS.Play();
                    dev.TriggerSubtitle();
                    doc.TriggerSubtitle();

                }
                go2 = true;
                go = false;
            }
        }
        if (go2)
        {
            time2 += Time.deltaTime;
            Debug.Log(end1.color);
            if (time2 > 44f)
            {
                time7 += Time.deltaTime;
                if (a5 > 0.0f && time7 >= 0.1f)
                {
                    a5 -= 0.1f;
                    end2.color = new Color(255, 255, 255, a5);
                    time7 = 0;
                }
                if (a5 < 0)
                {
                    go2 = false;
                    go3 = true;
                }
            }
            else if (time2 > 34f)
            {
                time6 += Time.deltaTime;
                if (a3 < 1.0f && time6 >= 0.1f)
                {
                    a3 += 0.1f;
                    end2.color = new Color(255, 255, 255, a3);
                    time6 = 0;
                }
            }
            else if (time2 > 32f)
            {
                time5 += Time.deltaTime;
                if (a4 > 0.0f && time5 >= 0.1f)
                {
                    a4 -= 0.1f;
                    end1.color = new Color(255, 255, 255, a4);
                    time5 = 0;
                }
            }
            else if (time2 > 22f)
            {
                time4 += Time.deltaTime;
                if (a2 < 1.0f && time4 >= 0.1f)
                {
                    a2 += 0.1f;
                    end1.color = new Color(255, 255, 255, a2);
                    time4 = 0;
                }
            }
            else if (time2 > 18.5f)
            {
                tui.SetActive(false);
                endingTextUI.SetActive(true);
                time3 += Time.deltaTime;
                if (a < 1.0f && time3 >= 0.1f)
                {
                    a += 0.05f;
                    fade.color = new Color(0, 0, 0, a);
                    time3 = 0;
                }
            }
            else if (time2 > 15.5f)
            {
                ttry.text = "Æ®¶óÀÌ È½¼ö : "+GameManager.gameTry.ToString();
                tui.SetActive(true);
            }
        }
        if (go3)
        {
            camHandler.EndCameraSet();
            if (endingTextUI.activeSelf == true) endingTextUI.SetActive(false);
            endingButtonfunc.ShowEndingButton();
            go3 = false;
        }
    }

    public void StartEnding()
    {
        if (messi != null)
        {
            c.color = new Color(0, 0, 0, 0);
            c.enabled = false;
            if (main != null && forMessi != null && bath != null && realwakgood != null && wakCam != null && checkTime != null && tui != null)
            {
                AS.Stop();
                checkTime.enabled = false;
                main.enabled = false;
                bath.enabled = false;
                //c.color = new Color (0, 0, 0, 0);

                cursorUI.SetActive(false);
                forMessi.GetComponent<Light>().enabled = true;
            }
            messi.GetComponent<messiMove>().enabled = false;
            messi.transform.position = new Vector3(340.2f, 20.18f, -1109.19f);
            animator.SetBool("isEnd", true);
            if (wakgood != null && realwakgood != null)
            {
                go = true;
                messi.transform.LookAt(wakgood.transform);
            }
        }
    }
}
