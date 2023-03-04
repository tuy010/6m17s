using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTime : MonoBehaviour
{
    float delay = 1;
    public float speed = 1.0f;

    public int minute;
    public int sec;
    // Start is called before the first frame update
    void Start()
    {
        minute = 0;
        sec = 0;
    }

    // Update is called once per frame
    void Update()
    {
        delay -= Time.deltaTime * speed;
        if (delay < 0.0f)
        {
            delay = 1.0f;
            if (!GameManager.tutorial && !GameManager.production)
            {
                sec++;
                if (sec >= 60)
                {
                    sec = 0;
                    minute++;
                }
            }
        }
    }
}
