using UnityEngine;
using System.Linq;

public class Location
{
    private Point[] points = null;

    private SnakePoints snakePoints = null;

    public void Init()
    {
        GenerateLocation();
        Subscribe();
    }

    #region Subscribe 

    private void Subscribe()
    {
        MainController.Instance.OnDestroyEvent += OnDestroyHandler;
        SnakeController.OnAddSnakeEvent += OnAddSnakeHandler;
    }

    private void Unsubscribe()
    {
        MainController.Instance.OnDestroyEvent -= OnDestroyHandler;
        SnakeController.OnAddSnakeEvent -= OnAddSnakeHandler;
        if (snakePoints != null)
        {
            snakePoints.OnChangeSnakePointEvent -= OnChangeSnakePointHandler;
        }
    }

    private void OnDestroyHandler()
    {
        Unsubscribe();
    }

    public void OnAddSnakeHandler(SnakePoints snakePoints)
    {
        this.snakePoints = snakePoints;
        SubcribeSnake();
    }

    private void SubcribeSnake()
    {
        snakePoints.OnChangeSnakePointEvent += OnChangeSnakePointHandler;
    }

    private void OnChangeSnakePointHandler(bool isActive, PointPosition pointPosition)
    {
        GetPoint(pointPosition).ChangeState(isActive);
    }

    #endregion

    private Point GetPoint(PointPosition pointPosition)
    {
        return points.Where(x => x.PointPosition.Width == pointPosition.Width &&
                                 x.PointPosition.Height == pointPosition.Height).FirstOrDefault();
    }

    private void GenerateLocation()
    {
        GeneratePointPositionLocation();
    }

    private void GeneratePointPositionLocation()
    {
        points = new Point[MainController.Instance.Width * MainController.Instance.Height];
        int id = 0;
        for (int width = 0; width < MainController.Instance.Width; width++)
        {
            for (int height = 0; height < MainController.Instance.Height; height++)
            {
                points[id] = SpawnPoints(width, height);
                id++;
            }
        }
    }

    private Point SpawnPoints(int width, int height)
    {
        Point point = MainController.Instance.Spawner.SpawnPoints(width, height);
        point.Init(width, height);
        return point;
    }
}