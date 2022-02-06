using UnityEngine;

public class GameController
{
    private Location location = new Location();

    private SnakeController snakeController = null;

    public void Init()
    {
        location.Init();
        snakeController = new SnakeController();
        snakeController.Init(0);
    }
}