using System.Collections.Generic;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine;

public class TestControll : MonoBehaviour
{
    private Joystick joystick = null;

    private void Awake()
    {
        joystick = new Joystick();
    }

    private void Init()
    {
        joystick.Game.Move.performed += PerformeHandler;
        joystick.Game.Enable();
    }

    private void OnEnable()
    {
        Init();
    }

    private void OnDisable()
    {
        joystick.Game.Disable();
    }

    private void PerformeHandler(InputAction.CallbackContext callbackContext)
    {

        CustomDebug.LogOnlyEditor($"PerformeHandler {callbackContext}", Color.green);
    }
}