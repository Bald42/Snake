using UnityEngine;
using System;

public class SnakePoints : BasePointPositions
{
    /// <summary>��������� ������� ����� ������� ���� ������������/��������������, ��������� ���������</summary>
    public Action<bool, PointPosition> OnChangeSnakePointEvent = null;

    private int id = 0;

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

    // TODO ������� ����� �������

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