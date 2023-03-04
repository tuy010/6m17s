using UnityEngine;
using System.Collections;

//
// Hint Box Script
// This is not required for Security Camera Deluxe, but is used in the test scene. Delete it if you want :)
// Copyright 2014 While Fun Games
// http://whilefun.com
//

public class HintBoxScript : MonoBehaviour {

	public string hintText = "YOUR HINT TEXT HERE";

	private float boxWidth = Screen.width/2;
	private float boxHeight = Screen.height/2;

	private bool showHint = false;

	void OnTriggerEnter(Collider other){
		
		if(other.CompareTag("Player")){
			showHint = true;
		}
		
	}

	void OnTriggerExit(Collider other){
		
		if(other.CompareTag("Player")){
			showHint = false;
		}
		
	}

	void OnGUI(){

		if(showHint){
			GUI.TextArea(new Rect(Screen.width/2 - boxWidth/2, Screen.height/2 - boxHeight/2, boxWidth, boxHeight), hintText);
		}

	}

}
