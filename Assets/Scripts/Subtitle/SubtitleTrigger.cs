using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubtitleTrigger : MonoBehaviour
{
    public GameObject subtitleSys;
    SubtitleManager subtitleManager;
    public Subtitle subtitle;

    public bool isShow = false;
    int index = 0;
    int endIndex = 0;
    float timer = 0;

    private void Start()
    {
        subtitleManager = subtitleSys.GetComponent<SubtitleManager>();
    }
    private void Update()
    {
        if(isShow)
        {
            timer += Time.deltaTime;
            if(timer >= subtitle.time[index])
            {
                if(index+1 == subtitle.time.Length)
                {
                    timer = 0;
                    index = 0;
                    subtitleManager.EndSubtitle();
                    isShow = false;
                }
                else
                {
                    timer = 0;
                    index++;
                    subtitleManager.ShowNextSentence();
                }             
            }
        }
    }

    public void TriggerSubtitle(int startIndex = 0, int endIdx = -1)
    {
        if(!isShow)
        {
            timer = 0;
            isShow = true;
            index = startIndex;
            if (endIdx == -1) endIndex = subtitle.time.Length;
            else endIndex = endIdx;
            subtitleManager.StartSubtitle(subtitle);
        }     
    }

    public void ShowOneSentence(string subtitleData, string name = null )
    {
        if (name == null) name = subtitle.name;
        Debug.Log(name);
        subtitleManager.ShowOneSentence(subtitleData, name, subtitle.nameColor);
    }

    public void OffSubtitle()
    {
        isShow = false;
        subtitleManager.OffSubtitle();
    }
    
}
