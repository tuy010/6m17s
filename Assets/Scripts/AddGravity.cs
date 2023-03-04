using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddGravity : MonoBehaviour
{
    private Rigidbody rb;
    AudioSource AS;
    
    [SerializeField]
    private float gravityForce = 100f; 
    private bool sound = true;
    private bool start = false;
    private float time = 0;
    private float temptime = 0;
    // Start is called before the first frame update
    void Start()
    {
        AS = this.GetComponent<AudioSource>();
        AS.volume = 0.2f;
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // FixedUpdate is called once per frame
    private void FixedUpdate()
    {
        if (start == false)
        {
            temptime += Time.deltaTime;
            if (temptime > 1f) start = true;
        }
        rb.AddForce(Vector3.down * gravityForce);   
        if (sound == false)
        {
            time += Time.deltaTime;
            if (time > 1.5f)
            {   
                sound = true;
                time = 0;
            }
        } 
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("floor") && GameManager.production == false && sound == true && start == true)
        {
            AS.enabled = true;
            AS.Play();
            sound = false;
        }
    }
}
