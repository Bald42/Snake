using UnityEngine;
using System;

public class WallPoints : BasePointPositions
{
    public Action<PointPosition> OnAddWallPointEvent = null;

    // TODO ������� ����� �������

    #region Changes

    public void AddPoint(PointPosition pointPosition)
    {
        pointPositions.Add(pointPosition);
        OnAddWallPointEvent?.Invoke(pointPosition);
    }

    #endregion
}