using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLock : MonoBehaviour
{
    public Light light1;
    private Collider collider1;
    // Start is called before the first frame update
    void Start()
    {
        collider1 = GetComponent<BoxCollider>();
        collider1.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (light1.enabled == true)
        {
            collider1.enabled = true;
        }
        else if (light1.enabled == false) collider1.enabled = false;
    }
}
