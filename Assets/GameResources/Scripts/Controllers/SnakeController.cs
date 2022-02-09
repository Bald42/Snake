using System.Collections.Generic;
using System.Collections;
using static Enums;
using UnityEngine;
using System;

public class SnakeController : MonoBehaviour
{
    public static Action<SnakePoints> OnAddSnakeEvent = null;
    public static Action<PointPosition, int> OnEatingFoodEvent = null;
    public static Action<int> OnDeadEvent = null;

    private int id = 0;
    private ActionController actionController = null;
    private DirectionMove currentDirectionMove = DirectionMove.Right;
    private float defaultSpeed = 0.5f;
    private float currentSpeed = 0.5f;

    private SnakePoints snakePoints = null;
    private Coroutine move = null;
    private int widthNextPoint = 0;
    private int heightNextPoint = 0;

    private WaitForSeconds delayMove = null;

    private Location location = null;

    public void Init(int id, PointPosition startPointPosition)
    {
        SnakeController snakeController = Spawner.SpawnGameObjectWithMonoBehaviour<SnakeController>(MainController.Instance.gameObject, $"SnakeController {id}");
        snakeController.InitAfterSpawn(id, startPointPosition);
    }

    private void InitAfterSpawn(int id, PointPosition startPointPosition)
    {
        CashLinks();
        this.id = id;
#if UNITY_EDITOR
        actionController = new PCActionController();
#else
        // TODOOO пока нет других контроллеров
        actionController = new PCActionController();
#endif
        CreateSnakePoints(startPointPosition);
        actionController.Init();
        Subscribe();
    }

    private void CashLinks()
    {
        location = MainController.Instance.GameController.Location;
    }

    #region Subscribes

    private void OnDestroyHandler()
    {
        Unsubscribe();
        StopAllCoroutines();
        move = null;
    }

    private void Subscribe()
    {
        MainController.Instance.OnDestroyEvent += OnDestroyHandler;
        MainController.Instance.GameController.OnStartEvent += OnStartHandler;

        actionController.OnChangeDirectionMoveEvent += OnChangeDirectionMoveHandler;
        actionController.OnStartHoldMoveEvent += OnStartHoldMoveHandler;
        actionController.OnStopHoldMoveEvent += OnStopHoldMoveHandler;
    }

    private void Unsubscribe()
    {
        MainController.Instance.OnDestroyEvent -= OnDestroyHandler;
        MainController.Instance.GameController.OnStartEvent -= OnStartHandler;

        actionController.OnChangeDirectionMoveEvent -= OnChangeDirectionMoveHandler;
        actionController.OnStartHoldMoveEvent -= OnStartHoldMoveHandler;
        actionController.OnStopHoldMoveEvent -= OnStopHoldMoveHandler;
    }

    private void OnChangeDirectionMoveHandler(DirectionMove directionMove)
    {
        if (snakePoints.PointPositionsCount > 1)
        {
            if ((currentDirectionMove == DirectionMove.Left && directionMove == DirectionMove.Right) ||
                (currentDirectionMove == DirectionMove.Right && directionMove == DirectionMove.Left) ||
                (currentDirectionMove == DirectionMove.Top && directionMove == DirectionMove.Down) ||
                (currentDirectionMove == DirectionMove.Down && directionMove == DirectionMove.Top))
            {
                return;
            }
        }
        currentDirectionMove = directionMove;
    }

    private void OnStartHandler()
    {
        StartMove();
    }

    private void OnStartHoldMoveHandler()
    {
        ChangeSpeedForHold(true);
        StartMove();
    }

    private void OnStopHoldMoveHandler()
    {
        ChangeSpeedForHold(false);
        StartMove();
    }

    #endregion

    private void StartMove()
    {
        if (move != null)
        {
            StopCoroutine(move);
        }
        move = StartCoroutine(Move());
    }

    private void ChangeSpeedForHold(bool isHold)
    {
        currentSpeed = isHold ? defaultSpeed * 0.2f : defaultSpeed;
    }

    private IEnumerator Move()
    {
        while (true)
        {
            MoveNextPoint();
            delayMove = new WaitForSeconds(currentSpeed);
            yield return delayMove;
        }
    }

    private void MoveNextPoint()
    {
        PointPosition nextPointPosition = NextPointPosition;
        if (location.IsFoodPoint(nextPointPosition))
        {
            snakePoints.AddPoint(nextPointPosition);
            OnEatingFoodEvent?.Invoke(nextPointPosition, id);
        }
        snakePoints.MoveNextPoint(nextPointPosition);
    }

    private PointPosition NextPointPosition
    {
        get
        {
            widthNextPoint = snakePoints.FirstPointPositions.Width;
            heightNextPoint = snakePoints.FirstPointPositions.Height;

            switch (currentDirectionMove)
            {
                case DirectionMove.Left:
                    {
                        widthNextPoint--;
                    }
                    break;
                case DirectionMove.Right:
                    {
                        widthNextPoint++;
                    }
                    break;
                case DirectionMove.Top:
                    {
                        heightNextPoint++;
                    }
                    break;
                case DirectionMove.Down:
                    {
                        heightNextPoint--;
                    }
                    break;
                default:
                    break;
            }

            if (widthNextPoint >= MainController.Instance.Width)
            {
                widthNextPoint = 0;
            }
            else if (widthNextPoint < 0)
            {
                widthNextPoint = MainController.Instance.Width - 1;
            }

            if (heightNextPoint >= MainController.Instance.Height)
            {
                heightNextPoint = 0;
            }
            else if (heightNextPoint < 0)
            {
                heightNextPoint = MainController.Instance.Height - 1;
            }

            PointPosition pointPosition = new PointPosition(widthNextPoint, heightNextPoint);
            return pointPosition;
        }
    }

    private void CreateSnakePoints(PointPosition startPointPosition)
    {
        snakePoints = new SnakePoints(id);
        OnAddSnakeEvent?.Invoke(snakePoints);
        snakePoints.AddPoint(startPointPosition);
    }
}