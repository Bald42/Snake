using System.Collections.Generic;
using UnityEngine;
using System.Linq;
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

    public List<PointPosition> PointPositions
    {
        get
        {
            return pointPositions;
        }
    }

    private PointPosition lastPointPositions
    {
        get
        {
            return pointPositions[pointPositions.Count - 1];
        }
    }

    public int PointPositionsCount
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
            return pointPositions[0];
        }
    }

    public SnakePoints(int id)
    {
        this.id = id;
    }

    public PointPosition GetPointPosition(PointPosition pointPosition)
    {
        return pointPositions.Where(x => x.Width == pointPosition.Width && x.Height == pointPosition.Height).FirstOrDefault();
    }

    // TODO сделать через враппер

    #region Changes

    public void AddPoint(PointPosition pointPosition)
    {
        pointPositions.Insert(0, pointPosition);
        OnChangeSnakePointEvent?.Invoke(true, pointPosition);
    }

    public void MoveNextPoint(PointPosition pointPosition)
    {
        AddPoint(pointPosition);
        OnChangeSnakePointEvent?.Invoke(false, lastPointPositions);
        pointPositions.RemoveAt(pointPositions.Count - 1);
    }

    #endregion
}