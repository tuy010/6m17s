using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionTrig_Door : InteractionTrigger
{
    [SerializeField] DoorAction action;

    AudioSource audioSource;
    [SerializeField] private AudioClip[] sounds = new AudioClip[3];
    [SerializeField] private float openVolume = 0.25f;
    [SerializeField] private float lockedVolume = 0.5f;

    [SerializeField]
    GameObject sys;
    private ChangePasswordManager sys_passwordManager;

    [SerializeField] EndingProduction endingProduction;
    public bool isLockedDoor = false;


    private void Start()
    {
        if (action.open) explanation = "문 닫기";
        else explanation = "문 열기";

        audioSource = GetComponent<AudioSource>();

        if (sys == null)
        {
            sys = GameObject.Find("System");
        }
        sys_passwordManager = sys.GetComponent<ChangePasswordManager>();
    }
    void Update()
    {
        if(isWorking&&!action.isMoving)
        {
            isWorking = false;
        }
    }
    public override void InteractionFunction()
    {
       base.InteractionFunction();
       if(!action.isMoving)
        {
            if (isLockedDoor && !sys_passwordManager.is724Open)
            {
                audioSource.clip = sounds[2];
                audioSource.volume = lockedVolume;
                audioSource.Play();
            }
            else if(isLockedDoor && sys_passwordManager.is724Open)
            {
                if (action.open) audioSource.clip = sounds[0];
                else if (!action.open) audioSource.clip = sounds[1];
                audioSource.volume = openVolume;
                audioSource.Play();

                action.open = !action.open;
                if (action.open) explanation = "문 닫기";
                else explanation = "문 열기";
                action.isMoving = true;

                //ADD CLEAR FUNCTION
                endingProduction.StartEnding();
            }
            else
            {
                if (action.open) audioSource.clip = sounds[0];
                else if (!action.open) audioSource.clip = sounds[1];
                audioSource.volume = openVolume;
                audioSource.Play();

                action.open = !action.open;
                if (action.open) explanation = "문 닫기";
                else explanation = "문 열기";
                action.isMoving = true;
            }  
        }
    }
}
