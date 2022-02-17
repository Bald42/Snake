using UnityEngine.SceneManagement;
using static Enums;
using UnityEngine;
using System;

public class GameController
{
    public Action OnStartEvent = null;
    private Location location = new Location();

    private SnakeController snakeController = null;
    private FoodController foodController = null;

    private bool isActive = false;

    private GameState gameState = GameState.Null;
    private string nameScene = "Game";

    public Location Location
    {
        get
        {
            return location;
        }
    }

    public void Init()
    {
        location.Init();
        InitSnake();
        InitFoodController();
        Subscribe();
        ChangeState(GameState.WaitStart);
    }

    private void InitSnake()
    {
        snakeController = new SnakeController();
        PointPosition pointPosition = new PointPosition(3, 3);
        snakeController.Init(0, pointPosition);
    }

    private void InitFoodController()
    {
        foodController = new FoodController();
        foodController.Init();
    }

    #region Subscribe 
    // TODO всё временно, только для тестов
    private void Subscribe()
    {
        MainController.Instance.OnDestroyEvent += OnDestroyHandler;
        MainController.Instance.OnUpdateEvent += OnUpdateHandler;
        SnakeController.OnDeadSnakeEvent += OnDeadSnakeHandler;
    }

    private void Unsubscribe()
    {
        MainController.Instance.OnDestroyEvent -= OnDestroyHandler;
        MainController.Instance.OnUpdateEvent -= OnUpdateHandler;
        SnakeController.OnDeadSnakeEvent -= OnDeadSnakeHandler;
    }

    private void OnDestroyHandler()
    {
        Unsubscribe();
    }

    private void OnUpdateHandler()
    {
        //OnUpdate();
    }

    private void OnDeadSnakeHandler(int _)
    {
        ChangeState(GameState.Finish);
    }

    #endregion

    private void OnUpdate()
    {
        switch (gameState)
        {
            case GameState.WaitStart:
                {
                    if (!isActive && Input.anyKeyDown)
                    {
                        isActive = true;
                        OnStartEvent?.Invoke();
                    }
                }
                break;
            case GameState.Finish:
                {
                    if (Input.anyKeyDown)
                    {
                        SceneManager.LoadScene(nameScene);
                    }
                }
                break;
            default:
                break;
        }
    }

    private void ChangeState(GameState gameState)
    {
        this.gameState = gameState;
    }
}