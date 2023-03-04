using UnityEngine;
using UnityEngine.UI;

// This script should be applied to the player in a way that makes sense for them to turn on switches based on buttons
public class PlayerLightControlScript : MonoBehaviour {

	// Set this to any key you wish. When released, the player will attempt to flip the nearest switch
	public KeyCode flipSwitchKey = KeyCode.F;

    [SerializeField, Tooltip("The UI Image that will act as the player reticle")]
    private Image myReticle;
    [SerializeField,Tooltip("The Sprite that will indicate a switch can be interacted with")]
    private Sprite reticleOn;
    [SerializeField,Tooltip("The Sprite that will indicate there is no switch to interact with")]
    private Sprite reticleOff;

    void Awake()
    {

        if(myReticle == null)
        {
            Debug.LogWarning("PlayerLightControlScript:: No reticle assigned. Reticle highlighting will not work as expected.");
        }

        Cursor.lockState = CursorLockMode.Locked;

    }

    void Update () {


        Ray interactionRay = Camera.main.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit hitInteractable;
        Physics.Raycast(interactionRay, out hitInteractable, 3.0f);

        // If we're looking at a light
        if (hitInteractable.transform != null && hitInteractable.transform.gameObject.GetComponent<LightSwitchScript>())
        {

            // Check if it's in range
            if (hitInteractable.distance < hitInteractable.transform.gameObject.GetComponent<LightSwitchScript>().getMaxSwitchDistance())
            {

                // Highlight the interaction in the UI
                highlightReticle();

                // Then check for interaction input and flip switch
                if (Input.GetKeyUp(flipSwitchKey) || Input.GetMouseButtonDown(0))
                {
                    hitInteractable.transform.gameObject.GetComponent<LightSwitchScript>().flipSwitch();
                }

            }
            else
            {

                // If we are not in range, un-highlight UI
                unHighlightReticle();

            }

        }
        else
        {

            // If we no switch at all, un-highlight UI
            unHighlightReticle();

        }


        //// Get closest game object tagged as LightSwitch and try to toggle it
        //if (Input.GetKeyUp(flipSwitchKey))
        //{

        //    float minDistance = Mathf.Infinity;

        //    GameObject[] lightSwitches = GameObject.FindGameObjectsWithTag("LightSwitch");

        //    GameObject theSwitch = null;

        //    foreach (GameObject go in lightSwitches)
        //    {

        //        float tempDisance = Vector3.Distance(transform.position, go.transform.position);

        //        if (tempDisance < minDistance)
        //        {

        //            minDistance = tempDisance;
        //            theSwitch = go;

        //        }

        //    }

        //    // Is the closest switch close enough, and at a reasonable angle?
        //    if (theSwitch)
        //    {

        //        Vector3 horizontalRelationToSwitch = new Vector3(theSwitch.transform.position.x, transform.position.y, theSwitch.transform.position.z);
        //        Vector3 switchDirRelativeToTarget = horizontalRelationToSwitch - transform.position;
        //        Vector3 forwardRelativeToSwitch = transform.forward;

        //        float switchEulerAngleFromPlayer = Vector3.Angle(switchDirRelativeToTarget, forwardRelativeToSwitch);

        //        if ((switchEulerAngleFromPlayer < maxAngleToSwitch) && (minDistance < theSwitch.GetComponent<LightSwitchScript>().getMaxSwitchDistance()))
        //        {

        //            theSwitch.GetComponent<LightSwitchScript>().flipSwitch();

        //        }

        //    }

        //}

	}

    private void highlightReticle()
    {
        if (myReticle)
        {
            myReticle.overrideSprite = reticleOn;
        }
    }

    private void unHighlightReticle()
    {
        if (myReticle)
        {
            myReticle.overrideSprite = reticleOff;
        }
    }

}
