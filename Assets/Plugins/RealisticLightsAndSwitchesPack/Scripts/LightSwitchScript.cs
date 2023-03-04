using UnityEngine;
using System.Collections;

//
// Light Script
// This script controls the Light Switch objects in your Light System
// Copyright 2014 While Fun Games
// http://whilefun.com
//
[RequireComponent(typeof(BoxCollider))]
public class LightSwitchScript : MonoBehaviour {

	// The sounds for your switch
	public AudioClip switchUp;
	public AudioClip switchDown;
	// Speed at which to play the "Toggle Switch" animation.
	public float animationSpeed = 10.0f;
	// Set this to true if your switch animation is something that loops. For example, a rotary switch always turns
	// clockwise (so set this to true), whereas a standard house switch that flips up and down does not loop (so leave it false)
	public bool loopForwardAnimation = false;
	// The max distance the player can be from this specific switch and still activate it. For standard wall switches, 1.25 is a good start. For things
	// like pull string switches that are attached on the ceiling above the player, a higher value is required.
	public float maxSwitchDistance = 1.25f;

	// Other internal stuff.
	private bool switchTurnedOn = true;
	private Transform switchTransform;

	void Awake(){

		Transform[] childTransforms = gameObject.GetComponentsInChildren<Transform>();

		foreach(Transform child in childTransforms){
			
			if(child.CompareTag("LightSwitchModel")){
				
				switchTransform = child;
				
			}
			
		}

		if(!switchTransform){
			Debug.LogError("LightSwitchScript:: Cannot find child LightSwitchModel transform! Ensure LightSwitch object has child model tagged LightSwitchModel.");
		}
		if(!transform.parent){
			Debug.LogError("LightSwitchScript:: Switch has no parent object (Parent should be Lighting System)!");
		}

        if(gameObject.GetComponent<BoxCollider>() == null)
        {
            Debug.LogWarning("LightSwitchScript:: Light Switch named '"+gameObject.name+"' is missing a Box Collider! Add one to the prefab to ensure interactions work as expected.", gameObject);
        }

	}

	public void flipSwitch(){

		if(switchTurnedOn){

			switchTurnedOn = false;

			gameObject.GetComponent<AudioSource>().clip = switchDown;
			gameObject.GetComponent<AudioSource>().Play();

			if(loopForwardAnimation){
				switchTransform.GetComponent<Animation>()["ToggleSwitch"].speed = animationSpeed;
				switchTransform.GetComponent<Animation>().Play("ToggleSwitch");
			}else{
				switchTransform.GetComponent<Animation>()["ToggleSwitch"].speed = animationSpeed;
				switchTransform.GetComponent<Animation>().Play("ToggleSwitch");
			}

		}else{

			switchTurnedOn = true;

			gameObject.GetComponent<AudioSource>().clip = switchUp;
			gameObject.GetComponent<AudioSource>().Play();

			if(loopForwardAnimation){
				switchTransform.GetComponent<Animation>()["ToggleSwitch"].speed = animationSpeed;
				switchTransform.GetComponent<Animation>().Play("ToggleSwitch");
			}else{
				switchTransform.GetComponent<Animation>()["ToggleSwitch"].speed = -animationSpeed;
				switchTransform.GetComponent<Animation>().Play("ToggleSwitch");
			}

		}

		// Tell parent Lighting System to toggle its state
		// We don't make it the same as the switch, because two-way switching systems
		// can have individual swithces in the off position but the lights are still on
		transform.parent.GetComponent<LightingSystemScript>().toggleLights();

	}

	// Simply sets switch
	public void setSwitchStateToOn(){
		switchTurnedOn = true;
		switchTransform.GetComponent<Animation>()["ToggleSwitch"].speed = -animationSpeed;
		switchTransform.GetComponent<Animation>().Play("ToggleSwitch");
	}

	public void setSwitchStateToOff(){
		switchTurnedOn = false;
		switchTransform.GetComponent<Animation>()["ToggleSwitch"].speed = animationSpeed;
		switchTransform.GetComponent<Animation>().Play("ToggleSwitch");
	}

	public float getMaxSwitchDistance(){
		return maxSwitchDistance;
	}

}
