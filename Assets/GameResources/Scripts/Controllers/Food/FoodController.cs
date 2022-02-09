using System.Collections.Generic;
using UnityEngine;
using System;

public class FoodController : MonoBehaviour
{
    public static Action<FoodPoints> OnAddFoodPointsEvent = null;
    private FoodPoints foodPoints = null;

    public void Init(int id = 0)
    {
        FoodController foodController = Spawner.SpawnGameObjectWithMonoBehaviour<FoodController>(MainController.Instance.gameObject, $"FoodController {id}");
        foodController.InitAfterSpawn(id);
    }

    private void InitAfterSpawn(int id)
    {
        foodPoints = new FoodPoints(id);
        OnAddFoodPointsEvent?.Invoke(foodPoints);
        CreateRandomPoint();
        Subscribe();
    }

    #region Subscribes

    private void OnDestroyHandler()
    {
        Unsubscribe();
    }

    private void Subscribe()
    {
        MainController.Instance.OnDestroyEvent += OnDestroyHandler;
        SnakeController.OnEatingFoodEvent += OnEatingFoodEvent;
    }

    private void Unsubscribe()
    {
        MainController.Instance.OnDestroyEvent -= OnDestroyHandler;
        SnakeController.OnEatingFoodEvent -= OnEatingFoodEvent;
    }

    private void OnEatingFoodEvent(PointPosition pointPosition, int _)
    {
        foodPoints.DeletePoint(pointPosition);
        CreateRandomPoint();
    }

    #endregion

    private void CreateRandomPoint()
    {
        bool isFind = false;
        while (!isFind)
        {
            int rndWidth = UnityEngine.Random.Range(0, MainController.Instance.Width);
            int rndHeight = UnityEngine.Random.Range(0, MainController.Instance.Height);
            PointPosition rndPointPosition = new PointPosition(rndWidth, rndHeight);
            if (MainController.Instance.GameController.Location.IsEmptyPoint(rndPointPosition))
            {
                isFind = true;
                foodPoints.AddPoint(rndPointPosition);
            }
        }
    }
}