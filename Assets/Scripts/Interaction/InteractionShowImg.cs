using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionShowImg : InteractionTrigger
{
    [SerializeField]
    bool isShow = false;

    [SerializeField]
    GameObject showImgUI;
    [SerializeField]
    Image imageUI;
    [SerializeField]
    Sprite imageSource;
    [SerializeField]
    GameObject sys;

    private AudioSource audioSource;


    private void Start()
    {
        isShow = false;
        explanation = "살펴보기";
        if (sys == null)
        {
            sys = GameObject.Find("System");
        }
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (isWorking && isShow)
        {
            if(Input.GetKeyUp(KeyCode.Escape))
            {
                isWorking = false;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                sys.GetComponent<CameraCon>().CloseInteractionShowObject();
            }
        }
        if (!isWorking && isShow)
        {
            showImgUI.SetActive(false);
            isShow = false;
        }
    }

    public override void InteractionFunction()
    {
        base.InteractionFunction();
        isShow = true;
        showImgUI.SetActive(true);
        imageUI.sprite = imageSource;
        sys.GetComponent<CameraCon>().ShowInteractionShowObject();
        audioSource.Play();
    }
}
