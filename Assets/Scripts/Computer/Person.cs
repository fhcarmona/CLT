using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Person
{
    public string name;
    public string sentence;

    public Color color;

    public bool isRightAlign;

    public Person(string name, string sentence, Color color, bool isRightAlign)
    {
        this.name = name;
        this.sentence = sentence;
        this.color = color;
        this.isRightAlign = isRightAlign;
    }
}
