using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class messiMove : MonoBehaviour
{
    Vector3 mousePos, transPos, WorldMouse;
    static public Vector3 turnPos;
    private float DelayTime;
    private float PosGap;
    private float vecAngle;
    private float dot;
    //[SerializeField] private GameObject wakHead;
    [SerializeField] private GameObject system;
    
    public GameObject messiLeck;
    public GameObject messiTail;
    private CameraCon cameraCon;
    private Vector3 nowPos;
    private Vector3 subVec;
    private Vector3 tmpVec;
    private Vector3 angleVec;
    private Vector3 LeckTailVec;
    private Vector3 transTailVec;

    public bool messiinBath = false;
    public bool messiinRoom = false;
    private Animator animator;
    private messiCursor mc;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        mc = GetComponent<messiCursor>();
    }

    void Start()
    {
        transPos = transform.position;
        turnPos = Vector3.zero;
        cameraCon = system.GetComponent<CameraCon>();
        animator.SetBool("isWalk", false);
    }

    // Update is called once per frame
    void Update()
    {
        nowPos = this.transform.position;
        // if (PosGap > 5f)
        // {
        //     animator.SetBool("isWalk", true);
        // }
        // else
        // {
        //     animator.SetBool("isWalk", false);
        // }
        DelayTime += Time.deltaTime;
        if ((Input.GetMouseButtonDown(0)) && (cameraCon.nowCam == CameraCon.CamType.messi) && (DelayTime > 0.2f) && (messiCursor.canClick == true))
        {
            TargetPos();
            DelayTime = 0;
        }
        MoveToTarget();
        //float diff = transform.rotation.eulerAngles.y - Quaternion.LookRotation(subVec).normalized.eulerAngles.y;
        // if (Mathf.Abs(diff) <= 30)
        // {
        //     animator.SetBool("is90turnright", false);
        //     animator.SetBool("is90turnleft", false);
        // }
    }

    void TargetPos()
    {
        LeckTailVec = messiLeck.transform.position - messiTail.transform.position;
        turnPos = transform.position;
        mousePos = Input.mousePosition;
        WorldMouse = new Vector3(mousePos.x, mousePos.y, 230f);
        transPos = Camera.main.ScreenToWorldPoint(WorldMouse);
        subVec = transPos - transform.position;
        //vecAngle = Mathf.Atan2(angleVec.y, angleVec.x) * Mathf.Rad2Deg;
        //vecAngle = Vector3.SignedAngle(LeckTailVec, transTailVec, transform.forward);
        //Debug.Log(r);
        //Debug.Log(transform.rotation.eulerAngles);
    }

    void MoveToTarget()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(subVec).normalized, Time.deltaTime * 7f);
        
        //Debug.Log(transform.rotation.eulerAngles + "----" + Quaternion.LookRotation(subVec).normalized.eulerAngles);
        PosGap = Vector3.Distance(transform.position, transPos);

        if (PosGap > 5f)
        {
            animator.SetBool("isWalk", true);
            // animator.SetBool("isWalk", true);
            var dir = transPos - transform.position;
            transform.position += dir.normalized * Time.deltaTime * 50f;
            // animator.SetBool("is90turnright", false);
            // animator.SetBool("is90turnleft", false);
            
        }
        else
        {
            animator.SetBool("isWalk", false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("T_bathroom"))
        {
            if (cameraCon.nowCam == CameraCon.CamType.wak && messiinBath == false) messiinBath = true;

            else if (cameraCon.nowCam == CameraCon.CamType.wak && messiinBath == true) messiinBath = false;

            else if (cameraCon.nowCam == CameraCon.CamType.messi && messiinBath == false && (messiMove.turnPos.z < nowPos.z)) //&& (messiMove.lastPos.z < nowPos.z) && )
            {
                cameraCon.ShowBathCam();
                messiinBath = true;
            }

            else if (cameraCon.nowCam == CameraCon.CamType.messi && messiinBath == true && (messiMove.turnPos.z > nowPos.z)) // && (messiMove.lastPos.z > nowPos.z) && )
            {
                cameraCon.ShowMainCam();
                messiinBath = false;
            }
        }

        else if (other.gameObject.CompareTag("T_room"))
        {
            if (cameraCon.nowCam == CameraCon.CamType.wak && messiinRoom == false) messiinRoom = true;

            else if (cameraCon.nowCam == CameraCon.CamType.wak && messiinRoom == true) messiinRoom = false;

            else if (cameraCon.nowCam == CameraCon.CamType.messi && messiinRoom == false && (messiMove.turnPos.x < nowPos.x)) // (messiMove.lastPos.x < nowPos.x) && )
            {
                cameraCon.ShowRoomCam();
                messiinRoom = true;
            }

            else if (cameraCon.nowCam == CameraCon.CamType.messi && messiinRoom == true && (messiMove.turnPos.x > nowPos.x)) //(messiMove.lastPos.x > nowPos.x) && )
            {
                cameraCon.ShowMainCam();
                messiinRoom = false;
            }
        }
    }
}