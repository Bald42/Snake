using System.Collections.Generic;
using System.Collections;
using static Enums;
using UnityEngine;
using System;

public class SnakeController : MonoBehaviour
{
    public static Action<SnakePoints> OnAddSnakeEvent = null;
    private int id = 0;
    private ActionController actionController = null;
    private DirectionMove currentDirectionMove = DirectionMove.Right;
    private DirectionMove lastDirectionMove = DirectionMove.Right;
    private float speed = 1f;
    private SnakePoints snakePoints = null;
    private Coroutine move = null;
    private int widthNextPoint = 0;
    private int heightNextPoint = 0;

    public void Init(int id, PointPosition startPointPosition)
    {
        SnakeController snakeController = Spawner.SpawnGameObjectWithMonoBehaviour<SnakeController>(MainController.Instance.gameObject, $"SnakeController {id}");
        snakeController.InitAfterSpawn(id, startPointPosition);
    }

    private void InitAfterSpawn(int id, PointPosition startPointPosition)
    {
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

    #region Subscribes

    private void Subscribe()
    {
        MainController.Instance.OnDestroyEvent += OnDestroyHandler;
        MainController.Instance.GameController.OnStartEvent += OnStartHandler;
        actionController.OnChangeDirectionMoveEvent += OnChangeDirectionMoveHandler;
    }

    private void Unsubscribe()
    {
        MainController.Instance.OnDestroyEvent -= OnDestroyHandler;
        MainController.Instance.GameController.OnStartEvent -= OnStartHandler;
        actionController.OnChangeDirectionMoveEvent -= OnChangeDirectionMoveHandler;
    }

    private void OnDestroyHandler()
    {
        Unsubscribe();
        StopAllCoroutines();
    }

    private void OnChangeDirectionMoveHandler(DirectionMove directionMove)
    {
        currentDirectionMove = directionMove;
    }

    private void OnStartHandler()
    {
        if (move != null)
        {
            StopCoroutine(move);
        }
        move = StartCoroutine(Move());
    }

    #endregion

    private IEnumerator Move()
    {
        var delay = new WaitForSeconds(0.5f);
        while (true)
        {
            yield return delay;
            MoveNextPoint();
        }
    }

    private void MoveNextPoint()
    {
        PointPosition pointPosition = NextPointPosition;
        snakePoints.MoveNextPoint(pointPosition);
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