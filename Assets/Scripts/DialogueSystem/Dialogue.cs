using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public string Name;

    [TextArea(0, 5)]
    public string[] Sentences;
    //image
}
