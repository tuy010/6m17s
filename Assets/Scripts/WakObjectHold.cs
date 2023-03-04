using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WakObjectHold : MonoBehaviour
{
    [SerializeField] private GameObject system;
    private CameraCon cameraCon;
    [SerializeField] private GameObject interactExplanationUI;
    [SerializeField] private TextMeshProUGUI interactExplanationText;
    GameObject Object = null;
    GameObject beforeObject = null;
    GameObject tempObject = null; //Object = null에 대한 예외처리
    GameObject[] holdableObj;
    [SerializeField] GameObject interactionObject = null;
    Rigidbody ObjectRigid;
    Rigidbody holdableRigid;
    public Transform PlayerTransform;
    public Camera Cam;
    Vector3 CamRay;
    int layerMask = 1 << 7;
    int layerMaskForInteraction = 1 << 8;
    [SerializeField] bool isPickup = false;
    int countChild = 0;
    [SerializeField] bool ishit = false;
    public Text door_open_text;

    private float rayLength = 70f;
    private float spinPower = 70f;
    AudioSource AS;


    [SerializeField] InteractionTrigger IT;

    void Start()
    {
        if(!GameManager.tutorial&& !GameManager.production)
        {
            AS = Cam.GetComponent<AudioSource>();
            AS.Play();
        }
        cameraCon = system.GetComponent<CameraCon>();
        holdableObj = GameObject.FindGameObjectsWithTag("holdable");
    }

    void Update()
    {
        if(cameraCon.nowCam == CameraCon.CamType.wak)
        {
            CamRay = Cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f));

            if (isPickup == false)
            {
                RayObjectCheck();
            }

            if (Input.GetMouseButton(0))
            {
                PickUp();
                if (Input.GetMouseButton(1))
                {
                    Spin();
                }
            }

            else if (Input.GetKeyDown(KeyCode.E))
            {
                Interaction();
            }

            if (Input.GetMouseButtonUp(0))
            {
                Drop();
            }
        }
        else
        {
            Object = null;
            interactExplanationUI.SetActive(false);
        }
    }

    void RayObjectCheck()
    {
        RaycastHit hit;
       //Debug.DrawRay(CamRay, transform.forward * rayLength, Color.red, 0.3f);

        if (Physics.Raycast(CamRay, Cam.transform.forward, out hit, rayLength, layerMask))
        {
            if (Object == null)
            {
                Object = hit.collider.gameObject;
                ObjectRigid = Object.GetComponent<Rigidbody>();
                this.GetComponent<DrawOutline>().VisibleOutline(Object);
                tempObject = Object;
                beforeObject = Object;
            }
            else
            {
                Object = hit.collider.gameObject;
                ObjectRigid = Object.GetComponent<Rigidbody>();
                tempObject = Object;
            }
            
            if (beforeObject != Object)
            {
                this.GetComponent<DrawOutline>().VisibleOutline(Object);
                this.GetComponent<DrawOutline>().InvisibleOutline(beforeObject);
                tempObject = Object;
                beforeObject = Object;
            }
            ishit = true;
        }
        else
        {
            ishit = false;
            if (Object != null && beforeObject != null)
            {
                this.GetComponent<DrawOutline>().InvisibleOutline(Object);
                this.GetComponent<DrawOutline>().InvisibleOutline(beforeObject);
                tempObject = Object;
                Object = null;
            }
        }
        if(!isPickup&&!ishit&& cameraCon.nowCam == CameraCon.CamType.wak)
        {
            if(Physics.Raycast(CamRay, Cam.transform.forward, out hit, rayLength, layerMaskForInteraction))
            {
                interactionObject = hit.collider.gameObject;
                IT = interactionObject.GetComponent<InteractionTrigger>();
                if(IT.isWorking)
                {
                    interactionObject = null;
                    interactExplanationUI.SetActive(false);
                }
                else
                {
                    interactExplanationUI.SetActive(true);
                    interactExplanationText.text = IT.explanation;
                }
            }
            else
            {
                interactionObject = null;
                interactExplanationUI.SetActive(false);
                IT = null;
            }
        }
        else if (cameraCon.nowCam != CameraCon.CamType.wak)
        {
            interactExplanationUI.SetActive(false);
        }
    }

    void PickUp()
    {
        //PlayerTransform.DetachChildren();
        if (Object != null && ishit == true)
        {
            countChild = PlayerTransform.childCount;
            
            if (countChild == 0)
            {
                Object.transform.SetParent(PlayerTransform);
                ObjectRigid.isKinematic = true;
                isPickup = true;
            }

            this.GetComponent<DrawOutline>().InvisibleOutline(Object);
        }
    }

    void Interaction()
    {
        if (!isPickup && !ishit && interactionObject != null && cameraCon.nowCam == CameraCon.CamType.wak)
        {
            if (!IT.isWorking)
            {
                IT.InteractionFunction();
            }
        }
    }

    void Drop()
    {
        if (Object != null && tempObject !=  null)
        {
            PlayerTransform.DetachChildren();
            isPickup = false;
            Object = null;
        }
        for (int i = 0; i < holdableObj.Length; i++) //큐브끼리 겹쳐진 후 무중력 상태가 유지되는 버그의 예외처리
        {
            holdableRigid = holdableObj[i].GetComponent<Rigidbody>();
            holdableRigid.isKinematic = false;
        }
        if (Object != null)
        {
           if (Object.name == ("SmartPhone")) Object.transform.eulerAngles = new Vector3(0, Object.transform.rotation.eulerAngles.y, 0);
        }
    }

    void Spin()
    {
        Object = tempObject;
        Object.transform.Rotate(0, spinPower * Time.deltaTime, 0, Space.World);

        if (Object.name == ("SmartPhone"))
        {
            Object.transform.eulerAngles = new Vector3(0, Object.transform.rotation.eulerAngles.y, 0);
        }
    }
}

