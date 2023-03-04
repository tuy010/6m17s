using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DigitalClock_724 : MonoBehaviour {
    public enum RoomNums
    {
        ROOM723,
        ROOM724
    }
    public Material blackmat;
    public GameObject hamburger = null;
    
    //[SerializeField] private Light[] Lights = new Light[8];
    //public Camera WakCam;
    //public Camera downCam;
    //public Camera wakpushCam;
    //public GameObject door;
   // public GameObject door2;
   // private bool CamMove = false;
   // private float time;

    //-------------------------------------------------
    [SerializeField] CheckTime checkTime;
    //public float speed = 1.0f; 
    public int sec;      // start Time
    public int minute;        // start Time
    //-------------------------------------------------
    Renderer objRenderer; 
    //-------------------------------------------------
    //float delay;
    //-------------------------------------------------
    [SerializeField]
    private GameObject sysObj;
    private ChangePasswordManager sys_passwordManager;
    [SerializeField]
    private RoomNums roomNum;
    //public GameObject[] fire;
    //[SerializeField] private GameObject[] smokes;
    //[SerializeField] private bool is724 = false;

//----------------------------------------------------------------------------------------------------------------------------------------------
//----------------------------------------------------------------------------------------------------------------------------------------------
//----------------------------------------------------------------------------------------------------------------------------------------------
    void Start()
    {
        objRenderer = GetComponent<Renderer>();
        objRenderer.materials[5].SetTextureOffset("_MainTex", new Vector2(0.0f, 0.9f)); // 가운데 " : " 깜빡임 키기
        objRenderer.materials[5].SetTextureOffset("_EmissionMap", new Vector2(0.0f, 0.9f));
        if(sysObj == null)
        {
            sysObj = GameObject.Find("System");
        }
        if(sys_passwordManager==null) sys_passwordManager = sysObj.GetComponent<ChangePasswordManager>();
        if (checkTime == null) checkTime = sysObj.GetComponent<CheckTime>();
    }
    //----------------------------------------------------------------------------------------------------------------------------------------------
    //----------------------------------------------------------------------------------------------------------------------------------------------
    //----------------------------------------------------------------------------------------------------------------------------------------------
    void Update()
    {
        if (roomNum == RoomNums.ROOM723 && sys_passwordManager.nowState == ChangePasswordManager.State.ChangePassword_723 && sys_passwordManager.is723Changed)
        {
            showPassword(sys_passwordManager.password723);
        }
        else if (roomNum == RoomNums.ROOM724 && sys_passwordManager.nowState == ChangePasswordManager.State.ChangePassword_724 && sys_passwordManager.is724Changed)
        {
            showPassword(sys_passwordManager.password724);
        }
        else
        {
            minute = checkTime.minute;
            sec = checkTime.sec;

            float offset = 0.0f - 0.1f * (float)(minute % 10);
            objRenderer.materials[4].SetTextureOffset("_MainTex", new Vector2(0.0f, offset));
            objRenderer.materials[4].SetTextureOffset("_EmissionMap", new Vector2(0.0f, offset));

            // Minute 10er
            offset = 0.0f - 0.1f * (float)((minute / 10) % 10);
            objRenderer.materials[3].SetTextureOffset("_MainTex", new Vector2(0.0f, offset));
            objRenderer.materials[3].SetTextureOffset("_EmissionMap", new Vector2(0.0f, offset));

            // 3시
            objRenderer.materials[1].SetTextureOffset("_MainTex", new Vector2(0.0f, -0.3f));
            objRenderer.materials[1].SetTextureOffset("_EmissionMap", new Vector2(0.0f, -0.3f));

            // 20시
            objRenderer.materials[2].SetTextureOffset("_MainTex", new Vector2(0.0f, -0.2f));
            objRenderer.materials[2].SetTextureOffset("_EmissionMap", new Vector2(0.0f, -0.2f));
        }
        //--------------------------------------------------------------------------------------------------------------------------
        //delay -= Time.deltaTime * speed;
        //if(delay < 0.0f)
        //{ 
        //    delay = 1.0f;
        //    if(!GameManager.tutorial&& !GameManager.production)
        //    {
        //        sec ++;
        //        if (sec >= 60)
        //        {
        //            sec = 0;
        //            minute++;
        //        }
        //    }
        //    if(is724)
        //    {
        //        if (minute > 0 && sec > 8)
        //        {
        //            fire[0].SetActive(true);
        //            smokes[0].SetActive(true);
        //        }
        //        if (minute > 1 && sec > 1)
        //        {
        //            Material[] mat = hamburger.GetComponent<Renderer>().materials;
        //            for (int i = 0; i < 6; i++)
        //            {
        //                mat[i] = blackmat;
        //            }
        //            hamburger.GetComponent<Renderer>().materials = mat;
        //            fire[0].SetActive(false);
        //            fire[1].SetActive(true);
        //            smokes[0].SetActive(false);
        //            smokes[1].SetActive(true);
        //            smokes[2].SetActive(true);

        //        }
        //        if (minute > 1 && sec > 55)
        //        {
        //            fire[2].SetActive(true);
        //        }
        //        if (minute > 2 && sec > 49)
        //        {
        //            fire[3].SetActive(true);
        //        }
        //        if (minute > 3 && sec > 43)
        //        {
        //            fire[4].SetActive(true);
        //        }
        //        if (minute > 4 && sec > 25)
        //        {
        //            fire[5].SetActive(true);
        //        }
        //        if (minute > 1 && smokes[2].transform.position.x < 100f)
        //        {
        //            smokes[2].transform.position += new Vector3(2, 0, 0);
        //        }
        //    }
        //}
        //if(minute > 5 && sec > 13)
        //{
        //    door2.transform.rotation = Quaternion.Euler(0, 0, 0);
        //    for (int i = 0; i < 8; i++)
        //    {
        //        Lights[i].enabled = false;
        //    }
        //    time += Time.deltaTime;
        //    wakpushCam.depth = 10;
        //    if (time > 0.5f && CamMove == false)
        //    {
        //        Quaternion targetRotation2 = Quaternion.Euler(0, 90f, 0);
        //        door.transform.localRotation = Quaternion.Slerp(door.transform.localRotation, targetRotation2, 5f * Time.deltaTime);
        //    }
        //    if (time > 0.70f && CamMove == false)
        //    {
        //        wakpushCam.transform.position = Vector3.MoveTowards(wakpushCam.transform.position, downCam.transform.position, Time.deltaTime * 70);
        //        wakpushCam.transform.rotation = Quaternion.RotateTowards(wakpushCam.transform.rotation, downCam.transform.rotation, Time.deltaTime * 70);
        //        if (wakpushCam.transform.position == downCam.transform.position && wakpushCam.transform.rotation == downCam.transform.rotation) CamMove = true;
        //    }
        //    if (CamMove == true)
        //    {
        //        wakpushCam.transform.position = Vector3.MoveTowards(wakpushCam.transform.position, WakCam.transform.position, Time.deltaTime * 70);
        //        wakpushCam.transform.rotation = Quaternion.RotateTowards(wakpushCam.transform.rotation, WakCam.transform.rotation, Time.deltaTime * 70);
        //    }
        //    if (time > 1.25f && CamMove == true)
        //    {
        //        GameManager.gameTry += 1;
        //        SceneManager.LoadScene(0);
        //    }
        //}
        //--------------------------------------------------------------------------------------------------------------------------

        // // Minute 1er 1초
        // float offset = 0.0f - 0.1f * (float)(minute % 10);
        // objRenderer.materials[4].SetTextureOffset("_MainTex",     new Vector2(0.0f, offset));
        // objRenderer.materials[4].SetTextureOffset("_EmissionMap", new Vector2(0.0f, offset));

        // // Minute 10er 10초
        // offset = 0.0f - 0.1f * (float)((minute / 10) % 10);
        // objRenderer.materials[3].SetTextureOffset("_MainTex",     new Vector2(0.0f, offset));
        // objRenderer.materials[3].SetTextureOffset("_EmissionMap", new Vector2(0.0f, offset));

        // // Hour 1er 1분
        // offset = 0.0f - 0.1f * (float)(hour % 10);
        // objRenderer.materials[1].SetTextureOffset("_MainTex",     new Vector2(0.0f, offset));
        // objRenderer.materials[1].SetTextureOffset("_EmissionMap", new Vector2(0.0f, offset));

        // // Hour 10er 10분
        // offset = 0.0f - 0.1f * (float)((hour / 10) % 10);
        // objRenderer.materials[2].SetTextureOffset("_MainTex",     new Vector2(0.0f, offset));
        // objRenderer.materials[2].SetTextureOffset("_EmissionMap", new Vector2(0.0f, offset));  
    }

    private void showPassword(int password)
    {
        //2
        float offset = 0.0f - 0.1f * (float)(((password / 100) % 10));
        objRenderer.materials[1].SetTextureOffset("_MainTex", new Vector2(0.0f, offset));
        objRenderer.materials[1].SetTextureOffset("_EmissionMap", new Vector2(0.0f, offset));

        //1
        offset = 0.0f - 0.1f * (float)((password / 1000) % 10);
        objRenderer.materials[2].SetTextureOffset("_MainTex", new Vector2(0.0f, offset));
        objRenderer.materials[2].SetTextureOffset("_EmissionMap", new Vector2(0.0f, offset));

        //4
        offset = 0.0f - 0.1f * (float)(password % 10);
        objRenderer.materials[4].SetTextureOffset("_MainTex", new Vector2(0.0f, offset));
        objRenderer.materials[4].SetTextureOffset("_EmissionMap", new Vector2(0.0f, offset));

        //3
        offset = 0.0f - 0.1f * (float)((password / 10) % 10);
        objRenderer.materials[3].SetTextureOffset("_MainTex", new Vector2(0.0f, offset));
        objRenderer.materials[3].SetTextureOffset("_EmissionMap", new Vector2(0.0f, offset));
    }
}
