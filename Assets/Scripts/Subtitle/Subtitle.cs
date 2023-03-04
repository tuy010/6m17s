using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Subtitle
{
    public string name;
    public Color nameColor;

    [TextArea(3,10)]
    public string[] sentence;
    public float[] time;
}
