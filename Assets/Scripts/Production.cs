using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Production : MonoBehaviour
{
    public Camera clockCam;
    public Camera fireCam;
    public Camera cardCam;
    public Camera wakCam;
    public Camera MainCam;
    [SerializeField] Camera realWakCam;

    AudioSource ASCC;
    public AudioClip ccs;
    public AudioClip dub1;
    [SerializeField] private ProductionSubtitle productionSubtitle;

    private bool gofireCam = false;
    private bool gocardCam = false;
    private bool gocwakCam = false;
    private bool audioplayed1 = false;
    private bool audioplayed2 = false;
    private float time = 0;
    private bool skip = false;


    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.production == true)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            
            this.ASCC = clockCam.GetComponent<AudioSource>();
            clockCam.GetComponent<AudioListener>().enabled = true;
            fireCam.GetComponent<AudioListener>().enabled = false;
            cardCam.GetComponent<AudioListener>().enabled = false;
            wakCam.GetComponent<AudioListener>().enabled = false;
            realWakCam.GetComponent<AudioListener>().enabled = false;
            clockCam.depth = 3;
            GameManager.tutorial = false;
        }
        else
        {
            clockCam.enabled = false;
            fireCam.enabled = false;
            cardCam.enabled = false;
            wakCam.enabled = false;
            clockCam.GetComponent<AudioListener>().enabled = false;
            fireCam.GetComponent<AudioListener>().enabled = false;
            cardCam.GetComponent<AudioListener>().enabled = false;
            wakCam.GetComponent<AudioListener>().enabled = false;
            realWakCam.GetComponent<AudioListener>().enabled = true;
            clockCam.depth = -9;
            clockCam.depth = -9;
            fireCam.depth = -9;
            cardCam.depth = -9;
            wakCam.depth = -9;
            MainCam.depth = -9;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.production == true)
        {
            time += Time.deltaTime;
            if (Clock_Digital.end617 == true && gofireCam == false && skip == false)
            {
                //playAudio();
                Debug.Log("1111111111");
                if (audioplayed1 == false)
                {
                    ASCC.clip = ccs;
                    ASCC.Play();
                    audioplayed1 = true;
                }

                if (time > 23f)
                {
                    fireCam.depth = 3;
                    clockCam.depth = -1;
                    if (audioplayed2 == false)
                    {
                        ASCC.Stop();
                        ASCC.clip = dub1;
                        productionSubtitle.StartProductionSubtitles();
                        ASCC.Play();
                        audioplayed2 = true;
                    }
                    gofireCam = true;
                    skip = true;
                    time = 0;
                }
            }
            else if (gofireCam == true && gocardCam == false && gocwakCam == false)
            {
                Debug.Log("22222222");
                if (time > 18f)
                {
                    cardCam.depth = 4;
                    fireCam.depth = -1;
                    gofireCam = false;
                    gocardCam = true;
                    time = 0;
                }
            }
            else if (gocardCam == true && gocwakCam == false)
            {
                Debug.Log("3333333333");
                if (time > 1.5f)
                {
                    wakCam.depth = 5;
                    cardCam.depth = -1;
                    gocardCam = false;
                    gocwakCam = true;
                    time = 0;
                }
            }
            else if (gocwakCam == true)
            {
                Debug.Log("44444444444");
                if (time > 24f)
                {
                    wakCam.depth = -1;
                    cardCam.depth = -2;
                    fireCam.depth = -3;
                    clockCam.depth = -4;
                    gocwakCam = false;

                    clockCam.enabled = false;
                    fireCam.enabled = false;
                    cardCam.enabled = false;
                    wakCam.enabled = false;
                    clockCam.GetComponent<AudioListener>().enabled = false;
                    fireCam.GetComponent<AudioListener>().enabled = false;
                    cardCam.GetComponent<AudioListener>().enabled = false;
                    wakCam.GetComponent<AudioListener>().enabled = false;
                    realWakCam.GetComponent<AudioListener>().enabled = true;
                    GameManager.production = false;
                    GameManager.tutorial = true;
                }
            }
        }

    }
}
