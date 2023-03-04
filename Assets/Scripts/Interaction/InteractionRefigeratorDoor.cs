using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionRefigeratorDoor : InteractionTrigger
{
    private bool isOpen = false;
    [SerializeField] float doorDelay = 0f;
    [SerializeField] float doorOpenAngle = -90f;
    [SerializeField] float doorCloseAngle = 0f;
    [SerializeField] float smoot = 3f;

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] sounds;
    private bool isPlaySound = false;
    // Start is called before the first frame update
    void Start()
    {
        if (isOpen) explanation = "문 닫기";
        else explanation = "문 열기";
    }
    private void Update()
    {
        if (isWorking)
        {
            if (isOpen)
            {
                doorDelay += Time.deltaTime;
                Quaternion targetRotation = Quaternion.Euler(0, doorOpenAngle, 0);
                transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smoot * Time.deltaTime);
                if(!isPlaySound)
                {
                    audioSource.clip = sounds[0];
                    audioSource.Play();
                    isPlaySound = true;
                }
            }
            else
            {
                doorDelay += Time.deltaTime;
                Quaternion targetRotation2 = Quaternion.Euler(0, doorCloseAngle, 0);
                transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation2, smoot * Time.deltaTime);
                if (!isPlaySound)
                {
                    audioSource.clip = sounds[1];
                    audioSource.Play();
                    isPlaySound = true;
                }
            }
            if (doorDelay > 2f)
            {
                isWorking = false;
                doorDelay = 0f;
                isPlaySound = false;
            }
        }
    }

    public override void InteractionFunction()
    {
        base.InteractionFunction();
        isWorking = true;
        isOpen = !isOpen;
        if (isOpen) explanation = "문 닫기";
        else explanation = "문 열기";
    }
}
