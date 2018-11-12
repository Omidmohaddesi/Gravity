using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Conversation
{
    //[TextArea(3, 10)]
    public Line[] lines;
    public string name;
}

[System.Serializable]
public class Line
{
    public string name;
    public string text;
    public Sprite portrait;
    public int portraitPos = 1;
  
}