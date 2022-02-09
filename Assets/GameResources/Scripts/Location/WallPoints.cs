using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class WallPoints
{
    public Action<PointPosition> OnAddWallPointEvent = null;
    private List<PointPosition> pointPositions = new List<PointPosition>();

    public List<PointPosition> PointPositions
    {
        get
        {
            return pointPositions;
        }
    }

    // TODO сделать через враппер

    #region Changes

    public PointPosition GetPointPosition(PointPosition pointPosition)
    {
        return pointPositions.Where(x => x.Width == pointPosition.Width && x.Height == pointPosition.Height).FirstOrDefault();
    }

    public void AddPoint(PointPosition pointPosition)
    {
        pointPositions.Add(pointPosition);
        OnAddWallPointEvent?.Invoke(pointPosition);
    }

    #endregion
}