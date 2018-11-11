using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Conversation
{
    [TextArea(3, 10)]
    public string[] sentences;
    public string name;
    public Sprite portrait_1_talksfirst;
    public Sprite portrait_2;
}