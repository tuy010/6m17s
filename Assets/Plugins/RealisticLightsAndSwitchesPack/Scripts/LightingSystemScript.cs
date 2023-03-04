using UnityEngine;
using System.Collections;

//
// Lighting System Script
// This script controls all logic, lighting, and switching inside the Light System.
// Copyright 2014 While Fun Games
// http://whilefun.com
//

// Notes about the Lighting System object:
// Must have at least one child each of Light Switch and Light
// When switched, the lights go on or off and the switch animates as appropriate
// If the system has more than one switch in it, the zero-ith switch will be set to the 
// opposite of the system's "Start With Lights On" value (this is to reflect the nature
// of multi-way switch systems. One switch is always opposite the rest.

public class LightingSystemScript : MonoBehaviour {

	// Set this to true if you want the light system to be turned on when the scene starts.
	public bool startSceneWithLightsOn = false;

	// Other internal stuff.
	private bool lightsOn = false;
	private Transform[] childLightSwitches;
	private Transform[] childLights;

	// Use this for initialization
	void Awake () {

		childLights = new Transform[0];
		childLightSwitches = new Transform[0];

		Transform[] tempLights;
		tempLights = gameObject.GetComponentsInChildren<Transform>();

		foreach(Transform child in tempLights){

			if(child.CompareTag("Light")){

				System.Array.Resize(ref childLights, childLights.Length + 1);
				childLights[childLights.Length-1] = child;

			}else if(child.CompareTag("LightSwitch")){

				System.Array.Resize(ref childLightSwitches, childLightSwitches.Length + 1);
				childLightSwitches[childLightSwitches.Length-1] = child;

			}

		}

		// Warn user if there are no Light or LightSwitch objects (i.e. we don't have a valid light system!)
		if(childLights.Length == 0){
			Debug.LogError("There are no child objects tagged as Light! Add at least one Light to the Light System.");
		}
		if(childLightSwitches.Length == 0){
			Debug.LogError("There are no child objects tagged as LightSwitch! Add at least one LightSwitch to the Light System");
		}
		
	}

	void Start(){

		if(startSceneWithLightsOn){

			lightsOn = true;

			foreach(Transform l in childLights){
				l.GetComponent<LightScript>().turnLightOn();
			}
			foreach(Transform s in childLightSwitches){
				s.GetComponent<LightSwitchScript>().setSwitchStateToOn();
			}

			// If there is more than one light switch, then we have a 2-way switch or 
			// greater, in which case one switch should be flipped the opposite way.
			if(childLightSwitches.Length > 1){
				childLightSwitches[0].GetComponent<LightSwitchScript>().setSwitchStateToOff();
			}

		}else{

			lightsOn = false;

			foreach(Transform l in childLights){
				l.GetComponent<LightScript>().turnLightOff();
			}
			foreach(Transform s in childLightSwitches){
				s.GetComponent<LightSwitchScript>().setSwitchStateToOff();
			}

			if(childLightSwitches.Length > 1){
				childLightSwitches[0].GetComponent<LightSwitchScript>().setSwitchStateToOn();
			}

		}

	}

	public void toggleLights(){

		// If lights are currently On, turn them off
		if(lightsOn){

			lightsOn = false;

			foreach(Transform t in childLights){
				t.GetComponent<LightScript>().turnLightOff();
			}
			
		}else{

			lightsOn = true;

			foreach(Transform t in childLights){
				t.GetComponent<LightScript>().turnLightOn();
			}
			
		}

	}

	public bool isLightOn(){
		return lightsOn;
	}
	
}


