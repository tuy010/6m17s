using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAction : MonoBehaviour
{
    public bool open = false;
    public bool isMoving = false;
    public float doorDelay = 0f;
    public float doorOpenAngle = 90f;
    public float doorCloseAngle = 0f;
    public float smoot = 3f;



    // Start is called before the first frame update

    public void ChangeDoorState()
    {
        open = !open;
    }

    // Update is called once per frame
    void Update()
    {
        if(isMoving)
        {
            if (open)
            {
                doorDelay += Time.deltaTime;
                Quaternion targetRotation = Quaternion.Euler(0, doorOpenAngle, 0);
                transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smoot * Time.deltaTime);               
            }
            else
            {
                doorDelay += Time.deltaTime;
                Quaternion targetRotation2 = Quaternion.Euler(0, doorCloseAngle, 0);
                transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation2, smoot * Time.deltaTime);
            }
            if (doorDelay > 2f)
            {
                isMoving = false;
                doorDelay = 0f;
            }
        }
    }
}
