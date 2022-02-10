using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BasePointPositions
{
    protected List<PointPosition> pointPositions = new List<PointPosition>();

    protected PointPosition GetPointPosition(PointPosition pointPosition)
    {
        return pointPositions.Where(x => x.Id == pointPosition.Id).FirstOrDefault();
    }

    public bool HasPoint(PointPosition pointPosition)
    {
        return GetPointPosition(pointPosition) != null;
    }
}