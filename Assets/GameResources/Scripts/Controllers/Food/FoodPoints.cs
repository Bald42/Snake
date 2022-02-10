using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class FoodPoints : BasePointPositions
{
    public Action<PointPosition> OnAddFoodPointEvent = null;

    private int id = 0;

    public int Id
    {
        get
        {
            return id;
        }
    }

    public FoodPoints(int id)
    {
        this.id = id;
    }

    public FoodPoints()
    {
    }

    // TODO сделать через враппер

    #region Changes

    public void AddPoint(PointPosition pointPosition)
    {
        pointPositions.Add(pointPosition);
        OnAddFoodPointEvent?.Invoke(pointPosition);
    }

    public void DeletePoint(PointPosition pointPosition)
    {
        pointPositions.Remove(GetPointPosition(pointPosition));
    }

    #endregion
}