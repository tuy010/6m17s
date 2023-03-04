using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SmokeAndRestart : MonoBehaviour
{
    enum Steps
    {
        Phase1,
        Phase2,
        Phase3,
        Phase4,
        Phase5,
        Phase6,
        Phase7,
        Phase8
    }
    [SerializeField] Steps nowStep;
    [SerializeField] CheckTime checkTime;

    public GameObject[] fire;
    [SerializeField] private GameObject[] smokes;
    public Material blackmat;
    public GameObject hamburger = null;

    [SerializeField] private CameraCon cameraCon;
    private bool isParticleOn = false;

    public Camera WakCam;
    public Camera wakpushCam;
    public Camera downCam;
    public GameObject door;
    public GameObject door2;
    private bool CamMove = false;
    private float time;


    private void Start()
    {
        WakCam.enabled = false;
        wakpushCam.enabled = false;
        downCam.enabled = false;

        foreach (GameObject f in fire) f.SetActive(false);
    }
    void Update()
    {
        if(cameraCon.nowCam == CameraCon.CamType.messi && !isParticleOn)
        {
            isParticleOn = true;
            ReLoadParticle();
        }
        else if(cameraCon.nowCam != CameraCon.CamType.messi && isParticleOn)
        {
            isParticleOn = false;
            ReLoadParticle();
        }
        switch(nowStep)
        {
            case Steps.Phase1:
                if (checkTime.minute > 0 && checkTime.sec > 8)
                {
                    fire[0].SetActive(true);
                    smokes[0].SetActive(true);
                    nowStep = Steps.Phase2;
                    ReLoadParticle();
                }
                break;
            case Steps.Phase2:
                if (checkTime.minute > 1 && checkTime.sec > 1)
                {
                    Material[] mat = hamburger.GetComponent<Renderer>().materials;
                    for (int i = 0; i < 6; i++)
                    {
                        mat[i] = blackmat;
                    }
                    hamburger.GetComponent<Renderer>().materials = mat;
                    fire[0].SetActive(false);
                    fire[1].SetActive(true);
                    smokes[0].SetActive(false);
                    smokes[1].SetActive(true);
                    smokes[2].SetActive(true);
                    nowStep= Steps.Phase3;
                    ReLoadParticle();
                }
                break;
            case Steps.Phase3:
                if (checkTime.minute > 1 && checkTime.sec > 55)
                {
                    fire[2].SetActive(true);
                    nowStep = Steps.Phase4;
                    ReLoadParticle();
                }
                break;
            case Steps.Phase4:
                if (checkTime.minute > 2 && checkTime.sec > 49)
                {
                    fire[3].SetActive(true);
                    nowStep = Steps.Phase5;
                    ReLoadParticle();
                }
                break;
            case Steps.Phase5:
                if (checkTime.minute > 3 && checkTime.sec > 43)
                {
                    fire[4].SetActive(true);
                    nowStep = Steps.Phase6;
                    ReLoadParticle();
                }
                break;
            case Steps.Phase6:
                if (checkTime.minute > 4 && checkTime.sec > 25)
                {
                    fire[5].SetActive(true);
                    nowStep = Steps.Phase7;
                    ReLoadParticle();
                }
                break;
            case Steps.Phase7:
                if (checkTime.minute > 5 && checkTime.sec > 13)
                {
                    WakCam.enabled = true;
                    wakpushCam.enabled = true;
                    downCam.enabled = true;
                    door2.transform.rotation = Quaternion.Euler(0, 0, 0);
                    for (int i = 0; i < 8; i++)
                    {
                       //Lights[i].enabled = false;
                    }
                    time += Time.deltaTime;
                    wakpushCam.depth = 10;
                    if (time > 0.5f && CamMove == false)
                    {
                        Quaternion targetRotation2 = Quaternion.Euler(0, 90f, 0);
                        door.transform.localRotation = Quaternion.Slerp(door.transform.localRotation, targetRotation2, 5f * Time.deltaTime);
                    }
                    if (time > 0.70f && CamMove == false)
                    {
                        wakpushCam.transform.position = Vector3.MoveTowards(wakpushCam.transform.position, downCam.transform.position, Time.deltaTime * 70);
                        wakpushCam.transform.rotation = Quaternion.RotateTowards(wakpushCam.transform.rotation, downCam.transform.rotation, Time.deltaTime * 70);
                        if (wakpushCam.transform.position == downCam.transform.position && wakpushCam.transform.rotation == downCam.transform.rotation) CamMove = true;
                    }
                    if (CamMove == true)
                    {
                        wakpushCam.transform.position = Vector3.MoveTowards(wakpushCam.transform.position, WakCam.transform.position, Time.deltaTime * 70);
                        wakpushCam.transform.rotation = Quaternion.RotateTowards(wakpushCam.transform.rotation, WakCam.transform.rotation, Time.deltaTime * 70);
                    }
                    if (time > 1.25f && CamMove == true)
                    {
                        nowStep = Steps.Phase8;
                        GameManager.gameTry += 1;
                        SceneManager.LoadScene(0);
                    }
                }
                break;
            default:
                break;
        }
        if (nowStep >= Steps.Phase2 && checkTime.minute > 1 && smokes[2].transform.position.x < 100f)
        {
            smokes[2].transform.position += new Vector3(2*Time.deltaTime, 0, 0);
        }
    }

    private void ReLoadParticle()
    {
        if (!isParticleOn)
        {
            if (nowStep > Steps.Phase1)
            {
                if (fire[0].activeSelf)
                {
                    fire[0].GetComponent<ParticleSystem>().Stop();
                    fire[0].GetComponent<ParticleSystem>().Clear();
                }
                if (smokes[0].activeSelf)
                {
                    smokes[0].GetComponent<ParticleSystem>().Stop();
                    smokes[0].GetComponent<ParticleSystem>().Clear();
                }
                if (nowStep > Steps.Phase2)
                {
                    fire[1].GetComponent<ParticleSystem>().Stop();
                    fire[1].GetComponent<ParticleSystem>().Clear();
                    smokes[1].GetComponent<ParticleSystem>().Stop();
                    smokes[1].GetComponent<ParticleSystem>().Clear();
                    ParticleSystem[] tmp = smokes[2].GetComponentsInChildren<ParticleSystem>();
                    foreach (ParticleSystem p in tmp)
                    {
                        p.GetComponent<ParticleSystem>().Stop();
                        p.GetComponent<ParticleSystem>().Clear();
                    }

                    if (nowStep > Steps.Phase3)
                    {
                        fire[2].GetComponent<ParticleSystem>().Stop();
                        fire[2].GetComponent<ParticleSystem>().Clear();

                        if (nowStep > Steps.Phase4)
                        {
                            fire[3].GetComponent<ParticleSystem>().Stop();
                            fire[3].GetComponent<ParticleSystem>().Clear();

                            if (nowStep > Steps.Phase5)
                            {
                                fire[4].GetComponent<ParticleSystem>().Stop();
                                fire[4].GetComponent<ParticleSystem>().Clear();

                                if (nowStep > Steps.Phase6){
                                    fire[5].GetComponent<ParticleSystem>().Stop();
                                    fire[5].GetComponent<ParticleSystem>().Clear();
                                }
                            }
                        }
                    }
                }
            }
        }
        else
        {
            if (nowStep > Steps.Phase1)
            {
                if (fire[0].activeSelf) fire[0].GetComponent<ParticleSystem>().Play();
                if (smokes[0].activeSelf) smokes[0].GetComponent<ParticleSystem>().Play();
                if (nowStep > Steps.Phase2)
                {
                    fire[1].GetComponent<ParticleSystem>().Play();
                    smokes[1].GetComponent<ParticleSystem>().Play();
                    ParticleSystem[] tmp = smokes[2].GetComponentsInChildren<ParticleSystem>();
                    foreach (ParticleSystem p in tmp)
                    {
                        p.GetComponent<ParticleSystem>().Play();
                    }

                    if (nowStep > Steps.Phase3)
                    {
                        fire[2].GetComponent<ParticleSystem>().Play();

                        if (nowStep > Steps.Phase4)
                        {
                            fire[3].GetComponent<ParticleSystem>().Play();

                            if (nowStep > Steps.Phase5)
                            {
                                fire[4].GetComponent<ParticleSystem>().Play();

                                if (nowStep > Steps.Phase6)
                                {
                                    fire[5].GetComponent<ParticleSystem>().Play();
                                }
                            }
                        }
                    }
                }
            }
        }


    }
}
