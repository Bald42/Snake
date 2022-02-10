using static Enums;
using UnityEngine;

public class PCSnakeInputController : SnakeInputController
{
    protected override void OnUpdate()
    {
        KeyboardInput();
    }

    private void KeyboardInput()
    {
        CheckGetKeyDownMove();
        CheckHoldMove();
    }

    private void CheckGetKeyDownMove()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            OnChangeDirectionMoveEvent?.Invoke(DirectionMove.Left);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            OnChangeDirectionMoveEvent?.Invoke(DirectionMove.Right);
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            OnChangeDirectionMoveEvent?.Invoke(DirectionMove.Top);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            OnChangeDirectionMoveEvent?.Invoke(DirectionMove.Down);
        }
    }

    private void CheckHoldMove()
    {
        if (Input.GetKey(KeyCode.A) ||
            Input.GetKey(KeyCode.D) ||
            Input.GetKey(KeyCode.W) ||
            Input.GetKey(KeyCode.S))
        {
            CheckStartHold();
        }

        if (Input.GetKeyUp(KeyCode.A) ||
            Input.GetKeyUp(KeyCode.D) ||
            Input.GetKeyUp(KeyCode.W) ||
            Input.GetKeyUp(KeyCode.S))
        {
            if (!Input.GetKey(KeyCode.A) &&
                !Input.GetKey(KeyCode.D) &&
                !Input.GetKey(KeyCode.W) &&
                !Input.GetKey(KeyCode.S))
            {
                CheckStopHold();
            }
        }
    }
}