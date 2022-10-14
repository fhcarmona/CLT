using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window
{
    public GameObject program;
    public GameObject taskbarProgram;

    public Window(GameObject program, GameObject taskbarProgram)
    {
        this.program = program;
        this.taskbarProgram = taskbarProgram;
    }
}
