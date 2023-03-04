using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionTrig_gosuguPoster : InteractionTrigger
{
    AudioSource audioSource;
    SubtitleTrigger subtitleTrigger;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        subtitleTrigger = GetComponent<SubtitleTrigger>();
    }

    private void Update()
    {
        if(isWorking)
        {
            if (subtitleTrigger.isShow == false)
            {
                isWorking = false;
            }
        }
    }

    public override void InteractionFunction()
    {
        base.InteractionFunction();
        subtitleTrigger.TriggerSubtitle();
        audioSource.Play();
    }

}
