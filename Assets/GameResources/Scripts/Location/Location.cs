using UnityEngine;
using System.Linq;

public class Location
{
    private Point[] points = null;

    private SnakePoints snakePoints = null;
    private FoodPoints foodPoints = null;
    private WallPoints wallPoints = null;

    public void Init()
    {
        GenerateLocation();
        CheckGenerateWall();
        Subscribe();
    }

    #region Subscribe 

    private void OnDestroyHandler()
    {
        Unsubscribe();
    }

    private void Subscribe()
    {
        MainController.Instance.OnDestroyEvent += OnDestroyHandler;
        SnakeController.OnAddSnakeEvent += OnAddSnakeHandler;
        FoodController.OnAddFoodPointsEvent += OnAddFoodPointsHandler;
    }

    private void Unsubscribe()
    {
        MainController.Instance.OnDestroyEvent -= OnDestroyHandler;
        SnakeController.OnAddSnakeEvent -= OnAddSnakeHandler;
        FoodController.OnAddFoodPointsEvent -= OnAddFoodPointsHandler;

        if (snakePoints != null)
        {
            snakePoints.OnChangeSnakePointEvent -= OnChangeSnakePointHandler;
        }

        if (foodPoints != null)
        {
            foodPoints.OnAddFoodPointEvent -= OnAddFoodPointHandler;
        }

        if (wallPoints != null)
        {
            wallPoints.OnAddWallPointEvent -= OnAddWallPointHandler;
        }
    }

    private void OnAddFoodPointsHandler(FoodPoints foodPoints)
    {
        if (foodPoints != null)
        {
            foodPoints.OnAddFoodPointEvent -= OnAddFoodPointHandler;
        }
        this.foodPoints = foodPoints;
        SubscribeFood();
    }

    private void SubscribeFood()
    {
        foodPoints.OnAddFoodPointEvent += OnAddFoodPointHandler;
    }

    private void OnAddFoodPointHandler(PointPosition pointPosition)
    {
        GetPoint(pointPosition).ChangeState(true);
    }

    private void OnAddSnakeHandler(SnakePoints snakePoints)
    {
        if (snakePoints != null)
        {
            snakePoints.OnChangeSnakePointEvent -= OnChangeSnakePointHandler;
        }
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

    private void SubscribeWall()
    {
        wallPoints.OnAddWallPointEvent += OnAddWallPointHandler;
    }

    private void OnAddWallPointHandler(PointPosition pointPosition)
    {
        GetPoint(pointPosition).ChangeState(true);
    }

    #endregion

    private Point GetPoint(PointPosition pointPosition)
    {
        return points.Where(x => x.PointPosition.Width == pointPosition.Width &&
                                 x.PointPosition.Height == pointPosition.Height).FirstOrDefault();
    }

    public bool IsEmptyPoint(PointPosition pointPosition)
    {
        bool isEmpty = true;

        if (isEmpty && wallPoints != null)
        {
            isEmpty = wallPoints.GetPointPosition(pointPosition) == null;
        }

        if (isEmpty && snakePoints != null)
        {
            isEmpty = snakePoints.GetPointPosition(pointPosition) == null;
        }

        if (isEmpty && foodPoints != null)
        {
            isEmpty = foodPoints.GetPointPosition(pointPosition) == null;
        }
        return isEmpty;
    }

    public bool IsDamagePoint(PointPosition pointPosition)
    {
        bool isDamagePoint = false;

        if (!isDamagePoint && wallPoints != null)
        {
            isDamagePoint = wallPoints.GetPointPosition(pointPosition) != null;
        }

        if (!isDamagePoint && snakePoints != null)
        {
            isDamagePoint = snakePoints.GetPointPosition(pointPosition) != null;
        }

        return isDamagePoint;
    }

    public bool IsFoodPoint(PointPosition pointPosition)
    {
        bool isFood = true;
        isFood = foodPoints.GetPointPosition(pointPosition) != null;
        return isFood;
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

    private void CheckGenerateWall()
    {
        if (!MainController.Instance.HasWall)
        {
            return;
        }
        wallPoints = new WallPoints();
        SubscribeWall();
        for (int i = 0; i < MainController.Instance.Width; i++)
        {
            PointPosition pointPosition0 = new PointPosition(i, 0);
            PointPosition pointPosition1 = new PointPosition(i, MainController.Instance.Height - 1);
            wallPoints.AddPoint(pointPosition0);
            wallPoints.AddPoint(pointPosition1);
        }
        for (int i = 1; i < MainController.Instance.Height - 1; i++)
        {
            PointPosition pointPosition0 = new PointPosition(0, i);
            PointPosition pointPosition1 = new PointPosition(MainController.Instance.Width - 1, i);
            wallPoints.AddPoint(pointPosition0);
            wallPoints.AddPoint(pointPosition1);
        }
    }

    private Point SpawnPoints(int width, int height)
    {
        Point point = MainController.Instance.Spawner.SpawnPoints(width, height);
        point.Init(width, height);
        return point;
    }
}