using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class EditorActionController : ActionController
{
    protected override void OnUpdate()
    {
        KeyboardInput();
    }

    private void KeyboardInput()
    {
        if (Input.GetKey(KeyCode.A))
        {
        }
    }
}