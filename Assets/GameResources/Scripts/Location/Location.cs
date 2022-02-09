using UnityEngine;
using System.Linq;

public class Location
{
    private Point[] points = null;

    private SnakePoints snakePoints = null;
    private FoodPoints foodPoints = null;

    public void Init()
    {
        GenerateLocation();
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

    #endregion

    private Point GetPoint(PointPosition pointPosition)
    {
        return points.Where(x => x.PointPosition.Width == pointPosition.Width &&
                                 x.PointPosition.Height == pointPosition.Height).FirstOrDefault();
    }

    public bool IsEmptyPoint(PointPosition pointPosition)
    {
        bool isEmpty = true;
        // TODO proverit
        isEmpty = snakePoints.PointPositions.Where(x => x.Width == pointPosition.Width &&
                                                        x.Height == pointPosition.Height).Count() == 0;
        if (isEmpty && foodPoints != null)
        {
            isEmpty = foodPoints.PointPositions.Where(x => x.Width == pointPosition.Width &&
                                                           x.Height == pointPosition.Height).Count() == 0;
        }
        return isEmpty;
    }

    public bool IsDamagePoint(PointPosition pointPosition)
    {
        bool isDamagePoint = true;
        // TODO proverit
        isDamagePoint = snakePoints.PointPositions.Where(x => x.Width == pointPosition.Width &&
                                                              x.Height == pointPosition.Height).Count() != 0;
        return isDamagePoint;
    }

    public bool IsFoodPoint(PointPosition pointPosition)
    {
        bool isFood = true;
        isFood = foodPoints.PointPositions.Where(x => x.Width == pointPosition.Width &&
                                                      x.Height == pointPosition.Height).Count() != 0;
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

    private Point SpawnPoints(int width, int height)
    {
        Point point = MainController.Instance.Spawner.SpawnPoints(width, height);
        point.Init(width, height);
        return point;
    }
}