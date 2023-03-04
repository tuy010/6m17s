using UnityEngine;
using System.Collections;

//
// Light Script
// This script controls the materials and colors applied to each Light in your Light System
// Copyright 2014 While Fun Games
// http://whilefun.com
//

public class LightScript : MonoBehaviour {

	// These are the two materials that will be swapped when the light is turned on and off. It is recommended
	// that you use a Self-Illuminated Shader for the "lightIlluminatorOn" material.
	public Material lightIlluminatorOn;
	public Material lightIlluminatorOff;
	// These are the two colors that will be swapped when the light is turned on and off. They are applied
	// to the part of the mesh tagged with "LightMeshIlluminator" (e.g. the light bulb) and all child lights (Points, Spots, etc.)
	public Color lightIlluminationColorOn;
	public Color lightIlluminationColorOff;

	// Other internal stuff.
	private Renderer[] lightMeshIlluminators;
	private Light[] lightSources;

	void Awake(){

		lightMeshIlluminators = new Renderer[0];

		Renderer[] childRenderers = gameObject.GetComponentsInChildren<Renderer>();
		
		foreach(Renderer child in childRenderers){
			
			if(child.CompareTag("LightMeshIlluminator")){
				
				System.Array.Resize(ref lightMeshIlluminators, lightMeshIlluminators.Length + 1);
				lightMeshIlluminators[lightMeshIlluminators.Length-1] = child;
				
			}
			
		}

		// Get child lights
		lightSources = gameObject.GetComponentsInChildren<Light>();

		if(lightSources.Length == 0){
			Debug.LogWarning("LightScript:: No child lights found! You probably want to add at least a Point Light to this Light object.");
		}
		if(lightMeshIlluminators.Length == 0){
			Debug.LogWarning("LightScript:: No child meshes tagged with LightMeshIlluminator! The Light System might not look right wihtout this.");
		}

	}

	public void turnLightOn(){

		foreach(Light l in lightSources){
			l.enabled = true;
			l.color = lightIlluminationColorOn;
		}

		foreach(Renderer r in lightMeshIlluminators){
			r.material = lightIlluminatorOn;
			r.material.color = lightIlluminationColorOn;
		}

	}

	public void turnLightOff(){

		foreach(Light l in lightSources){
			l.enabled = false;
			l.color = lightIlluminationColorOff;
		}

		foreach(Renderer r in lightMeshIlluminators){
			r.material = lightIlluminatorOff;
			r.material.color = lightIlluminationColorOff;
		}

	}

}
