using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//All interaction object will inheritance this class
//DO NOT EDIT THIS CODE
public class InteractionTrigger : MonoBehaviour
{
    public bool isWorking = false;
    public string explanation;

    public virtual void InteractionFunction()
    {
        isWorking = true;
    }
}
