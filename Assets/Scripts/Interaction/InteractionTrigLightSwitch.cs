using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
using static DigitalClock_724;

public class InteractionTrigLightSwitch : InteractionTrigger
{
    enum RoomNums
    {
        ROOM723,
        ROOM724,
        BOTH,
        NON
    }
    enum SwitchPlaces
    {
        RESTROOM,   //100
        BEDROOM,    //1
        LIVINGROOM, //10
        HALLWAY,    //1000
        NON
    }


    public bool isOn = false;
    [SerializeField] private Light[] Lights = new Light[0];
    public AudioClip switchUp;
	public AudioClip switchDown;
	public float animationSpeed = 10.0f;
	public bool loopForwardAnimation = true;
	public float maxSwitchDistance = 1.25f;
	private Transform switchTransform;
    AudioSource audioSource;

    [SerializeField]
    private GameObject sysObj;
    private ChangePasswordManager sys_passwordManager;

    [SerializeField]
    private RoomNums roomNum;
    [SerializeField]
    private SwitchPlaces switchPlace;

    [SerializeField]
    private bool isChangePassword = false;

    private void Start()
    {
        isOn = false;
        if (isOn)
        {
            foreach (Light light in Lights) light.enabled = true;
            explanation = "불 끄기";
        }
        else
        {
            foreach (Light light in Lights) light.enabled = false;
            explanation = "불 켜기";
        }
        //audioSource = GetComponent<AudioSource>();
        if (sysObj == null)
        {
            sysObj = GameObject.Find("System");
        }
        sys_passwordManager = sysObj.GetComponent<ChangePasswordManager>();
    }

    private void Update()
    {
        if(!isChangePassword)
        {
            if(roomNum == RoomNums.BOTH && sys_passwordManager.nowState != ChangePasswordManager.State.NotWorking)
            {
                isChangePassword = true;
                explanation = "스위치 작동";
            }
            else if (roomNum == RoomNums.ROOM723 && sys_passwordManager.nowState == ChangePasswordManager.State.ChangePassword_723)
            {
                isChangePassword = true;
                explanation = "스위치 작동";
            }
            else if (roomNum == RoomNums.ROOM724 && sys_passwordManager.nowState == ChangePasswordManager.State.ChangePassword_724)
            {
                isChangePassword = true;
                explanation = "스위치 작동";
            }
        }
        else
        {
            if(sys_passwordManager.nowState == ChangePasswordManager.State.NotWorking)
            {
                isChangePassword = false;
                if (Lights[0].GetComponent<Light>().enabled == true) setSwitchStateToOn();
                else setSwitchStateToOff();
            }
        }
    }

    public override void InteractionFunction()
    {
        base.InteractionFunction();
        //audioSource.Play();
        if (!isChangePassword)
        {
            if (isOn)
            {
                foreach (Light light in Lights) light.enabled = false;
                explanation = "불 켜기";
            }
            else
            {
                foreach (Light light in Lights) light.enabled = true;
                explanation = "불 끄기";
            }
        }
        else
        {
            switch (switchPlace)
            {
                case SwitchPlaces.RESTROOM:
                    changePassword(1);
                    break;
                case SwitchPlaces.BEDROOM:
                    changePassword(2);
                    break;
                case SwitchPlaces.LIVINGROOM:
                    changePassword(3);
                    break;
                case SwitchPlaces.HALLWAY:
                    changePassword(4);
                    break;
                default:
                    break;
            }
        }
        //isOn = !isOn;
        flipSwitch();
        isWorking = false;
    }
    
    public void flipSwitch()
    {
        if(isOn)
        {
            gameObject.GetComponent<AudioSource>().clip = switchDown;
            gameObject.GetComponent<AudioSource>().Play();

            if(loopForwardAnimation)
            {
                this.GetComponent<Animation>()["ToggleSwitch"].speed = animationSpeed;
                this.GetComponent<Animation>().Play("ToggleSwitch");
            }
            else
            {
                this.GetComponent<Animation>()["ToggleSwitch"].speed = animationSpeed;
                this.GetComponent<Animation>().Play("ToggleSwitch");
            }
            isOn = false;
        }
        else
        {
            gameObject.GetComponent<AudioSource>().clip = switchUp;
            gameObject.GetComponent<AudioSource>().Play();

            if(loopForwardAnimation)
            {
                this.GetComponent<Animation>()["ToggleSwitch"].speed = animationSpeed;
                this.GetComponent<Animation>().Play("ToggleSwitch");
            }
            else
            {
                this.GetComponent<Animation>()["ToggleSwitch"].speed = -animationSpeed;
                this.GetComponent<Animation>().Play("ToggleSwitch");
            }

            isOn = true;
        }

        // Tell parent Lighting System to toggle its state
        // We don't make it the same as the switch, because two-way switching systems
        // can have individual swithces in the off position but the lights are still on
        //transform.parent.GetComponent<LightingSystemScript>().toggleLights();
	}

	// Simply sets switch
	public void setSwitchStateToOn()
    {
		isOn = true;
        explanation = "불 끄기";
        this.GetComponent<Animation>()["ToggleSwitch"].speed = -animationSpeed;
        this.GetComponent<Animation>().Play("ToggleSwitch");
        
    }

	public void setSwitchStateToOff()
    {
		isOn = false;
        explanation = "불 켜기";
        this.GetComponent<Animation>()["ToggleSwitch"].speed = animationSpeed;
        this.GetComponent<Animation>().Play("ToggleSwitch");
        
    }

	public float getMaxSwitchDistance()
    {
		return maxSwitchDistance;
	}

    private void changePassword(int numtype)
    {
        if (sys_passwordManager.nowState == ChangePasswordManager.State.ChangePassword_723)
        {
            switch (numtype)
            {
                case 1:
                    if (sys_passwordManager.password723 % 10 == 9) sys_passwordManager.password723 -= 9;
                    else sys_passwordManager.password723 += 1;
                    break;
                case 2:
                    if ((sys_passwordManager.password723 /10)% 10 == 9) sys_passwordManager.password723 -= 90;
                    else sys_passwordManager.password723 += 10;
                    break;
                case 3:
                    if ((sys_passwordManager.password723 / 100) % 10 == 9) sys_passwordManager.password723 -= 900;
                    else sys_passwordManager.password723 += 100;
                    break;
                case 4:
                    if ((sys_passwordManager.password723 / 1000) % 10 == 9) sys_passwordManager.password723 -= 9000;
                    else sys_passwordManager.password723 += 1000;
                    break;
                default:
                    return;
            }
        }
        else if (sys_passwordManager.nowState == ChangePasswordManager.State.ChangePassword_724)
        {
            switch (numtype)
            {
                case 1:
                    if (sys_passwordManager.password724 % 10 == 9) sys_passwordManager.password724 -= 9;
                    else sys_passwordManager.password724 += 1;
                    break;
                case 2:
                    if ((sys_passwordManager.password724 / 10) % 10 == 9) sys_passwordManager.password724 -= 90;
                    else sys_passwordManager.password724 += 10;
                    break;
                case 3:
                    if ((sys_passwordManager.password724 / 100) % 10 == 9) sys_passwordManager.password724 -= 900;
                    else sys_passwordManager.password724 += 100;
                    break;
                case 4:
                    if ((sys_passwordManager.password724 / 1000) % 10 == 9) sys_passwordManager.password724 -= 9000;
                    else sys_passwordManager.password724 += 1000;
                    break;
                default:
                    return;
            }
        }
    }
}
