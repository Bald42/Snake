using static Enums;
using UnityEngine;

public class PCActionController : ActionController
{
    protected override void OnUpdate()
    {
        KeyboardInput();
    }

    private void KeyboardInput()
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
}