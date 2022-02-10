using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class FoodPoints
{
    public Action<PointPosition> OnAddFoodPointEvent = null;

    private int id = 0;
    private List<PointPosition> pointPositions = new List<PointPosition>();

    public int Id
    {
        get
        {
            return id;
        }
    }

    public List<PointPosition> PointPositions
    {
        get
        {
            return pointPositions;
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

    public PointPosition GetPointPosition(PointPosition pointPosition)
    {
        return pointPositions.Where(x => x.Id == pointPosition.Id).FirstOrDefault();
    }

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