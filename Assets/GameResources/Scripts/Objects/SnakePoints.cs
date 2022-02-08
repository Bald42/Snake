using System.Collections.Generic;
using UnityEngine;
using System;

public class SnakePoints
{
    /// <summary>”казываем позицию точки которую надо активировать/деактивировать, указываем состо€ние</summary>
    public Action<bool, PointPosition> OnChangeSnakePointEvent = null;

    private int id = 0;
    private List<PointPosition> pointPositions = new List<PointPosition>();

    public int Id
    {
        get
        {
            return id;
        }
    }

    private PointPosition lastPointPositions
    {
        get
        {
            return pointPositions[0];
        }
    }

    private int PointPositionsCount
    {
        get
        {
            return pointPositions.Count;
        }
    }

    public PointPosition FirstPointPositions
    {
        get
        {
            return pointPositions[pointPositions.Count - 1];
        }
    }

    public SnakePoints(int id)
    {
        this.id = id;
    }

    // TODO сделать через враппер

    #region Changes

    public void AddPoint(PointPosition pointPosition)
    {
        pointPositions.Add(pointPosition);
        OnChangeSnakePointEvent?.Invoke(true, pointPosition);
    }

    public void MoveNextPoint(PointPosition pointPosition)
    {
        AddPoint(pointPosition);
        OnChangeSnakePointEvent?.Invoke(false, lastPointPositions);
        pointPositions.RemoveAt(0);
    }

    #endregion
}