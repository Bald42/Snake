using UnityEngine;
using System;

public class GameController
{
    public Action OnStartEvent = null;
    private Location location = new Location();

    private SnakeController snakeController = null;
    private bool isActive = false;

    public void Init()
    {
        location.Init();
        InitSnake();
        Subscribe();
    }

    private void InitSnake()
    {
        snakeController = new SnakeController();
        PointPosition pointPosition = new PointPosition(3, 3);
        snakeController.Init(0, pointPosition);
    }

    #region Subscribe 
    // TODO всё временно, только для тестов
    private void Subscribe()
    {
        MainController.Instance.OnDestroyEvent += OnDestroyHandler;
        MainController.Instance.OnUpdateEvent += OnUpdateHandler;
    }

    private void Unsubscribe()
    {
        MainController.Instance.OnDestroyEvent -= OnDestroyHandler;
        MainController.Instance.OnUpdateEvent -= OnUpdateHandler;
    }

    private void OnDestroyHandler()
    {
        Unsubscribe();
    }

    private void OnUpdateHandler()
    {
        OnUpdate();
    }

    #endregion

    private void OnUpdate()
    {
        if (!isActive && Input.anyKeyDown)
        {
            isActive = true;
            OnStartEvent?.Invoke();
        }
    }
}