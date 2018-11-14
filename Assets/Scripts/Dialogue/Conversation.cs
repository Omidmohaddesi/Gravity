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
    public Sprite portrait;
    public int portraitPos = 1;
    [TextArea(3, 10)]
    public string text;
  
  
}