using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SubtitleManager : MonoBehaviour
{
    [SerializeField] private GameObject subtitleUI;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI subtitleText;

    private string nameStr;
    private Color nameColor;
    private Queue<string> sentences;

    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartSubtitle (Subtitle data)
    {
        sentences.Clear();

        subtitleUI.SetActive(true);
        nameStr = data.name;
        nameColor = data.nameColor;
        foreach(string s in data.sentence)
        {
            sentences.Enqueue(s);
        }
        ShowNextSentence();
    }

    public void ShowNextSentence()
    {
        Debug.Log(this.name+" : ShowNextSentence()");
        string sentence = sentences.Dequeue();

        if (sentence!= "<empty>")
        {
            nameText.text = nameStr;
            nameText.color = nameColor;
            subtitleText.text = sentence;
        }
    }

    public void EndSubtitle()
    {
        subtitleUI.SetActive(false);
        subtitleText.text = null;
        sentences.Clear();
    }

    public void ShowOneSentence(string subtitle, string name, Color color)
    {
        subtitleUI.SetActive(true);
        nameText.text = name;
        nameText.color = color;
        subtitleText.text = subtitle;
    }

    public void OffSubtitle()
    {
        subtitleUI.SetActive(false);
        nameText.text = null;
        subtitleText.text = null;
        sentences.Clear();
    }
}
