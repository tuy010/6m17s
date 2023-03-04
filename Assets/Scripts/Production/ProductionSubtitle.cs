using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductionSubtitle : MonoBehaviour
{
    [SerializeField] private SubtitleTrigger subDev;
    [SerializeField] private SubtitleTrigger subDoc;
    void Start()
    {
        
    }

    public void StartProductionSubtitles()
    {
        subDev.TriggerSubtitle();
        subDoc.TriggerSubtitle();
    }
}
